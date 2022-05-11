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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UserControlCustomers.xaml
    /// </summary>
    public partial class UserControlCustomers : UserControl
    {
        CrmContext db;
        BindingList<Customer> customers;

        public UserControlCustomers(CrmContext db)
        {
            InitializeComponent();
            this.db = db;
            db.Customers.Load();
            customers = db.Customers.Local.ToBindingList();
            cutomersGrid.ItemsSource = customers;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void RemoveElement(object sender, RoutedEventArgs e)
        {
            var selectedItem = cutomersGrid.SelectedItem;
            if (selectedItem != null)
            {
                var customerCurrentId = ((Customer)selectedItem).Id;
                var currentCustomer = customers.FirstOrDefault(x => x.Id == customerCurrentId);
                customers.Remove(currentCustomer);
                db.SaveChanges();
            }
        }
    }
}
