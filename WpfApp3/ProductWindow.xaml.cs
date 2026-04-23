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
using System.Windows.Shapes;
using WpfApp3.Elements;
using WpfApp3.ViewModel;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {

        dbContext dbContext = new dbContext();

        public void LoadProducts()
        {
            var products = dbContext.Products.ToList();
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            foreach (var p in products) { 
                productViewModels.Add(new ProductViewModel(p));
            }
            Lol.ItemsSource = productViewModels;
        }

        public ProductWindow()
        {
            InitializeComponent();
            LoadProducts();
        }
        public ProductWindow(User user)
        {
            
            InitializeComponent();
            LoadProducts();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
