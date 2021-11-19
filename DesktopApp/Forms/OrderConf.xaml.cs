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
    /// Interakční logika pro OrderConf.xaml
    /// </summary>
    public partial class OrderConf : Page
    {
        public OrderConf()
        {
            InitializeComponent();

            Orders.ItemsSource = OrderBusiness.SelectAll();
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if(Orders.SelectedItem == null)
            {
                MessageBox.Show("Nebyla vybrána objednávka!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if((Orders.SelectedItem as OrderBusiness).State.Split(' ')[0] != "Zadáno") 
            {
                MessageBox.Show("Objednávka již byla zpracována!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ConfForm form = new ConfForm((Orders.SelectedItem as OrderBusiness).Order_id);
            this.NavigationService.Navigate(form);
        }
    }
}
