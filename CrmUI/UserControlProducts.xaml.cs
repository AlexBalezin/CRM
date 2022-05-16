using Caliburn.Micro;
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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data.Entity;
using CrmBl;
using CrmBl.Model;

namespace CrmUI
{
    /// <summary>
    /// Логика взаимодействия для UserControlProducts.xaml
    /// </summary>
    public partial class UserControlProducts : UserControl
    {
        CrmContext db;
        BindingList<Product> products;
        Cart cart;

        public UserControlProducts(CrmContext db, Cart cart)
        {
            InitializeComponent();
            this.db = db;
            this.cart = cart;
            db.Products.Load();
            db.Sellers.Load();
            products = db.Products.Local.ToBindingList();
            productsGrid.ItemsSource = products; 
            (productsGrid.Columns[3] as DataGridComboBoxColumn).ItemsSource = db.Sellers.Local.ToBindingList().Where(x => x.Name != null);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void RemoveElement(object sender, RoutedEventArgs e)
        {
            var selectedItem = productsGrid.SelectedItem;
            if (selectedItem != null)
            {
                var productCurrentId = ((Product)selectedItem).Id;
                var currentProduct = products.FirstOrDefault(x => x.Id == productCurrentId);
                products.Remove(currentProduct);
                db.SaveChanges();
            }
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            var selectedItem = productsGrid.SelectedItem;
            if (selectedItem != null)
            {
                AddingToCartWindow toCartWindow = new AddingToCartWindow((Product)selectedItem, cart, db, this);
                toCartWindow.ShowDialog();
            }
        }
    }
}
