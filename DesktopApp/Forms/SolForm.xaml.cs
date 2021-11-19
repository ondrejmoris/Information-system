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
    /// Interakční logika pro SolForm.xaml
    /// </summary>
    public partial class SolForm : Page
    {
        private int order_id;
        private int tech_id;
        private SolutionBusiness solution = new SolutionBusiness();
        public SolForm(int order_id, int tech_id)
        {
            InitializeComponent();

            this.order_id = order_id;
            this.tech_id = tech_id;
            this.desc.Text = solution == null ? "" : solution.Description;
        }

        private void btnAddSol_Click(object sender, RoutedEventArgs e)
        {
            if(this.desc.Text.Length == 0)
            {
                MessageBox.Show("Nebyl zadán popis!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SolutionBusiness.Check(order_id))
            {
                MessageBox.Show("Řešení již bylo vytvořeno!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this.solution.Description = this.desc.Text;
            this.solution.Order_order_id = order_id;
            this.solution.Date = DateTime.Now;

            if(SolutionBusiness.Insert(solution) == 0)
            {
                MessageBox.Show("Chyba zápisu do DB", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.solution = SolutionBusiness.getByOrderId(order_id);
            MessageBox.Show("Řešení přidáno!", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnAddGoods_Click(object sender, RoutedEventArgs e)
        {
            if (!SolutionBusiness.Check(order_id))
            {
                MessageBox.Show("Řešení nebylo přídáno!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.NavigationService.Navigate(new AddGods(solution.Solution_id));
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                this.Goods.ItemsSource = GoodsBusiness.getBySolId(solution.Solution_id);
            }
            catch(Exception exce)
            {
                MessageBox.Show("Není co aktualizovat!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } 
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(this.Goods.Items.Count == 0 || !SolutionBusiness.Check(this.order_id))
            {
                MessageBox.Show("Informace nejsou kompletní!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!OrderBusiness.TechDone(order_id))
            {
                MessageBox.Show("Chyba zápisu do DB", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Zpracováni dokončeno!", "Úspěch!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.NavigationService.Navigate(new Tech(tech_id));
            this.NavigationService.RemoveBackEntry();
        }

    }
}
