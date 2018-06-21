using System.Threading.Tasks;

namespace OhUtils
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using Dapper;
    using Npgsql;

    /// <summary>
    /// Класс для выполнения sql-запросов
    /// </summary>
    public class SqlQueryPerformer : IDisposable
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public SqlQueryPerformer()
        {
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("В конфигурационном файле не найдено значение для параметра 'ConnectionString'!");

            DbConnection = new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// Соединение с БД
        /// </summary>
        private IDbConnection DbConnection { get; set; }

        /// <summary>
        /// Создание новой команды
        /// </summary>
        /// <param name="commandText">Текст команды</param>
        /// <param name="connection">Соединение</param>
        /// <param name="transaction">Транзакция</param>
        /// <param name="timeout">Таймаут времени выполнения</param>
        private IDbCommand NewDbCommand(string commandText, IDbConnection connection, IDbTransaction transaction,
            int timeout)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            command.Connection = connection;
            command.Transaction = transaction;
            command.CommandTimeout = timeout;
            return command;
        }

        /// <summary>
        /// Открытие транзакции
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            OpenConnection();
            return DbConnection.BeginTransaction();
        }

        /// <summary>
        /// Открытие соединения с БД
        /// </summary>
        /// <returns></returns>
        private void OpenConnection()
        {
            try
            {
                switch (DbConnection.State)
                {
                    case ConnectionState.Closed:
                      
                        DbConnection.Open();
                        break;

                    case ConnectionState.Broken:
                        DbConnection.Close();
                        DbConnection.Open();
                        break;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Ошибка при открытии соединения с БД! ", exception);
            }
        }

        /// <summary>
        /// Закрытие соединения с БД
        /// </summary>
        /// <returns></returns>
        private void CloseConnection()
        {
            try
            {
                if (DbConnection != null && DbConnection.State == ConnectionState.Open)
                {
                    DbConnection.Close();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Ошибка при закрытии соединения с БД!", exception);
            }
        }

        /// <summary>
        /// Выполнение sql-запроса без транзакции
        /// </summary>
        /// <param name="sqlQuery">Текст sql-запроса</param>
        /// <returns></returns>
        public void PerformSql(string sqlQuery)
        {
            PerformSql(sqlQuery, DbConnection, null, 3000);
        }

        /// <summary>
        /// Выполнение sql-запроса c транзакцией
        /// </summary>
        /// <param name="sqlQuery">Текст sql-запроса</param>
        /// <param name="transaction">Транзакция</param>
        /// <returns></returns>
        public void PerformSql(string sqlQuery, IDbTransaction transaction)
        {
            PerformSql(sqlQuery, DbConnection, transaction, 3000);
        }

        /// <summary>
        /// Выполнение sql-запроса
        /// </summary>
        /// <param name="sqlQuery">Текст sql-запроса</param>
        /// <param name="connection">Соединение</param>
        /// <param name="transaction">Транзакция</param>
        /// <param name="dbCommandTimeout">Таймаут выполнения</param>
        /// <returns></returns>
        private void PerformSql(string sqlQuery, IDbConnection connection, IDbTransaction transaction, int dbCommandTimeout)
        {
            try
            {            
                OpenConnection();

                using (var myCommand = NewDbCommand(sqlQuery, connection, transaction, dbCommandTimeout))
                {
                    myCommand.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                transaction?.Rollback();
                throw new Exception(
                    $" Sql-команда: {sqlQuery}\n Содержимое ошибки: {exception.Message} ", exception);
            }
        }

        /// <summary>
        /// Выполнение запроса с получением объекта
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="transaction">Транзакция</param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteSqlToEnumerable<T>(string sqlQuery, IDbTransaction transaction = null)
        {
            try
            {
                OpenConnection();
                return DbConnection.Query<T>(sqlQuery, transaction: transaction);
            }
            catch (Exception exception)
            {
                throw new Exception(
                    $" Sql-команда: {sqlQuery}\n Содержимое ошибки: {exception.Message} ", exception);
            }
        }


        /// <summary>
        /// Выполнение запроса с получением объекта
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="transaction">Транзакция</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteSqlToEnumerableAsync<T>(string sqlQuery, IDbTransaction transaction = null)
        {
            try
            {
                OpenConnection();
                return await DbConnection.QueryAsync<T>(sqlQuery, transaction: transaction);
            }
            catch (Exception exception)
            {
                throw new Exception(
                    $" Sql-команда: {sqlQuery}\n Содержимое ошибки: {exception.Message} ", exception);
            }
        }

        /// <summary>
        /// Выполнение запроса с получением результата
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="transaction">Транзакция</param>
        /// <returns></returns>
        public T PerformScalar<T>(string sqlQuery, IDbTransaction transaction = null)
        {
            try
            {
                OpenConnection();
                return DbConnection.ExecuteScalar<T>(sqlQuery, transaction: transaction);
            }
            catch (Exception exception)
            {
                throw new Exception(
                    $" Sql-команда: {sqlQuery}\n Содержимое ошибки: {exception.Message} ", exception);
            }
        }
        
        /// <summary>
        /// Потоковая загрузка данных
        /// </summary>
        public void StreamingLoad(string sqlQuery, Stream stream)
        {
            OpenConnection();

            var command = new NpgsqlCopyIn(new NpgsqlCommand(sqlQuery, (NpgsqlConnection)DbConnection), (NpgsqlConnection)DbConnection, stream);
            command.Start();
            command.End();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            CloseConnection();
        }
    }
}
