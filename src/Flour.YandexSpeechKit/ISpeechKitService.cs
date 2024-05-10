namespace Flour.YandexSpeechKit;

public interface ISpeechKitService
{
    Task<Stream> GenerateSpeech(string text, CancellationToken token = default);
}