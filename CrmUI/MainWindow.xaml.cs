using CrmBl;
using MaterialDesignThemes.Wpf;
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
using CrmUI.ViewModal;
using CrmBl.Model;
using System.Data.Entity;
using Microsoft.Toolkit.Uwp.Notifications;

namespace CrmUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CrmContext db;

        public MainWindow()
        {
            InitializeComponent();
            db = new CrmContext();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string login = loginField.Text.Trim();
            string password = passwordField.Password.Trim();
            string passwordRepeat = repeatPasswordField.Password.Trim();
            string email = emailField.Text.Trim().ToLower();

            if (Check(login, email, password, passwordRepeat))
            {
                if (!db.Users.Any(x => x.Login == login))
                {
                    User user = new User(login, email, password);
                    Customer customer = new Customer() { User = user, Name = user.Login };
                    db.Users.Add(user);
                    Cart cart = new Cart() { Customer = customer };
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    UserPageWindow userPageWindow = new UserPageWindow(user);
                    userPageWindow.Show();
                    Close();
                }
                else
                {
                    var notification = new ToastContentBuilder();
                    notification.AddText("Такой пользователь уже зарегестрирован");
                    notification.Show();
                }
            }

        }

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationWindow authenticationWindow = new AuthenticationWindow();
            authenticationWindow.Show();
            Hide();

        }
        private bool Check(string login, string email, string password, string passwordRepeat)
        {
            if (login.Length < 5)
            {
                loginField.ToolTip = "Логин должен содержать больше 5 символов";
                loginField.Background = Brushes.DarkRed;
            }
            else
            {
                loginField.ToolTip = "";
                loginField.Background = Brushes.Transparent;
            }

            if (!email.Contains("@"))
            {
                emailField.ToolTip = "Email должен содержать" + "\"" + "@" + "\"";
                emailField.Background = Brushes.DarkRed;
            }
            else
            {
                emailField.ToolTip = "";
                emailField.Background = Brushes.Transparent;
            }

            if (password.Length < 5)
            {
                passwordField.ToolTip = "Пароль должен содержать больше 5 символов";
                passwordField.Background = Brushes.DarkRed;
            }
            else
            {
                passwordField.ToolTip = "";
                passwordField.Background = Brushes.Transparent;
            }

            if (password != passwordRepeat)
            {
                repeatPasswordField.ToolTip = "Введенные пароли должны совпадать";
                repeatPasswordField.Background = Brushes.DarkRed;
            }
            else
            {
                repeatPasswordField.ToolTip = "";
                repeatPasswordField.Background = Brushes.Transparent;
            }

            if (loginField.Background != Brushes.DarkRed &&
                 emailField.Background != Brushes.DarkRed &&
                 passwordField.Background != Brushes.DarkRed &&
                 repeatPasswordField.Background != Brushes.DarkRed
                 )
                return true;
            else
                return false;

        }
    }
}
