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
    public class GoodsTable
    {
        public static String SQL_SELECT_ALL = "SELECT * FROM \"Goods\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Goods\" WHERE goods_id = @id";
        public static String SQL_INSERT = "INSERT INTO \"Goods\" (price, type, solution_solution_id) VALUES (@price, @type, @solutionSolutionId)";
        public static String SQL_DELETE_ID = "DELETE FROM \"Goods\" WHERE goods_id = @id";
        public static String SQL_UPDATE = "UPDATE \"Goods\" SET price = @price, type = @type, solution_solution_id = @solutionSolutionId WHERE solution_id = @id";

        /// <summary>
        /// Insert the record.
        /// </summary>
        public static int Insert(Goods goods)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, goods);

            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }

        /// <summary>
        /// Update the record.
        /// </summary>
        public static int Update(Goods goods)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, goods);
            int ret = db.ExecuteNonQuery(command);

            db.Close();

            return ret;
        }


        /// <summary>
        /// Select the records.
        /// </summary>
        public static Collection<Goods> SelectAll()
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ALL);
            SqlDataReader reader = db.Select(command);

            Collection<Goods> goodss = Read(reader);
            reader.Close();

            db.Close();

            return goodss;
        }

        /// <summary>
        /// Select the record.
        /// </summary>

        public static Goods Select(int id)
        {
            Database db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<Goods> goodss = Read(reader);
            Goods goods = null;
            if (goodss.Count == 1)
            {
                goods = goodss[0];
            }
            reader.Close();

            db.Close();

            return goods;
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
        private static void PrepareCommand(SqlCommand command, Goods goods)
        {
            command.Parameters.AddWithValue("@id", goods.Goods_id);
            command.Parameters.AddWithValue("@price", goods.Price);
            command.Parameters.AddWithValue("@type", goods.Type);
            command.Parameters.AddWithValue("@solutionSolutionId", goods.Solution_solution_id);
        }

        private static Collection<Goods> Read(SqlDataReader reader)
        {
            Collection<Goods> goodss = new Collection<Goods>();

            while (reader.Read())
            {
                int i = -1;
                Goods goods = new Goods();
                goods.Goods_id = reader.GetInt32(++i);
                goods.Price = (float)reader.GetDouble(++i);
                goods.Type = reader.GetString(++i);
                goods.Solution_solution_id = reader.GetInt32(++i);

                goodss.Add(goods);
            }
            return goodss;
        }
    }
}
