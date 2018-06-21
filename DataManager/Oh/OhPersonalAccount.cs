using System.Threading.Tasks;
using OhDataManager.Entities.Main;

namespace OhDataManager.Oh
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AbstractDm;
    using Entities;
    using OhUtils;

    /// <summary>
    /// Реализация менеджера данных
    /// </summary>
    [DataManager(DataManagerType = "Oh", AbstractDataManagerName = "DmPersonalAccount")]

    public class OhPersonalAccount : DmPersonalAccount
    {
        /// <summary>
        /// Получение списка лицевых счетов по адресу
        /// </summary>
        /// <param name="personalAccountId"></param>
        public override PersonalAccount GetPersonalAccountInfo(long personalAccountId)
        {
            using (var sqlExecutor = new SqlQueryPerformer())
            {
                var sqlQuery =
                    $@"SELECT *
                        FROM {Extensions.Extensions.GetTableName<PersonalAccount>()} 
                        WHERE ""Id"" = {personalAccountId}";

                return sqlExecutor.ExecuteSqlToEnumerable<PersonalAccount>(sqlQuery).FirstOrDefault();
            }
        }
        
        /// <summary>
        /// Получение начислений лицевого счета
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="calculationDate"></param>
        public async override  Task<IEnumerable<PersonalAccountAccrual>> GetPersonalAccountAccruals(long personalAccountId,
            DateTime calculationDate)
        {
            using (var sqlExecutor = new SqlQueryPerformer())
            {
                var sqlQuery =
                    $@"SELECT *
                        FROM {Extensions.Extensions.GetTableName<PersonalAccountAccrual>()} 
                        WHERE ""PersonalAccountId"" = {personalAccountId}
                        AND ""CalculationDate"" = '{calculationDate.ToShortDateString()}'";

                return await sqlExecutor.ExecuteSqlToEnumerableAsync<PersonalAccountAccrual>(sqlQuery);
            }
        }

        /// <summary>
        /// Получение платежей лицевого счета
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        public async override Task<IEnumerable<PersonalAccountPayment>> GetPersonalAccountPayments(long personalAccountId, DateTime? periodBegin = null, DateTime? periodEnd = null)
        {
            using (var sqlExecutor = new SqlQueryPerformer())
            {
                var sqlQuery =
                    $@"SELECT *
                        FROM {Extensions.Extensions.GetTableName<PersonalAccountPayment>()} 
                        WHERE ""PersonalAccountId"" = {personalAccountId}
{
                        (periodBegin.HasValue
                            ? $@" AND ""CalculationDate"" >= '{periodBegin.Value.ToShortDateString()}'"
                            : "")
}
{
                        (periodEnd.HasValue
                            ? $@" AND ""CalculationDate"" <= '{periodEnd.Value.ToShortDateString()}'"
                            : "")
}
";
                return await sqlExecutor.ExecuteSqlToEnumerableAsync<PersonalAccountPayment>(sqlQuery);
            }
        }

        /// <summary>
        /// Получение социальных платежей лицевого счета
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        public async override Task<IEnumerable<PersonalAccountSocialPayment>> GetPersonalAccountSocialPayments(long personalAccountId, DateTime? periodBegin = null, DateTime? periodEnd = null)
        {
            using (var sqlExecutor = new SqlQueryPerformer())
            {
                var sqlQuery =
                    $@"SELECT *
                        FROM {Extensions.Extensions.GetTableName<PersonalAccountSocialPayment>()} 
                        WHERE ""PersonalAccountId"" = {personalAccountId}
{
                        (periodBegin.HasValue
                            ? $@" AND ""CalculationDate"" >= '{periodBegin.Value.ToShortDateString()}'"
                            : "")
}
{
                        (periodEnd.HasValue
                            ? $@" AND ""CalculationDate"" <= '{periodEnd.Value.ToShortDateString()}'"
                            : "")
}
";
                return await sqlExecutor.ExecuteSqlToEnumerableAsync<PersonalAccountSocialPayment>(sqlQuery);
            }
        }

        /// <summary>
        /// Получение показаний приборов учета лицевого счета
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        public override async Task<IEnumerable<PersonalAccountMeterReading>> GetPersonalAccountMeterReadings(
            long personalAccountId, DateTime? periodBegin = null, DateTime? periodEnd = null)
        {
            using (var sqlExecutor = new SqlQueryPerformer())
            {

                var sqlQuery =
                    $@"SELECT *
                        FROM {
                        Extensions.Extensions.GetTableName<PersonalAccountMeterReading>()
                        } 
                        WHERE ""PersonalAccountId"" = {personalAccountId}
{
                        //маленькая магия: если дата начала и дата окончания равны,
                        //то берем конкретный месяц, иначе - берем период
                        (periodBegin.HasValue && periodEnd.HasValue && periodBegin == periodEnd
                            ? $@" AND ""CalculationDate"" = '{periodBegin.Value.ToShortDateString()}'"
                            : "")

                        }

{
                        (periodBegin.HasValue && periodBegin != periodEnd
                            ? $@" AND ""CalculationDate"" >= '{periodBegin.Value.ToShortDateString()}'"
                            : "")
                        }
{
                        (periodEnd.HasValue && periodBegin != periodEnd
                            ? $@" AND ""CalculationDate"" <= '{periodEnd.Value.ToShortDateString()}'"
                            : "")
                        }

";
                return await sqlExecutor.ExecuteSqlToEnumerableAsync<PersonalAccountMeterReading>(sqlQuery);
            }
        }
        
        /// <summary>
        /// Добавление предварительной оплаты
        /// </summary>
        public override bool SavePayResultPre(UniPay pay)
        {
            using (var sqlExecutor = new SqlQueryPerformer())
            {
                var sqlQuery = "insert into webfon.uniteller_requests (id,date_create, " +
                               "\"Customer_IDP\",\"LastName\",\"FirstName\",\"MiddleName\"," +
                               "\"Inn\",\"Address\",\"Email\",\"Comment\",\"Subtotal_P\"," +
                               "\"Shop_IDP\",\"LifeTime\",\"Signature\") " + $" values('{pay.OrderIdp}',now()," +
                               $"'{pay.CustomerIdp}','{pay.LastName}','{pay.FirstName}','{pay.MiddleName}'," +
                               $"'{pay.Inn}','{pay.Address}','{pay.Email}','{pay.Comment}','{pay.SubtotalP}'," +
                               $"'{pay.ShopIdp}','{pay.LifeTime}','{pay.Signature}')";
                sqlExecutor.PerformSql(sqlQuery);
            }
            return true;
        }

        /// <summary>
        /// Обновить статус предварительной оплаты
        /// </summary>
        public override bool UpdatePayResultPre(string orderId,string[] arr)
        {
            using (var sqlExecutor = new SqlQueryPerformer())
            {

                var sqlQuery = string.Format(
                     "update webfon.uniteller_requests set \"Status\" = '{1}',"+
                     "\"ApprovalCode\" = '{2}', \"BillNumber\" = '{3}' where " +
                     " id = '{0}'",
                    orderId,arr[0], arr[1], arr[2]);
                sqlExecutor.PerformSql(sqlQuery);

            }
            return true;
        }
    }
}
