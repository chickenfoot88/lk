using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Storage.Intf
{
    /// <summary>
    /// Управляющая организация
    /// </summary>
    public interface IManagmentOrganization : IBaseEntity
    {
        /// <summary>
        /// Наименование управляющей организации
        /// </summary>
        string ManagmentOrganizationName { get; set; }
    }
}