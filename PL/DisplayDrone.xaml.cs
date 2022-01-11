using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        private Model.PL pL;
        private BlApi.IBL bl;
        Drone Drone = new Drone();
        BackgroundWorker backgroundWorker;
        private Random random = new Random();

        public delegate void Navigation(int id);
        public event Navigation Back;
        public event Navigation PackagePage;

        /// <summary>
        /// ctor for creating new drone from user input in that page
        /// </summary>
        public DisplayDrone()
        {
            InitializeComponent();
            this.pL = new Model.PL();
            MainGrid.DataContext = Drone;
            bl = BlApi.BlFactory.GetBL();

            Drone_MaxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            Mode.IsChecked = true;

            Drone.drone = new BO.Drone();           //init object 
            Drone.drone.DronePackageProcess = new BO.PackageProcess();
            Drone.drone.DronePackageProcess.Sender = new BO.ClientPackage();
            Drone.drone.DronePackageProcess.Receiver = new BO.ClientPackage();
            Drone.drone.DronePackageProcess.CollectLocation = new BO.Location();
            Stations_List.ItemsSource = pL.DiplayStationWithChargSlot();
        }

        /// <summary>
        /// ctor to display the drone page of selected item
        /// </summary>
        /// <param name="id"></param>
        public DisplayDrone(int id)
        {
            bl = BlApi.BlFactory.GetBL();
            this.pL = new Model.PL();
            Drone.drone = bl.DisplayDrone(id);
            InitializeComponent();
            MainGrid.DataContext = Drone;
            bl = BlApi.BlFactory.GetBL();

            //if (Drone.drone.Status == BO.DroneStatus.Shipping)
            //    ShipVisibility.IsChecked = true;
            if (Drone.drone.DronePackageProcess == null) 
                Drone.drone.DronePackageProcess = new BO.PackageProcess();
            
            Drone_MaxWeight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));


            backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += Simulator_DoWork;

            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.RunWorkerCompleted += Simulator_RunWorkerCompleted;
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

                var d = Model.Model.drones.First(d => d.ID == Drone.drone.ID);d. Status = BO.DroneStatus.Maintenance; d.Battery = Drone.drone.Battery;
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

                Model.Model.drones.First(d => d.ID == Drone.drone.ID).Model = Drone.drone.Model;
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

                var d = Model.Model.drones.First(d => d.ID == Drone.drone.ID);d.Status = BO.DroneStatus.Available; d.Battery = Drone.drone.Battery;

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

                var d = Model.Model.drones.First(d => d.ID == Drone.drone.ID); d.Status = BO.DroneStatus.Shipping; d.PackageID = Drone.drone.DronePackageProcess.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// open package page of package delivery from the drone page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                BO.Drone drone = bl.DisplayDrone(Drone.drone.ID);
                Model.Model.drones.Add(new PO.DroneToList(){ ID = drone.ID, Battery= drone.Battery, DroneLocation = drone.DroneLocation, MaxWeight = drone.MaxWeight, Model = drone.Model, Status = drone.Status });

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

        /// <summary>
        /// going to previous page click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            if (backgroundWorker.IsBusy != true)
            {
                backgroundWorker.RunWorkerAsync(); // Start the asynchronous operation.
                simulator.IsChecked = true;
            }
        }

        private void Cancellation_Click(object sender, RoutedEventArgs e)
        {
            backgroundWorker.CancelAsync();
        }


        bool stop()
        {
            return backgroundWorker.CancellationPending;
        }

        void update(string update, int id)
        {
            Drone.drone = pL.GetDrone(Drone.drone.ID);

            var droneToList = Model.Model.drones.First(d => d.ID == Drone.drone.ID);
            droneToList.Battery = Drone.drone.Battery;
            droneToList.DroneLocation = Drone.drone.DroneLocation;
            droneToList.Status = Drone.drone.Status;
            if (Drone.drone.DronePackageProcess != null) droneToList.PackageID = Drone.drone.DronePackageProcess.Id;
            else droneToList.PackageID = 0;

            PO.PackageToList packageToList;
            PO.StationToList stationToList;

            switch (update)
            {
                case "No packages":
                    addPackages();
                    break;

                case "charging":
                    if(Model.Model.Station.station != null && Model.Model.Station.station.ID == id)
                            Model.Model.Station.station = bl.DisplayStation(id);
                    stationToList = Model.Model.stations.First(s => bl.GetStationWithDrones(s.ID).ChargingDronesList.Any(d => d.ID == Drone.drone.ID));
                    stationToList.AvailableChargingSlots--; stationToList.BusyChargingSlots++;
                    break;

                case "Finish charging":
                    if (Model.Model.Station.station != null && Model.Model.Station.station.ID == id)
                        Model.Model.Station.station = bl.DisplayStation(id);
                    stationToList = Model.Model.stations.First(s => s.ID == id);
                    stationToList.AvailableChargingSlots++; stationToList.BusyChargingSlots--;
                    break;

                case "Associate":
                    if (Model.Model.Package.package != null && Model.Model.Package.package.ID == id)
                        Model.Model.Package.package = bl.DisplayPackage(id);
                    packageToList = Model.Model.packages.First(p => p.Id == Drone.drone.DronePackageProcess.Id);
                    packageToList.Status = BO.PackageStatus.Associated;
                    break;

                case "PickedUp":
                    if (Model.Model.Package.package != null && Model.Model.Package.package.ID == id)
                        Model.Model.Package.package = bl.DisplayPackage(id);
                    packageToList = Model.Model.packages.First(p => p.Id == Drone.drone.DronePackageProcess.Id);
                    packageToList.Status = BO.PackageStatus.PickedUp;
                    break;

                case "Delivered":
                    if (Model.Model.Package.package != null && Model.Model.Package.package.ID == id)
                        Model.Model.Package.package = bl.DisplayPackage(id);
                    BO.Package package = bl.DisplayPackage(id);

                    if (Model.Model.Client.client != null && Model.Model.Client.client.ID == package.SenderClient.ID)
                        Model.Model.Client.client = bl.DisplayClient(package.SenderClient.ID);
                    else if(Model.Model.Client.client != null && Model.Model.Client.client.ID == package.TargetClient.ID)
                        Model.Model.Client.client = bl.DisplayClient(package.TargetClient.ID);

                    packageToList = Model.Model.packages.First(p => p.Id == id);
                    packageToList.Status = BO.PackageStatus.Delivered;
                    break;

                default:
                    break;
            }
            
        }

        private void Simulator_DoWork(object sender, DoWorkEventArgs e)
        {
            bl.StartSimulator(Drone.drone.ID, update, stop);
        }

        private void Simulator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            simulator.IsChecked = false;
        }


        void addPackages()
        {
            IEnumerable<BO.ClientToList> clients = bl.DisplayClientList();

            for (int i = 0; i < random.Next(4, 8); i++)
            {
               BO.Package package =  new BO.Package()
                {
                    SenderClient = new BO.ClientPackage() { ID = clients.ElementAt(random.Next(0, clients.Count())).Id },
                    TargetClient = new BO.ClientPackage() { ID = clients.ElementAt(random.Next(0, clients.Count())).Id },
                    Priority = (BO.Priorities)random.Next(0, 3),
                    Weight = (BO.WeightCategories)random.Next(0, 3)
                };

                int id = bl.AddPackage(package);
                BO.PackageToList packageToList = bl.GetPackageToList(id);
                PO.PackageToList poPackageTo = (PO.PackageToList)packageToList.CopyPropertiesToNew(typeof(PO.PackageToList));
                Model.Model.packages.Add(poPackageTo);
            }
        }

    }
}

