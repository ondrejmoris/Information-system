using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Newtonsoft.Json;

namespace DataLayer
{
    public sealed class BranchTable
    {
        private static BranchTable instance = null;

        private Collection<Branch> br;

        private BranchTable()
        {
            br = readJson();
        }

        public static BranchTable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BranchTable();
                }

                return instance;
            }
        }

        private Collection<Branch> readJson()
        {
            return JsonConvert.DeserializeObject<Collection<Branch>>(File.ReadAllText("../../../branch.json"));
        }

        private bool writeJson(Collection<Branch> branches)
        {
            try 
            {
                File.WriteAllText("../../../branch.json", JsonConvert.SerializeObject(branches, Formatting.Indented));
            }
            catch(Exception e)
            {
                Console.WriteLine("Fail to write data! " + e);
                return false;
            }          
            refresh();
            return true;
        }

        public Collection<Branch> SellectAll()
        {
            return br;
        }

        public bool Insert(Branch branch)
        {
            br.Add(branch);
            if (!writeJson(br))
                return false;

            return true;
        }

        public bool Update(Branch branch)
        {
            for(int i = 0; i < br.Count; i++)
            {
                if(br[i].Branch_id == branch.Branch_id)
                {
                    br[i] = branch;
                    if (!writeJson(br))
                        return false;
                    return true;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            for(int i = 0; i < br.Count; i++)
            {
                if (br[i].Branch_id == id)
                {
                    br.RemoveAt(i);
                    if (!writeJson(br))
                        return false;
                    return true;
                }
            }
            return false;
        }

        private void refresh()
        {
            br = readJson();
        }
    }
}
