using CrmBl;
using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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

namespace CrmUI
{
    /// <summary>
    /// Логика взаимодействия для UserControlCart.xaml
    /// </summary>
    public partial class UserControlCart : UserControl
    {
        CrmContext db;
        User user;
        Customer customer;
        Cart cart;

        public UserControlCart(CrmContext db, User user)
        {
            InitializeComponent();
            this.user = user;
            this.db = db;
            db.Carts.Load();
            db.Users.Load();
            db.Customers.Load();

            customer = db.Customers.Where(c => c.User.Id == user.Id).FirstOrDefault();
            cart = db.Carts.Where(c => c.Customer.User.Id == user.Id).FirstOrDefault();
            FillDataGrid(this);
        }

        private void Pay(object sender, RoutedEventArgs e)
        {
            if (!cartGrid.Items.IsEmpty)
            {
                Check check = new Check() { Customer = customer, Sum = decimal.Parse(Itog.Text) };
                db.Checks.Add(check);
                db.SaveChanges();
                foreach (var item in cartGrid.Items)
                {
                    var product = ((ItemCart)item).Product;
                    Sell sell = new Sell() { Product = product, Check = check, Count = product.Count};
                    db.Sells.Add(sell);
                    db.SaveChanges();
                }
                cartGrid.ItemsSource = null;
                cart.Sum = 0;
                cart.Products.Clear();
                db.SaveChanges();
            }
        }

        public static void FillDataGrid(UserControlCart userControlCart)
        {
            decimal sum = 0;
            userControlCart.db.Carts.Load();
            List<ItemCart> itemCarts = new List<ItemCart>();
            foreach (var product in userControlCart.cart.Products)
            {
                itemCarts.Add(new ItemCart()
                {
                    Product = product.Key,
                    Price = product.Key.Price,
                    Count = product.Value,
                });
                sum += product.Key.Price * product.Value;
            }
            userControlCart.cart.Sum = sum;
            userControlCart.db.SaveChanges();
            userControlCart.cartGrid.ItemsSource = null;
            userControlCart.cartGrid.ItemsSource = itemCarts;
            userControlCart.Itog.Text = sum.ToString();
        }
    }

    internal struct ItemCart
    {
        public Product Product { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

    }
}
