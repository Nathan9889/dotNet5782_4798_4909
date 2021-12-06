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


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Drone_Weight.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }

        private void idInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var bc = new BrushConverter();

            idInput.Background = (Brush)bc.ConvertFrom("#00FFFF00");
        }
    }
}
