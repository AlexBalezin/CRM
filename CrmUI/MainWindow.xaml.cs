using CrmBl;
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
using CrmUI.ViewModal;
using CrmBl.Model;
using System.Data.Entity;

namespace CrmUI
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
        ItemMenu itemPurchase;
        Cart cart;

        public MainWindow()
        {

            InitializeComponent();

            db = new CrmContext();
            cart = new Cart();

            var menuProduct = new List<SubItem>();
            var menuSeller = new List<SubItem>();
            var menuCustomer = new List<SubItem>();
            var menuPurchase = new List<SubItem>();

            menuProduct.Add(new SubItem("Список товаров", new UserControlProducts(db, cart)));
            itemProduct = new ItemMenu("Товар", menuProduct, PackIconKind.AlphaPBox);

            menuSeller.Add(new SubItem("Список продавцов", new UserControlSellers(db)));
            itemSeller = new ItemMenu("Продавец", menuSeller, PackIconKind.About);

            menuCustomer.Add(new SubItem("Список покупателей", new UserControlCustomers(db)));
            itemCustomer = new ItemMenu("Покупатель", menuCustomer, PackIconKind.About);

            menuPurchase.Add(new SubItem("Корзина", new UserControlCart(db, cart)));
            menuPurchase.Add(new SubItem("Чеки", new UserControlChecks(db)));
            itemPurchase = new ItemMenu("Покупка", menuPurchase, PackIconKind.About);

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

                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
    }
}
