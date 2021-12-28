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
using Model;
using PL;

namespace PL
{

    /// <summary>
    /// Interaction logic for DisplayPackagesList.xaml
    /// </summary>
    public partial class DisplayPackagesList : Page
    {
        BlApi.IBL BL;
        Model.PL PL;
    
        private ObservableCollection<BO.PackageToList> packages = new ObservableCollection<BO.PackageToList>();

        public delegate void PackagePage(int id);
        public event PackagePage AddClik; // Event for opening an add page
        public event PackagePage DoubleClik; // Event for opening an actions page


        public DisplayPackagesList()
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();
            this.PL  = new Model.PL();
            PackageListView.DataContext = packages;
            InitializeList();

            PrioritySelector.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.PackageStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }


        /// <summary>
        /// Initialize list entries
        /// </summary>
        private void InitializeList()
        {
            foreach (var Package in PL.getPackageList())
            {
                packages.Add(Package);
            }
        }

        /// <summary>
        /// Add Package Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_New_Package(object sender, RoutedEventArgs e)
        {
            if(AddClik!=null) AddClik(-1);
        }

        /// <summary>
        /// View actions on a package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackagesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           if(PackageListView.SelectedItem != null && DoubleClik!=null) DoubleClik(((BO.PackageToList)PackageListView.SelectedItem).Id);
            PackageListView.SelectedItems.Clear();
        }


        //private void ExitButton(object sender, RoutedEventArgs e)
        //{

        //}

        /// <summary>
        /// Filter display of packages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            packages.Clear();
            InitializeList();
            Show_Packages(this, new RoutedEventArgs()); // אחרי סינון
           
        }

        /// <summary>
        /// View the packages by grouping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_Packages(object sender, RoutedEventArgs e)
        {
            if (PackageListView != null)
            {
                if(Show_Normally.IsChecked == true) //Normal show
                {
                    var p = BL.DisplayPackageList();
                    var temp = new ObservableCollection<BO.PackageToList>( packages);
                    packages.Clear();
                    foreach (var item in p)
                    {
                        if(temp.Any(p=> p.Id==item.Id)) packages.Add(item);
                    }
                }
                else if(Show_Receiver.IsChecked == true) // View by customer receives
                {
                    var p = BL.PackagesGroupingReceiver();
                    var temp = new ObservableCollection<BO.PackageToList>(packages);
                    packages.Clear();
                    foreach (var group in p)
                    {
                        foreach (BO.PackageToList item in group)
                        { if (temp.Any(p => p.Id == item.Id)) packages.Add(item); }
                    }
                }
                else // Show by customer sending
                {
                    var p = BL.PackagesGroupingSender(); 
                    var temp = new ObservableCollection<BO.PackageToList>(packages);
                    packages.Clear();
                    foreach (var group in p)
                    {
                        foreach (BO.PackageToList item in group)
                        { if (temp.Any(p => p.Id == item.Id)) packages.Add(item);}
                    }
                }
            }
        }



        private void FilterdList(object sender, SelectionChangedEventArgs e = null) // Updates the list according to the requested filter
        {
            IEnumerable<BO.PackageToList> filtered ;
            if (PrioritySelector.SelectedItem == null && WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null)filtered = PL.getPackageList();
            else if (PrioritySelector.SelectedItem == null && WeightSelector.SelectedItem == null && StatusSelector.SelectedItem != null) filtered = PL.getPackageList().Where(p => p.Status == (BO.PackageStatus)StatusSelector.SelectedItem).ToList();
            else if (PrioritySelector.SelectedItem == null && WeightSelector.SelectedItem != null && StatusSelector.SelectedItem != null) filtered = PL.getPackageList().Where(p => p.Status == (BO.PackageStatus)StatusSelector.SelectedItem && p.Weight == (BO.WeightCategories)WeightSelector.SelectedItem).ToList();
            else if (PrioritySelector.SelectedItem != null && WeightSelector.SelectedItem == null && StatusSelector.SelectedItem != null) filtered = PL.getPackageList().Where(p => p.Status == (BO.PackageStatus)StatusSelector.SelectedItem && p.Priority == (BO.Priorities)PrioritySelector.SelectedItem).ToList();

            else if (PrioritySelector.SelectedItem == null && WeightSelector.SelectedItem != null && StatusSelector.SelectedItem == null) filtered = PL.getPackageList().Where(p => p.Weight == (BO.WeightCategories)WeightSelector.SelectedItem).ToList();
            else if (PrioritySelector.SelectedItem != null && WeightSelector.SelectedItem != null && StatusSelector.SelectedItem == null) filtered = PL.getPackageList().Where(p => p.Weight == (BO.WeightCategories)WeightSelector.SelectedItem && p.Priority == (BO.Priorities)PrioritySelector.SelectedItem).ToList();

            else if (PrioritySelector.SelectedItem != null && WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null) filtered = PL.getPackageList().Where(p => p.Priority == (BO.Priorities)PrioritySelector.SelectedItem).ToList();

            else filtered = PL.getPackageList().Where(p => p.Status == (BO.PackageStatus)StatusSelector.SelectedItem && p.Priority == (BO.Priorities)WeightSelector.SelectedItem && p.Weight == (BO.WeightCategories)WeightSelector.SelectedItem).ToList();
            packages.Clear();
            if(filtered != null) foreach (var package in filtered) { packages.Add(package); }
            Show_Packages(this, new RoutedEventArgs()); // After filtering the display should be maintained

        }

        /// <summary>
        /// Filter by date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterdByDate(object sender, SelectionChangedEventArgs e)
        {
            if(StartDate.SelectedDate != null && EndDate.SelectedDate != null)
            {
                FilterdList(this); // Maintain filtering without selecting previous dates
                var listFilterdByDate = BL.GetPackageFilterByDate((DateTime)StartDate.SelectedDate, (DateTime)EndDate.SelectedDate);
                List<BO.PackageToList> temp = new List<BO.PackageToList>();

                if (packages != null)
                {
                    foreach (var BlPackage in listFilterdByDate)
                    {
                        if (packages.Any(p => p.Id == BlPackage.Id))
                        {
                            BO.PackageToList packageToList = new BO.PackageToList();
                            BlPackage.CopyPropertiesTo(packageToList);
                            temp.Add(packageToList);
                        }
                    }
                }

                packages.Clear();
                foreach (var tempPackage in temp)
                {
                    packages.Add(tempPackage); // // If after the filter without the date it exists in the list of the filter and also in the list of the date then we will insert it
                }

                Show_Packages(this, new RoutedEventArgs()); //  After filtering the display should be maintained
            }
        }

        /// <summary>
        /// Refreshing a list and maintaining the filtering and display - a function listed for an event on the next page
        /// </summary>
        /// <param name="t"></param>
        public void RefreshList(int t)
        {
            var p = PL.getPackageList();
            packages.Clear();
            foreach (var package in p) packages.Add(package);
            FilterdList(this);
            Show_Packages(this, new RoutedEventArgs());
        }
    }
}
