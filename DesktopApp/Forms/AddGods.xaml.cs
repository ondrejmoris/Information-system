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
    /// Interakční logika pro AddGods.xaml
    /// </summary>
    public partial class AddGods : Page
    {
        private int solId;
        public AddGods(int solId)
        {
            InitializeComponent();

            this.solId = solId;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            float price;
            if(this.price.Text.Length == 0 || float.TryParse(this.price.Text,out price) == false)
            {
                MessageBox.Show("Špatně zadaná cena!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(this.desc.Text.Length == 0)
            {
                MessageBox.Show("Špatně zadaný popis!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            GoodsBusiness goods = new GoodsBusiness();
            goods.Price = price;
            goods.Solution_solution_id = solId;
            goods.Type = this.desc.Text;

            if(GoodsBusiness.Insert(goods) == 0)
            {
                MessageBox.Show("Chyba při zápisu do DB!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Zboží přídáno!", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
            this.NavigationService.GoBack();
            this.NavigationService.RemoveBackEntry();
        }
    }
}
