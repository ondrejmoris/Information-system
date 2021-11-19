using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLayer;

namespace DesktopApp.Forms
{
    /// <summary>
    /// Interakční logika pro ConfForm.xaml
    /// </summary>
    public partial class ConfForm : Page
    {
        private int id;
        private OrderBusiness order;
        public ConfForm(int id)
        {
            InitializeComponent();
            this.id = id;
            order = OrderBusiness.Select(id);
            CustomerBusiness customer = CustomerBusiness.Select(order.Customer_customer_id);

            this.firstName.Text = customer.First_name;
            this.lastName.Text = customer.Last_name;
            this.phoneNumber.Text = customer.Phone_number.ToString();

            this.orderId.Text = order.Order_id.ToString();
            this.state.Text = order.State;
            this.startDate.Text = order.Start_date.ToString();
            this.endDate.Text = order.End_date.ToString();
            this.description.Text = order.Description;
            this.type.Text = order.Computer_type;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Opravud chceš zrušit tuto objednávku?", "Pozor", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Reason reason = new Reason(Int32.Parse(this.orderId.Text));
                    this.NavigationService.Navigate(reason);
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OrderEdit(id));
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Dáváme fixni id zaměstnance protože neřešíme přihlašování
            order.Employee_technician_id = 1;
            if (!OrderBusiness.confirmOrder(order))
            {
                MessageBox.Show("Chyba při zápisu do DB!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Objednávka potvrzena!", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
            this.NavigationService.Navigate(new OrderConf());
            //this.NavigationService.GoBack();
            this.NavigationService.RemoveBackEntry();
        }
    }
}
