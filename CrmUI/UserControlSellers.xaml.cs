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
    /// Логика взаимодействия для UserControlSellers.xaml
    /// </summary>
    public partial class UserControlSellers : UserControl
    {
        CrmContext db;
        BindingList<Seller> sellers;

        public UserControlSellers(CrmContext db)
        {
            InitializeComponent();
            this.db = db;
            db.Sellers.Load();
            sellers = db.Sellers.Local.ToBindingList();
            sellersGrid.ItemsSource = sellers;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void RemoveElement(object sender, RoutedEventArgs e)
        {
            var selectedItem = sellersGrid.SelectedItem;
            if (selectedItem != null)
            {
                var sellerCurrentId = ((Seller)selectedItem).Id;
                var currentSeller = sellers.FirstOrDefault(x => x.Id == sellerCurrentId);
                sellers.Remove(currentSeller);
                db.SaveChanges();
            }
        }
    }
}
