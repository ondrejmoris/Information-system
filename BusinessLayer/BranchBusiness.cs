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
    public class BranchBusiness
    {
        public int Branch_id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public static Collection<BranchBusiness> SelectAll()
        {
            Collection<BranchBusiness> branches = new Collection<BranchBusiness>();

            foreach (Branch dto in BranchTable.Instance.SellectAll())
            {
                branches.Add(Read(dto));
            }

            return branches;
        }

        public static BranchBusiness Select(int id)
        {
            foreach(Branch b in BranchTable.Instance.SellectAll())
            {
                if (b.Branch_id == id)
                    return Read(b);
            }
            return new BranchBusiness();
        }
        public static bool Insert(BranchBusiness branch)
        {
            return BranchTable.Instance.Insert(Write(branch));
        }

        public static bool Update(BranchBusiness branch)
        {
            return BranchTable.Instance.Update(Write(branch));
        }

        public static bool Delete(int id)
        {
            return BranchTable.Instance.Delete(id);
        }

        private static BranchBusiness Read(Branch dto)
        {
            BranchBusiness branch = new BranchBusiness();
            branch.Branch_id = dto.Branch_id;
            branch.Name = dto.Name;
            branch.Address = dto.Address;

            return branch;
        }

        private static Branch Write(BranchBusiness branch)
        {
            Branch dto = new Branch();
            dto.Branch_id = branch.Branch_id;
            dto.Name = branch.Name;
            dto.Address = branch.Address;

            return dto;
        }
    }
}
