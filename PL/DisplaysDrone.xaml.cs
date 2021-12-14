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
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplaysDrone.xaml
    /// </summary>
    public partial class DisplaysDrone : Window
    {
        BlApi.IBL BL;
        BO.Drone selectedDrone;


        /// <summary>
        /// Set up a window closing event to refresh a drone list
        /// </summary>
        /// <param name="ob"></param>
    

        
        //***
        //used for Deleting the X button
        private const int GWL_STYLE = -16;    
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        //***

        /// <summary>
        ///Constructor for adding a drone window
        /// </summary>
        /// <param name="bL"></param>
        public DisplaysDrone()
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();

            InitializeAddDrone();
        }

        /// <summary>
        /// Constructor for updating existing drone
        /// </summary>
        /// <param name="bL"></param>
        /// <param name="drone"></param>
        /// <param name="droneListWindow"></param>
        public DisplaysDrone(BO.DroneToList drone, DisplaysDronesList droneListWindow)
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();

            InitializeDisplayDrone(drone.ID);
           
        }

        /// <summary>
        /// Initialize for adding a drone window
        /// </summary>
        void InitializeAddDrone()
        {
            Add_New_Drone.Visibility = Visibility.Visible;
            Drone_Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            Stations_List.ItemsSource = BL.DisplayStationListWitAvailableChargingSlots();
            Add_New_Drone.Visibility = Visibility.Visible;

            var bc = new BrushConverter();
            IdInput.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");
            IdInput.Background = (Brush)bc.ConvertFrom("#FFFFFFE1");

            DroneModel.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");
            DroneModel.Background = (Brush)bc.ConvertFrom("#FFFFFFE1");

            StationID.Visibility = Visibility.Visible;
            Station_Id_Labl.Visibility = Visibility.Visible;
            StationID.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");
            StationID.Background = (Brush)bc.ConvertFrom("#FFFFFFE1");
            Drone_Weight.Style = (Style)this.FindResource("ComboBoxTest2");

            Stations_Labl.Visibility = Visibility.Visible;
            Stations_List.Visibility = Visibility.Visible;

            Add_Drone_Button.Visibility = Visibility.Visible;
            cancel.Visibility = Visibility.Visible;

            Buttons.Visibility = Visibility.Hidden;   //hiding update window buttons
            Shipping_Label.Visibility = Visibility.Hidden;
            Package_Process.Visibility = Visibility.Hidden;
            Battery.Visibility = Visibility.Hidden;
            Battary_Label.Visibility = Visibility.Hidden;

            StatusSelector.Text = BO.DroneStatus.Maintenance.ToString();
        }


        /// <summary>
        /// Initialize for drone updates window
        /// </summary>
        /// <param name="DroneId"></param>
        void InitializeDisplayDrone(int DroneId)
        {
            selectedDrone = BL.DisplayDrone(DroneId);

            var bc = new BrushConverter();
            DroneModel.Background = (Brush)bc.ConvertFrom("#FFFFFFE1");

            IdInput.Text = $"{selectedDrone.ID}";
            IdInput.IsReadOnly = true;
            Battery.Text = $"{selectedDrone.Battery}";

            Drone_Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            Drone_Weight.SelectedItem = selectedDrone.MaxWeight;
            Drone_Weight.IsReadOnly = true;
            Drone_Weight.IsEnabled = false;
            Drone_Weight.Style = (Style)this.FindResource("ComboBoxTestInDisplayDrone");

            DroneModel.Text = selectedDrone.Model;

            StatusSelector.Text = selectedDrone.Status.ToString();

            Location.Text = $"{selectedDrone.DroneLocation}";


            if (selectedDrone.DronePackageProcess == null)
            {
                Package_Process.Visibility = Visibility.Hidden;
                Shipping_Label.Visibility = Visibility.Hidden;
            }
            else
            {
                Package_Process.Text = $"{selectedDrone.DronePackageProcess}";
            }

            //Only the buttons that can perform an action will be available for pressing
            switch (selectedDrone.Status)
            {
                case BO.DroneStatus.Available:
                    ChargeButton.IsEnabled = true;
                    AssociateButton.IsEnabled = true;
                    ReleaseButton.IsEnabled = false;
                    PickUpButton.IsEnabled = false;
                    DeliverButton.IsEnabled = false;
                    break;

                case BO.DroneStatus.Maintenance:
                    ReleaseButton.IsEnabled = true;
                    ChargeButton.IsEnabled = false;
                    AssociateButton.IsEnabled = false;
                    PickUpButton.IsEnabled = false;
                    DeliverButton.IsEnabled = false;
                    break;

                case BO.DroneStatus.Shipping:
                    switch (selectedDrone.DronePackageProcess.PackageShipmentStatus)
                    {
                        case BO.ShipmentStatus.Waiting:
                            PickUpButton.IsEnabled = true;
                            ReleaseButton.IsEnabled = false;
                            ChargeButton.IsEnabled = false;
                            AssociateButton.IsEnabled = false;
                            DeliverButton.IsEnabled = false;
                            break;

                        case BO.ShipmentStatus.OnGoing:
                            DeliverButton.IsEnabled = true;
                            ReleaseButton.IsEnabled = false;
                            ChargeButton.IsEnabled = false;
                            AssociateButton.IsEnabled = false;
                            PickUpButton.IsEnabled = false;
                            break;

                    }
                    break;
            }
        }

        /// <summary>
        /// Change the frame color if the ID input is incorrect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void idInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            if (IdInput.Text != null && IdInput.Text != string.Empty && (IdInput.Text).All(char.IsDigit))
            {
                IdInput.BorderBrush = (Brush)bc.ConvertFrom("#FF99B4D1");
            }
            else IdInput.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");

        }

        /// <summary>
        /// Change the frame color if the DroneModel input is incorrect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            string text = DroneModel.Text;
            if (text != null && text != "" && char.IsLetter(text.ElementAt(0)))
            {
                DroneModel.BorderBrush = (Brush)bc.ConvertFrom("#FF99B4D1");
            }
            else DroneModel.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");
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
                Location.Text = $"{BL.DisplayStation(id).StationLocation}";
            }
            catch (Exception)
            {
                Location.Text = null;
            }
        }

        /// <summary>
        /// Cancel insert and close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow.Close();
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
            SolidColorBrush red = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE92617"));
            if (SolidColorBrush.Equals(((SolidColorBrush)StationID.BorderBrush).Color , red.Color) || SolidColorBrush.Equals(((SolidColorBrush)DroneModel.BorderBrush).Color , red.Color)
                || SolidColorBrush.Equals(((SolidColorBrush)IdInput.BorderBrush).Color , red.Color) || Drone_Weight.Style == (Style)this.FindResource("ComboBoxTest2"))
            {
                MessageBox.Show("Please enter correct input","Error input" ,MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                BO.Drone drone = new BO.Drone();
                drone.ID = int.Parse(IdInput.Text);
                drone.Model = DroneModel.Text;
                drone.MaxWeight = (BO.WeightCategories)Drone_Weight.SelectedItem;
                try
                {
                    BL.AddDrone(drone, int.Parse(StationID.Text));

                    MessageBox.Show("Drone have been Added Successfully !", "Drone Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    DroneWindow.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Change the frame color if the  Drone_Weight input is incorrect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drone_Weight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(Drone_Weight.SelectedItem != null )
            {
                Drone_Weight.Style = (Style)this.FindResource("ComboBoxTestAfterCorrectInput");
            }
            else Drone_Weight.Style = (Style)this.FindResource("ComboBoxTest2");
        }


        /// <summary>
        /// Select a station for adding a drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stations_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stations_List.SelectedItem = Stations_List.SelectedItem.ToString().ElementAt(5);
            StationID.Text = ((BO.StationToList) Stations_List.SelectedItem).ID.ToString();
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
                BL.ChargeDrone(selectedDrone.ID);

                MessageBox.Show("Drone sent to charge", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                InitializeDisplayDrone(selectedDrone.ID);
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
                DroneModel.Text = selectedDrone.Model;
            }
            else
            {
                try
                {
                    BL.UpdateDroneName(selectedDrone.ID, DroneModel.Text);
                    MessageBox.Show($"Name have been changed to {DroneModel.Text} !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeDisplayDrone(selectedDrone.ID);
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
                BL.FinishCharging(selectedDrone.ID);
                InitializeDisplayDrone(selectedDrone.ID);
                MessageBox.Show($"Drone have been unplugged, Battery left: {selectedDrone.Battery}%", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                BL.packageToDrone(selectedDrone.ID);

                MessageBox.Show("Package have been Associated to drone successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                InitializeDisplayDrone(selectedDrone.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Picking up a package by a drone, displaying an appropriate message, and refreshing the display as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PickUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.PickedUpByDrone(selectedDrone.ID);

                MessageBox.Show("Package have been picked up successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                InitializeDisplayDrone(selectedDrone.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Delivery of a package to the customer by a drone, displaying an appropriate message, and refreshing the display as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeliverButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.DeliveredToClient(selectedDrone.ID);

                MessageBox.Show("Package have been delivered successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                InitializeDisplayDrone(selectedDrone.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Closing a Drone window and activating the event 'CloseWindowEvent', for which a refresh function of the drone list is registered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}


