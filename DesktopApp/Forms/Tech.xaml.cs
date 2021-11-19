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
    /// Interakční logika pro Tech.xaml
    /// </summary>
    public partial class Tech : Page
    {
        int id;
        public Tech(int id)
        {
            InitializeComponent();
            this.id = id;

            Orders.ItemsSource = OrderBusiness.getTechOrder(id);
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if (Orders.SelectedItem == null)
            {
                MessageBox.Show("Nebyla vybrána objednávka!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.NavigationService.Navigate(new SolForm((Orders.SelectedItem as OrderBusiness).Order_id, id));
        }
    }
}
