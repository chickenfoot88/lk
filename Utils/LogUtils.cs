using System;

namespace OhUtils
{
    using NLog;

    /// <summary>
    /// Класс утилит для работы с логгером
    /// </summary>
    public static class LogUtils
    {
        /// <summary>
        /// Экземпляр логгера
        /// </summary>
        private static readonly Logger CurrentLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Активность логгера
        /// по умолчанию - выключен
        /// </summary>
        private static bool _loggerEnabled = true;

        /// <summary>
        /// Запись в лог-файл
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="type">Тип сообщения</param>
        public static void WriteLog(string message, ELogType type)
        {
            //если логгер выключен, то выходим
            if (!_loggerEnabled) return;

            //если логгер включен, то пишем в лог-файл
            switch (type)
            {
                case ELogType.Error:
                    CurrentLogger.Error(message);
                    break;
                case ELogType.Warn:
                    CurrentLogger.Warn(message);
                    break;
                case ELogType.Info:
                    CurrentLogger.Info(message);
                    break;
                case ELogType.Debug:
                    CurrentLogger.Debug(message);
                    break;
                case ELogType.Fatal:
                    CurrentLogger.Fatal(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }


        /// <summary>
        /// Запись ошибки в лог-файл
        /// </summary>
        /// <param name="exception">Ошибка</param>
        /// <param name="message">Текст сообщения</param>
        /// <param name="type">Тип сообщения</param>
        public static void WriteException(Exception exception, ELogType type, string message = null)
        {
            //если логгер выключен, то выходим
            if (!_loggerEnabled) return;

            //если логгер включен, то пишем в лог-файл
            switch (type)
            {
                case ELogType.Error:
                    CurrentLogger.Error(exception, message);
                    break;
                case ELogType.Warn:
                    CurrentLogger.Warn(exception, message);
                    break;
                case ELogType.Info:
                    CurrentLogger.Info(exception, message);
                    break;
                case ELogType.Debug:
                    CurrentLogger.Debug(exception, message);
                    break;
                case ELogType.Fatal:
                    CurrentLogger.Fatal(exception, message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        /// Переключатель активности лога
        /// </summary>
        /// <param name="active">Устанавливаемое состояние активности лога (true:включен, false: выключен)</param>
        public static void EnableLogger(bool active)
        {
            _loggerEnabled = active;
        }

        /// <summary>
        /// Тип лога
        /// </summary>
        public enum ELogType
        {
            Error = 1,
            Info = 2,
            Warn = 3,
            Debug = 4,
            Fatal = 5
        }
    }
}
