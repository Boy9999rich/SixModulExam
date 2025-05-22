using Serilog;
using Serilog.Configuration;

namespace SixModulExam.SerllogSettings
{
    public static class TelegramSinkExtensions
    {
        public static LoggerConfiguration Telegram(
        this LoggerSinkConfiguration loggerConfiguration,
        string telegramApiKey,
        string telegramChatId)
        {
            return loggerConfiguration.Sink(new TelegramSink(telegramApiKey, telegramChatId));
        }
    }
}
