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
    /// Interaction logic for DisplaysDronesList.xaml
    /// </summary>
    public partial class DisplaysDronesList : Window
    {
        IBL.IBL BL;
        DisplaysDrone DroneWindow;
        public DisplaysDronesList(IBL.IBL bL)
        {
            InitializeComponent();
            this.BL = bL;
            DronesListView.ItemsSource = BL.DisplayDroneList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));


            //StatusSelector.Items.Add("Select");
            //for (int i = 0; i < 3; i++) StatusSelector.Items.Add(((IBL.BO.DroneStatus)i).ToString());

            //WeightSelector.Items.Add("Select");
            //for (int i = 0; i < 3; i++) WeightSelector.Items.Add(((IBL.BO.WeightCategories)i).ToString());
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBL.BO.DroneStatus status = (IBL.BO.DroneStatus)StatusSelector.SelectedItem;

            if(WeightSelector.SelectedItem == null)
            {
                switch (status)
                {
                    case IBL.BO.DroneStatus.Available:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.Status == IBL.BO.DroneStatus.Available);
                        break;

                    case IBL.BO.DroneStatus.Maintenance:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.Status == IBL.BO.DroneStatus.Maintenance);
                        break;

                    case IBL.BO.DroneStatus.Shipping:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.Status == IBL.BO.DroneStatus.Shipping);
                        break;
                }
            }
            else
            {
                switch (status)
                {
                    case IBL.BO.DroneStatus.Available:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.Status == IBL.BO.DroneStatus.Available && d.MaxWeight == (IBL.BO.WeightCategories) WeightSelector.SelectedItem);
                        break;

                    case IBL.BO.DroneStatus.Maintenance:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.Status == IBL.BO.DroneStatus.Maintenance && d.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
                        break;

                    case IBL.BO.DroneStatus.Shipping:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.Status == IBL.BO.DroneStatus.Shipping && d.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
                        break;
                }
            }
            
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBL.BO.WeightCategories weight = (IBL.BO.WeightCategories)WeightSelector.SelectedItem;
            if(StatusSelector.SelectedItem == null)
            {
                switch (weight)
                {
                    case IBL.BO.WeightCategories.Heavy:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.MaxWeight == IBL.BO.WeightCategories.Heavy);
                        break;

                    case IBL.BO.WeightCategories.Light:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.MaxWeight == IBL.BO.WeightCategories.Light);
                        break;

                    case IBL.BO.WeightCategories.Medium:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.MaxWeight == IBL.BO.WeightCategories.Medium);
                        break;
                }
            }
            else
            {
                switch (weight)
                {
                    case IBL.BO.WeightCategories.Heavy:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.MaxWeight == IBL.BO.WeightCategories.Heavy && d.Status == (IBL.BO.DroneStatus)StatusSelector.SelectedItem);
                        break;

                    case IBL.BO.WeightCategories.Light:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.MaxWeight == IBL.BO.WeightCategories.Light && d.Status == (IBL.BO.DroneStatus)StatusSelector.SelectedItem);
                        break;

                    case IBL.BO.WeightCategories.Medium:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.MaxWeight == IBL.BO.WeightCategories.Medium && d.Status == (IBL.BO.DroneStatus)StatusSelector.SelectedItem);
                        break;
                }
            }
        }

        private void Add_New_Drone(object sender, RoutedEventArgs e)
        {
            DroneWindow = new DisplaysDrone(BL);
            DroneWindow.RefreshListEvent += RefreshListView;
            DroneWindow.Show();
        }


        private void RefreshListView(object ob)
        {
            DronesListView.Items.Refresh();
            if (WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null) DronesListView.ItemsSource = BL.DisplayDroneList();
            if (WeightSelector.SelectedItem != null) WeightSelector_SelectionChanged(this, null);
            if (StatusSelector.SelectedItem != null) StatusSelector_SelectionChanged(this, null);
        }
    }
}
