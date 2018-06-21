namespace WebAppAuth.Services
{
    /// <summary>
    /// Результат выполнения
    /// </summary>
    public class ServiceResult<T> : IServiceResult<T> 
    {
        /// <summary>
        /// Успешность выполнения
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Информационное сообщение
        /// </summary>
        public string InfoMessage { get; set; }

        /// <summary>
        /// Данные
        /// </summary>
        public T Content { get; set; }
    }
}
