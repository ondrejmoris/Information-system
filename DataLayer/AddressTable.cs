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
    public class AddressTable
    {
        public static String SQL_SELECT_ALL = "SELECT * FROM \"Address\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Address\" WHERE address_id = @id";
        public static String SQL_INSERT = "INSERT INTO \"Address\" (city, street, number, postal_number, customer_customer_id, employee_employee_id) VALUES (@city, @street, @number, @postalNumber, @customerId, @employeeId)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Address\" WHERE address_id = @id";
        public static String SQL_UPDATE = "UPDATE \"Address\" SET city = @city, street = @street, number = @number, postal_number = @postalNumber, customer_customer_id = @customerId, employee_employee_id = @employeeId  WHERE address_id = @id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Address address)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, address);

            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Address address)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, address);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Address> SelectAll()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            Collection<Address> addresses = Read(reader);
            reader.Close();

            db.Close();

            return addresses;
        }

        /// <summary>
        /// Select the record.
        /// </summary>

        public static Address Select(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Address> addresses = Read(reader);
            Address address = null;
            if (addresses.Count == 1)
            {
                address = addresses[0];
            }
            reader.Close();

            db.Close();

            return address;
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
        private static void PrepareCommand(SqlCommand command, Address address)
        {
            command.Parameters.AddWithValue("@id", address.Address_id);
            command.Parameters.AddWithValue("@city", address.City);
            command.Parameters.AddWithValue("@street", address.Street);
            command.Parameters.AddWithValue("@number", address.Number);
            command.Parameters.AddWithValue("@postalNumber", address.Postal_number == null ? DBNull.Value : (object)address.Postal_number);
            command.Parameters.AddWithValue("@customerId", address.Customer_customer_id == null ? DBNull.Value : (object)address.Customer_customer_id);
            command.Parameters.AddWithValue("@employeeId", address.Employee_employee_id == null ? DBNull.Value : (object)address.Employee_employee_id);           
        }

        private static Collection<Address> Read(SqlDataReader reader)
        {
            Collection<Address> addresses = new Collection<Address>();

            while (reader.Read())
            {
                int i = -1;
                Address address = new Address();
                address.Address_id = reader.GetInt32(++i);
                address.City = reader.GetString(++i);
                address.Street = reader.GetString(++i);
                address.Number = reader.GetInt32(++i);
                if (!reader.IsDBNull(++i))
                {
                    address.Postal_number = reader.GetInt32(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    address.Customer_customer_id = reader.GetInt32(i);
                }
                if (!reader.IsDBNull(++i))
                {
                    address.Employee_employee_id = reader.GetInt32(i);
                }

                addresses.Add(address);
            }
            return addresses;
        }

        // It is not a function from the functional analysis
        // it is an example how to work with stored procedures
        /*
        public static string CheckUser(int idUser, Database pDb)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("CheckUser");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@idUser";
            input.DbType = DbType.Int32;
            input.Value = idUser;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            // 3. create output parameters
            SqlParameter output = new SqlParameter();
            output.ParameterName = "@result";
            output.DbType = DbType.String;
            output.Direction = ParameterDirection.Output;
            command.Parameters.Add(output);

            // 4. execute procedure
            int ret = db.ExecuteNonQuery(command);

            // 5. get values of the output parameters
            string result = command.Parameters["@result"].Value.ToString();

            if (pDb == null)
            {
                db.Close();
            }
            return result;
        } */
    }
}
