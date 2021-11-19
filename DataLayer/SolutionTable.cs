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
    public class SolutionTable
    {
        public static String SQL_SELECT_ALL = "SELECT * FROM \"Solution\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Solution\" WHERE solution_id = @id";
        public static String SQL_INSERT = "INSERT INTO \"Solution\" (date, description, order_order_id) VALUES (@date, @desc, @orderOrderId)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Solution\" WHERE solution_id = @id";
        public static String SQL_UPDATE = "UPDATE \"Solution\" SET date = @date, description = @desc, order_order_id = @orderOrderId WHERE solution_id = @id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Solution solution)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, solution);

            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Solution solution)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, solution);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Solution> SelectAll()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            Collection<Solution> solutions = Read(reader);
            reader.Close();

            db.Close();

            return solutions;
        }

        /// <summary>
        /// Select the record.
        /// </summary>

        public static Solution Select(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Solution> solutions = Read(reader);
            Solution solution = null;
            if (solutions.Count == 1)
            {

                solution = solutions[0];
            }
            reader.Close();

            db.Close();

            return solution;
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
        private static void PrepareCommand(SqlCommand command, Solution solution)
        {
            command.Parameters.AddWithValue("@id", solution.Solution_id);
            command.Parameters.AddWithValue("@date", solution.Date);
            command.Parameters.AddWithValue("@desc", solution.Description);
            command.Parameters.AddWithValue("@orderOrderId", solution.Order_order_id);
        }

        private static Collection<Solution> Read(SqlDataReader reader)
        {
            Collection<Solution> solutions = new Collection<Solution>();

            while (reader.Read())
            {
                int i = -1;
                Solution solution = new Solution();
                solution.Solution_id = reader.GetInt32(++i);
                solution.Date = reader.GetDateTime(++i);
                solution.Description = reader.GetString(++i);
                solution.Order_order_id = reader.GetInt32(++i);

                solutions.Add(solution);
            }
            return solutions;
        }
    }
}
