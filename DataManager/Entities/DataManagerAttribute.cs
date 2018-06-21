namespace OhDataManager.Entities
{
    using global::System;

    /// <summary>
    /// Атрибут менеджеров данных
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DataManagerAttribute: Attribute
    {
        /// <summary>
        /// Тип менеджера данных
        /// </summary>
        public string DataManagerType { get; set; }

        /// <summary>
        /// Название менеджера данных, от кого наследуется
        /// </summary>
        public string AbstractDataManagerName { get; set; }
    }
}
