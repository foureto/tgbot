using ForetoBot.Business.Services.FileStore.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ForetoBot.Business.Services.FileStore;

internal class GoogleFileStore(IOptions<GoogleStoreSettings> options, ILogger<GoogleFileStore> logger) : IFileStore
{
    private readonly string _bucketName = options.Value.BucketName?.Trim('/')
                                          ?? throw new ArgumentNullException(nameof(options.Value.BucketName));

    private readonly string _url = options.Value.BaseUrl?.Trim('/')
                                   ?? throw new ArgumentNullException(nameof(options.Value.BaseUrl));

    private readonly StorageClient _client = StorageClient.Create(GoogleCredential.FromFile(
        options.Value.KeyFile ?? throw new ArgumentNullException(nameof(options.Value.KeyFile))));

    public async Task<StoredFile> Save(SaveFileRequest request, CancellationToken token = default)
    {
        var extension = Path.GetExtension(request.FileName);
        var filename = $"{Guid.NewGuid():N}{extension}";
        var filePath = GetFolderPath(request);

        request.FileStream.Position = 0;

        var dataObject = await _client.UploadObjectAsync(
            _bucketName, $"{filePath}/{filename}", request.MimeType, request.FileStream, cancellationToken: token);

        if (string.IsNullOrWhiteSpace(dataObject?.Id))
        {
            logger.LogWarning("Upload of file {Name} failed", request.FileName);
            return null;
        }

        var result = new StoredFile
        {
            Url = $"{_url}/{request.SubPath?.Trim('/')}/{filename}",
            FileName = filename,
            FilePath = filePath,
            MimeType = request.MimeType
        };

        logger.LogInformation("New file created, {Url}", result.Url);

        return result;
    }

    public Task RemoveFile(RemoveFileRequest request, CancellationToken token = default)
        => _client.DeleteObjectAsync(_bucketName, request.FilePath, cancellationToken: token);

    private string GetFolderPath(SaveFileRequest request)
        => $"{request.SubPath?.Trim('/')}";
}