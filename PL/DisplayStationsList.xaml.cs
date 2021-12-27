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



        public DisplayStationsList()
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();
            this.PL = new Model.PL();
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
            {
                if (DoubleClik != null) DoubleClik(((BO.StationToList)StationsListView.SelectedItem).ID);
            }
            StationsListView.SelectedItems.Clear();
        }


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
                        if (temp.Any(s => s.ID == item.ID))
                            stations.Add(item);
                    }
                }
                //else if (Show_Existing.IsChecked == true)
                //{
                //    var s = BL.GroupStationByExistingSlots();
                //    var temp = new ObservableCollection<BO.StationToList>(stations);
                //    stations.Clear();
                //    foreach (var group in s)
                //    {
                //        foreach (BO.StationToList item in group)
                //        {
                //            if (temp.Any(s => s.ID == item.ID))
                //                stations.Add(item);
                //        }
                //    }
                //}
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

        private void Add_New_Station(object sender, RoutedEventArgs e)
        {
            if(AddClik!=null) AddClik(-1);
        }

        
        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            stations.Clear();
            InitializeList();
            Show_Stations(this, new RoutedEventArgs()); 

        }

        public void RefreshList(int x)
        {
            var p = PL.GetStationList();
            stations.Clear();
            foreach (var station in p) stations.Add(station);
            Show_Stations(this, new RoutedEventArgs());
        }
    }
}
