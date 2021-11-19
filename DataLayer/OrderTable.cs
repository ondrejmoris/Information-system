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
    public class OrderTable
    {
        public static String SQL_SELECT_ALL = "SELECT * FROM \"Order\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Order\" WHERE order_id = @id";
        public static String SQL_INSERT = "INSERT INTO \"Order\" (state, start_date, end_date, description, computer_type, customer_customer_id, employee_employee_id, employee_technician_id) VALUES (@state, @startDate, @endDate, @desc, @computerType, @customerCustomerId, @employeeEmployeeId, @employeeTechnicianId)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Order\" WHERE order_id = @id";
        public static String SQL_UPDATE = "UPDATE \"Order\" SET state = @state, start_date = @startDate, end_date = @endDate, description = @desc, computer_type = @computerType, customer_customer_id = @customerCustomerId, employee_employee_id = @employeeEmployeeId, employee_technician_id = @employeeTechnicianId WHERE order_id = @id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Order order)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, order);

            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Order order)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, order);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Order> SelectAll()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            Collection<Order> orders = Read(reader);
            reader.Close();

            db.Close();

            return orders;
        }

        /// <summary>
        /// Select the record.
        /// </summary>

        public static Order Select(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Order> orders = Read(reader);
            Order order = null;
            if (orders.Count == 1)
            {
                order = orders[0];
            }
            reader.Close();

            db.Close();

            return order;
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
        private static void PrepareCommand(SqlCommand command, Order order)
        {
            command.Parameters.AddWithValue("@id", order.Order_id);
            command.Parameters.AddWithValue("@state", order.State);
            command.Parameters.AddWithValue("@startDate", order.Start_date);
            command.Parameters.AddWithValue("@endDate", order.End_date == null ? DBNull.Value : (object)order.End_date);
            command.Parameters.AddWithValue("@desc", order.Description);
            command.Parameters.AddWithValue("@computerType", order.Computer_type);
            command.Parameters.AddWithValue("@customerCustomerId", order.Customer_customer_id);
            command.Parameters.AddWithValue("@employeeEmployeeId", order.Employee_employee_id);
            command.Parameters.AddWithValue("@employeeTechnicianId", order.Employee_technician_id);
        }

        private static Collection<Order> Read(SqlDataReader reader)
        {
            Collection<Order> orders = new Collection<Order>();

            while (reader.Read())
            {
                int i = -1;
                Order order = new Order();
                order.Order_id = reader.GetInt32(++i);
                order.State = reader.GetString(++i);
                order.Start_date = reader.GetDateTime(++i);
                if (!reader.IsDBNull(++i))
                {
                    order.End_date = reader.GetDateTime(i);
                }
                order.Description = reader.GetString(++i);
                order.Computer_type = reader.GetString(++i);
                order.Customer_customer_id = reader.GetInt32(++i);
                order.Employee_employee_id = reader.GetInt32(++i);
                order.Employee_technician_id = reader.GetInt32(++i);

                orders.Add(order);
            }
            return orders;
        }
    }
}
