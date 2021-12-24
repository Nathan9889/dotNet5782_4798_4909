using System;
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
    /// Logique d'interaction pour DisplayStation.xaml
    /// </summary>
    public partial class DisplayStation : Page
    {
        private MainWindow mainWindow;
        private Model.PL pL;
        Station Station;


        //public DisplayStation(MainWindow mainWindow)
        //{
        //    InitializeComponent();
        //    this.BL = BlApi.BlFactory.GetBL();
        //    this.MainWindow = mainWindow;
        //    InitializeAddStation();
        //}

        public DisplayStation(MainWindow mainWindow, Model.PL pL)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.pL = pL;
        }


        public DisplayStation(MainWindow mainWindow, Model.PL pL, Model.Station station)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.pL = pL;
            this.Station = station;
            MainGrid.DataContext = station;
            //Package_Priority.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            //Package_Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

        //public DisplayStation(BO.StationToList station, MainWindow mainWindow)
        //{
        //    InitializeComponent();
        //    this.BL = BlApi.BlFactory.GetBL();
        //    this.MainWindow = mainWindow;
        //    InitializeDisplayStation(station.ID);
        //}

        /// <summary>
        /// Initialize for adding a drone window
        /// </summary>
        //void InitializeAddStation()
        //{
        //    Add_New_Station.Visibility = Visibility.Visible;
        //    //Charging_Drone_List.ItemsSource = BL.DisplayChargingDroneList();
        //    Add_New_Station.Visibility = Visibility.Visible;

        //    var bc = new BrushConverter();
        //    IdInput.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");
        //    IdInput.Background = (Brush)bc.ConvertFrom("#FFFFFFE1");

        //    NameInput.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");
        //    NameInput.Background = (Brush)bc.ConvertFrom("#FFFFFFE1");


        //    ChargingDrone_Labl.Visibility = Visibility.Visible;
        //    ChargingDrone_Labl.Visibility = Visibility.Visible;

        //    Add_Station_Button.Visibility = Visibility.Visible;
        //    cancel.Visibility = Visibility.Visible;

        //    Buttons.Visibility = Visibility.Hidden;   //hiding update window buttons
        //    //Shipping_Label.Visibility = Visibility.Hidden;
        //    //Package_Process.Visibility = Visibility.Hidden;
        //    //Battery.Visibility = Visibility.Hidden;
        //    //Battary_Label.Visibility = Visibility.Hidden;

        //    //StatusSelector.Text = BO.DroneStatus.Maintenance.ToString();
        //}



        /// <summary>
        /// Initialize for drone updates window
        /// </summary>
        /// <param name="DroneId"></param>
        //void InitializeDisplayStation(int StationId)
        //{
        //    selectedStation = BL.DisplayStation(StationId);

        //    var bc = new BrushConverter();
        //    NameInput.Background = (Brush)bc.ConvertFrom("#FFFFFFE1");

        //    IdInput.Text = $"{selectedStation.ID}";
        //    IdInput.IsReadOnly = true;
        //    Charge_slot_input.Text = $"{selectedStation.AvailableChargeSlots}";
        //    StationLocation.Text = $"{selectedStation.StationLocation}";
        //    NameInput.Text = selectedStation.Name;
        //    StationLocation.IsReadOnly = true;


        ////Only the buttons that can perform an action will be available for pressing
        //switch (selectedDrone.Status)
        //{
        //    case BO.DroneStatus.Available:
        //        ChargeButton.IsEnabled = true;
        //        AssociateButton.IsEnabled = true;
        //        ReleaseButton.IsEnabled = false;
        //        PickUpButton.IsEnabled = false;
        //        DeliverButton.IsEnabled = false;
        //        break;

        //    case BO.DroneStatus.Maintenance:
        //        ReleaseButton.IsEnabled = true;
        //        ChargeButton.IsEnabled = false;
        //        AssociateButton.IsEnabled = false;
        //        PickUpButton.IsEnabled = false;
        //        DeliverButton.IsEnabled = false;
        //        break;

        //    case BO.DroneStatus.Shipping:
        //        switch (selectedDrone.DronePackageProcess.PackageShipmentStatus)
        //        {
        //            case BO.ShipmentStatus.Waiting:
        //                PickUpButton.IsEnabled = true;
        //                ReleaseButton.IsEnabled = false;
        //                ChargeButton.IsEnabled = false;
        //                AssociateButton.IsEnabled = false;
        //                DeliverButton.IsEnabled = false;
        //                break;

        //            case BO.ShipmentStatus.OnGoing:
        //                DeliverButton.IsEnabled = true;
        //                ReleaseButton.IsEnabled = false;
        //                ChargeButton.IsEnabled = false;
        //                AssociateButton.IsEnabled = false;
        //                PickUpButton.IsEnabled = false;
        //                break;

        //        }
        //        break;
        //}


        //}


        /// <summary>
        /// Renaming the drone. Checking the correctness of the name, and sending it to BL and displaying an appropriate message and refreshing the display as needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Change_Station_Name_Click(object sender, RoutedEventArgs e)
        //{

        //    try
        //    {
        //        BL.UpdateStationName(selectedStation.ID, NameInput.Text);
        //        MessageBox.Show($"Name have been changed to {NameInput.Text} !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //        InitializeDisplayStation(selectedStation.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //}

        //private void Change_Slot_Click(object sender, RoutedEventArgs e)
        //{


        //    try
        //    {
        //        int result = Int32.Parse(Charge_slot_input.Text);
        //        BL.UpdateStationNumCharge(selectedStation.ID, result);
        //        MessageBox.Show($" Num of Chargeslot have been updated !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //        InitializeDisplayStation(selectedStation.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow.Content = new DisplayStationsList(MainWindow);
        //}




        /** add in main
        private void StationList_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDisplay.Visibility = Visibility.Hidden;
            Frame.Content = new DisplayStationsList(this);

        }

        private void ClientList_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDisplay.Visibility = Visibility.Hidden;
            Frame.Content = new DisplayClientsList(this);
        }
        */


    }
}
