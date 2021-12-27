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

namespace PL
{
    /// <summary>
    /// Interaction logic for ClientMde.xaml
    /// </summary>
    public partial class ClientMde : Page
    {
        BlApi.IBL bL = BlApi.BlFactory.GetBL();
        public ClientMde()
        {
            InitializeComponent();
            Client_Packages.ItemsSource = bL.DisplayClientList();
           
        }

        private void Client_Packages_SubmenuOpened(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
