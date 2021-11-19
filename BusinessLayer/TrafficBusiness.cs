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
    public class TrafficBusiness
    {
        public int Traffic_id { get; set; }
        public DateTime Date { get; set; }
        public float Profit { get; set; }
        public float Spending { get; set; }
        public int Order_count { get; set; }
        public int Branch_branch_id { get; set; }

        public static Collection<TrafficBusiness> SelectAll()
        {
            Collection<TrafficBusiness> traffics = new Collection<TrafficBusiness>();

            foreach (Traffic dto in TrafficTable.Instance.SelectAll())
            {
                traffics.Add(Read(dto));
            }

            return traffics;
        }

        public static TrafficBusiness Select(int id)
        {
            foreach (Traffic b in TrafficTable.Instance.SelectAll())
            {
                if (b.Traffic_id == id)
                    return Read(b);
            }
            return new TrafficBusiness();
        }
        public static bool Insert(TrafficBusiness traffic)
        {
            return TrafficTable.Instance.Insert(Write(traffic));
        }

        public static bool Update(TrafficBusiness traffic)
        {
            return TrafficTable.Instance.Update(Write(traffic));
        }

        public static bool Delete(int id)
        {
            return TrafficTable.Instance.Delete(id);
        }

        private static TrafficBusiness Read(Traffic dto)
        {
            TrafficBusiness traffic = new TrafficBusiness();
            traffic.Traffic_id = dto.Traffic_id;
            traffic.Date = dto.Date;
            traffic.Profit = dto.Profit;
            traffic.Spending = dto.Spending;
            traffic.Order_count = dto.Order_count;
            traffic.Branch_branch_id = dto.Branch_branch_id;

            return traffic;
        }

        private static Traffic Write(TrafficBusiness traffic)
        {
            Traffic dto = new Traffic();
            dto.Traffic_id = traffic.Traffic_id;
            dto.Date = traffic.Date;
            dto.Profit = traffic.Profit;
            dto.Spending = traffic.Spending;
            dto.Order_count = traffic.Order_count;
            dto.Branch_branch_id = traffic.Branch_branch_id;

            return dto;
        }
    }
}
