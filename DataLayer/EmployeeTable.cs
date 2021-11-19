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
    public class EmployeeTable
    {
        public static String SQL_SELECT_ALL = "SELECT * FROM \"Employee\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Employee\" WHERE employee_id = @id";
        public static String SQL_INSERT = "INSERT INTO \"Employee\" (first_name, last_name, phone_number, rank) VALUES (@firstName, @lastName, @phoneNumber, @rank)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Employee\" WHERE employee_id = @id";
        public static String SQL_UPDATE = "UPDATE \"Employee\" SET first_name = @firstName, last_name = @lastName, phone_number = @phoneNumber, rank = @rank WHERE employee_id = @id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Employee employee)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, employee);

            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Employee employee)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, employee);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Employee> SelectAll()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            Collection<Employee> employees = Read(reader);
            reader.Close();

            db.Close();

            return employees;
        }

        /// <summary>
        /// Select the record.
        /// </summary>

        public static Employee Select(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Employee> employees = Read(reader);
            Employee employee = null;
            if (employees.Count == 1)
            {
                employee = employees[0];
            }
            reader.Close();

            db.Close();

            return employee;
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
        private static void PrepareCommand(SqlCommand command, Employee employee)
        {
            command.Parameters.AddWithValue("@id", employee.Employee_id);
            command.Parameters.AddWithValue("@firstName", employee.First_name);
            command.Parameters.AddWithValue("@lastName", employee.Last_name);
            command.Parameters.AddWithValue("@phoneNumber", employee.Phone_number);
            command.Parameters.AddWithValue("@rank", employee.Rank);
        }

        private static Collection<Employee> Read(SqlDataReader reader)
        {
            Collection<Employee> employees = new Collection<Employee>();

            while (reader.Read())
            {
                int i = -1;
                Employee employee = new Employee();
                employee.Employee_id = reader.GetInt32(++i);
                employee.First_name = reader.GetString(++i);
                employee.Last_name = reader.GetString(++i);
                employee.Phone_number = reader.GetInt32(++i);
                employee.Rank = reader.GetString(++i);

                employees.Add(employee);
            }
            return employees;
        }
    }
}
