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
            Lol.ItemsSource = dbContext.Products.ToList();
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
