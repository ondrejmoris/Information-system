using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace WebApp
{
    public partial class _Default : Page
    {
        private CustomerBusiness customer;
        private AddressBusiness cusAddress;
        public static bool visState = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Až bude přihlášení tak se bude dávat ID
            customer = CustomerBusiness.Select(1);
            cusAddress = AddressBusiness.SelectByPerosnId(1);
            this.personal.Visible = visState;
        }

        protected void confirm_Click(object sender, EventArgs e)
        {
            ClientScriptManager CSM = Page.ClientScript;
            if (this.radN.Checked == false && this.radS.Checked == false)
            {
                string strconfirm = "<script>if(!window.confirm('Údaje nesjou kompletní! Chcete pokračovat nebo zrušit objednávku?')){window.location.href='Default.aspx'}</script>";
                CSM.RegisterClientScriptBlock(this.GetType(), "Confirm", strconfirm, false);
                return;
            }
            if(this.Desc.InnerText.Length == 0)
            {
                string strconfirm = "<script>if(!window.confirm('Údaje nesjou kompletní! Chcete pokračovat nebo zrušit objednávku?')){window.location.href='Default.aspx'}</script>";
                CSM.RegisterClientScriptBlock(this.GetType(), "Confirm", strconfirm, false);
                return;
            }
            this.radN.Disabled = true;
            this.radS.Disabled = true;
            this.Desc.Disabled = true;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Úspěch. Prosím zkontrolujte své osobní údaje!');", true);
            showPersonal();
        }
        private void showPersonal()
        {
            this.Phone.Value = this.customer.Phone_number.ToString();
            this.city.Value = this.cusAddress.City;
            this.houseNum.Value = this.cusAddress.Number.ToString();
            this.address.Value = this.cusAddress.Street;

            visState = true;
            this.personal.Visible = visState;
        }

        protected void confirmPers_Click(object sender, EventArgs e)
        {
            ClientScriptManager CSM = Page.ClientScript;
            if (this.Phone.Value.Length != 9 || this.houseNum.Value.Length == 0 || this.city.Value.Length == 0 || this.address.Value.Length == 0)
            {
                string strconfirm = "<script>if(!window.confirm('Údaje nesjou kompletní! Chcete pokračovat nebo zrušit objednávku?')){window.location.href='Default.aspx'}</script>";
                CSM.RegisterClientScriptBlock(this.GetType(), "Confirm", strconfirm, false);
                return;
            }
            OrderBusiness order = new OrderBusiness();
            order.Computer_type = this.radN.Checked ? "N" : "PC";
            order.Description = this.Desc.Value;
            order.Start_date = DateTime.Now;
            // Customer id se bude měnti až bude hotové přihlášení
            order.Customer_customer_id = 1;
            order.Employee_employee_id = EmployeeBusiness.getEmplToOrder();
            order.State = "Zadáno";
            order.Employee_technician_id = EmployeeBusiness.getTech();
            if(OrderBusiness.Insert(order) == 0 || AddressBusiness.Update(cusAddress) == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Chyba při zápisu do DB!');", true);
                return;
            }
            //Server.Transfer("Default.aspx");
            string strconfirm1 = "<script>window.alert('Úspěch. Objednávka vytvořena!');window.location.href='Default.aspx'</script>";
            CSM.RegisterClientScriptBlock(this.GetType(), "Confirm", strconfirm1, false);

            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Úspěch. Objednávka vytvořena!');", true);
        }
    }
}