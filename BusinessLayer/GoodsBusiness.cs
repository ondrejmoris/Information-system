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
    public class GoodsBusiness
    {
        public int Goods_id { get; set; }
        public float Price { get; set; }
        public string Type { get; set; }
        public int Solution_solution_id { get; set; }

        public static Collection<GoodsBusiness> SelectAll()
        {
            Collection<GoodsBusiness> goods = new Collection<GoodsBusiness>();

            foreach (Goods dto in GoodsTable.SelectAll())
            {
                goods.Add(Read(dto));
            }

            return goods;
        }

        public static Collection<GoodsBusiness> getBySolId(int id)
        {
            Collection<GoodsBusiness> goods = new Collection<GoodsBusiness>();

            foreach(GoodsBusiness god in GoodsBusiness.SelectAll())
            {
                if (god.Solution_solution_id == id)
                    goods.Add(god);
            }
            return goods;
        }

        public static GoodsBusiness Select(int id)
        {
            return Read(GoodsTable.Select(id));
        }

        public static int Insert(GoodsBusiness goods)
        {
            return GoodsTable.Insert(Write(goods));
        }

        public static int Update(GoodsBusiness goods)
        {
            return GoodsTable.Update(Write(goods));
        }

        public static int Delete(int id)
        {
            return GoodsTable.Delete(id);
        }

        private static GoodsBusiness Read(Goods dto)
        {
            GoodsBusiness goods = new GoodsBusiness();
            goods.Goods_id = dto.Goods_id;
            goods.Price = dto.Price;
            goods.Type = dto.Type;
            goods.Solution_solution_id = dto.Solution_solution_id;

            return goods;
        }

        private static Goods Write(GoodsBusiness goods)
        {
            Goods dto = new Goods();
            dto.Goods_id = goods.Goods_id;
            dto.Price = goods.Price;
            dto.Type = goods.Type;
            dto.Solution_solution_id = goods.Solution_solution_id;

            return dto;
        }
    }
}
