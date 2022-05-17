using CrmBl;
using CrmBl.Model;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace CrmUI
{

    /// <summary>
    /// Логика взаимодействия для AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        Cart cart;
        User currentUser;

        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private bool Check(string login, string password)
        {
            using (CrmContext db = new CrmContext())
            {
                currentUser = db.Users.Where(x => x.Login == login && x.Pass == password).FirstOrDefault();
                if (currentUser != null)
                    return true;
                else
                    return false;
            }

        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = loginField.Text.Trim();
            string password = passwordField.Password.Trim();

            if (Check(login, password))
            {
                passwordField.ToolTip = "";
                passwordField.Background = Brushes.Transparent;
                loginField.ToolTip = "";
                loginField.Background = Brushes.Transparent;
                var notification = new ToastContentBuilder();
                notification.AddText("Вход выполнен");
                notification.Show();
                using (CrmContext db = new CrmContext())
                {
                    db.Carts.Load();
                    cart = db.Carts.Local.FirstOrDefault(c => c.User.Id == currentUser.Id);
                }
                UserPageWindow userPageWindow = new UserPageWindow(cart);
                userPageWindow.Show();
                Close();
            }
            else
            {
                var notification = new ToastContentBuilder();
                notification.AddText("Неверный логин или пароль");
                notification.Show();
            }

        }
    }
}
