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

        }
    }
}
