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

using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplaysDronesList.xaml
    /// </summary>
    public partial class DisplaysDronesList : Window
    {
        IBL.IBL BL;
        DisplaysDrone DroneWindow;

        /// <summary>
        ///  Deleting the X button
        /// </summary>
        private const int GWL_STYLE = -16;   //used to remove X button for bonus
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void Window_Loaded(object sender, RoutedEventArgs e)  //function used to remove X button for bonus
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        //********



        /// <summary>
        /// Initialize the drone list view window
        /// </summary>
        /// <param name="bL"></param>
        public DisplaysDronesList(IBL.IBL bL)
        {
            InitializeComponent();
            this.BL = bL;
            DronesListView.ItemsSource = BL.DisplayDroneList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }

        /// <summary>
        /// Displays the list of drones according to the filters of the 2 options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Displays the list of drones according to the filters of the 2 options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// By clicking on the button Add Drone - Displays the drone window, and registers for the event of closing the window the function that refreshes the drone list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_New_Drone(object sender, RoutedEventArgs e)
        {
            DroneWindow = new DisplaysDrone(BL);
            DroneWindow.CloseWindowEvent += RefreshListView;
            DroneWindow.Show();
        }

        /// <summary>
        /// A function that refreshes the list of drones
        /// </summary>
        /// <param name="ob"></param>
        private void RefreshListView(object ob) // עדכון לרשימות
        {
            DronesListView.Items.Refresh();
            if (WeightSelector.SelectedItem == null && StatusSelector.SelectedItem == null) DronesListView.ItemsSource = BL.DisplayDroneList();
            if (WeightSelector.SelectedItem != null) WeightSelector_SelectionChanged(this, null);
            if (StatusSelector.SelectedItem != null) StatusSelector_SelectionChanged(this, null);
        }

        /// <summary>
        /// Double-clicking the drone in the list - Displays the drone window, and registers for the event of closing the window the function that refreshes the drone list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((IBL.BO.DroneToList)DronesListView.SelectedItem != null)
            {
                DroneWindow = new DisplaysDrone(BL, (IBL.BO.DroneToList)DronesListView.SelectedItem, this);
                DroneWindow.CloseWindowEvent += RefreshListView;
                DroneWindow.Show();
            }
            DronesListView.SelectedItems.Clear();
        }

        /// <summary>
        /// Closes the drone list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Reset the drone list filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            StatusSelector.SelectedItem = null;
            WeightSelector.SelectedItem = null;
        }
    }
}
