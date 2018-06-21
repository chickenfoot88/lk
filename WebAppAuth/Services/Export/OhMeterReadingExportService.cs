using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using OhDataManager.Entities.System;
using OhDataManager.Enums;
using WebAppAuth.Context;
using WebAppAuth.Services.FileManager;

namespace WebAppAuth.Services.Export
{
    /// <summary>
    /// Сервис выгрузки показаний ИПУ
    /// </summary>
    public sealed class OhMeterReadingExportService : BaseFileExport
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fileStorageManager"></param>
        public OhMeterReadingExportService(IFileStorageManager fileStorageManager) : base(fileStorageManager)
        {
            UnloadType = EExportType.MeterReading;
        }

        /// <summary>
        /// Экспорт данных
        /// </summary>
        /// <returns></returns>
        public override async Task<ExportResult> ExportData(int userIdentityId, long exportedFileId)
        {
            using (var context = new ApplicationDbContext())
            {
                var employee = await context.Employee.FirstOrDefaultAsync(x => x.ApplicationUserId == userIdentityId);
                if (employee == null)
                    throw new Exception("Сотрудник не привязан к организации");

                return UnloadMeterReadings(employee.OrganizationId, exportedFileId);
            }
        }

        /// <summary>
        /// Выгрузка показаний ПУ
        /// </summary>
        /// <param name="organizationId">Код организации</param>
        /// <param name="exportedFileId"></param>
        /// <returns></returns>
        public ExportResult UnloadMeterReadings(long organizationId, long exportedFileId)
        {
            // Здесь будет результат работы, который потом запишется в лог-файл
            var listCounters = new List<string>();
            // Здесь будет протокол выгрузки
            var unloadLog = new List<string>();

            var fileName =
                $"ПОКАЗАНИЯ_ИПУ_{organizationId}_{exportedFileId}_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.txt";
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var personalAccountMeterReadings = context.PersonalAccountMeterReading
                        .Where(x => 
                        //x.OrganizationId == organizationId && 
                        x.EnteredValue != null && !x.ExportedFileId.HasValue);

                    //нет новых введенных показаний ПУ
                    if (!personalAccountMeterReadings.Any())
                    {
                        unloadLog.Add("Нет новых показаний ПУ");
                        unloadLog.Add("Время окончания выгрузки: " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"));

                        return new ExportResult
                        {
                            Data = new MemoryStream(),
                            Log =
                                new MemoryStream(
                                    System.Text.Encoding.GetEncoding(1251)
                                        .GetBytes(string.Join(Environment.NewLine, unloadLog))),
                            FileName = fileName,
                            Status = EUnloadStatus.NoData,
                            Success = true
                        };
                    }

                    listCounters.Clear();

                    //формируем информационное описание
                    listCounters.Add(
                        $"***|Portal|0|{DateTime.Now.ToShortDateString()}|{DateTime.Now.ToShortDateString()}|{personalAccountMeterReadings.Count()}|");

                    var pack = personalAccountMeterReadings
                        .GroupBy(x => x.PaymentCode)
                        .ToDictionary(x => x.Key,
                            x => x.Select(y => new
                            {
                                y.IndexNumber,
                                y.EnteredValue,
                                y.ServiceName,
                                EnteredDate = y.EnteredDate.GetValueOrDefault().ToShortDateString(),
                                y.MeterDeviceNumber,
                                y.OuterSystemMeterDeviceId,
                                CalculationDate = y.CalculationDate.ToShortDateString()
                            }));

                    foreach (var onePack in pack)
                    {
                        listCounters.Add(
                            $"###|{onePack.Key}|{onePack.Value.First().CalculationDate}|{onePack.Value.Count()}||");

                        onePack.Value
                            .ForEach(x =>
                                listCounters.Add(
                                    $"@@@|{x.IndexNumber}|{x.EnteredValue}|{x.ServiceName}|{x.EnteredDate}|{x.MeterDeviceNumber}|{x.OuterSystemMeterDeviceId}")
                            );
                    }
                    //выгрузка ОДПУ пока не реализована
                    //var houseMeterReadings = context.HouseMeterReading.Where(
                    //    x => x.OrganizationId == organizationId && x.EnteredValue != null);

                    //houseMeterReadings.ForEach(x =>
                    //{
                    //    listCounters.Add(
                    //        $"~~~|{x.EnteredValue}|{x.ServiceName}|{x.EnteredDate.GetValueOrDefault().ToShortDateString()}|{x.MeterDeviceNumber}|{x.OuterSystemMeterDeviceId}");
                    //    x.ExportedFileId = exportedFileId;
                    //});

                    personalAccountMeterReadings.ForEach(x => x.ExportedFileId = exportedFileId);

                    context.SaveChangesAsync();

                    unloadLog.Add("Показания ПУ успешно выгружены!");
                }
            }
            catch (Exception ex)
            {
                unloadLog.Add("Ошибка при считывании из БД показаний ПУ: " + ex);
                unloadLog.Add("Время окончания выгрузки: " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"));
                return new ExportResult
                {
                    Data = new MemoryStream(),
                    Log =
                        new MemoryStream(
                            System.Text.Encoding.GetEncoding(1251)
                                .GetBytes(string.Join(Environment.NewLine, unloadLog))),
                    FileName = fileName,
                    Status = EUnloadStatus.UnloadError,
                    Success = false
                };
            }

            return new ExportResult
            {
                Data =
                    new MemoryStream(
                        System.Text.Encoding.GetEncoding(1251).GetBytes(string.Join(Environment.NewLine, listCounters))),
                Log =
                    new MemoryStream(
                        System.Text.Encoding.GetEncoding(1251).GetBytes(string.Join(Environment.NewLine, unloadLog))),
                FileName = fileName,
                Status = EUnloadStatus.Success,
                Success = true
            };
        }
    }
}
