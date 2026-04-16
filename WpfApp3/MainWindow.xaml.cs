using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Elements;
using WpfApp3.Helpers;

namespace WpfApp3
{



    public partial class MainWindow : Window
    {

        public dbContext context = new dbContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginEnter.Text;
            string password = PasswordEnter.Password;

            var user = context.Users.Where(u => u.Email == login && u.Passw == password).FirstOrDefault();

            if (user == null)
            {
                MessageHelper.ShowError("Неверный логин или пароль");
                return;
            }
            else
            {
                CurrentUser.currentUser = user;
                new ProductWindow().Show();
                this.Close();

            }



        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}