using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Question
    {
		public int Question_id { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public string Type { get; set;}
		public int Customer_customer_id { get; set; }
		public int Employee_employee_id { get; set; }
}
}
