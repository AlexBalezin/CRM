using CrmBl;
using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
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
using Telegram.Bot;

namespace CrmUI
{
    /// <summary>
    /// Логика взаимодействия для UserControlChecks.xaml
    /// </summary>
    public partial class UserControlChecks : UserControl
    {
        CrmContext db;
        User user;
        TelegramBotClient bot;

        public UserControlChecks(CrmContext db, User user, TelegramBotClient bot)
        {
            InitializeComponent();
            this.db = db;
            this.user = user;
            this.bot = bot;
            FillDataGridChecks(this, user);
        }

        private void SendCheck(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ItemCheck)checkGrid.SelectedItem;
            var message = $"Чек от {selectedItem.Date}. Товар: {selectedItem.NameProduct}. На сумму: {selectedItem.Sum}";

            bot.SendTextMessageAsync(1432445917, message);
        }

        public static void FillDataGridChecks(UserControlChecks userControlChecks, User user)
        {
            userControlChecks.db.Checks.Load();
            userControlChecks.db.Sells.Load();
            userControlChecks.db.Products.Load();
            userControlChecks.db.Customers.Load();
            userControlChecks.db.Users.Load();

            var customer = userControlChecks.db.Customers.Where(c => c.User.Id == user.Id).FirstOrDefault();
            var checks = userControlChecks.db.Checks.Where(c => c.Customer.Id == customer.Id);
            var sells = userControlChecks.db.Sells;
            var products = userControlChecks.db.Products;

            var items = from check in checks
                        join sell in sells on check.Id equals sell.Check.Id
                        join product in products on sell.Product.Id equals product.Id
                        select new { check.Id, product.Name, check.Sum, check.Created};

            List<ItemCheck> itemChecks = new List<ItemCheck>();
            foreach (var item in items)
                itemChecks.Add(new ItemCheck() { Id = item.Id, NameProduct = item.Name, Sum = item.Sum, Date  = item.Created });

            userControlChecks.checkGrid.ItemsSource = null;
            userControlChecks.checkGrid.ItemsSource = itemChecks;
        }

        public static void AddCheck(Check check)
        {

        }

    }

    struct ItemCheck
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
    }
}
