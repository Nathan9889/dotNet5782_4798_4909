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
            DisplayDronesList page = new DisplayDronesList(this);
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
            DisplayPackagesList page = new DisplayPackagesList();
            page.AddClik += AddPackagePage;
            page.DoubleClik += PackageDisplyPage;
            this.Frame.Content = page;
        }

        private void AddPackagePage(int num) // פתיחת עמוד הוספת חבילה 
        {
            var page = new DisplayPackage();
            page.Back += ((DisplayPackagesList)this.Frame.Content).RefreshList;
            this.Frame.Content = page;
        }
        private void AddClientPage(int num) // פתיחת חלון הוספת לקוח
        {
            var page = new DisplayClient();
            page.Back += ((DisplayClientsList)this.Frame.Content).RefreshList;
            this.Frame.Content = page;
        }

        private void PackageDisplyPage(int id)// פתיחת חלון פעולות חבילה
        {
            var page = new DisplayPackage(id);
            page.Back += ((DisplayPackagesList)this.Frame.Content).RefreshList;
            this.Frame.Content = page;
        }

        private void ClientDisplyPage(int id)// פתיחת חלון פעולות לקוח
        {
            var page = new DisplayClient(id);
            page.Back += ((DisplayClientsList)this.Frame.Content).RefreshList;
            this.Frame.Content = page;
        }

        private void AddStationPage(int num) // פתיחת חלון הוספת תחנה
        {
            var page = new DisplayStation();
            page.Back += ((DisplayStationsList)this.Frame.Content).RefreshList;
            this.Frame.Content = page;
        }

        private void StationDisplyPage(int id)// פתיחת חלון פעולות תחנה
        {
            var page = new DisplayStation(id);
            page.Back += ((DisplayStationsList)this.Frame.Content).RefreshList;
            this.Frame.Content = page;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindowDisplay.Visibility = Visibility.Hidden;
            DisplayClientsList page = new DisplayClientsList();
            page.AddClik += AddClientPage;
            page.DoubleClik += ClientDisplyPage;
            this.Frame.Content = page;
        }

        private void Stations_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDisplay.Visibility = Visibility.Hidden;
            DisplayStationsList page = new DisplayStationsList();
            page.AddClik += AddStationPage;
            page.DoubleClik += StationDisplyPage;
            this.Frame.Content = page;
        }
    }
}