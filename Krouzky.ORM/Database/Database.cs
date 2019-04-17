namespace Krouzky.ORM.Database
{
    #region UsingRegion

    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    #endregion

    /// <summary>
    ///     Represents a MS SQL Database
    /// </summary>
    public class Database
    {
        public Database()
        {
            this.Connection = new SqlConnection();
        }

        private SqlConnection Connection { get; }
        private SqlTransaction SqlTransaction { get; set; }

        /// <summary>
        ///     Connect
        /// </summary>
        public bool Connect(string conString)
        {
            if ((this.Connection != null) && (this.Connection.State != ConnectionState.Open))
            {
                this.Connection.ConnectionString = conString;
                this.Connection.Open();
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Connect
        /// </summary>
        public bool Connect()
        {
            if ((this.Connection == null) || (this.Connection.State == ConnectionState.Open))
            {
                return false;
            }

            // connection string is stored in file App.config or Web.config
            if ((ConfigurationManager.ConnectionStrings == null) ||
//                (ConfigurationManager.ConnectionStrings["ConnectionStringOracle"] == null))
                (ConfigurationManager.ConnectionStrings["ConnectionStringMsSql"] == null))
            {
                return false;
            }

//            return this.Connect(ConfigurationManager.ConnectionStrings["ConnectionStringOracle"].ConnectionString);
            return this.Connect(ConfigurationManager.ConnectionStrings["ConnectionStringMsSql"].ConnectionString);
        }

        /// <summary>
        ///     Close
        /// </summary>
        public void Close()
        {
            this.Connection?.Close();
        }

        /// <summary>
        ///     Begin a transaction.
        /// </summary>
        public void BeginTransaction()
        {
            this.SqlTransaction = this.Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        /// <summary>
        ///     End a transaction.
        /// </summary>
        public void EndTransaction()
        {
            this.SqlTransaction?.Commit();
            this.Close();
        }

        /// <summary>
        ///     If a transaction is failed call it.
        /// </summary>
        public void Rollback()
        {
            this.SqlTransaction?.Rollback();
        }

        /// <summary>
        ///     Insert a record encapulated in the command.
        /// </summary>
        public int ExecuteNonQuery(SqlCommand command)
        {
            int rowNumber = 0;
            try
            {
                if (command != null)
                {
                    rowNumber = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return rowNumber;
        }

        /// <summary>
        ///     Create command
        /// </summary>
        public SqlCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, this.Connection);

            if (this.SqlTransaction != null)
            {
                command.Transaction = this.SqlTransaction;
            }

            return command;
        }

        /// <summary>
        ///     Select encapulated in the command.
        /// </summary>
        public SqlDataReader Select(SqlCommand command)
        {
            SqlDataReader sqlReader = command?.ExecuteReader();
            return sqlReader;
        }
    }
}