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
    public class CustomerBusiness
    {
        public string First_name { get; set; }
        public int Id { get; set; }
        public string Last_name { get; set; }
        public int Phone_number { get; set; }

        public static Collection<CustomerBusiness> SelectAll()
        {
            Collection<CustomerBusiness> customers = new Collection<CustomerBusiness>();

            foreach (Customer dto in CustomerTable.SelectAll())
            {
                customers.Add(Read(dto));
            }

            return customers;
        }

        public static CustomerBusiness Select(int id)
        {
            return Read(CustomerTable.Select(id));
        }

        public static int Insert(CustomerBusiness customer)
        {
            return CustomerTable.Insert(Write(customer));
        }

        public static int Update(CustomerBusiness customer)
        {
            return CustomerTable.Update(Write(customer));
        }

        public static int Delete(int id)
        {
            return CustomerTable.Delete(id);
        }

        private static CustomerBusiness Read(Customer dto)
        {
            CustomerBusiness customer = new CustomerBusiness();
            customer.First_name = dto.First_name;
            customer.Id = dto.Customer_id;
            customer.Last_name = dto.Last_name;
            customer.Phone_number = dto.Phone_number;

            return customer;
        }

        private static Customer Write(CustomerBusiness customer)
        {
            Customer dto = new Customer();
            dto.Customer_id = customer.Id;
            dto.First_name = customer.First_name;
            dto.Last_name = customer.Last_name;
            dto.Phone_number = customer.Phone_number;

            return dto;
        }
    }
}
