using System.Text.Json.Serialization;
using ForetoBot.Business.Extensions.JsonConverters;
using Telegram.Bot.Types.Enums;

namespace ForetoBot.Business.Handlers.Telegram;

public class TgUpdateEvent
{
    [JsonPropertyName("update_id")] public int Id { get; set; }
    [JsonPropertyName("message")] public TgMessage Message { get; set; }
}

public class TgMessage
{
    [JsonPropertyName("message_id")] public int MessageId { get; set; }
    [JsonConverter(typeof(UnitTimeToDateTimeConverter))]
    [JsonPropertyName("date")] public DateTimeOffset Date { get; set; }
    [JsonPropertyName("text")] public string Text { get; set; }
    [JsonPropertyName("from")] public TgFrom From { get; set; }
    [JsonPropertyName("chat")] public TgChat Chat { get; set; }
}

public class TgChat
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
    [JsonPropertyName("username")] public string Username { get; set; }

    [JsonConverter(typeof(TgChatTypeConverter))]
    [JsonPropertyName("type")]
    public ChatType Type { get; set; }
}

public class TgFrom
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("is_bot")] public bool IsBot { get; set; }
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
    [JsonPropertyName("username")] public string Username { get; set; }
    [JsonPropertyName("language_code")] public string LanguageCode { get; set; }
    [JsonPropertyName("is_premium")] public bool IsPremium { get; set; }
}