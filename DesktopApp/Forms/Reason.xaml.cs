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
    /// Interakční logika pro Reason.xaml
    /// </summary>
    /// 
    // Po přihlášení se bude posílat ID přihlášeného zaměstnance (piřhlášení neřešíme).
    public partial class Reason : Page
    {
        private int id;
        public Reason(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(this.reason.Text.Length == 0)
            {
                MessageBox.Show("Nezadal si důvod!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!OrderBusiness.cancelOrder(id, this.reason.Text))
            {
                MessageBox.Show("Chyba!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Objednávka byla zrušena!", "Úspěch!", MessageBoxButton.OK, MessageBoxImage.Information);
            OrderConf conf = new OrderConf();
            this.NavigationService.Navigate(conf);
            this.NavigationService.RemoveBackEntry();
        }
    }
}
