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
        Cart cart;

        public UserControlCart(CrmContext db, Cart cart)
        {
            InitializeComponent();
            this.cart = cart;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public static void FillDataGrid(UserControlCart userControlCart)
        {
            decimal sum = 0;
            List<ItemCart> itemCarts = new List<ItemCart>();
            foreach (var product in userControlCart.cart.Products)
            {
                itemCarts.Add(new ItemCart()
                {
                    ProductName = product.Key.Name,
                    Price = product.Key.Price,
                    Count = product.Value,
                });
                sum += product.Key.Price * product.Value;
            }
            userControlCart.cartGrid.ItemsSource = null;
            userControlCart.cartGrid.ItemsSource = itemCarts;
            userControlCart.Itog.Text = sum.ToString();
        }
    }

    internal struct ItemCart
    {
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

    }
}
