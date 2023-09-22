using StockroomBinar.BD;
using StockroomBinar.Class;
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

namespace StockroomBinar.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeliveresPage.xaml
    /// </summary>
    public partial class DeliveresPage : Page
    {
        public DeliveresPage()
        {
            InitializeComponent();
            DeliversView.ItemsSource = Connect.bd.Deliveries.ToList();
            ReadyProcentSort.SelectedIndex = 0;
            DateSort.SelectedIndex = 0;
        }

        private void LockInfoNatif_Click(object sender, RoutedEventArgs e)
        {
            var a = DeliversView.SelectedItem as Deliveries;
            if (a != null)
            {
                MyFrame.Navigate(new DeliveresInfoPage(a));
            }
        }
    }
}
