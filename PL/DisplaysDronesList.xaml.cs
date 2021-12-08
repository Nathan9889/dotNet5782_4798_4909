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


        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBL.BO.DroneStatus? status;

            if (StatusSelector.SelectedItem == null) status = null;
            else status = (IBL.BO.DroneStatus)StatusSelector.SelectedItem;

            if (WeightSelector.SelectedItem == null)
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

                    case null:
                        DronesListView.ItemsSource = BL.DisplayDroneList();
                        break;
                }
            }
            else
            {
                switch (status)
                {
                    case IBL.BO.DroneStatus.Available:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.Status == IBL.BO.DroneStatus.Available && d.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
                        break;

                    case IBL.BO.DroneStatus.Maintenance:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.Status == IBL.BO.DroneStatus.Maintenance && d.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
                        break;

                    case IBL.BO.DroneStatus.Shipping:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d => d.Status == IBL.BO.DroneStatus.Shipping && d.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
                        break;

                    case null:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d=> d.MaxWeight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
                        break;

                }
            }

        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBL.BO.WeightCategories? weight;

            if (WeightSelector.SelectedItem == null) weight = null;
            else weight = (IBL.BO.WeightCategories)WeightSelector.SelectedItem;

            if (StatusSelector.SelectedItem == null)
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

                    case null:
                        DronesListView.ItemsSource = BL.DisplayDroneList();
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

                    case null:
                        DronesListView.ItemsSource = BL.DisplayDroneListFilter(d=> d.Status == (IBL.BO.DroneStatus)StatusSelector.SelectedItem);
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


        private void RefreshListView(object ob) // עדכון לרשימות
        {
            DronesListView.Items.Refresh();
            if (WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null) DronesListView.ItemsSource = BL.DisplayDroneList();
            if (WeightSelector.SelectedItem != null) WeightSelector_SelectionChanged(this, null);
            if (StatusSelector.SelectedItem != null) StatusSelector_SelectionChanged(this, null);
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneWindow = new DisplaysDrone(BL, (IBL.BO.DroneToList)DronesListView.SelectedItem, this);
            DroneWindow.RefreshListEvent += RefreshListView;
            DroneWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DronesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
            WeightSelector.SelectedItem = null;
        }
    }
}
