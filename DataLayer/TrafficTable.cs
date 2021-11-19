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
    public sealed class TrafficTable
    {
        private static TrafficTable instance = null;

        private Collection<Traffic> tr;

        private TrafficTable()
        {
            this.tr = readJson();
        }

        public static TrafficTable Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new TrafficTable();
                }

                return instance;
            }
        }
    
        private Collection<Traffic> readJson()
        {
            return JsonConvert.DeserializeObject<Collection<Traffic>>(File.ReadAllText("../../../traffic.json"));
        }

        private bool writeJson(Collection<Traffic> traffics)
        {
            try
            {
                File.WriteAllText("../../../traffic.json", JsonConvert.SerializeObject(traffics, Formatting.Indented));
            }
            catch(Exception e)
            {
                Console.WriteLine("Fail to write data! " + e);
                return false;
            }
            refresh();
            return true;

        }

        public Collection<Traffic> SelectAll()
        {
            return tr;
        }

        public bool Insert(Traffic traffic)
        {
            tr.Add(traffic);
            if (!writeJson(tr))
                return false;

            return true;
        }

        public bool Update(Traffic traffic)
        {
            for (int i = 0; i < tr.Count; i++)
            {
                if (tr[i].Traffic_id == traffic.Traffic_id)
                {
                    tr[i] = traffic;
                    if (!writeJson(tr))
                        return false;
                    return true;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            for (int i = 0; i < tr.Count; i++)
            {
                if (tr[i].Traffic_id == id)
                {
                    tr.RemoveAt(i);
                    if (!writeJson(tr))
                        return false;
                    return true;
                }
            }
            return false;
        }

        private void refresh()
        {
            tr = readJson();
        }
    }
}
