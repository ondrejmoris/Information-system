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
    /// Interakční logika pro OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : Page
    {
        private OrderBusiness order;
        public OrderEdit(int id)
        {
            InitializeComponent();

            order = OrderBusiness.Select(id);
            this.description.Text = order.Description;
            if(order.Computer_type.Contains("N"))
            {
                this.typeNotebook.IsChecked = true;
            }
            else
            {
                this.typePc.IsChecked = true;
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(this.description.Text.Length == 0)
            {
                MessageBox.Show("Nebyl zadán popis!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            order.Description = this.description.Text;
            order.Computer_type = this.typeNotebook.IsChecked == true ? "N" : "PC";

            if(OrderBusiness.Update(this.order) == 0)
            {
                MessageBox.Show("Chyba při zápisu do DB!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Úprava byla úspěšná!", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
            this.NavigationService.Navigate(new ConfForm(this.order.Order_id));
            this.NavigationService.RemoveBackEntry();
        }
    }
}
