using CrmBl;
using CrmBl.Model;
using CrmUI.ViewModal;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telegram.Bot;

namespace CrmUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class UserPageWindow : Window
    {
        CrmContext db;
        User user;
        Cart cart;
        TelegramBotClient bot;
        string token = "";

        public UserPageWindow(User user)
        {
            // чтение из json файла
            try
            {
                using (StreamReader reader = new StreamReader(@"D:\Projects\WPF\CRM\CrmBl\Data.json"))
                {
                    string jsonString = reader.ReadToEnd();
                    var json = JObject.Parse(jsonString);
                    token = json["token"].ToString();
                }
            }
            catch { }


            InitializeComponent();
            bot = new TelegramBotClient(token);
            this.user = user;

            db = new CrmContext();
            db.Carts.Load();
            db.Customers.Load();
            db.Users.Load();
            cart = db.Carts.Where(x => x.Customer.User.Id == user.Id).FirstOrDefault();

            var menuProduct = new List<SubItem>();
            var menuSeller = new List<SubItem>();
            var menuCustomer = new List<SubItem>();
            var menuPurchase = new List<SubItem>();

            menuProduct.Add(new SubItem("Список товаров", new UserControlProducts(db, cart)));
            ItemMenu itemProduct = new ItemMenu("Товар", menuProduct, PackIconKind.AlphaPBox);

            menuSeller.Add(new SubItem("Список продавцов", new UserControlSellers(db)));
            ItemMenu itemSeller = new ItemMenu("Продавец", menuSeller, PackIconKind.About);

            menuCustomer.Add(new SubItem("Список покупателей", new UserControlCustomers(db)));
            ItemMenu itemCustomer = new ItemMenu("Покупатель", menuCustomer, PackIconKind.About);

            menuPurchase.Add(new SubItem("Корзина", new UserControlCart(db, user)));
            menuPurchase.Add(new SubItem("Чеки", new UserControlChecks(db, user, bot)));
            ItemMenu itemPurchase = new ItemMenu("Покупка", menuPurchase, PackIconKind.About);

            Menu.Children.Add(new UserControlMenuItem(itemProduct, this, db));
            Menu.Children.Add(new UserControlMenuItem(itemSeller, this, db));
            Menu.Children.Add(new UserControlMenuItem(itemCustomer, this, db));
            Menu.Children.Add(new UserControlMenuItem(itemPurchase, this, db));

        }


        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (screen != null)
            {
                if (screen is UserControlProducts)
                {
                    (((UserControlProducts)screen).productsGrid.Columns[3] as System.Windows.Controls.DataGridComboBoxColumn).ItemsSource = db.Sellers.Local.ToBindingList().Where(x => x.Name != null);
                }

                if (screen is UserControlCart)
                {
                    UserControlCart.FillDataGrid((UserControlCart)screen);
                }

                if (screen is UserControlChecks)
                {
                    UserControlChecks.FillDataGridChecks((UserControlChecks)screen, user);
                }

                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
    }
}
