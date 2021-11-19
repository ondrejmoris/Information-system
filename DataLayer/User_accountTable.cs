using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DataLayer
{
    public class User_accountTable
    {
        public static String SQL_SELECT_ALL = "SELECT * FROM \"user_account\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"user_account\" WHERE user_id = @id";
        public static String SQL_INSERT = "INSERT INTO \"user_account\" (user_name, password, customer_customer_id) VALUES (@userName, @passwd, @customerCustomerId)";
        public static String SQL_DELETE_ID = "DELETE FROM \"user_account\" WHERE user_id = @id";
        public static String SQL_UPDATE = "UPDATE \"user_account\" SET user_name = @userName, password = @passwd, customer_customer_id = @customerCustomerId WHERE user_id = @id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(User_account user_Account)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, user_Account);

            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(User_account user_Account)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, user_Account);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<User_account> SelectAll()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            Collection<User_account> user_Accounts = Read(reader);
            reader.Close();

            db.Close();

            return user_Accounts;
        }

        /// <summary>
        /// Select the record.
        /// </summary>

        public static User_account Select(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<User_account> user_Accounts = Read(reader);
            User_account user_Account = null;
            if (user_Accounts.Count == 1)
            {

                user_Account = user_Accounts[0];
            }
            reader.Close();

            db.Close();

            return user_Account;
        }

        /// <summary>
        /// Delete the record.
        /// </summary>

        public static int Delete(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@id", id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        ///  Prepare a command.
        /// </summary>
        private static void PrepareCommand(SqlCommand command, User_account user_Account)
        {
            command.Parameters.AddWithValue("@id", user_Account.User_id);
            command.Parameters.AddWithValue("@userName", user_Account.User_name);
            command.Parameters.AddWithValue("@passwd", user_Account.Password);
            command.Parameters.AddWithValue("@customerCustomerId", user_Account.Customer_customer_id);
        }

        private static Collection<User_account> Read(SqlDataReader reader)
        {
            Collection<User_account> user_Accounts = new Collection<User_account>();

            while (reader.Read())
            {
                int i = -1;
                User_account user_Account = new User_account();
                user_Account.User_id = reader.GetInt32(++i);
                user_Account.User_name = reader.GetString(++i);
                user_Account.Password = reader.GetString(++i);
                user_Account.Customer_customer_id = reader.GetInt32(++i);

                user_Accounts.Add(user_Account);
            }
            return user_Accounts;
        }
    }
}
