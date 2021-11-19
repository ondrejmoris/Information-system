using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DataLayer;

namespace BusinessLayer
{
    public class AddressBusiness
    {
        public int Address_id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int? Postal_number { get; set; }
        public int? Customer_customer_id { get; set; }
        public int? Employee_employee_id { get; set; }

        public static Collection<AddressBusiness> SelectAll()
        {
            Collection<AddressBusiness> adderesses = new Collection<AddressBusiness>();

            foreach (Address a in AddressTable.SelectAll())
            {
                adderesses.Add(Read(a));
            }

            return adderesses;
        }

        public static AddressBusiness SelectByPerosnId(int id)
        {
            foreach(AddressBusiness address in AddressBusiness.SelectAll())
            {
                if (address.Customer_customer_id == id)
                    return address;
            }
            return null;
        }

        public static AddressBusiness Select(int id)
        {
            return Read(AddressTable.Select(id));
        }

        public static int Insert(AddressBusiness address)
        {
            return AddressTable.Insert(Write(address));
        }

        public static int Update(AddressBusiness address)
        {
            return AddressTable.Update(Write(address));
        }

        public static int Delete(int id)
        {
            return AddressTable.Delete(id);
        }

        private static AddressBusiness Read(Address a)
        {
            AddressBusiness adderess = new AddressBusiness();
            adderess.Address_id = a.Address_id;
            adderess.City = a.City;
            adderess.Street = a.Street;
            adderess.Number = a.Number;
            adderess.Postal_number = a.Postal_number == null ? null : a.Postal_number;
            adderess.Customer_customer_id = a.Customer_customer_id == null ? null : a.Customer_customer_id;
            adderess.Employee_employee_id = a.Employee_employee_id == null ? null : a.Employee_employee_id;

            return adderess;
        }

        private static Address Write(AddressBusiness address)
        {
            Address dtoAdderess = new Address();
            dtoAdderess.Address_id = address.Address_id;
            dtoAdderess.City = address.City;
            dtoAdderess.Street = address.Street;
            dtoAdderess.Number = address.Number;
            dtoAdderess.Postal_number = address.Postal_number == null ? null : address.Postal_number;
            dtoAdderess.Customer_customer_id = address.Customer_customer_id == null ? null : address.Customer_customer_id;
            dtoAdderess.Employee_employee_id = address.Employee_employee_id == null ? null : address.Employee_employee_id;

            return dtoAdderess;
        }
    }
}
