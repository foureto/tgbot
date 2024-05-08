namespace Flour.YandexSpeechKit;

public interface ISpeechKitService
{
    Task<byte[]> GenerateSpeech(string text, CancellationToken token);
}