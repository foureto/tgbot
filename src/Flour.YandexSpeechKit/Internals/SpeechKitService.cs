using Grpc.Core;
using Microsoft.Extensions.Options;
using Speechkit.Tts.V3;

namespace Flour.YandexSpeechKit.Internals;

internal class SpeechKitService(
    Synthesizer.SynthesizerClient synthesizerClient,
    IOptions<SpeechKitSettings> settings) : ISpeechKitService
{
    public async Task<Stream> GenerateSpeech(string text, CancellationToken token = default)
    {
        using var result = synthesizerClient.UtteranceSynthesis(new UtteranceSynthesisRequest
        {
            Text = text,
            OutputAudioSpec = new AudioFormatOptions
            {
                RawAudio = new RawAudio
                {
                    AudioEncoding = RawAudio.Types.AudioEncoding.Linear16Pcm,
                    SampleRateHertz = 22050,
                },
                ContainerAudio = new ContainerAudio
                {
                    ContainerAudioType = ContainerAudio.Types.ContainerAudioType.OggOpus,
                }
            }
        }, new Metadata
        {
            { "Authorization", $"Api-Key {settings.Value.ApiKey}" },
            { "x-folder-id", settings.Value.FolderId }
        }, cancellationToken: token);


        var ms = new MemoryStream();
        await foreach (var chunk in result.ResponseStream.ReadAllAsync(cancellationToken: token))
            chunk.AudioChunk.Data.WriteTo(ms);

        ms.Position = 0;
        return ms;
    }
}