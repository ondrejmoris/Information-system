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
    /// Interakční logika pro JsonTest.xaml
    /// </summary>
    public partial class JsonTest : Page
    {
        public JsonTest()
        {
            InitializeComponent();
            this.Traffics.ItemsSource = TrafficBusiness.SelectAll();
            this.Branchs.ItemsSource = BranchBusiness.SelectAll();
        }

        private void btnAddTraffic_Click(object sender, RoutedEventArgs e)
        {
            TrafficBusiness traffic = new TrafficBusiness();
            traffic.Traffic_id = 2;
            traffic.Date = DateTime.Now;
            traffic.Profit = 85444;
            traffic.Spending = 9999;
            traffic.Order_count = 8;
            traffic.Branch_branch_id = 1;

            TrafficBusiness.Insert(traffic);
            this.NavigationService.Navigate(new JsonTest());
        }

        private void btnAddBranch_Click(object sender, RoutedEventArgs e)
        {
            BranchBusiness branch = new BranchBusiness();
            branch.Branch_id = 2;
            branch.Name = "haaaaa";
            branch.Address = "Dom, 354";

            BranchBusiness.Insert(branch);
            this.NavigationService.Navigate(new JsonTest());
        }
    }
}
