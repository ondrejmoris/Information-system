using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DataLayer;
using System.Collections.ObjectModel;

namespace BusinessLayer
{
    public class SolutionBusiness
    {
        public int Solution_id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Order_order_id { get; set; }

        public static Collection<SolutionBusiness> SelectAll()
        {
            Collection<SolutionBusiness> solutions = new Collection<SolutionBusiness>();

            foreach (Solution dto in SolutionTable.SelectAll())
            {
                solutions.Add(Read(dto));
            }

            return solutions;
        }

        public static SolutionBusiness Select(int id)
        {
            return Read(SolutionTable.Select(id));
        }
        public static bool Check(int order_id)
        {
            foreach(SolutionBusiness solution in SolutionBusiness.SelectAll())
            {
                if (solution.Order_order_id == order_id)
                    return true;
            }
            return false;
        }

        public static SolutionBusiness getByOrderId(int id)
        {
            foreach(SolutionBusiness solution in SolutionBusiness.SelectAll())
            {
                if (solution.Order_order_id == id)
                    return solution;
            }
            return null;
        }

        public static int Insert(SolutionBusiness solution)
        {
            return SolutionTable.Insert(Write(solution));
        }

        public static int Update(SolutionBusiness solution)
        {
            return SolutionTable.Update(Write(solution));
        }

        public static int Delete(int id)
        {
            return SolutionTable.Delete(id);
        }

        private static SolutionBusiness Read(Solution dto)
        {
            SolutionBusiness solution = new SolutionBusiness();
            solution.Solution_id = dto.Solution_id;
            solution.Date = dto.Date;
            solution.Description = dto.Description;
            solution.Order_order_id = dto.Order_order_id;

            return solution;
        }

        private static Solution Write(SolutionBusiness solution)
        {
            Solution dto = new Solution();
            dto.Solution_id = solution.Solution_id;
            dto.Date = solution.Date;
            dto.Description = solution.Description;
            dto.Order_order_id = solution.Order_order_id;

            return dto;
        }
    }
}
