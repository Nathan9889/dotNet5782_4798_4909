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
using Model;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayDrone.xaml
    /// </summary>
    public partial class DisplayDrone : Page
    {
        //BlApi.IBL BL;
        //BO.Drone Drone;


        private Model.PL pL;
        Drone Drone = new Drone();


        public delegate void Navigation(int id);
        public event Navigation Back;
        public event Navigation PackagePage;


        public DisplayDrone()
        {
            InitializeComponent();
            this.pL = new Model.PL();
            MainGrid.DataContext = Drone;
           
            Drone_MaxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            //ClientsList.ItemsSource = pL.getClientList();
            //ClientsList2.ItemsSource = pL.getClientList();

            Mode.IsChecked = true;


            Drone.drone = new BO.Drone();
            Drone.drone.DronePackageProcess = new BO.PackageProcess();
            Drone.drone.DronePackageProcess.Sender = new BO.ClientPackage();
            Drone.drone.DronePackageProcess.Receiver = new BO.ClientPackage();
            Drone.drone.DronePackageProcess.CollectLocation = new BO.Location();
            Stations_List.ItemsSource = pL.DiplayStationWithChargSlot();
        }

        public DisplayDrone(int id)
        {
            this.pL = new Model.PL();
            Drone.drone = pL.GetDrone(id);
            InitializeComponent();

            if (Drone.drone.Status == BO.DroneStatus.Shipping)
                ShipVisibility.IsChecked = true;

            if (Drone.drone.DronePackageProcess == null) Drone.drone.DronePackageProcess = new BO.PackageProcess();
            MainGrid.DataContext = Drone;
           
            Drone_MaxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            

        }



         /// <summary>
        /// Sending a drone for charging and displaying an appropriate message and refreshing the display as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChargeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.ChargeDrone(Drone.drone.ID);
                if (Back != null) Back(-1);
                MessageBox.Show("Drone sent to charge", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Drone.drone = pL.GetDrone(Drone.drone.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// Renaming the drone. Checking the correctness of the name, and sending it to BL and displaying an appropriate message and refreshing the display as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Change_Name_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush red = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE92617"));
            if (SolidColorBrush.Equals(((SolidColorBrush)DroneModel.BorderBrush).Color, red.Color))
            {
                MessageBox.Show("Please enter correct Name", "Error input", MessageBoxButton.OK, MessageBoxImage.Error);
                DroneModel.Text = Drone.drone.Model;
            }
            else
            {
                try
                {
                    pL.UpdateDroneName(Drone.drone.ID, DroneModel.Text);
                    if (Back != null) Back(-1);
                    MessageBox.Show("Name have been changed successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Drone.drone = pL.GetDrone(Drone.drone.ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        /// <summary>
        /// Releasing a drone from charging, displaying an appropriate message and refreshing the display as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.FinishCharging(Drone.drone.ID);
                if (Back != null) Back(-1);
                MessageBox.Show($"Drone have been unplugged, Battery left: {Drone.drone.Battery}%", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Drone.drone = pL.GetDrone(Drone.drone.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        /// <summary>
        /// Assigning a drone to a package, displaying an appropriate message and refreshing the display as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.packageToDrone(Drone.drone.ID);
                if (Back != null) Back(-1);
                MessageBox.Show("Package have been Associated to drone successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Drone.drone = pL.GetDrone(Drone.drone.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Back != null) Back(-1);
            this.NavigationService.GoBack();
        }




        /// <summary>
        /// Cancel insert and close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Back != null) Back(-1);
            this.NavigationService.GoBack();
        }


        private void Package_Process_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id =  Drone.drone.DronePackageProcess.Id;
            if (PackagePage != null && id != 0)
                PackagePage(id);
        }

        /// <summary>
        /// Closing a Drone window and activating the event 'CloseWindowEvent', for which a refresh function of the drone list is registered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            if (Back != null) Back(-1);
            this.NavigationService.GoBack();
            // Close();
        }




        /// <summary>
        /// Adding a drone.
        ///Check that the inputs are correct and add.
        ///And display an appropriate message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Drone_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.AddDrone(Drone.drone);
                if (Back != null) Back(-1);
                MessageBox.Show($"The Drone was successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to add drone {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        /// <summary>
        /// Select a station for adding a drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stations_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stations_List.SelectedItem = Stations_List.SelectedItem.ToString().ElementAt(5);
            StationID.Text = ((BO.StationToList)Stations_List.SelectedItem).ID.ToString();
        }

        /// <summary>
        ///  Change the frame color if the StationID input is incorrect
        ///  And adding the station location if the ID is correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationID_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            if (StationID.Text != null && StationID.Text != string.Empty && (StationID.Text).All(char.IsDigit))
            {
                StationID.BorderBrush = (Brush)bc.ConvertFrom("#FF99B4D1");
            }
            else StationID.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");

            try
            {
                int id = int.Parse(StationID.Text.ToString());
                Location.Text = $"{pL.GetStation(id).StationLocation}";
            }
            catch (Exception)
            {
                Location.Text = null;
            }
        }

        
        
    }
}

