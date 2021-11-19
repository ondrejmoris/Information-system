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
    public class ComplaintBusiness
    {
		public int Complaint_id { get; set; }
		public DateTime Start_date { get; set; }
		public DateTime? End_date { get; set; }
		public string Reason { get; set; }
		public int Order_order_id { get; set; }
		public int Employee_employee_id { get; set; }
		public string Result { get; set; }

        public static Collection<ComplaintBusiness> SelectAll()
        {
            Collection<ComplaintBusiness> complaints = new Collection<ComplaintBusiness>();

            foreach (Complaint dto in ComplaintTable.SelectAll())
            {
                complaints.Add(Read(dto));
            }

            return complaints;
        }

        public static ComplaintBusiness Select(int id)
        {
            return Read(ComplaintTable.Select(id));
        }

        public static int Insert(ComplaintBusiness complaint)
        {
            return ComplaintTable.Insert(Write(complaint));
        }

        public static int Update(ComplaintBusiness complaint)
        {
            return ComplaintTable.Update(Write(complaint));
        }

        public static int Delete(int id)
        {
            return ComplaintTable.Delete(id);
        }

        private static ComplaintBusiness Read(Complaint dto)
        {
            ComplaintBusiness complaint = new ComplaintBusiness();
            complaint.Complaint_id = dto.Complaint_id;
            complaint.Start_date = dto.Start_date;
            complaint.End_date = dto.End_date == null ? null : dto.End_date;
            complaint.Reason = dto.Reason;
            complaint.Order_order_id = dto.Order_order_id;
            complaint.Employee_employee_id = dto.Employee_employee_id;
            complaint.Result = dto.Result;

            return complaint;
        }

        private static Complaint Write(ComplaintBusiness complaint)
        {
            Complaint dto = new Complaint();
            dto.Complaint_id = complaint.Complaint_id;
            dto.Start_date = complaint.Start_date;
            dto.End_date = complaint.End_date == null ? null : complaint.End_date;
            dto.Reason = complaint.Reason;
            dto.Order_order_id = complaint.Order_order_id;
            dto.Employee_employee_id = complaint.Employee_employee_id;
            dto.Result = complaint.Result;

            return dto;
        }
    }
}
