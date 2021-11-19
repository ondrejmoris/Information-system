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
    /// Interakční logika pro Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(this.lastName.Text.Length == 0 || this.firstName.Text.Length == 0)
            {
                MessageBox.Show("Nezadal si jméno nebo přijmení!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            EmployeeBusiness employee = EmployeeBusiness.getEmployee(this.firstName.Text, this.lastName.Text);

            if (employee == null)
            {
                MessageBox.Show("Zaměstnanec nenalezen!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (employee.Rank.Contains("zaměstnanec"))
            {
                // Dodělat posílaní id
                this.NavigationService.Navigate(new OrderConf());
            }
            else if (employee.Rank.Contains("technik"))
            {
                this.NavigationService.Navigate(new Tech(employee.Id));
            }
            else if (employee.Rank.Contains("vedoucí"))
            {
                // Navigovat na stránku vedoucího
            }
            else
            {
                MessageBox.Show("Zaměstnanec nenalezen!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void btnTestJson_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new JsonTest());
        }
    }
}
