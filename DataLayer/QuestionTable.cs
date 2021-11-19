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
    public class QuestionTable
    {
        public static String SQL_SELECT_ALL = "SELECT * FROM \"Question\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Question\" WHERE question_id = @id";
        public static String SQL_INSERT = "INSERT INTO \"Question\" (date, description, type, customer_customer_id, employee_employee_id) VALUES (@date, @desc, @type, @customerCustomerId, @employeeEmployeeId)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Question\" WHERE question_id = @id";
        public static String SQL_UPDATE = "UPDATE \"Question\" SET date = @date, description = @desc, type = @type, customer_customer_id = @customerCustomerId, employee_employee_id = @employeEmployeeId WHERE question_id = @id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Question question)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, question);

            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Question question)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, question);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Question> SelectAll()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            Collection<Question> questions = Read(reader);
            reader.Close();

            db.Close();

            return questions;
        }

        /// <summary>
        /// Select the record.
        /// </summary>

        public static Question Select(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Question> questions = Read(reader);
            Question question = null;
            if (questions.Count == 1)
            {

                question = questions[0];
            }
            reader.Close();

            db.Close();

            return question;
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
        private static void PrepareCommand(SqlCommand command, Question question)
        {
            command.Parameters.AddWithValue("@id", question.Question_id);
            command.Parameters.AddWithValue("@date", question.Date);
            command.Parameters.AddWithValue("@desc", question.Description);
            command.Parameters.AddWithValue("@type", question.Type);
            command.Parameters.AddWithValue("@customerCustomerId", question.Customer_customer_id);
            command.Parameters.AddWithValue("@employeeEmployeeId", question.Employee_employee_id);
        }

        private static Collection<Question> Read(SqlDataReader reader)
        {
            Collection<Question> questions = new Collection<Question>();

            while (reader.Read())
            {
                int i = -1;
                Question question = new Question();
                question.Question_id = reader.GetInt32(++i);
                question.Date = reader.GetDateTime(++i);
                question.Description = reader.GetString(++i);
                question.Type = reader.GetString(++i);
                question.Customer_customer_id = reader.GetInt32(++i);
                question.Employee_employee_id = reader.GetInt32(++i);

                questions.Add(question);
            }
            return questions;
        }
    }
}
