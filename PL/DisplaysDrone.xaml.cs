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

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplaysDrone.xaml
    /// </summary>
    public partial class DisplaysDrone : Window
    {
        IBL.IBL BL;
        public DisplaysDrone(IBL.IBL bL)
        {
            InitializeComponent();
            this.BL = bL;
            Add_New_Drone.Visibility = Visibility.Visible;
            Drone_Weight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            Stations_List.ItemsSource = BL.DisplayStationListFilter(s => s.AvailableChargingSlots > 0);
            Add_New_Drone.Visibility = Visibility.Visible;


        }

        public delegate void RefreshList(object ob);
        public event RefreshList RefreshListEvent;

        private void idInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            if (IdInput.Text != null && IdInput.Text != string.Empty && (IdInput.Text).All(char.IsDigit))
            {
                IdInput.BorderBrush = (Brush)bc.ConvertFrom("#FF99B4D1");
            }
            else IdInput.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

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

        private void StationID_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();
            if (StationID.Text != null && StationID.Text != string.Empty && (StationID.Text).All(char.IsDigit))
            {
                StationID.BorderBrush = (Brush)bc.ConvertFrom("#FF99B4D1");
            }
            else StationID.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617");
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow.Close();
        }

        private void Add_Drone_Button_Click(object sender, RoutedEventArgs e)
        { 
            SolidColorBrush red = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE92617"));
            if (SolidColorBrush.Equals(((SolidColorBrush)StationID.BorderBrush).Color , red.Color) || SolidColorBrush.Equals(((SolidColorBrush)DroneModel.BorderBrush).Color , red.Color)
                || SolidColorBrush.Equals(((SolidColorBrush)IdInput.BorderBrush).Color , red.Color) || Drone_Weight.SelectedItem == null)
            {
                MessageBox.Show("Please enter correct input","Error input" ,MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                IBL.BO.Drone drone = new IBL.BO.Drone();
                drone.ID = int.Parse(IdInput.Text);
                drone.Model = DroneModel.Text;
                drone.MaxWeight = (IBL.BO.WeightCategories)Drone_Weight.SelectedItem;
                try
                {
                    BL.AddDrone(drone, int.Parse(StationID.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                MessageBox.Show("The addition was successful", "Added a drone", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshListEvent(this);
                DroneWindow.Close();
            }
        }

        private void Drone_Weight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bc = new BrushConverter();
            Drone_Weight.BorderBrush = (Brush)bc.ConvertFrom("#FFE92617"); ;
           
                
        }

        

        private void Stations_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stations_List.SelectedItem = Stations_List.SelectedItem.ToString().ElementAt(5);
            StationID.Text = ((IBL.BO.StationToList) Stations_List.SelectedItem).ID.ToString();
        }



       
    }
}


