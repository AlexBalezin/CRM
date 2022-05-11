using CrmBl;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModal;

namespace WpfApp1
{


    /// <summary>
    /// Логика взаимодействия UserControlMenuItem.xaml
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        MainWindow _context;
        CrmContext _db;

        public UserControlMenuItem(ItemMenu itemMenu, MainWindow context, CrmContext db)
        {
            InitializeComponent();

            _context = context;
            _db = db;
            
            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        private void ListViewMenu_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var screen = ((SubItem)((ListView)sender).SelectedItem).Screen;
            _context.SwitchScreen(screen);
        }
    }
}
