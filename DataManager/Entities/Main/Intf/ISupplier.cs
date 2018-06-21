using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Поставщик услуг
    /// </summary>
    public interface ISupplier : IBaseEntity
    {
        /// <summary>
        /// Наименование поставщика
        /// </summary>
        string SupplierName { get; set; }
    }
}