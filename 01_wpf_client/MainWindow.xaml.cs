using data_access;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace _01_wpf_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SportShopManager manager = null;

        public MainWindow()
        {
            InitializeComponent();

            string connStr = ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;
            manager = new(connStr);

            grid.ItemsSource = manager.GetAllProducts();
        }
    }
}
