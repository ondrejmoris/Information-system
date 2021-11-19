using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Address
    {
		public int Address_id { get; set; }
		public string City { get; set;}
	    public string Street { get; set;}
        public int Number { get; set; }
        public int? Postal_number { get; set; }
        public int? Customer_customer_id { get; set; }
        public int? Employee_employee_id { get; set; }
    }
}
