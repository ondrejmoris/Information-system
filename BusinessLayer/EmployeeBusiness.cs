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
    public class EmployeeBusiness : CustomerBusiness
    {
        public string Rank { get; set; }

        public static new Collection<EmployeeBusiness> SelectAll()
        {
            Collection<EmployeeBusiness> employees = new Collection<EmployeeBusiness>();

            foreach (Employee dto in EmployeeTable.SelectAll())
            {
                employees.Add(Read(dto));
            }

            return employees;
        }

        public static EmployeeBusiness getEmployee(string firstName, string lastName)
        {
            foreach(EmployeeBusiness employee in EmployeeBusiness.SelectAll())
            {
                if(employee.First_name.Contains(firstName) && employee.Last_name.Contains(lastName))
                {
                    return employee;
                }
            }
            return null;
        }

        public static int getEmplToOrder()
        {
            Collection<EmployeeBusiness> employees = EmployeeBusiness.SelectAll();
            Collection<OrderBusiness> orders = OrderBusiness.SelectAll();
            int id = 0;
            int count = 0;
            foreach (EmployeeBusiness e in employees)
            {
                if (e.Rank.Contains("zaměstnanec"))
                {
                    if (id == 0 && count == 0)
                    {
                        id = e.Id;
                        foreach (OrderBusiness o in orders)
                        {
                            if (o.Employee_employee_id == id)
                                count++;
                        }
                    }
                    else
                    {
                        int tmpCnt = 0;
                        foreach (OrderBusiness o in orders)
                        {
                            if (o.Employee_employee_id == id)
                                tmpCnt++;
                        }
                        if (tmpCnt > count)
                        {
                            id = e.Id;
                            count = tmpCnt;
                        }
                    }
                }
            }
            return id;
        }

        public static int getTech()
        {
            Collection<EmployeeBusiness> employees = EmployeeBusiness.SelectAll();
            Collection<OrderBusiness> orders = OrderBusiness.SelectAll();
            int id = 0;
            int count = 0;
            foreach(EmployeeBusiness e in employees)
            {
                if (e.Rank.Contains("technik"))
                {
                    if(id == 0 && count == 0)
                    {
                        id = e.Id;
                        foreach(OrderBusiness o in orders)
                        {
                            if (o.Employee_technician_id == id)
                                count++;
                        }
                    }
                    else
                    {
                        int tmpCnt = 0;
                        foreach(OrderBusiness o in orders)
                        {
                            if (o.Employee_technician_id == id)
                                tmpCnt++;
                        }
                        if(tmpCnt > count)
                        {
                            id = e.Id;
                            count = tmpCnt;
                        }
                    }
                }
            }
            return id;
        }

        public static new EmployeeBusiness Select(int id)
        {
            return Read(EmployeeTable.Select(id));
        }

        public static int Insert(EmployeeBusiness employee)
        {
            return EmployeeTable.Insert(Write(employee));
        }

        public static int Update(EmployeeBusiness employee)
        {
            return EmployeeTable.Update(Write(employee));
        }

        public static new int Delete(int id)
        {
            return EmployeeTable.Delete(id);
        }

        private static EmployeeBusiness Read(Employee dto)
        {
            EmployeeBusiness employee = new EmployeeBusiness();
            employee.Id = dto.Employee_id;
            employee.First_name = dto.First_name;
            employee.Last_name = dto.Last_name;
            employee.Phone_number = dto.Phone_number;
            employee.Rank = dto.Rank;

            return employee;
        }

        private static Employee Write(EmployeeBusiness employee)
        {
            Employee dto = new Employee();
            dto.Employee_id = employee.Id;
            dto.First_name = employee.First_name;
            dto.Last_name = employee.Last_name;
            dto.Phone_number = employee.Phone_number;
            dto.Rank = employee.Rank;

            return dto;
        }
    }
}
