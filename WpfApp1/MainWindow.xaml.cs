﻿using CrmBl;
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
using WpfApp1.ViewModal;
using CrmBl.Model;
using System.Data.Entity;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CrmContext db;
        ItemMenu itemProduct;
        ItemMenu itemSeller;
        ItemMenu itemCustomer;
        ItemMenu itemReceipt;

        public MainWindow()
        {

            InitializeComponent();

            db = new CrmContext();

            var menuProduct = new List<SubItem>();
            var menuSeller = new List<SubItem>();
            var menuCustomer = new List<SubItem>();
            var menuReceipt = new List<SubItem>();

            menuProduct.Add(new SubItem("Список товаров", new UserControlProducts(db)));
            itemProduct = new ItemMenu("Товар", menuProduct, PackIconKind.AlphaPBox);

            menuSeller.Add(new SubItem("Список продавцов", new UserControlSellers(db)));
            itemSeller = new ItemMenu("Продавец", menuSeller, PackIconKind.About);

            menuCustomer.Add(new SubItem("Список покупателей", new UserControlCustomers(db)));
            itemCustomer = new ItemMenu("Покупатель", menuCustomer, PackIconKind.About);

            itemReceipt = new ItemMenu("Чек", menuReceipt, PackIconKind.About);

            Menu.Children.Add(new UserControlMenuItem(itemProduct, this, db));
            Menu.Children.Add(new UserControlMenuItem(itemSeller, this, db));
            Menu.Children.Add(new UserControlMenuItem(itemCustomer, this, db));
            Menu.Children.Add(new UserControlMenuItem(itemReceipt, this, db));

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

                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);


            }
        }
    }
}
