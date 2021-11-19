using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Complaint
    {
		public int Complaint_id { get; set; }
		public DateTime Start_date { get; set; }
		public DateTime? End_date { get; set; }
		public string Reason { get; set; }
		public int Order_order_id { get; set; }
		public int Employee_employee_id { get; set; }
		public string Result { get; set; }
	}
}
