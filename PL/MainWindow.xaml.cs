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
            content = this.Content;
            new Model.Model();
        }


        /// <summary>
        /// Opening page for adding a Package 
        /// </summary>
        /// <param name="num"></param>
        private void AddPackagePage(int num) 
        {
            var page = new DisplayPackage();
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening page for adding a Drone 
        /// </summary>
        /// <param name="num"></param>
        private void AddDronePage(int num)
        {
            var page = new DisplayDrone();
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening page for adding a Client
        /// </summary>
        /// <param name="num"></param>
        private void AddClientPage(int num) 
        {
            var page = new DisplayClient();
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening page for adding a Station
        /// </summary>
        /// <param name="num"></param>
        private void AddStationPage(int num) 
        {
            var page = new DisplayStation();
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening page for Updating a Package 
        /// </summary>
        /// <param name="id"></param>
        private void PackageDisplayPage(int id) 
        {
            if( Model.Model.drones.Any(d => d.DronePage != null && d.DronePage.backgroundWorker.IsBusy) )
                MessageBox.Show($"Sorry, could not open package view during simulator", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                var page = new DisplayPackage(id);
                page.ClientPage += ClientDisplayPageFromPackage;
                page.DronePage += DroneDisplayPageFromPackage;
                this.Frame.Content = page;
            }
        }

        /// <summary>
        /// Opening page for Updating a Drone 
        /// </summary>
        /// <param name="id"></param>
        private void DroneDisplayPage(int id) 
        {
            if (Model.Model.drones.First(d => d.ID == id).DronePage == null)
            {
                var droneDisplayPage = new DisplayDrone(id);
                droneDisplayPage.PackagePage += PackageDisplayFromDrone;
                this.Frame.Content = droneDisplayPage;
                Model.Model.drones.First(d => d.ID == id).DronePage = droneDisplayPage;
            }
            else this.Frame.Content = Model.Model.drones.First(d => d.ID == id).DronePage;
        }

        /// <summary>
        /// Opening page for Updating a Client 
        /// </summary>
        /// <param name="id"></param>
        private void ClientDisplayPage(int id) 
        {
            var page = new DisplayClient(id);
            page.PackagePage += PackageDisplayFromClient;
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening page for Updating a Station 
        /// </summary>
        /// <param name="id"></param>
        private void StationDisplayPage(int id) 
        {
            var page = new DisplayStation(id);
            page.DronePage += DroneDiplayFromStation;
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening a Client page from the package page
        /// </summary>
        /// <param name="id"></param>
        private void ClientDisplayPageFromPackage(int id)
        {
            var page = new DisplayClient(id);
            page.PackagePage += PackageDisplayFromClient;
            this.Frame.Content = page;
        }

        /// <summary>
        ///  Opening a Drone page from the package page
        /// </summary>
        /// <param name="id"></param>
        private void DroneDisplayPageFromPackage(int id)
        {
            var page = new DisplayDrone(id);
            page.PackagePage += PackageDisplayFromDrone;
            this.Frame.Content = page;
        }

        /// <summary>
        ///  Opening a Drone page from the Station page
        /// </summary>
        /// <param name="id"></param>
        private void DroneDiplayFromStation(int id)
        {
            var page = new DisplayDrone(id);
            
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening a Package page from the Client page
        /// </summary>
        /// <param name="id"></param>
        private void PackageDisplayFromClient(int id)
        {
            var page = new DisplayPackage(id);
            page.DronePage += DroneDisplayPageFromPackage;
            page.ClientPage += ClientDisplayPageFromPackage;
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening a Package page from the Drone page
        /// </summary>
        /// <param name="id"></param>
        private void PackageDisplayFromDrone(int id)
        {
            var page = new DisplayPackage(id);
            page.ClientPage += ClientDisplayPageFromPackage;
            page.DronePage += DroneDisplayPageFromPackage;
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening Client List page with double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientsListButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DisplayClientsList page = new DisplayClientsList();
                page.AddClik += AddClientPage;
                page.DoubleClik += ClientDisplayPage;
                this.Frame.Content = page;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Opening Package List page with double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackageListButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindowDisplay.Visibility = Visibility.Hidden;
                DisplayPackagesList page = new DisplayPackagesList();
                page.AddClik += AddPackagePage;
                page.DoubleClik += PackageDisplayPage;
                this.Frame.Content = page;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Opening Station List page with double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stations_Click(object sender, RoutedEventArgs e)
        {

            DisplayStationsList page = new DisplayStationsList();
            page.AddClik += AddStationPage;
            page.DoubleClik += StationDisplayPage;
            this.Frame.Content = page;
        }

        /// <summary>
        /// Opening Drone List page with double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drones_List_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindowDisplay.Visibility = Visibility.Hidden;
                DisplayDronesList page = new DisplayDronesList();
                page.AddClik += AddDronePage;
                page.DoubleClik += DroneDisplayPage;
                this.Frame.Content = page;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void Manager_login_Click(object sender, RoutedEventArgs e)
        {
            Buttons_For_Lists.Visibility = Visibility.Visible;
            MainWindowDisplay.Visibility = Visibility.Hidden;
            Frame.Visibility = Visibility.Visible;
        }

    }
}