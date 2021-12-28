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
       
        private Model.PL pL;

        Station Station = new Station();

        public delegate void Navigation(int id);
        public event Navigation Back;
        public event Navigation DronePage;
        


        public DisplayStation()
        {
            InitializeComponent();
            this.pL = new Model.PL();
            MainGrid.DataContext = Station;

            Station.station = new BO.Station();
            Station.station.StationLocation = new BO.Location();
            Mode.IsChecked = true;

        }


        public DisplayStation(int id)
        {
            this.pL = new Model.PL();
            Station.station = pL.GetStation(id);
            InitializeComponent();

            MainGrid.DataContext = Station;
           
            ChargingDroneList.ItemsSource = Station.station.ChargingDronesList;


        }

        private void Change_Station_Name_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.UpdateStationName(Station.station.ID, NameInput.Text);
                MessageBox.Show($"Name have been changed to {NameInput.Text} !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Station.station = pL.GetStation(Station.station.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Change_Slot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int result = Int32.Parse(Charge_slot_input.Text);
                pL.UpdateSlotNumber(Station.station.ID, result);
                MessageBox.Show($" Number of Charge slot have been updated !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Station.station = pL.GetStation(Station.station.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Back != null)
                Back(-1);
            this.NavigationService.GoBack();
        }




        private void Add_Station_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.AddStation(Station.station);
                if (Back != null) Back(-1);
                MessageBox.Show($"The station was successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to add station {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void ChargingDroneList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ChargingDrone ch = ChargingDroneList.SelectedItem as BO.ChargingDrone;
            if (DronePage != null && ch!=null && ch.ID != 0)
                DronePage(ch.ID);
        }





        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Back != null) Back(-1);
            this.NavigationService.GoBack();
        }

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
