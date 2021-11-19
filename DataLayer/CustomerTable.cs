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
    public class CustomerTable
    {
        public static String SQL_SELECT_ALL = "SELECT * FROM \"Customer\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Customer\" WHERE customer_id = @id";
        public static String SQL_INSERT = "INSERT INTO \"Customer\" (first_name, last_name, phone_number) VALUES (@firstName, @lastName, @phoneNumber)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Customer\" WHERE customer_id = @id";
        public static String SQL_UPDATE = "UPDATE \"Customer\" SET first_name = @firstName, last_name = @lastName, phone_number = @phoneNumber WHERE customer_id = @id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Customer customer)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, customer);

            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Customer customer)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, customer);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Customer> SelectAll()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            Collection<Customer> customers = Read(reader);
            reader.Close();

            db.Close();

            return customers;
        }

        /// <summary>
        /// Select the record.
        /// </summary>

        public static Customer Select(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Customer> customers = Read(reader);
            Customer customer = null;
            if (customers.Count == 1)
            {

                customer = customers[0];
            }
            reader.Close();

            db.Close();

            return customer;
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
        private static void PrepareCommand(SqlCommand command, Customer customer)
        {
            command.Parameters.AddWithValue("@id", customer.Customer_id);
            command.Parameters.AddWithValue("@firstName", customer.First_name);
            command.Parameters.AddWithValue("@lastName", customer.Last_name);
            command.Parameters.AddWithValue("@phoneNumber", customer.Phone_number);
        }

        private static Collection<Customer> Read(SqlDataReader reader)
        {
            Collection<Customer> customers = new Collection<Customer>();

            while (reader.Read())
            {
                int i = -1;
                Customer customer = new Customer();
                customer.First_name = reader.GetString(++i);
                customer.Customer_id = reader.GetInt32(++i);
                customer.Last_name = reader.GetString(++i);
                customer.Phone_number = reader.GetInt32(++i);

                customers.Add(customer);
            }
            return customers;
        }
    }
}
