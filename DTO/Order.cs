using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Order
    {
		public int Order_id { get; set; }
		public string State { get; set;}
		public DateTime Start_date { get; set; }
		public DateTime? End_date { get; set; }
		public string Description { get; set; }
		public string Computer_type { get; set;}
		public int Customer_customer_id { get; set; }
		public int Employee_employee_id { get; set; }
		public int Employee_technician_id { get; set; }
    }
}
