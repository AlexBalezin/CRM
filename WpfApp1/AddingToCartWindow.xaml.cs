using CrmBl.Model;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddingToCartWindow.xaml
    /// </summary>
    public partial class AddingToCartWindow : Window
    {
        Product product;

        public AddingToCartWindow(Product product)
        {
            InitializeComponent();
            this.product = product;
            NameProduct.Text = product.Name;
            Count.Text = "1";
            Price.Text = product.Price.ToString();
            Minus.IsEnabled = false;
            ValidateButton();
        }

        private void Add(object sender, RoutedEventArgs e)
        {


        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(Count.Text);
            --count;
            Count.Text = count.ToString();
            ValidateButton();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(Count.Text);
            ++count;
            Count.Text = count.ToString();
            ValidateButton();
        }

        private void ValidateButton()
        {
            int count = int.Parse(Count.Text);
            Minus.IsEnabled = count == 1 ? false : true;
            Plus.IsEnabled = count == product.Count ? false : true;
        }
    }
}
