using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DisplayDronesList.xaml
    /// </summary>
    public partial class DisplayDronesList : Page
    {
        BlApi.IBL BL;
        Model.PL PL;

        private ObservableCollection<BO.DroneToList> drones = new ObservableCollection<BO.DroneToList>();

        public delegate void DronePage(int id);
        public event DronePage AddClik;
        public event DronePage DoubleClik;



        /// <summary>
        /// Initialize the drone list view window
        /// </summary>
        /// <param name="bL"></param>
        public DisplayDronesList() 
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();
            this.PL = new Model.PL();
            DronesListView.DataContext = drones;
            InitializeList();

            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }


        private void InitializeList()
        {
            foreach (var Drone in PL.GetDroneList())
            {
                drones.Add(Drone);
            }
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DronesListView.SelectedItem != null && DoubleClik != null) 
                DoubleClik(((BO.DroneToList)DronesListView.SelectedItem).ID);
            DronesListView.SelectedItems.Clear();
        }

        /// <summary>
        /// By clicking on the button Add Drone - Displays the drone window, and registers for the event of closing the window the function that refreshes the drone list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_New_Drone(object sender, RoutedEventArgs e)
        {
            if (AddClik != null) AddClik(-1);

        }


        ///// <summary>
        ///// Closes the drone list window
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ExitButton(object sender, RoutedEventArgs e)
        //{
        //    this.NavigationService.GoBack();

        //    ////////********************************************************
            
        //}


        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            drones.Clear();
            InitializeList();
            Show_Drones(this, new RoutedEventArgs()); // אחרי סינון

        }


        private void Show_Drones(object sender, RoutedEventArgs e)
        {
            if (DronesListView != null)
            {
                if (Show_Normally.IsChecked == true)
                {
                    var d = BL.DisplayDroneList();
                    var temp = new ObservableCollection<BO.DroneToList>(drones);
                    drones.Clear();
                    foreach (var item in d)
                    {
                        if (temp.Any(x => x.ID == item.ID)) drones.Add(item);
                    }
                }
                else if (Show_Status.IsChecked == true)
                {
                    var d = BL.DroneGroupbyStatus();
                    var temp = new ObservableCollection<BO.DroneToList>(drones);
                    drones.Clear();
                    foreach (var group in d)
                    {
                        foreach (BO.DroneToList item in group)
                        { if (temp.Any(x => x.ID == item.ID)) drones.Add(item); }
                    }
                }
              
            }
        }





        /// <summary>
        /// Displays the list of drones according to the filters of the 2 options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.DroneStatus? status;
            drones.Clear();

            if (StatusSelector.SelectedItem == null) status = null;
            else status = (BO.DroneStatus)StatusSelector.SelectedItem;

            if (WeightSelector.SelectedItem == null)
            {
                switch (status)
                {
                    case BO.DroneStatus.Available:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.Status == BO.DroneStatus.Available)) drones.Add(d);
                        break;

                    case BO.DroneStatus.Maintenance:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.Status == BO.DroneStatus.Maintenance))  drones.Add(d);
                        break;

                    case BO.DroneStatus.Shipping:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.Status == BO.DroneStatus.Shipping)) drones.Add(d);
                        break;

                    case null:
                        InitializeList();
                        break;
                }
            }
            else
            {
                switch (status)
                {
                    case BO.DroneStatus.Available:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.Status == BO.DroneStatus.Available && d.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem)) drones.Add(d);
                        break;

                    case BO.DroneStatus.Maintenance:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.Status == BO.DroneStatus.Maintenance && d.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem)) drones.Add(d);
                        break;

                    case BO.DroneStatus.Shipping:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.Status == BO.DroneStatus.Shipping && d.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem)) drones.Add(d);
                        break;

                    case null:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.MaxWeight == (BO.WeightCategories)WeightSelector.SelectedItem)) drones.Add(d);
                        break;

                }
            }

        }

        /// <summary>
        /// Displays the list of drones according to the filters of the 2 options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.WeightCategories? weight;
            drones.Clear();

            if (WeightSelector.SelectedItem == null) weight = null;
            else weight = (BO.WeightCategories)WeightSelector.SelectedItem;

            if (StatusSelector.SelectedItem == null)
            {
                switch (weight)
                {
                    case BO.WeightCategories.Heavy:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.MaxWeight == BO.WeightCategories.Heavy)) drones.Add(d);
                        break;

                    case BO.WeightCategories.Light:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.MaxWeight == BO.WeightCategories.Light)) drones.Add(d);
                        break;

                    case BO.WeightCategories.Medium:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.MaxWeight == BO.WeightCategories.Medium)) drones.Add(d);
                        break;
                    case null:
                        InitializeList();
                        break;
                }
            }
            else
            {
                switch (weight)
                {
                    case BO.WeightCategories.Heavy:

                        foreach (var d in BL.DisplayDroneListFilter(d => d.MaxWeight == BO.WeightCategories.Heavy && d.Status == (BO.DroneStatus)StatusSelector.SelectedItem)) drones.Add(d);
                        break;

                    case BO.WeightCategories.Light:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.MaxWeight == BO.WeightCategories.Light && d.Status == (BO.DroneStatus)StatusSelector.SelectedItem)) drones.Add(d);
                        break;

                    case BO.WeightCategories.Medium:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.MaxWeight == BO.WeightCategories.Medium && d.Status == (BO.DroneStatus)StatusSelector.SelectedItem)) drones.Add(d);
                        break;

                    case null:
                        foreach (var d in BL.DisplayDroneListFilter(d => d.Status == (BO.DroneStatus)StatusSelector.SelectedItem)) drones.Add(d);

                        break;
                }
            }
        }

       

        /// <summary>
        /// A function that refreshes the list of drones
        /// </summary>
        /// <param name="ob"></param>
        private void RefreshListView(object sender, EventArgs e) // עדכון לרשימות
        {
            DronesListView.Items.Refresh();
            if (WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null) DronesListView.ItemsSource = BL.DisplayDroneList();
            if (WeightSelector.SelectedItem != null) WeightSelector_SelectionChanged(this, null);
            if (StatusSelector.SelectedItem != null) StatusSelector_SelectionChanged(this, null);
        }


        public void RefreshList(int t)
        {
            var d = PL.GetDroneList();
            drones.Clear();
            foreach (var drone in d) drones.Add(drone);
            //FilterdList(this);
            Show_Drones(this, new RoutedEventArgs());

            //?
            RefreshListView(this, EventArgs.Empty);
        }












        //private void DroneFilterdList(object sender, SelectionChangedEventArgs e = null) // מעדכנת את הרשימה בהתאם
        //{
        //    IEnumerable<BO.PackageToList> filtered;
        //    if (PrioritySelector.SelectedItem == null && WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null) filtered = PL.getPackageList();
        //    else if (PrioritySelector.SelectedItem == null && WeightSelector.SelectedItem == null && StatusSelector.SelectedItem != null) filtered = PL.getPackageList().Where(p => p.Status == (BO.PackageStatus)StatusSelector.SelectedItem).ToList();
        //    else if (PrioritySelector.SelectedItem == null && WeightSelector.SelectedItem != null && StatusSelector.SelectedItem != null) filtered = PL.getPackageList().Where(p => p.Status == (BO.PackageStatus)StatusSelector.SelectedItem && p.Weight == (BO.WeightCategories)WeightSelector.SelectedItem).ToList();
        //    else if (PrioritySelector.SelectedItem != null && WeightSelector.SelectedItem == null && StatusSelector.SelectedItem != null) filtered = PL.getPackageList().Where(p => p.Status == (BO.PackageStatus)StatusSelector.SelectedItem && p.Priority == (BO.Priorities)PrioritySelector.SelectedItem).ToList();

        //    else if (PrioritySelector.SelectedItem == null && WeightSelector.SelectedItem != null && StatusSelector.SelectedItem == null) filtered = PL.getPackageList().Where(p => p.Weight == (BO.WeightCategories)WeightSelector.SelectedItem).ToList();
        //    else if (PrioritySelector.SelectedItem != null && WeightSelector.SelectedItem != null && StatusSelector.SelectedItem == null) filtered = PL.getPackageList().Where(p => p.Weight == (BO.WeightCategories)WeightSelector.SelectedItem && p.Priority == (BO.Priorities)PrioritySelector.SelectedItem).ToList();

        //    else if (PrioritySelector.SelectedItem != null && WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null) filtered = PL.getPackageList().Where(p => p.Priority == (BO.Priorities)PrioritySelector.SelectedItem).ToList();

        //    else filtered = PL.getPackageList().Where(p => p.Status == (BO.PackageStatus)StatusSelector.SelectedItem && p.Priority == (BO.Priorities)WeightSelector.SelectedItem && p.Weight == (BO.WeightCategories)WeightSelector.SelectedItem).ToList();
        //    packages.Clear();
        //    if (filtered != null) foreach (var package in filtered) { packages.Add(package); }
        //    Show_Packages(this, new RoutedEventArgs()); // אחרי סינון צריך לשמור על ההצגה

        //}


        ///// <summary>
        ///// Reset the drone list filter
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Reset_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    StatusSelector.SelectedItem = null;
        //    WeightSelector.SelectedItem = null;
        //}

        //public void RefreshList(int t)
        //{
        //    RefreshListView(this, EventArgs.Empty);
        //}






    }
}

