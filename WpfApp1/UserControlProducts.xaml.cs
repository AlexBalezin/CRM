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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UserControlProducts.xaml
    /// </summary>
    public partial class UserControlProducts : UserControl
    {
        CrmContext db;
        BindingList<Product> products;

        public UserControlProducts(CrmContext db)
        {
            InitializeComponent();
            this.db = db;
            products = db.Products.Local.ToBindingList();
            db.Products.Load();
            productsGrid.ItemsSource = products;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
    }
}
