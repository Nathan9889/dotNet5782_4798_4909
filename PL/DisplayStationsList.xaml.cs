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
        
        private ObservableCollection<BO.StationToList> stations = new ObservableCollection<BO.StationToList>();
        public delegate void StationPage(int id);
        public event StationPage AddClik;
        public event StationPage DoubleClik;


        /// <summary>
        /// Ctoe with init elements
        /// </summary>
        public DisplayStationsList()
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();
            this.PL = new Model.PL();
            StationsListView.DataContext = stations;
            InitializeList();
        }

        /// <summary>
        /// func that initialize observable  list
        /// </summary>
        void InitializeList()
        {

            foreach (var station in PL.GetStationList())
            {
                stations.Add(station);
            }
        }

        /// <summary>
        /// open station page of selected station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StationsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StationsListView.SelectedItem != null)
            {
                if (DoubleClik != null) DoubleClik(((BO.StationToList)StationsListView.SelectedItem).ID);
            }
            StationsListView.SelectedItems.Clear();
        }


        /// <summary>
        /// function that filter the display of the list with grouping by num slot for example
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_Stations(object sender, RoutedEventArgs e)
        {
            if (StationsListView != null)
            {
                if (Show_Normally.IsChecked == true)
                {
                    var s = BL.DisplayStationList();
                    var temp = new ObservableCollection<BO.StationToList>(stations);
                    stations.Clear();
                    foreach (var item in s)
                    {
                        if (temp.Any(s => s.ID == item.ID))  //init again
                            stations.Add(item);
                    }
                }
                else if (Show_Open.IsChecked == true)
                {
                    var s = BL.GroupStationByNumSlots();  // לרשימה כדי שיהיה העתק . לפי הסדר כדי יחזור למקורי ואז ימיין
                    var temp = new ObservableCollection<BO.StationToList>(stations);
                    stations.Clear();

                    foreach (var group in s)
                    {
                        foreach (BO.StationToList item in group)
                        {
                            if (temp.Any(s => s.ID == item.ID))
                                stations.Add(item);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// adding new station to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_New_Station(object sender, RoutedEventArgs e)
        {
            if(AddClik!=null) AddClik(-1);
        }

        /// <summary>
        /// reset the display list from filters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            stations.Clear();
            InitializeList();
            Show_Stations(this, new RoutedEventArgs()); 

        }

        /// <summary>
        /// Refresh the list of stations from changes
        /// </summary>
        /// <param name="x"></param>
        public void RefreshList(int x)
        {
            var p = PL.GetStationList();
            stations.Clear();
            foreach (var station in p) stations.Add(station);
            Show_Stations(this, new RoutedEventArgs());
        }
    }
}
