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
    public class User_accountBusiness
    {
        public int User_id { get; set; }
        public string User_name { get; set; }
        public string Password { get; set; }
        public int Customer_customer_id { get; set; }

        public static Collection<User_accountBusiness> SelectAll()
        {
            Collection<User_accountBusiness> user_Accounts = new Collection<User_accountBusiness>();

            foreach (User_account dto in User_accountTable.SelectAll())
            {
                user_Accounts.Add(Read(dto));
            }

            return user_Accounts;
        }

        public static User_accountBusiness Select(int id)
        {
            return Read(User_accountTable.Select(id));
        }

        public static int Insert(User_accountBusiness user_Account)
        {
            return User_accountTable.Insert(Write(user_Account));
        }

        public static int Update(User_accountBusiness user_Account)
        {
            return User_accountTable.Update(Write(user_Account));
        }

        public static int Delete(int id)
        {
            return User_accountTable.Delete(id);
        }

        private static User_accountBusiness Read(User_account dto)
        {
            User_accountBusiness user_Account = new User_accountBusiness();
            user_Account.User_id = dto.User_id;
            user_Account.User_name = dto.User_name;
            user_Account.Password = dto.Password;
            user_Account.Customer_customer_id = dto.Customer_customer_id;

            return user_Account;
        }

        private static User_account Write(User_accountBusiness user_Account)
        {
            User_account dto = new User_account();
            dto.User_id = user_Account.User_id;
            dto.User_name = user_Account.User_name;
            dto.Password = user_Account.Password;
            dto.Customer_customer_id = user_Account.Customer_customer_id;

            return dto;
        }
    }
}
