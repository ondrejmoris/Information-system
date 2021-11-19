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
    public class OrderBusiness
    {
		public int Order_id { get; set; }
		public string State { get; set; }
		public DateTime Start_date { get; set; }
		public DateTime? End_date { get; set; }
		public string Description { get; set; }
		public string Computer_type { get; set; }
		public int Customer_customer_id { get; set; }
		public int Employee_employee_id { get; set; }
		public int Employee_technician_id { get; set; }

        public static Collection<OrderBusiness> SelectAll()
        {
            Collection<OrderBusiness> orders = new Collection<OrderBusiness>();

            foreach (Order dto in OrderTable.SelectAll())
            {
                orders.Add(Read(dto));
            }

            return orders;
        }
        public static bool cancelOrder(int id, string description)
        {
            OrderBusiness order = OrderBusiness.Select(id);
            order.Description += " | " + description.ToUpper();
            order.End_date = DateTime.Now;
            order.State = "Zrušeno";
            // Po přihlášení se bude posílat ID přihlášeného zaměstnance (piřhlášení neřešíme).
            order.Employee_employee_id = 1;
            if (OrderBusiness.Update(order) == 0)
            {
                return false;
            }
            return true;
        }

        public static bool confirmOrder(OrderBusiness order)
        {
            order.Employee_technician_id = EmployeeBusiness.getTech();
            order.State = "Přijato";
            if (OrderBusiness.Update(order) == 0)
                return false;
            return true;
        }

        public static Collection<OrderBusiness> getTechOrder(int id)
        {
            Collection<OrderBusiness> orders = new Collection<OrderBusiness>();

            foreach(OrderBusiness order in OrderBusiness.SelectAll())
            {
                if (order.Employee_technician_id == id && order.State.Contains("Přijato"))
                    orders.Add(order);
            }
            return orders;
        }
        public static bool TechDone(int order_id)
        {
            foreach(OrderBusiness order in OrderBusiness.SelectAll())
            {
                if(order.Order_id == order_id)
                {
                    order.State = "Opraveno";
                    if (OrderBusiness.Update(order) == 0)
                        return false;
                    return true;
                }
            }
            return false;
        }

        public static OrderBusiness Select(int id)
        {
            return Read(OrderTable.Select(id));
        }

        public static int Insert(OrderBusiness order)
        {
            return OrderTable.Insert(Write(order));
        }

        public static int Update(OrderBusiness order)
        {
            return OrderTable.Update(Write(order));
        }

        public static int Delete(int id)
        {
            return OrderTable.Delete(id);
        }

        private static OrderBusiness Read(Order dto)
        {
            OrderBusiness order = new OrderBusiness();
            order.Order_id = dto.Order_id;
            order.State = dto.State;
            order.Start_date = dto.Start_date;
            order.End_date = dto.End_date == null ? null : dto.End_date;
            order.Description = dto.Description;
            order.Computer_type = dto.Computer_type;
            order.Customer_customer_id = dto.Customer_customer_id;
            order.Employee_employee_id = dto.Employee_employee_id;
            order.Employee_technician_id = dto.Employee_technician_id;

            return order;
        }

        private static Order Write(OrderBusiness order)
        {
            Order dto = new Order();
            dto.Order_id = order.Order_id;
            dto.State = order.State;
            dto.Start_date = order.Start_date;
            dto.End_date = order.End_date == null ? null : order.End_date;
            dto.Description = order.Description;
            dto.Computer_type = order.Computer_type;
            dto.Customer_customer_id = order.Customer_customer_id;
            dto.Employee_employee_id = order.Employee_employee_id;
            dto.Employee_technician_id = order.Employee_technician_id;

            return dto;
        }
    }
}
