using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DisplayStationsList.xaml
    /// </summary>
    public partial class DisplayStationsList : Page
    {
        BlApi.IBL BL;
        Model.PL PL;
        MainWindow MainWindow;
        private ObservableCollection<BO.StationToList> stations = new ObservableCollection<BO.StationToList>();


        

        public DisplayStationsList(MainWindow mainWindow)
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();
            this.PL = new Model.PL();
            this.MainWindow = mainWindow;
            StationsListView.ItemsSource = BL.DisplayStationList();
            StationsListView.DataContext = stations;
            InitializeList();
        }


        void InitializeList()
        {

            foreach (var station in PL.GetStationList())
            {
                stations.Add(station);
            }
        }

        private void StationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StationsListView.SelectedItem != null) 
                MainWindow.Frame.Content = new DisplayStation(MainWindow, PL, PL.GetStation(((BO.StationToList)StationsListView.SelectedItem).ID));
            StationsListView.SelectedItems.Clear();
        }

        private void Add_New_Station(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = new DisplayStation(MainWindow,PL);
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            MainWindow.DisplayMain();
        }


        private void Show_Stations(object sender, RoutedEventArgs e)
        {
            if (StationsListView != null)
            {
                if (Show_Normally.IsChecked == true)
                {
                    var s = BL.DisplayStationList();
                    stations.Clear();
                    foreach (var item in s) { stations.Add(item); }
                }
                else if (Show_Existing.IsChecked == true)
                {
                    var s = BL.GroupStationByExistingSlots();
                    stations.Clear();
                    foreach (var group in s)
                    {
                        foreach (BO.StationToList item in group)
                        { stations.Add(item); }
                    }
                }
                else
                {
                    var s = BL.GroupStationByNumSlots();  // לרשימה כדי שיהיה העתק . לפי הסדר כדי יחזור למקורי ואז ימיין
                    stations.Clear();
                    foreach (var group in s)
                    {
                        foreach (BO.StationToList item in group)
                        { stations.Add(item); }
                    }
                }
            }
        }

    }
}
