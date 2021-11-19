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
    public class ComplaintTable
    {
        public static String SQL_SELECT_ALL = "SELECT * FROM \"Complaint\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Complaint\" WHERE complaint_id = @id";
        public static String SQL_INSERT = "INSERT INTO \"Complaint\" (start_date, end_date, reason, order_order_id, employee_employee_id, result) VALUES (@startDate, @endDate, @reason, @orderOrderId, @employeeEmployeeId, @result)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Complaint\" WHERE complaint_id = @id";
        public static String SQL_UPDATE = "UPDATE \"Complaint\" SET start_date = @startDate, end_date = @endDate, reason = @reason, order_order_id = @orderOrderId, employee_employee_id = @employeEmployeeId, result = @result WHERE complaint_id = @id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Complaint complaint)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, complaint);

            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Complaint complaint)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, complaint);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Complaint> SelectAll()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            Collection<Complaint> complaints = Read(reader);
            reader.Close();

            db.Close();

            return complaints;
        }

        /// <summary>
        /// Select the record.
        /// </summary>

        public static Complaint Select(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Complaint> complaints = Read(reader);
            Complaint complaint = null;
            if (complaints.Count == 1)
            {
                
                complaint = complaints[0];
            }
            reader.Close();

            db.Close();

            return complaint;
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
        private static void PrepareCommand(SqlCommand command, Complaint complaint)
        {
            command.Parameters.AddWithValue("@id", complaint.Complaint_id);
            command.Parameters.AddWithValue("@startDate", complaint.Start_date);
            command.Parameters.AddWithValue("@endDate", complaint.End_date == null ? DBNull.Value : (object)complaint.End_date);
            command.Parameters.AddWithValue("@reason", complaint.Reason);
            command.Parameters.AddWithValue("@orderOrderId", complaint.Order_order_id);
            command.Parameters.AddWithValue("@employeeEmployeeId", complaint.Employee_employee_id);
            command.Parameters.AddWithValue("@result", complaint.Result == null ? DBNull.Value : (object)complaint.Result);
        }

        private static Collection<Complaint> Read(SqlDataReader reader)
        {
            Collection<Complaint> complaints = new Collection<Complaint>();

            while (reader.Read())
            {
                int i = -1;
                Complaint complaint = new Complaint();
                complaint.Complaint_id = reader.GetInt32(++i);
                complaint.Start_date = reader.GetDateTime(++i);
                if (!reader.IsDBNull(++i))
                {
                    complaint.End_date = reader.GetDateTime(i);
                }
                complaint.Reason = reader.GetString(++i);
                complaint.Order_order_id = reader.GetInt32(++i);
                complaint.Employee_employee_id = reader.GetInt32(++i);
                if (!reader.IsDBNull(++i))
                {
                    complaint.Result = reader.GetString(i);
                }

                complaints.Add(complaint);
            }
            return complaints;
        }
    }
}
