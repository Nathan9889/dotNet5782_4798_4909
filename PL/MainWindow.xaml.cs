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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBL BL;
        object content;

        public MainWindow()
        {
            InitializeComponent();
            BL = BlApi.BlFactory.GetBL();
            
        }



        private void DroneListAccessButton(object sender, RoutedEventArgs e)
        {
            content = this.Content;
            DisplayDronesList page =  new DisplayDronesList(this);
            //page.closePage += DiplayMain;
            MainWindowDisplay.Visibility = Visibility.Hidden;
            this.Frame.Content = page;
        }

        public void DisplayMain()
        {
            Frame.Visibility = Visibility.Hidden;
            MainWindowDisplay.Visibility = Visibility.Visible;
            this.Content = content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDisplay.Visibility = Visibility.Hidden;
            Frame.Content = new DisplayPackagesList(this);
        }
    }
}
