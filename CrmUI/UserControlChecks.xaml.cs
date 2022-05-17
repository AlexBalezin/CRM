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
    /// Логика взаимодействия для UserControlChecks.xaml
    /// </summary>
    public partial class UserControlChecks : UserControl
    {
        CrmContext db;
        BindingList<Check> checks;

        public UserControlChecks(CrmContext db)
        {
            InitializeComponent();
            this.db = db;
            FillDataGridChecks(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public static void FillDataGridChecks(UserControlChecks userControlChecks)
        {
            userControlChecks.db.Checks.Load();
            userControlChecks.checks = userControlChecks.db.Checks.Local.ToBindingList();
            userControlChecks.checkGrid.ItemsSource = null;
            userControlChecks.checkGrid.ItemsSource = userControlChecks.checks;
        }

        public static void AddCheck(Check check)
        {

        }
    }
}
