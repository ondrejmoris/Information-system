using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DataLayer
{
    public class Database
    {
        //private static string connectionString = "server=dbsys.cs.vsb.cz\"STUDENT;database=mor0185;user id=mor0185;password=M8pzUgudaR;";
        private SqlConnection Connection { get; set; }
        private SqlTransaction SqlTransaction { get; set; }
        public string Language { get; set; }

        public Database()
        {
            Connection = new SqlConnection();
            Language = "cs";
        }

        /// <summary>
        /// Connect
        /// </summary>
        public bool Connect(string conString)
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.ConnectionString = conString;
                Connection.Open();
            }
            return true;

        }

        /// <summary>
        /// Connect
        /// </summary>
        public bool Connect()
        {
            bool ret = true;
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                // connection string
                ret = Connect(ConfigurationManager.ConnectionStrings["ConnectionStringMsSql"].ConnectionString);
            }
            return ret;
        }

        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            Connection.Close();
        }

        /// <summary>
        /// Begin a transaction.
        /// </summary>
        public void BeginTransaction()
        {
            SqlTransaction = Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        /// <summary>
        /// End a transaction.
        /// </summary>
        public void EndTransaction()
        {
            SqlTransaction.Commit();
            Close();
        }

        /// <summary>
        /// If a transaction is failed call it.
        /// </summary>
        public void Rollback()
        {
            SqlTransaction.Rollback();
        }

        /// <summary>
        /// Insert a record encapulated in the command.
        /// </summary>
        public int ExecuteNonQuery(SqlCommand command)
        {
            int rowNumber = 0;
            try
            {
                rowNumber = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            return rowNumber;
        }

        /// <summary>
        /// Create command
        /// </summary>
        public SqlCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, Connection);

            if (SqlTransaction != null)
            {
                command.Transaction = SqlTransaction;
            }
            return command;
        }

        /// <summary>
        /// Select encapulated in the command.
        /// </summary>
        public SqlDataReader Select(SqlCommand command)
        {
            SqlDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }
    }
}
