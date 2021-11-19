using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Traffic
    {
        public int Traffic_id { get; set; }
        public DateTime Date { get; set; }
        public float Profit { get; set; }
        public float Spending { get; set; }
        public int Order_count { get; set; }
        public int Branch_branch_id { get; set; }
    }
}
