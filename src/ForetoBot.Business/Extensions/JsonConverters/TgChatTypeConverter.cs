using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types.Enums;

namespace ForetoBot.Business.Extensions.JsonConverters;

public class TgChatTypeConverter : JsonConverter<ChatType>
{
    public override ChatType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Enum.TryParse<ChatType>(reader.GetString(), out var result) ? result : ChatType.Private;
    }

    public override void Write(Utf8JsonWriter writer, ChatType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().ToLower());
    }
}