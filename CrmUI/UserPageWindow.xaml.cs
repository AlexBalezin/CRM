using CrmBl;
using CrmBl.Model;
using CrmUI.ViewModal;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace CrmUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class UserPageWindow : Window
    {
        CrmContext db;
        ItemMenu itemProduct;
        ItemMenu itemSeller;
        ItemMenu itemCustomer;
        ItemMenu itemPurchase;
        Cart cart;

        public UserPageWindow(Cart cart)
        {

            InitializeComponent();

            db = new CrmContext();
            this.cart = cart;

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
                    UserControlCart.FillDataGrid(cart, (UserControlCart)screen);
                }

                if (screen is UserControlChecks)
                {
                    UserControlChecks.FillDataGridChecks((UserControlChecks)screen);
                }

                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
    }
}
