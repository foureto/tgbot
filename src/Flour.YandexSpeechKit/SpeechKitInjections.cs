using Flour.YandexSpeechKit.Internals;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Speechkit.Tts.V3;

namespace Flour.YandexSpeechKit;

public static class SpeechKitInjections
{
    private const string DefaultSection = "yandex:speech";

    public static IServiceCollection AddSpeechKit(
        this IServiceCollection services, IConfiguration configuration, string section = DefaultSection)
    {
        return services
            .Configure<SpeechKitSettings>(e => configuration.GetSection(section).Bind(e))
            .AddScoped<ISpeechKitService, SpeechKitService>()
            .AddGrpcClient<Synthesizer.SynthesizerClient>((sp, opts) =>
            {
                var settings = sp.GetRequiredService<IOptions<SpeechKitSettings>>();
                opts.Address = new Uri(settings.Value.ApiUrl);
            }).Services;
    }
}