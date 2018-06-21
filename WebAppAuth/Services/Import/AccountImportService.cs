using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using OhDataManager.Entities.ImportHelper;
using OhDataManager.Entities.Main;
using OhDataManager.Entities.System;
using OhDataManager.Enums;
using OhUtils;
using WebAppAuth.Context;
using WebAppAuth.Services.FileManager;

namespace WebAppAuth.Services.Import
{
    /// <summary>
    /// Сервис загрузки ЛС
    /// </summary>
    public sealed class AccountImportService : BaseFileImport
    {
        /// <summary>
        /// Наименование схемы с импортированным файлом
        /// </summary>
        public string ImportSchemaName { get; } = "import.";

        /// <summary>
        /// Наименование схемы с архивными данными
        /// </summary>
        public string StorageSchemaName { get; } = "main.";

        /// <summary>
        /// Наименование схемы с оперативными данными
        /// </summary>
        public string MainSchemaName { get; } = "main.";

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fileStorageManager"></param>
        public AccountImportService(IFileStorageManager fileStorageManager) : base(fileStorageManager)
        {
            LoadType = EImportType.Account;
        }

        /// <summary>
        /// Импорт данных
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public override async Task RunImport(Stream inputStream)
        {
            using (var fileContent = ZipFile.Read(inputStream))
            {
                var files = fileContent.Where(x => x.FileName.EndsWith(".txt")).ToArray();
                
                using (var sqlQueryPerformer = new SqlQueryPerformer())
                {
                    ImportHelperDto importHelperDto = null;
                    try
                    {
                        var file = files.FirstOrDefault(x => x.FileName.Contains("InfoDescript.txt"));
                        if (file != null)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                file.Extract(memoryStream);
                                memoryStream.Seek(0, SeekOrigin.Begin);
                                
                                var content = new StreamReader(memoryStream, Encoding.GetEncoding(1251)).ReadLine()?.Split('|');

                                if (content == null)
                                {
                                    throw new Exception("Не заполнен заголовочный файл");
                                }

                                //Код организации
                                var organizationCode = Convert.ToInt64(content[5]);

                                Organization organization;

                                using (var context = new ApplicationDbContext())
                                {
                                    organization = context.Organization.FirstOrDefault(x => x.OrganizationExternalId == organizationCode);
                                    if (organization == null)
                                    {
                                        throw new Exception(
                                            $" Организация не зарегистрирована: {organizationCode}");
                                    }
                                }
                                importHelperDto = new ImportHelperDto
                                {
                                    CalculationDate = Convert.ToDateTime(content[10]),
                                    Organization = organization
                                };
                            }
                        }
                        else
                        {
                            throw new Exception("Не найден файл InfoDescript.txt");
                        }

                        file = files.FirstOrDefault(x => x.FileName.Contains("CharacterGilFond"));
                        if (file != null)
                        {
                            using (var stream = (Stream) file.OpenReader())
                            {
                                string baseTable = $@"{ImportSchemaName}{nameof(ImportHousingCharacteristic)}";

                                var childTable =
                                    $"{baseTable}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}";
                                StreamingLoad(stream, sqlQueryPerformer, baseTable, childTable);
                            }
                        }

                        file = files.FirstOrDefault(x => x.FileName.Contains("ChargExpenseServ"));
                        if (file != null)
                        {
                            using (var stream = (Stream) file.OpenReader())
                            {
                                string baseTable = $@"{ImportSchemaName}{nameof(ImportAccrual)}";
                                var childTable =
                                    $"{baseTable}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}";

                                StreamingLoad(stream, sqlQueryPerformer, baseTable, childTable);
                            }
                        }

                        file = files.FirstOrDefault(x => x.FileName.Contains("Counters"));
                        if (file != null)
                        {
                            using (var stream = (Stream) file.OpenReader())
                            {
                                string baseTable = $@"{ImportSchemaName}{nameof(ImportMeterReading)}";
                                var childTable =
                                    $"{baseTable}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}";

                                StreamingLoad(stream, sqlQueryPerformer, baseTable, childTable);
                            }
                        }

                        file = files.FirstOrDefault(x => x.FileName.Contains("Payment.txt"));
                        if (file != null)
                        {
                            using (var stream = (Stream) file.OpenReader())
                            {
                                string baseTable = $@"{ImportSchemaName}{nameof(ImportPayment)}";
                                var childTable =
                                    $"{baseTable}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}";

                                StreamingLoad(stream, sqlQueryPerformer, baseTable, childTable);
                            }
                        }

                        file = files.FirstOrDefault(x => x.FileName.Contains("PaymentDetails.txt"));
                        if (file != null)
                        {
                            using (var stream = (Stream) file.OpenReader())
                            {
                                string baseTable = $@"{ImportSchemaName}{nameof(ImportPaymentRequisite)}";
                                var childTable =
                                    $"{baseTable}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}";

                                StreamingLoad(stream, sqlQueryPerformer, baseTable, childTable);
                            }
                        }

                        file = files.FirstOrDefault(x => x.FileName.Contains("InfoSocProtection.txt"));
                        if (file != null)
                        {
                            using (var stream = (Stream) file.OpenReader())
                            {
                                string baseTable = $@"{ImportSchemaName}{nameof(ImportSocialPayment)}";
                                var childTable =
                                    $"{baseTable}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}";

                                StreamingLoad(stream, sqlQueryPerformer, baseTable, childTable);
                            }
                        }

                        //перенос данных
                        TransferToRemoteStorage(sqlQueryPerformer, importHelperDto);
                        //TransferToMainStorage(sqlExecutor, importInfo);
                        RegisterData(sqlQueryPerformer, importHelperDto);
                    }
                    catch
                    {
                        if (importHelperDto != null)
                        {
                            sqlQueryPerformer.PerformSql(
                                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportHousingCharacteristic)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
                            sqlQueryPerformer.PerformSql(
                                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportAccrual)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
                            sqlQueryPerformer.PerformSql(
                                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportMeterReading)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
                            sqlQueryPerformer.PerformSql(
                                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportPayment)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
                            sqlQueryPerformer.PerformSql(
                                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportPaymentRequisite)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
                            sqlQueryPerformer.PerformSql(
                                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportSocialPayment)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
                        }
                        throw;
                    }
                }
            }

        }
        
        /// <summary>
        /// Перенос данных в удаленное хранилище
        /// </summary>
        private void TransferToRemoteStorage(SqlQueryPerformer sqlQueryPerformer, ImportHelperDto importHelperDto)
        {
            var importHousingCharacteristicTableName = $@"{ImportSchemaName}{nameof(ImportHousingCharacteristic)}";
            var importAccrualTableName = $@"{ImportSchemaName}{nameof(ImportAccrual)}";
            var importMeterReadingTableName = $@"{ImportSchemaName}{nameof(ImportMeterReading)}";
            var importPaymentTableName = $@"{ImportSchemaName}{nameof(ImportPayment)}";
            var importPaymentRequisiteTableName = $@"{ImportSchemaName}{nameof(ImportPaymentRequisite)}";
            var importSocialPaymentTableName = $@"{ImportSchemaName}{nameof(ImportSocialPayment)}";

            string addressSpaceTableName = $@"{StorageSchemaName}""{nameof(AddressSpace)}""";
            string houseTableName = $@"{StorageSchemaName}""{nameof(House)}""";
            string managmentOrganizationTableName = $@"{StorageSchemaName}""{nameof(ManagmentOrganization)}""";
            string serviceTableName = $@"{StorageSchemaName}""{nameof(Service)}""";
            string meterServiceTableName = $@"{StorageSchemaName}""{nameof(MeterService)}""";
            string supplierTableName = $@"{StorageSchemaName}""{nameof(Supplier)}""";
            string personalAccountTableName = $@"{StorageSchemaName}""{nameof(PersonalAccount)}""";
            string personalAccountAccrualTableName = $@"{StorageSchemaName}""{nameof(PersonalAccountAccrual)}""";
            string personalAccountMeterReadingTableName =
                $@"{StorageSchemaName}""{nameof(PersonalAccountMeterReading)}""";
            string houseMeterReadingTableName = $@"{StorageSchemaName}""{nameof(HouseMeterReading)}""";
            string paymentTableName = $@"{StorageSchemaName}""{nameof(PersonalAccountPayment)}""";
            string paymentRequisiteTableName = $@"{StorageSchemaName}""{nameof(PersonalAccountPaymentRequisite)}""";
            string socialPaymentTableName = $@"{StorageSchemaName}""{nameof(PersonalAccountSocialPayment)}""";
            
            var sqlQuery =
                $"delete from {importHousingCharacteristicTableName} " +
                $"where \"{nameof(ImportHousingCharacteristic.PersonalAccountState)}\"!={(int)EPersonalAccountState.Opened}";
            sqlQueryPerformer.PerformSql(sqlQuery);


            //если есть подразделения, то грузим только их
            using (var dbContext = new ApplicationDbContext())
            {
                var subdivisionList = dbContext.OrganizationSubdivision
                    .Include(x => x.Organization)
                    .Where(
                        x =>
                            x.Organization.OrganizationExternalId == importHelperDto.Organization.OrganizationExternalId)
                            .Select(x=>x.OrganizationSubdivisionExternalId)
                    .ToList();

                if (subdivisionList.Any())
                {
                    sqlQuery = $" delete from {importHousingCharacteristicTableName} " +
                               $" where \"{nameof(ImportHousingCharacteristic.ManagmentOrganizationNameId)}\" " +
                               $" not in ({string.Join(",", subdivisionList)})";
                    sqlQueryPerformer.PerformSql(sqlQuery);
                }
            }


            sqlQuery =
                $"delete from {importAccrualTableName} " +
                $"where " +
                $" not exists (select 1 from {importHousingCharacteristicTableName} a " +
                $"where a.\"{nameof(ImportHousingCharacteristic.PaymentCode)}\" = \"{nameof(ImportAccrual.PaymentCode)}\")";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $"delete from {importMeterReadingTableName} " +
                $"where " +
                $" not exists (select 1 from {importHousingCharacteristicTableName} a " +
                $"where a.\"{nameof(ImportHousingCharacteristic.PaymentCode)}\" = \"{nameof(ImportMeterReading.PaymentCode)}\")";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $"delete from {importPaymentTableName} " +
                $"where  " +
                $" not exists (select 1 from {importHousingCharacteristicTableName} a " +
                $"where a.\"{nameof(ImportHousingCharacteristic.PaymentCode)}\" = \"{nameof(ImportPayment.PaymentCode)}\")";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $"delete from {importPaymentRequisiteTableName} " +
                $"where  " +
                $" not exists (select 1 from {importHousingCharacteristicTableName} a " +
                $"where a.\"{nameof(ImportHousingCharacteristic.PaymentCode)}\" = \"{nameof(ImportPaymentRequisite.PaymentCode)}\")";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $"delete from {importSocialPaymentTableName} " +
                $"where  " +
                $" not exists (select 1 from {importHousingCharacteristicTableName} a " +
                $"where a.\"{nameof(ImportHousingCharacteristic.PaymentCode)}\" = \"{nameof(ImportSocialPayment.PaymentCode)}\")";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@"INSERT INTO {addressSpaceTableName
                    }(""StreetFullAddress"", ""Town"", ""Place"", ""Street"", ""OrganizationId"")
                SELECT                
                COALESCE(CASE WHEN TRIM(""Place"") = '-' THEN '' ELSE TRIM(""Place"") || ', ' END, '') ||
                TRIM(COALESCE(""Street"", '')) AS ""StreetFullAddress"",
                TRIM(""Town"") AS ""Town"",
                TRIM(""Place"") AS ""Place"",
                TRIM(""Street"") AS ""Street"",
                {
                    importHelperDto.Organization.Id} AS ""OrganizationId""
                FROM {
                    importHousingCharacteristicTableName
                    } p
                WHERE NOT EXISTS
                (SELECT 1 FROM {addressSpaceTableName
                    } a
                WHERE a.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
                AND a.""StreetFullAddress"" = COALESCE(CASE WHEN TRIM(p.""Place"") = '-' THEN '' ELSE TRIM(p.""Place"") || ', ' END, '') ||
                TRIM(COALESCE(p.""Street"", ''))
                )
                GROUP BY 1, 2, 3, 4; ";

            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@" 
INSERT INTO {houseTableName
                    } 
(""HouseFullAddress"", ""StreetFullAddress"", 
""Town"", ""Place"", ""Street"", ""HouseNumber"", ""HousingNumber"", ""SquareHouse"", 
""SquareHouseMop"", ""SquareHouseHeating"", ""DataBankPrefix"", 
""AddressSpaceId"", ""OrganizationId""
)
SELECT 
COALESCE(CASE WHEN TRIM(p.""Place"")='-' THEN '' ELSE TRIM(p.""Place"")||', ' END,'')||
TRIM(COALESCE(p.""Street"",''))||', '||
'д. '||TRIM(COALESCE(p.""HouseNumber"",''))||
TRIM(COALESCE(CASE WHEN p.""HousingNumber""='-' THEN '' ELSE ', корп. '||p.""HousingNumber"" END,''))
AS ""HouseFullAddress"",
COALESCE(CASE WHEN TRIM(p.""Place"")='-' THEN '' ELSE TRIM(p.""Place"")||', ' END,'')||
TRIM(COALESCE(p.""Street"",'')) AS ""StreetFullAddress"",
TRIM(p.""Town"") AS ""Town"",
TRIM(p.""Place"") AS ""Place"",
TRIM(p.""Street"") AS ""Street"",
TRIM(p.""HouseNumber"") AS ""HouseNumber"",
TRIM(p.""HousingNumber"") AS ""HousingNumber"",
p.""SquareHouse"", 
p.""SquareHouseMop"", 
p.""SquareHouseHeating"", 
p.""DataBankPrefix"",

(SELECT ""Id""
FROM {
                    addressSpaceTableName} a 
WHERE a.""OrganizationId"" = {importHelperDto.Organization.Id
                    } 
AND a.""StreetFullAddress"" = COALESCE(CASE WHEN TRIM(p.""Place"")='-' THEN '' ELSE TRIM(p.""Place"")||', ' END,'')||
TRIM(COALESCE(p.""Street"",''))
) AS ""AddressSpaceId"",
{
                    importHelperDto.Organization.Id} AS ""OrganizationId""

FROM {importHousingCharacteristicTableName
                    } p
WHERE NOT EXISTS 
( SELECT 1 FROM {houseTableName}  a 
WHERE a.""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    }  
AND a.""HouseFullAddress"" = COALESCE(CASE WHEN TRIM(p.""Place"")='-' THEN '' ELSE TRIM(p.""Place"")||', ' END,'')||
TRIM(COALESCE(p.""Street"",''))||', '||
'д. '||TRIM(COALESCE(p.""HouseNumber"",''))||
TRIM(COALESCE(CASE WHEN p.""HousingNumber""='-' THEN '' ELSE ', корп. '||p.""HousingNumber"" END,''))
)
GROUP BY 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12;";

            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@" 
INSERT INTO {managmentOrganizationTableName
                    } (""ManagmentOrganizationName"", ""OrganizationId"")
SELECT 
TRIM(o.""ManagmentOrganizationName"") AS ""ManagmentOrganizationName"",
{
                    importHelperDto.Organization.Id}   AS ""OrganizationId""
from {importHousingCharacteristicTableName
                    } o 
WHERE NOT EXISTS
(
SELECT 1 FROM {managmentOrganizationTableName
                    } m
WHERE m.""OrganizationId"" = {importHelperDto.Organization.Id
                    }   
AND ""ManagmentOrganizationName"" = o.""ManagmentOrganizationName""
)
GROUP BY 1,2;";

            sqlQueryPerformer.PerformSql(sqlQuery);


            sqlQuery =
                $@" 
INSERT INTO {personalAccountTableName
                    }
(
            ""PersonalAccountNumber"", ""PaymentCode"", ""ApartmentFullAddress"", 
            ""HouseFullAddress"", ""StreetFullAddress"", ""Town"", ""Place"", ""Street"", 
            ""HouseNumber"", ""HousingNumber"", ""ApartmentAddress"", ""ApartmentNumber"", 
            ""RoomNumber"", ""PorchNumber"", ""ApartmentOwnerFullName"", ""ManagmentOrganizationName"", 
            ""ComfortableType"", ""PrivatiziedType"", ""FloorNumber"", ""ApartmentsCountOnFloor"", 
            ""SquareApartment"", ""SquareApartmentLiving"", ""SquareApartmentHeating"", 
            ""SquareHouse"", ""SquareHouseMop"", ""SquareHouseHeating"", ""CountResidents"", 
            ""CountDeparture"", ""CountArrive"", ""CountRooms"", ""DataBankPrefix"", 
            ""PersonalAccountState"", ""AddressSpaceId"", 
            ""HouseId"", ""ManagmentOrganizationId"", ""OrganizationId""
)
SELECT 
p.""PersonalAccountNumber"" AS ""PersonalAccountNumber"",
p.""PaymentCode"",

COALESCE(CASE WHEN TRIM(p.""Place"")='-' THEN '' ELSE TRIM(p.""Place"")||', ' END,'')||
TRIM(COALESCE(p.""Street"",''))||', '||
'д. '||TRIM(COALESCE(p.""HouseNumber"",''))||
TRIM(COALESCE(CASE WHEN p.""HousingNumber""='-' THEN '' ELSE ', корп. '||p.""HousingNumber"" END,''))||
', кв. '||TRIM(COALESCE(p. ""ApartmentNumber"",''))||
TRIM(COALESCE(CASE WHEN p.""RoomNumber""='-' THEN '' ELSE ', комн. '||p.""RoomNumber"" END,''))
AS ""ApartmentFullAddress"",

COALESCE(CASE WHEN TRIM(p.""Place"")='-' THEN '' ELSE TRIM(p.""Place"")||', ' END,'')||
TRIM(COALESCE(p.""Street"",''))||', '||
'д. '||TRIM(COALESCE(p.""HouseNumber"",''))||
TRIM(COALESCE(CASE WHEN p.""HousingNumber""='-' THEN '' ELSE ', корп. '||p.""HousingNumber"" END,''))
AS ""HouseFullAddress"",

COALESCE(CASE WHEN TRIM(p.""Place"")='-' THEN '' ELSE TRIM(p.""Place"")||', ' END,'')||
TRIM(COALESCE(p.""Street"",'')) AS ""StreetFullAddress"",

TRIM(p.""Town"") AS ""Town"",
TRIM(p.""Place"") AS ""Place"",
TRIM(p.""Street"") AS ""Street"",
TRIM(p.""HouseNumber"") AS ""HouseNumber"",
TRIM(p.""HousingNumber"") AS ""HousingNumber"",

'кв. '||TRIM(COALESCE(p.""ApartmentNumber"",''))||
TRIM(COALESCE(CASE WHEN p.""RoomNumber""='-' THEN '' ELSE ', комн. '||p.""RoomNumber"" END,''))
AS ""ApartmentAddress"",
p.""ApartmentNumber"" AS ""ApartmentNumber"",
p.""RoomNumber"" AS ""RoomNumber"",
p.""PorchNumber"",
p.""ApartmentOwnerFullName"",
p.""ManagmentOrganizationName"",
p.""ComfortableType"",
p.""PrivatiziedType"",
p.""FloorNumber"",
p.""ApartmentsCountOnFloor"",
p.""SquareApartment"",
p.""SquareApartmentLiving"",
p.""SquareApartmentHeating"",
p.""SquareHouse"", 
p.""SquareHouseMop"", 
p.""SquareHouseHeating"", 
p.""CountResidents"",
p.""CountDeparture"",
p.""CountArrive"",
p.""CountRooms"",
p.""DataBankPrefix"",
p.""PersonalAccountState"",
h.""AddressSpaceId"",
h.""Id"",
m.""Id"",

{
                    importHelperDto.Organization.Id}   AS ""OrganizationId""

FROM {
                    importHousingCharacteristicTableName}  p
left outer join {houseTableName
                    } h on (h.""OrganizationId"" = {importHelperDto.Organization.Id
                    }  
AND h.""HouseFullAddress""  = COALESCE(CASE WHEN TRIM(p.""Place"")='-' THEN '' ELSE TRIM(p.""Place"")||', ' END,'')||
TRIM(COALESCE(p.""Street"",''))||', '||
'д. '||TRIM(COALESCE(p.""HouseNumber"",''))||
TRIM(COALESCE(CASE WHEN p.""HousingNumber""='-' THEN '' ELSE ', корп. '||p.""HousingNumber"" END,''))
)
left outer join {
                    managmentOrganizationTableName} m
ON ( m.""OrganizationId"" = {importHelperDto.Organization.Id
                    }  
AND m.""ManagmentOrganizationName"" = TRIM(p.""ManagmentOrganizationName"")
) 
WHERE NOT EXISTS 
(
SELECT 1
FROM  {
                    personalAccountTableName
                    } pa
WHERE pa.""PaymentCode"" = p.""PaymentCode""
AND pa.""OrganizationId"" = {
                    importHelperDto.Organization.Id} )
;";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@" 
INSERT INTO {serviceTableName
                    } 
(""ServiceName"", ""OuterSystemServiceId"", ""OuterSystemBaseServiceId"", ""MeasureName"", ""ServiceGroupName"",
            ""OrganizationId"")
SELECT 
ch.""ServiceName"",
ch.""OuterSystemServiceId"",
ch.""OuterSystemBaseServiceId"",
ch.""MeasureName"",
ch.""ServiceGroupName"",
{
                    importHelperDto.Organization.Id} AS ""OrganizationId""
FROM {importAccrualTableName
                    } ch 
WHERE NOT EXISTS
(
SELECT 1 
FROM {serviceTableName}  s
WHERE s.""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    }
AND s.""OuterSystemServiceId""  = ch.""OuterSystemServiceId""
AND s.""MeasureName""  = TRIM(ch.""MeasureName"")
)
GROUP BY 1,2,3,4,5;";

            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@" 
INSERT INTO {supplierTableName
                    } (""SupplierName"", ""OrganizationId"")
SELECT 
ch.""SupplierName"",{
                    importHelperDto.Organization.Id} AS ""OrganizationId""
FROM  {importAccrualTableName
                    } ch 
WHERE NOT EXISTS
(
SELECT 1 
FROM {supplierTableName} s
WHERE s.""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    }
AND s.""SupplierName""  = TRIM(ch.""SupplierName"")
)
GROUP BY 1;";

            sqlQueryPerformer.PerformSql(sqlQuery);


            sqlQuery =
                $@" 
DELETE  FROM  {personalAccountAccrualTableName} 
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id} 
AND ""CalculationDate""='{importHelperDto.CalculationDate
                    }';
INSERT INTO {personalAccountAccrualTableName
                    }
            (""CalculationDate"", ""PersonalAccountNumber"", ""PaymentCode"", ""ApartmentFullAddress"", 
            ""ServiceName"", ""OuterSystemServiceId"", ""OuterSystemBaseServiceId"", 
            ""MeasureName"", ""ServiceGroupName"", ""Tariff"", 
""Consumption"", 
       ""ConsumptionChn"", ""ConsumptionImd"", ""ConsumptionNorm"", ""ConsumptionHouse"", 
       ""ConsumptionHouseByApartments"", ""ConsumptionHouseByApartmentsImd"", 
       ""ConsumptionHouseByApartmentsNorm"", ""ConsumptionHouseByNonResidential"", 
       ""ConsumptionLift"", ""ConsumptionHouseChn"", ""ConsumptionChmd"", 
       ""AccruedTariff"", ""AccruedTariffNondelivery"", ""SumNondelivery"", 
       ""ConsumptionNondelivery"", ""NondeliveryDaysCount"", ""Reval"", ""SumBalanceDelta"", 
       ""AccruedForPayment"", ""SumPayd"", ""SumOutsaldo"", ""SumInsaldo"", 
       ""SumTariffChn"", ""SumInsaldoChn"", ""SumOutsaldoChn"", ""RevalChn"", 
       ""SumBalanceDeltaChn"", ""AccruedForPaymentChn"", ""SumPaydChn"", ""AccruedBySocNorm"", 
       ""CorrectionCoefficientImd"", ""CorrectionCoefficientNorm"", ""SupplierName"", 
       ""ServiceDeliveryDays"", ""NondeliveryDaysCountOnPast"", ""CreationTime"", ""ChangedTime"", 
            ""BaseServiceId"", ""HouseId"", ""OrganizationId"", ""PersonalAccountId"", 
            ""ServiceId"", ""SupplierId""            
            )
SELECT 
ch.""CalculationDate"", 
pa.""PersonalAccountNumber"",
ch.""PaymentCode"",
pa.""ApartmentFullAddress"",
s.""ServiceName"",
s.""OuterSystemServiceId"", 
s.""OuterSystemBaseServiceId"", 
s.""MeasureName"", 
s.""ServiceGroupName"",
""Tariff"", 
""Consumption"", 
       ""ConsumptionChn"", ""ConsumptionImd"", ""ConsumptionNorm"", ""ConsumptionHouse"", 
       ""ConsumptionHouseByApartments"", ""ConsumptionHouseByApartmentsImd"", 
       ""ConsumptionHouseByApartmentsNorm"", ""ConsumptionHouseByNonResidential"", 
       ""ConsumptionLift"", ""ConsumptionHouseChn"", ""ConsumptionChmd"", 
       ""AccruedTariff"", ""AccruedTariffNondelivery"", ""SumNondelivery"", 
       ""ConsumptionNondelivery"", ""NondeliveryDaysCount"", ""Reval"", ""SumBalanceDelta"", 
       ""AccruedForPayment"", ""SumPayd"", ""SumOutsaldo"", ""SumInsaldo"", 
       ""SumTariffChn"", ""SumInsaldoChn"", ""SumOutsaldoChn"", ""RevalChn"", 
       ""SumBalanceDeltaChn"", ""AccruedForPaymentChn"", ""SumPaydChn"", ""AccruedBySocNorm"", 
       ""CorrectionCoefficientImd"", ""CorrectionCoefficientNorm"", supp.""SupplierName"", 
       ""ServiceDeliveryDays"", ""NondeliveryDaysCountOnPast"",
        now(), now(), 
            s.""BaseServiceId"", 
            pa.""HouseId"", 
            {
                    importHelperDto.Organization.Id
                    } AS ""OrganizationId"", 
            pa.""Id"" AS ""PersonalAccountId"", 
            s.""Id"" AS ""ServiceId"", 
            supp.""Id"" AS ""SupplierId""

FROM {
                    importAccrualTableName} ch 
INNER JOIN {personalAccountTableName
                    } pa ON 
    (pa.""OrganizationId"" = {importHelperDto.Organization.Id
                    } 
    AND pa.""PaymentCode"" = ch.""PaymentCode"")
LEFT OUTER JOIN {serviceTableName
                    } s ON 
	(s.""OrganizationId"" = {importHelperDto.Organization.Id
                    } 
	AND s.""OuterSystemServiceId"" = ch.""OuterSystemServiceId"" 
	AND s.""MeasureName"" = TRIM(ch.""MeasureName""))
LEFT OUTER JOIN {
                    supplierTableName} supp ON 
	(supp.""OrganizationId"" = {importHelperDto.Organization.Id
                    } 
	AND supp.""SupplierName"" = TRIM(ch.""SupplierName""));";

            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@" INSERT INTO {meterServiceTableName
                    }
(""ServiceName"", ""MeasureName"", ""OrganizationId"")
SELECT 
c.""ServiceName"",
c.""MeasureName"",
{
                    importHelperDto.Organization.Id}  AS ""OrganizationId""
FROM {importMeterReadingTableName
                    } c
WHERE NOT EXISTS
(
SELECT 1 
FROM {meterServiceTableName} s
WHERE s.""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    } 
AND s.""ServiceName""  = TRIM(c.""ServiceName"")
AND s.""MeasureName""  = TRIM(c.""MeasureName"")
)
GROUP BY 1,2;";

            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@" 
INSERT INTO {personalAccountMeterReadingTableName
                    }(
            ""CalculationDate"", ""PersonalAccountNumber"", ""PaymentCode"", 
            ""ApartmentFullAddress"", ""ServiceName"", ""OuterSystemServiceId"", 
            ""OuterSystemBaseServiceId"", ""MeasureName"", ""ServiceGroupName"", 
            ""IndexNumber"", ""MeterDeviceNumber"", ""OuterSystemMeterDeviceId"", 
            ""CalculationApplyingDate"", ""Indication"", ""CalculationApplyingDatePrevious"", 
            ""IndicationPrevious"", ""Multiplier"", ""Consumption"", ""ConsumptionAdditional"", 
            ""ConsumptionNonresidental"", ""ConsumptionLift"", ""Placement"", ""Capacity"", 
            ""TypeName"", ""VerificationDate"", ""VerificationDateNext"", ""CreationTime"", 
            ""ChangedTime"", ""BaseServiceId"", ""HouseId"", ""OrganizationId"", 
            ""PersonalAccountId"", ""MeterServiceId"")
SELECT 
c.""CalculationDate"", 
pa.""PersonalAccountNumber"",
pa.""PaymentCode"",
pa.""ApartmentFullAddress"",
s.""ServiceName"",
s.""OuterSystemServiceId"", 
s.""OuterSystemBaseServiceId"", 
s.""MeasureName"", 
s.""ServiceGroupName"",
c.""IndexNumber"",
c.""MeterDeviceNumber"", 
c.""OuterSystemMeterDeviceId"", 
c.""CalculationApplyingDate"", 
c.""Indication"", 
c.""CalculationApplyingDatePrevious"", 
c.""IndicationPrevious"", 
c.""Multiplier"", 
c.""Consumption"",  
c.""ConsumptionAdditional"",  
c.""ConsumptionNonresidental"",  
c.""ConsumptionLift"",  
c.""Placement"",  
c.""Capacity"", 
c.""TypeName"",  
c.""VerificationDate"",  
c.""VerificationDateNext"",  
now() AS ""CreationTime"",  
now() AS ""ChangedTime"",  
s.""BaseServiceId"",  
pa.""HouseId"",  
{
                    importHelperDto.Organization.Id
                    } AS ""OrganizationId"",  
pa.""Id"" AS ""PersonalAccountId"",  
s.""Id"" AS ""ServiceId""

FROM  {
                    importMeterReadingTableName} c 
INNER JOIN {personalAccountTableName
                    } pa ON 
    (pa.""OrganizationId"" = {importHelperDto.Organization.Id
                    } 
    AND pa.""PaymentCode"" = c.""PaymentCode"")
LEFT OUTER JOIN {meterServiceTableName
                    } s ON 
	(s.""OrganizationId"" = {importHelperDto.Organization.Id
                    } 
	AND s.""ServiceName"" = TRIM(c.""ServiceName"" )
	AND s.""MeasureName"" = TRIM(c.""MeasureName""))

WHERE c.""MeterReadingType"" = 3
AND NOT EXISTS
(
SELECT 1 FROM
{
                    personalAccountMeterReadingTableName} mr
WHERE mr.""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    } 
AND mr.""PaymentCode"" = c.""PaymentCode""
AND mr.""OuterSystemMeterDeviceId"" = c.""OuterSystemMeterDeviceId""
AND mr.""CalculationDate"" = c.""CalculationDate""
);";

            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@" 
INSERT INTO {houseMeterReadingTableName
                    } (
            ""CalculationDate"", ""ServiceName"", ""OuterSystemServiceId"", 
            ""OuterSystemBaseServiceId"", ""MeasureName"", ""ServiceGroupName"", 
            ""IndexNumber"", ""MeterDeviceNumber"", ""OuterSystemMeterDeviceId"", 
            ""CalculationApplyingDate"", ""Indication"", ""CalculationApplyingDatePrevious"", 
            ""IndicationPrevious"", ""Multiplier"", ""Consumption"", ""ConsumptionAdditional"", 
            ""ConsumptionNonresidental"", ""ConsumptionLift"", ""Placement"", ""Capacity"", 
            ""TypeName"", ""VerificationDate"", ""VerificationDateNext"",
            ""StreetFullAddress"", ""Town"", ""Place"", ""Street"", ""HouseFullAddress"", ""HouseNumber"", 
            ""HousingNumber"", ""SquareHouse"", ""SquareHouseMop"", ""SquareHouseHeating"", 
            ""DataBankPrefix"",""CreationTime"", ""ChangedTime"", 
	    ""AddressSpaceId"", ""BaseServiceId"", ""HouseId"", ""OrganizationId"", 
            ""MeterServiceId"")
SELECT 
c.""CalculationDate"", 
s.""ServiceName"",
s.""OuterSystemServiceId"", 
s.""OuterSystemBaseServiceId"", 
s.""MeasureName"", 
s.""ServiceGroupName"",
c.""IndexNumber"",
c.""MeterDeviceNumber"", 
c.""OuterSystemMeterDeviceId"", 
c.""CalculationApplyingDate"", 
c.""Indication"", 
c.""CalculationApplyingDatePrevious"", 
c.""IndicationPrevious"", 
c.""Multiplier"", 
c.""Consumption"",  
c.""ConsumptionAdditional"",  
c.""ConsumptionNonresidental"",  
c.""ConsumptionLift"",  
c.""Placement"",  
c.""Capacity"", 
c.""TypeName"",  
c.""VerificationDate"",  
c.""VerificationDateNext"",  
""StreetFullAddress"", ""Town"", ""Place"", ""Street"", ""HouseFullAddress"", ""HouseNumber"", 
            ""HousingNumber"", ""SquareHouse"", ""SquareHouseMop"", ""SquareHouseHeating"", 
            ""DataBankPrefix"",
now() AS ""CreationTime"",  
now() AS ""ChangedTime"",  
""AddressSpaceId"", 
s.""BaseServiceId"",  
pa.""HouseId"",  
{
                    importHelperDto.Organization.Id} AS ""OrganizationId"",  
s.""Id"" AS ""ServiceId""

FROM  {
                    importMeterReadingTableName}  c 
INNER JOIN {personalAccountTableName
                    } pa ON 
    (pa.""OrganizationId"" = {importHelperDto.Organization.Id
                    } 
    AND pa.""PaymentCode"" = c.""PaymentCode"")
LEFT OUTER JOIN {meterServiceTableName
                    } s ON 
	(s.""OrganizationId"" = {importHelperDto.Organization.Id
                    } 
	AND s.""ServiceName"" = TRIM(c.""ServiceName"")
	AND s.""MeasureName"" = TRIM(c.""MeasureName""))

WHERE c.""MeterReadingType"" = 1
AND NOT EXISTS
(
SELECT 1 FROM
{
                    houseMeterReadingTableName} mr
WHERE mr.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND mr.""OuterSystemMeterDeviceId"" = c.""OuterSystemMeterDeviceId""
AND mr.""CalculationDate"" = c.""CalculationDate""
)
GROUP BY 1,2,3,4,5,6,7,8,9,10,
11,12,13,14,15,16,17,18,19,20,
21,22,23,24,25,26,27,28,29,30,
31,32,33,34,35,36,37,38,39,40,41;";

            sqlQueryPerformer.PerformSql(sqlQuery);


            sqlQuery =
                $@" 
INSERT INTO {paymentTableName
                    }(
            ""CalculationDate"", ""PersonalAccountNumber"", ""PaymentCode"", ""ApartmentFullAddress"", 
            ""PaymentDate"", ""CalculationApplyingDate"", 
            ""PaydCalculationMonth"", ""SumPayment"", ""PaymentPlacement"", ""CreationTime"", 
            ""ChangedTime"", ""OrganizationId"",""HouseId"", ""PersonalAccountId"")
SELECT 
p.""CalculationDate"", 
pa.""PersonalAccountNumber"",
pa.""PaymentCode"",
pa.""ApartmentFullAddress"",
p.""PaymentDate"", 
p.""CalculationApplyingDate"", 
p.""PaydCalculationMonth"", 
p.""SumPayment"", 
p.""PaymentPlacement"", 
NOW() AS ""CreationTime"", 
NOW() AS ""ChangedTime"", 
{
                    importHelperDto.Organization.Id} AS ""OrganizationId"",
pa.""HouseId"",
pa.""Id""

FROM  {
                    importPaymentTableName} p 
INNER JOIN {personalAccountTableName
                    } pa ON 
    (pa.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
    AND pa.""PaymentCode"" = p.""PaymentCode"")
WHERE NOT EXISTS
(
SELECT 1
FROM {
                    paymentTableName} pay
WHERE pay.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND pay.""PaymentCode"" = p.""PaymentCode""
AND pay.""CalculationDate"" =  p.""CalculationDate"" 
); ";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@" 
INSERT INTO {paymentRequisiteTableName
                    }(
           ""CalculationDate"", ""PersonalAccountId"", ""PersonalAccountNumber"", 
            ""PaymentCode"", ""ApartmentFullAddress"", ""HouseId"", ""RequisiteType"", 
            ""RecipientName"", ""RecipientBankName"", ""RecipientCheckingAccount"", 
            ""RecipientCorrespondentAccount"", ""RecipientInn"", ""RecipientKpp"", 
            ""RecipientBankBik"", ""RecipientAddress"", ""RecipientPhone"", ""RecipientEmail"", 
            ""RecipientNote"", ""ProviderName"", ""ProviderBankName"", ""ProviderCheckingAccount"", 
            ""ProviderCorrespondentAccount"", ""ProviderInn"", ""ProviderKpp"", 
            ""ProviderBankBik"", ""ProviderAddress"", ""ProviderPhone"", ""ProviderEmail"", 
            ""ProviderNote"", ""ManagmentOrganizationCode"", ""Note"",
            ""CreationTime"", ""ChangedTime"", ""OrganizationId"")
SELECT 
p.""CalculationDate"", 
pa.""Id"",
pa.""PersonalAccountNumber"",
pa.""PaymentCode"",
pa.""ApartmentFullAddress"",
pa.""HouseId"",
""RequisiteType"", 
""RecipientName"", ""RecipientBankName"", ""RecipientCheckingAccount"", 
""RecipientCorrespondentAccount"", ""RecipientInn"", ""RecipientKpp"", 
""RecipientBankBik"", ""RecipientAddress"", ""RecipientPhone"", ""RecipientEmail"", 
""RecipientNote"", ""ProviderName"", ""ProviderBankName"", ""ProviderCheckingAccount"", 
""ProviderCorrespondentAccount"", ""ProviderInn"", ""ProviderKpp"", 
""ProviderBankBik"", ""ProviderAddress"", ""ProviderPhone"", ""ProviderEmail"", 
""ProviderNote"", ""ManagmentOrganizationCode"", ""Note"", 
NOW() AS ""CreationTime"", 
NOW() AS ""ChangedTime"", 
{
                    importHelperDto.Organization.Id} AS ""OrganizationId""

FROM  {importPaymentRequisiteTableName
                    } p 
INNER JOIN {personalAccountTableName} pa ON 
    (pa.""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    }
    AND pa.""PaymentCode"" = p.""PaymentCode"")
WHERE NOT EXISTS
(
SELECT 1
FROM {
                    paymentRequisiteTableName} pr
WHERE pr.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND pr.""PaymentCode"" = p.""PaymentCode""
AND pr.""CalculationDate"" =  p.""CalculationDate"" 
); ";
            sqlQueryPerformer.PerformSql(sqlQuery);


            sqlQuery =
                $@" 
INSERT INTO {socialPaymentTableName
                    }(
""CalculationDate"",
""PersonalAccountId"",
""PersonalAccountNumber"", 
""PaymentCode"", 
""ApartmentFullAddress"", 
""HouseId"", 
""SumAccrued"", ""SumPayoff"", ""PaymentPlacement"", 
""SumDelta"", ""OrganizationId"", ""CreationTime"", ""ChangedTime"", 
 ""RecipientName"", ""ArticleCode"", ""ArticleName"", 
""ArticleGroupName"", ""SubsidiesEndDate"", ""SumInsaldo"", ""SumRecalculation"")

            select 
p.""CalculationDate"", 
pa.""Id"",
pa.""PersonalAccountNumber"",
pa.""PaymentCode"",
pa.""ApartmentFullAddress"",
pa.""HouseId"",
""SumAccrued"", ""SumPayoff"", ""PaymentPlacement"", 
""SumDelta"",
{
                    importHelperDto.Organization.Id
                    } AS ""OrganizationId"",
NOW() AS ""CreationTime"", 
NOW() AS ""ChangedTime"", 
 ""RecipientName"", ""ArticleCode"", ""ArticleName"", 
""ArticleGroupName"", ""SubsidiesEndDate"", ""SumInsaldo"", ""SumRecalculation""

FROM  {
                    importSocialPaymentTableName} p 
INNER JOIN {personalAccountTableName
                    } pa ON 
    (pa.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
    AND pa.""PaymentCode"" = p.""PaymentCode"")
WHERE NOT EXISTS
(
SELECT 1
FROM {
                    socialPaymentTableName} pr
WHERE pr.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND pr.""PaymentCode"" = p.""PaymentCode""
AND pr.""CalculationDate"" =  p.""CalculationDate"" 
and pr.""ArticleCode"" = p.""ArticleCode""
); ";


            sqlQueryPerformer.PerformSql(sqlQuery);


            //TODO: допилить загрузку соц.выплат

            sqlQueryPerformer.PerformSql(
                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportHousingCharacteristic)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
            sqlQueryPerformer.PerformSql(
                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportAccrual)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
            sqlQueryPerformer.PerformSql(
                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportMeterReading)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
            sqlQueryPerformer.PerformSql(
                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportPayment)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
            sqlQueryPerformer.PerformSql(
                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportPaymentRequisite)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
            sqlQueryPerformer.PerformSql(
                $"DROP TABLE IF EXISTS {ImportSchemaName}{nameof(ImportSocialPayment)}_{importHelperDto.Organization.Id}_{importHelperDto.CalculationDate.ToString("yyyyMM")}");
        }

        /// <summary>
        /// Перенос данных в основное (оперативное) хранилище
        /// </summary>
        private void TransferToMainStorage(SqlQueryPerformer sqlQueryPerformer, ImportHelperDto importHelperDto)
        {
            var sqlQuery =
                $@"
INSERT INTO {MainSchemaName}""{nameof(AddressSpace)
                    }""
(""StreetFullAddress"", ""Town"", ""Place"", ""Street"", ""CreationTime"", 
            ""ChangedTime"", ""OrganizationId"")
SELECT  ""StreetFullAddress"", ""Town"", ""Place"", ""Street"", ""CreationTime"", 
            ""ChangedTime"", ""OrganizationId"" FROM {
                    StorageSchemaName}""{nameof(AddressSpace)}"" s
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id} 
AND NOT EXISTS
(
SELECT 1
FROM {MainSchemaName}""{
                    nameof(AddressSpace)}"" a
WHERE a.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND s.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND a.""StreetFullAddress"" = s.""StreetFullAddress""
);
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@"
INSERT INTO {MainSchemaName}""{nameof(ManagmentOrganization)
                    }""
(""ManagmentOrganizationName"", ""CreationTime"", ""ChangedTime"", 
            ""OrganizationId"")
SELECT ""ManagmentOrganizationName"", ""CreationTime"", ""ChangedTime"", 
            ""OrganizationId"" 
FROM {
                    StorageSchemaName}""{nameof(ManagmentOrganization)}"" s
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id} 
AND NOT EXISTS
(
SELECT 1
FROM {MainSchemaName}""{
                    nameof(ManagmentOrganization)}"" a
WHERE a.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND s.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND a.""ManagmentOrganizationName"" = s.""ManagmentOrganizationName""
);
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@"
INSERT INTO {MainSchemaName}""{nameof(Service)
                    }""
(""Id"", ""ServiceName"", ""OuterSystemServiceId"", ""OuterSystemBaseServiceId"", 
            ""MeasureName"", ""ServiceGroupName"", ""CreationTime"", ""ChangedTime"", 
            ""BaseServiceId"", ""OrganizationId"")
SELECT  ""Id"", ""ServiceName"", ""OuterSystemServiceId"", ""OuterSystemBaseServiceId"", 
            ""MeasureName"", ""ServiceGroupName"", ""CreationTime"", ""ChangedTime"", 
            ""BaseServiceId"", ""OrganizationId"" FROM {
                    StorageSchemaName}""{nameof(Service)}"" s
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    } 
AND NOT EXISTS
(
SELECT 1
FROM {MainSchemaName}""{nameof(Service)
                    }"" a
WHERE a.""OrganizationId"" = {importHelperDto.Organization.Id}
AND s.""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    }
AND a.""ServiceName"" = s.""ServiceName""
AND a.""MeasureName"" = s.""MeasureName""
);
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@"
INSERT INTO {MainSchemaName}""{nameof(Supplier)
                    }""
(""SupplierName"", ""CreationTime"", ""ChangedTime"", ""OrganizationId"")
SELECT ""SupplierName"", ""CreationTime"", ""ChangedTime"", ""OrganizationId"" FROM {
                    StorageSchemaName}""{nameof(Supplier)}"" s
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    } 
AND NOT EXISTS
(
SELECT 1
FROM {MainSchemaName}""{nameof(Supplier)
                    }"" a
WHERE a.""OrganizationId"" = {importHelperDto.Organization.Id}
AND s.""OrganizationId"" = {
                    importHelperDto.Organization.Id}
AND a.""SupplierName"" = s.""SupplierName""
);
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);


            sqlQuery =
                $@"
INSERT INTO {MainSchemaName}""{nameof(House)
                    }""
(""HouseFullAddress"", ""StreetFullAddress"", 
            ""Town"", ""Place"", ""Street"", ""HouseNumber"", ""HousingNumber"", ""SquareHouse"", 
            ""SquareHouseMop"", ""SquareHouseHeating"", ""DataBankPrefix"", ""CreationTime"", 
            ""ChangedTime"", ""AddressSpaceId"", ""OrganizationId"")
SELECT ""HouseFullAddress"", ""StreetFullAddress"", 
            ""Town"", ""Place"", ""Street"", ""HouseNumber"", ""HousingNumber"", ""SquareHouse"", 
            ""SquareHouseMop"", ""SquareHouseHeating"", ""DataBankPrefix"", ""CreationTime"", 
            ""ChangedTime"", ""AddressSpaceId"", ""OrganizationId"" FROM {
                    StorageSchemaName}""{nameof(House)}"" s
WHERE ""OrganizationId"" = {importHelperDto.Organization.Id
                    } 
AND NOT EXISTS
(
SELECT 1
FROM {MainSchemaName}""{nameof(House)
                    }"" a
WHERE a.""OrganizationId"" = {importHelperDto.Organization.Id}
AND s.""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    }
AND a.""HouseFullAddress"" = s.""HouseFullAddress""
);
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);

            sqlQuery =
                $@"
INSERT INTO {MainSchemaName}""{nameof(PersonalAccount)
                    }"" (""PersonalAccountNumber"",  ""PaymentCode"", 
            ""ApartmentFullAddress"", ""HouseFullAddress"", ""StreetFullAddress"", 
            ""Town"", ""Place"", ""Street"", ""HouseNumber"", ""HousingNumber"", ""ApartmentAddress"", 
            ""ApartmentNumber"", ""RoomNumber"", ""PorchNumber"", ""ApartmentOwnerFullName"", 
            ""ManagmentOrganizationName"", ""ComfortableType"", ""PrivatiziedType"", 
            ""FloorNumber"", ""ApartmentsCountOnFloor"", ""SquareApartment"", ""SquareApartmentLiving"", 
            ""SquareApartmentHeating"", ""SquareHouse"", ""SquareHouseMop"", ""SquareHouseHeating"", 
            ""CountResidents"", ""CountDeparture"", ""CountArrive"", ""CountRooms"", 
            ""DataBankPrefix"", ""PersonalAccountState"", ""CreationTime"", ""ChangedTime"", 
            ""AddressSpaceId"", ""HouseId"", ""ManagmentOrganizationId"", ""OrganizationId"")
SELECT ""PersonalAccountNumber"",  ""PaymentCode"", 
            ""ApartmentFullAddress"", ""HouseFullAddress"", ""StreetFullAddress"", 
            ""Town"", ""Place"", ""Street"", ""HouseNumber"", ""HousingNumber"", ""ApartmentAddress"", 
            ""ApartmentNumber"", ""RoomNumber"", ""PorchNumber"", ""ApartmentOwnerFullName"", 
            ""ManagmentOrganizationName"", ""ComfortableType"", ""PrivatiziedType"", 
            ""FloorNumber"", ""ApartmentsCountOnFloor"", ""SquareApartment"", ""SquareApartmentLiving"", 
            ""SquareApartmentHeating"", ""SquareHouse"", ""SquareHouseMop"", ""SquareHouseHeating"", 
            ""CountResidents"", ""CountDeparture"", ""CountArrive"", ""CountRooms"", 
            ""DataBankPrefix"", ""PersonalAccountState"", ""CreationTime"", ""ChangedTime"", 
            ""AddressSpaceId"", ""HouseId"", ""ManagmentOrganizationId"", ""OrganizationId""
FROM {
                    StorageSchemaName}""{nameof(PersonalAccount)}"" s
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id} 
AND NOT EXISTS
(
SELECT 1
FROM {MainSchemaName}""{
                    nameof(PersonalAccount)}"" a
WHERE a.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND s.""OrganizationId"" = {importHelperDto.Organization.Id
                    }
AND a.""PaymentCode"" = s.""PaymentCode""
);
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);


            sqlQuery =
                $@"
DELETE FROM {MainSchemaName}""{nameof(PersonalAccountAccrual)}"" 
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id}; 
INSERT INTO  {MainSchemaName}""{nameof(PersonalAccountAccrual)
                    }"" 
(""CalculationDate"", ""PersonalAccountNumber"", ""PaymentCode"", 
            ""ApartmentFullAddress"", ""ServiceName"", ""OuterSystemServiceId"", 
            ""OuterSystemBaseServiceId"", ""MeasureName"", ""ServiceGroupName"", 
            ""SupplierName"", ""Consumption"", ""ConsumptionChn"", ""ConsumptionImd"", 
            ""ConsumptionNorm"", ""ConsumptionHouse"", ""ConsumptionHouseByApartments"", 
            ""ConsumptionHouseByApartmentsImd"", ""ConsumptionHouseByApartmentsNorm"", 
            ""ConsumptionHouseByNonResidential"", ""ConsumptionLift"", ""ConsumptionHouseChn"", 
            ""ConsumptionChmd"", ""AccruedTariff"", ""AccruedTariffNondelivery"", 
            ""SumNondelivery"", ""ConsumptionNondelivery"", ""Reval"", ""SumBalanceDelta"", 
            ""AccruedForPayment"", ""SumPayd"", ""SumOutsaldo"", ""SumInsaldo"", 
            ""SumTariffChn"", ""SumInsaldoChn"", ""SumOutsaldoChn"", ""RevalChn"", 
            ""SumBalanceDeltaChn"", ""AccruedForPaymentChn"", ""SumPaydChn"", ""AccruedBySocNorm"", 
            ""IndexNumber"", ""Tariff"", ""NondeliveryDaysCount"", ""CorrectionCoefficientImd"", 
            ""CorrectionCoefficientNorm"", ""ServiceDeliveryDays"", ""NondeliveryDaysCountOnPast"", 
            ""CreationTime"", ""ChangedTime"", ""BaseServiceId"", ""HouseId"", 
            ""OrganizationId"", ""PersonalAccountId"", ""ServiceId"", ""SupplierId"")
SELECT ""CalculationDate"", ""PersonalAccountNumber"", ""PaymentCode"", 
            ""ApartmentFullAddress"", ""ServiceName"", ""OuterSystemServiceId"", 
            ""OuterSystemBaseServiceId"", ""MeasureName"", ""ServiceGroupName"", 
            ""SupplierName"", ""Consumption"", ""ConsumptionChn"", ""ConsumptionImd"", 
            ""ConsumptionNorm"", ""ConsumptionHouse"", ""ConsumptionHouseByApartments"", 
            ""ConsumptionHouseByApartmentsImd"", ""ConsumptionHouseByApartmentsNorm"", 
            ""ConsumptionHouseByNonResidential"", ""ConsumptionLift"", ""ConsumptionHouseChn"", 
            ""ConsumptionChmd"", ""AccruedTariff"", ""AccruedTariffNondelivery"", 
            ""SumNondelivery"", ""ConsumptionNondelivery"", ""Reval"", ""SumBalanceDelta"", 
            ""AccruedForPayment"", ""SumPayd"", ""SumOutsaldo"", ""SumInsaldo"", 
            ""SumTariffChn"", ""SumInsaldoChn"", ""SumOutsaldoChn"", ""RevalChn"", 
            ""SumBalanceDeltaChn"", ""AccruedForPaymentChn"", ""SumPaydChn"", ""AccruedBySocNorm"", 
            ""IndexNumber"", ""Tariff"", ""NondeliveryDaysCount"", ""CorrectionCoefficientImd"", 
            ""CorrectionCoefficientNorm"", ""ServiceDeliveryDays"", ""NondeliveryDaysCountOnPast"", 
            ""CreationTime"", ""ChangedTime"", ""BaseServiceId"", ""HouseId"", 
            ""OrganizationId"", ""PersonalAccountId"", ""ServiceId"", ""SupplierId""
FROM {
                    StorageSchemaName}""{nameof(PersonalAccountAccrual)}"" s
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id}
AND ""CalculationDate"" = '{
                    importHelperDto.CalculationDate.ToShortDateString()}';
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);


            sqlQuery =
                $@"
DELETE FROM {MainSchemaName}""{nameof(PersonalAccountMeterReading)
                    }"" 
WHERE ""OrganizationId"" = {importHelperDto.Organization.Id};  
INSERT INTO  {MainSchemaName
                    }""{nameof(PersonalAccountMeterReading)
                    }""
(""CalculationDate"", ""PersonalAccountNumber"", ""PaymentCode"", 
       ""ApartmentFullAddress"", ""ServiceName"", ""OuterSystemServiceId"", 
       ""OuterSystemBaseServiceId"", ""MeasureName"", ""ServiceGroupName"", 
       ""IndexNumber"", ""MeterDeviceNumber"", ""OuterSystemMeterDeviceId"", 
       ""CalculationApplyingDate"", ""Indication"", ""CalculationApplyingDatePrevious"", 
       ""IndicationPrevious"", ""Multiplier"", ""Consumption"", ""ConsumptionAdditional"", 
       ""ConsumptionNonresidental"", ""ConsumptionLift"", ""Placement"", ""Capacity"", 
       ""TypeName"", ""VerificationDate"", ""VerificationDateNext"", ""CreationTime"", 
       ""ChangedTime"", ""BaseServiceId"", ""HouseId"", ""OrganizationId"", 
       ""PersonalAccountId"", ""ServiceId"")
SELECT ""CalculationDate"", ""PersonalAccountNumber"", ""PaymentCode"", 
       ""ApartmentFullAddress"", ""ServiceName"", ""OuterSystemServiceId"", 
       ""OuterSystemBaseServiceId"", ""MeasureName"", ""ServiceGroupName"", 
       ""IndexNumber"", ""MeterDeviceNumber"", ""OuterSystemMeterDeviceId"", 
       ""CalculationApplyingDate"", ""Indication"", ""CalculationApplyingDatePrevious"", 
       ""IndicationPrevious"", ""Multiplier"", ""Consumption"", ""ConsumptionAdditional"", 
       ""ConsumptionNonresidental"", ""ConsumptionLift"", ""Placement"", ""Capacity"", 
       ""TypeName"", ""VerificationDate"", ""VerificationDateNext"", ""CreationTime"", 
       ""ChangedTime"", ""BaseServiceId"", ""HouseId"", ""OrganizationId"", 
       ""PersonalAccountId"", ""ServiceId""
FROM {
                    StorageSchemaName}""{nameof(PersonalAccountMeterReading)}"" s
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id}
AND ""CalculationDate"" = '{
                    importHelperDto.CalculationDate.ToShortDateString()}';
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);


            sqlQuery =
                $@"
DELETE FROM {MainSchemaName}""{nameof(HouseMeterReading)}"" 
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id};  
INSERT INTO  {MainSchemaName}""{
                    nameof(HouseMeterReading)
                    }""
(""CalculationDate"", ""ServiceName"", ""OuterSystemServiceId"", 
            ""OuterSystemBaseServiceId"", ""MeasureName"", ""ServiceGroupName"", 
            ""IndexNumber"", ""MeterDeviceNumber"", ""OuterSystemMeterDeviceId"", 
            ""CalculationApplyingDate"", ""Indication"", ""CalculationApplyingDatePrevious"", 
            ""IndicationPrevious"", ""Multiplier"", ""Consumption"", ""ConsumptionAdditional"", 
            ""ConsumptionNonresidental"", ""ConsumptionLift"", ""Placement"", ""Capacity"", 
            ""TypeName"", ""VerificationDate"", ""VerificationDateNext"", ""StreetFullAddress"", 
            ""Town"", ""Place"", ""Street"", ""HouseFullAddress"", ""HouseNumber"", 
            ""HousingNumber"", ""SquareHouse"", ""SquareHouseMop"", ""SquareHouseHeating"", 
            ""DataBankPrefix"", ""CreationTime"", ""ChangedTime"", ""AddressSpaceId"", 
            ""BaseServiceId"", ""HouseId"", ""OrganizationId"", ""ServiceId"")
SELECT ""CalculationDate"", ""ServiceName"", ""OuterSystemServiceId"", 
            ""OuterSystemBaseServiceId"", ""MeasureName"", ""ServiceGroupName"", 
            ""IndexNumber"", ""MeterDeviceNumber"", ""OuterSystemMeterDeviceId"", 
            ""CalculationApplyingDate"", ""Indication"", ""CalculationApplyingDatePrevious"", 
            ""IndicationPrevious"", ""Multiplier"", ""Consumption"", ""ConsumptionAdditional"", 
            ""ConsumptionNonresidental"", ""ConsumptionLift"", ""Placement"", ""Capacity"", 
            ""TypeName"", ""VerificationDate"", ""VerificationDateNext"", ""StreetFullAddress"", 
            ""Town"", ""Place"", ""Street"", ""HouseFullAddress"", ""HouseNumber"", 
            ""HousingNumber"", ""SquareHouse"", ""SquareHouseMop"", ""SquareHouseHeating"", 
            ""DataBankPrefix"", ""CreationTime"", ""ChangedTime"", ""AddressSpaceId"", 
            ""BaseServiceId"", ""HouseId"", ""OrganizationId"", ""ServiceId""
FROM {
                    StorageSchemaName}""{nameof(HouseMeterReading)}"" s
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id}
AND ""CalculationDate"" = '{
                    importHelperDto.CalculationDate.ToShortDateString()}';
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);


            sqlQuery =
                $@"
DELETE FROM {MainSchemaName}""{nameof(PersonalAccountPayment)}"" 
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id};  
INSERT INTO  {MainSchemaName}""{nameof(PersonalAccountPayment)
                    }""
(""CalculationDate"", ""PersonalAccountNumber"", ""PaymentCode"", 
       ""ApartmentFullAddress"", ""PaymentDate"", ""CalculationApplyingDate"", 
       ""PaydCalculationMonth"", ""SumPayment"", ""PaymentPlacement"", ""CreationTime"", 
       ""ChangedTime"", ""OrganizationId"",""HouseId"", ""PersonalAccountId"")
SELECT ""CalculationDate"", ""PersonalAccountNumber"", ""PaymentCode"", 
       ""ApartmentFullAddress"", ""PaymentDate"", ""CalculationApplyingDate"", 
       ""PaydCalculationMonth"", ""SumPayment"", ""PaymentPlacement"", ""CreationTime"", 
       ""ChangedTime"", ""OrganizationId"",""HouseId"", ""PersonalAccountId""
FROM {
                    StorageSchemaName}""{nameof(PersonalAccountPayment)}"" s
WHERE ""OrganizationId"" = {
                    importHelperDto.Organization.Id
                    }
AND ""CalculationDate"" = '{importHelperDto.CalculationDate.ToShortDateString()}';
            ";
            sqlQueryPerformer.PerformSql(sqlQuery);
        }

        /// <summary>
        /// Регистрация информации о загруженных данных
        /// </summary>
        /// <param name="sqlQueryPerformer"></param>
        /// <param name="importHelperDto"></param>
        private void RegisterData(SqlQueryPerformer sqlQueryPerformer, ImportHelperDto importHelperDto)
        {
            var sqlQuery =
                $@"
                SELECT COUNT(*) 
                FROM {MainSchemaName}""{nameof(CalculationMonth)
                    }""
                WHERE ""OrganizationId"" = {importHelperDto.Organization.Id
                    }
                AND ""CalculationDate"" = '{importHelperDto.CalculationDate}'";

            //означает, что файл просто перегружают
            if (sqlQueryPerformer.PerformScalar<int>(sqlQuery) != 0) return;

            sqlQuery =
                $@"
                SELECT COALESCE(CAST(MAX(""CalculationDate"") AS DATE), '1999-1-1') 
                FROM {
                    MainSchemaName}""{nameof(CalculationMonth)
                    }""
                WHERE ""IsCurrent"" IS TRUE
                AND ""OrganizationId"" = {
                    importHelperDto.Organization.Id}";

            //если загружается месяц, который раньше последнего загруженного
            if (sqlQueryPerformer.PerformScalar<DateTime>(sqlQuery) > importHelperDto.CalculationDate)
            {
                sqlQuery =
                    $@"
INSERT INTO {MainSchemaName}""{nameof(CalculationMonth)
                        }""
(""CalculationDate"",""OrganizationId"", ""IsCurrent"")
VALUES('{
                        importHelperDto.CalculationDate}',{importHelperDto.Organization.Id}, FALSE);";
                sqlQueryPerformer.PerformSql(sqlQuery);
            }
            else
            {
                sqlQuery =
                    $@"
UPDATE {MainSchemaName}""{nameof(CalculationMonth)
                        }"" SET ""IsCurrent"" = FALSE
WHERE ""OrganizationId"" = {importHelperDto.Organization.Id
                        };

INSERT INTO {MainSchemaName}""{nameof(CalculationMonth)
                        }""
(""CalculationDate"",""OrganizationId"", ""IsCurrent"")
VALUES('{
                        importHelperDto.CalculationDate}',{importHelperDto.Organization.Id}, TRUE);";

                sqlQueryPerformer.PerformSql(sqlQuery);
            }
        }
        
        /// <summary>
        /// Потоковая загрузка
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="sqlQueryPerformer"></param>
        /// <param name="baseTable"></param>
        /// <param name="childTable"></param>
        private void StreamingLoad(Stream stream, SqlQueryPerformer sqlQueryPerformer, string baseTable,
            string childTable)
        {
            var command =
                $@" 
DROP TABLE IF EXISTS {childTable};
CREATE TABLE {childTable} (LIKE {baseTable
                    } INCLUDING ALL) 
INHERITS({baseTable}) 
WITH (OIDS=FALSE)";
            sqlQueryPerformer.PerformSql(command);

            sqlQueryPerformer.StreamingLoad(
                $" COPY {childTable} FROM stdin WITH DELIMITER AS '|' NULL AS '' ENCODING 'WIN-1251'", stream);
        }

        private class ImportHelperDto
        {
            public Organization Organization { get; set; }

            public DateTime CalculationDate { get; set; }
        }
    }
}