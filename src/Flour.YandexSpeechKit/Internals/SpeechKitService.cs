using Microsoft.Extensions.Logging;
using Speechkit.Tts.V3;

namespace Flour.YandexSpeechKit.Internals;

internal class SpeechKitService(
    Synthesizer.SynthesizerClient synthesizerClient,
    ILogger<SpeechKitService> logger) : ISpeechKitService
{
    private readonly Synthesizer.SynthesizerClient _synthesizerClient = synthesizerClient;

    public Task<byte[]> GenerateSpeech(string text, CancellationToken token)
    {
        synthesizerClient.UtteranceSynthesis(new UtteranceSynthesisRequest
        {

        });
    }
}