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
using System.Windows.Shapes;

namespace CrmUI
{
    /// <summary>
    /// Логика взаимодействия для AddingToCartWindow.xaml
    /// </summary>
    public partial class AddingToCartWindow : Window
    {
        Product product;
        Cart cart;
        CrmContext db;
        UserControlProducts userControlProducts;
        BindingList<Product> products;

        public AddingToCartWindow(Product product, Cart cart, CrmContext db, UserControlProducts userControlProducts)
        {
            InitializeComponent();
            this.db = db;
            this.product = product;
            this.cart = cart;
            this.userControlProducts = userControlProducts;
            products = db.Products.Local.ToBindingList();

            NameProduct.Text = product.Name;
            Count.Text = "1";
            Price.Text = product.Price.ToString();
            Minus.IsEnabled = false;
            ValidateButton();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            cart.Add(product, int.Parse(Count.Text));
            var selectedItem = userControlProducts.productsGrid.SelectedItem;
            if (selectedItem != null)
            {
                var productCurrentId = ((Product)selectedItem).Id;
                var currentProduct = products.FirstOrDefault(x => x.Id == productCurrentId);
                currentProduct.Count -= int.Parse(Count.Text);
                db.SaveChanges();
                userControlProducts.productsGrid.ItemsSource = null;
                userControlProducts.productsGrid.ItemsSource = products;
            }

            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(Count.Text);
            decimal price = product.Price;

            --count;
            Count.Text = count.ToString();
            Price.Text = (price * count).ToString();
            ValidateButton();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(Count.Text);
            decimal price = product.Price;

            ++count;
            Count.Text = count.ToString();
            Price.Text = (price * count).ToString();
            ValidateButton();
        }

        private void ValidateButton()
        {
            int count = int.Parse(Count.Text);
            Minus.IsEnabled = count == 1 ? false : true;
            Plus.IsEnabled = count == product.Count ? false : true;
        }
    }
}
