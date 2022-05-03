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

        public MainWindow()
        {

            InitializeComponent();

            db = new CrmContext();

            var menuProduct = new List<SubItem>();
            var menuSeller = new List<SubItem>();
            var menuCustomer = new List<SubItem>();
            var menuReceipt = new List<SubItem>();

            menuProduct.Add(new SubItem("Список товаров", new UserControlProducts(db)));
            var itemProduct = new ItemMenu("Товар", menuProduct, PackIconKind.AlphaPBox);

            var itemSeller = new ItemMenu("Продавец", menuSeller, PackIconKind.About);
            var itemCustomer = new ItemMenu("Покупатель", menuCustomer, PackIconKind.About);
            var itemReceipt = new ItemMenu("Чек", menuReceipt, PackIconKind.About);

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
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }


    }
}
