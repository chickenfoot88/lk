namespace WebAppAuth.Services
{
    /// <summary>
    /// Результат выполнения
    /// </summary>
    public interface IServiceResult<T>
    {
        /// <summary>
        /// Успешность выполнения
        /// </summary>
        bool Success { get; set; }
        
        /// <summary>
        /// Информационное сообщение
        /// </summary>
        string InfoMessage { get; set; }

        /// <summary>
        /// Данные
        /// </summary>
        T Content { get; set; }
    }
}
