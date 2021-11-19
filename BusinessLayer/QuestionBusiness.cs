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
    public class QuestionBusiness
    {
		public int Question_id { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
		public int Customer_customer_id { get; set; }
		public int Employee_employee_id { get; set; }

        public static Collection<QuestionBusiness> SelectAll()
        {
            Collection<QuestionBusiness> questions = new Collection<QuestionBusiness>();

            foreach (Question dto in QuestionTable.SelectAll())
            {
                questions.Add(Read(dto));
            }

            return questions;
        }

        public static QuestionBusiness Select(int id)
        {
            return Read(QuestionTable.Select(id));
        }

        public static int Insert(QuestionBusiness question)
        {
            return QuestionTable.Insert(Write(question));
        }

        public static int Update(QuestionBusiness question)
        {
            return QuestionTable.Update(Write(question));
        }

        public static int Delete(int id)
        {
            return QuestionTable.Delete(id);
        }

        private static QuestionBusiness Read(Question dto)
        {
            QuestionBusiness question = new QuestionBusiness();
            question.Question_id = dto.Question_id;
            question.Date = dto.Date;
            question.Description = dto.Description;
            question.Type = dto.Type;
            question.Customer_customer_id = dto.Customer_customer_id;
            question.Employee_employee_id = dto.Employee_employee_id;

            return question;
        }

        private static Question Write(QuestionBusiness question)
        {
            Question dto = new Question();
            dto.Question_id = question.Question_id;
            dto.Date = question.Date;
            dto.Description = question.Description;
            dto.Type = question.Type;
            dto.Customer_customer_id = question.Customer_customer_id;
            dto.Employee_employee_id = question.Employee_employee_id;

            return dto;
        }
    }
}
