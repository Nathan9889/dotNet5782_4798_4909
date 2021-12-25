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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace PL
{
    /// <summary>
    /// Logique d'interaction pour DisplayPackage.xaml
    /// </summary>
    public partial class DisplayPackage : Page
    {
        
        private Model.PL pL;
        Package Package = new Package();

        public delegate void Navigation();
        public event Navigation Back;

        public DisplayPackage()
        {
            InitializeComponent();
            this.pL  = new Model.PL();
            MainGrid.DataContext = Package;
            Package_Priority.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            Package_Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            ClientsList.ItemsSource = pL.getClientList();
            Package_Weight.Style = (Style)this.FindResource("ComboBoxTest2");
            Package_Priority.Style = (Style)this.FindResource("ComboBoxTest2");
        }


        public DisplayPackage(int id)
        {
            this.pL = new Model.PL();
            Package.package = pL.GetPackage(id);
            InitializeComponent();
           
           
            MainGrid.DataContext = Package;
            Package_Priority.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            Package_Weight.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

     

        private void Delete_Package_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.DeletePackage(Package.package.ID);
                if (Back != null) Back();
                MessageBox.Show($"The package has been deleted !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to delete package {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }

        private void Add_Package_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Back != null) Back();
            this.NavigationService.GoBack();
        }

        private void PickUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.PickUpPackage(Package.package.DroneOfPackage.Id);
                MessageBox.Show($"The package has been collected !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Package.package = pL.GetPackage(Package.package.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to PickUp package {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void DeliverButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.DeliveredToClient(Package.package.DroneOfPackage.Id);
                MessageBox.Show($"The package was delivered to the Client !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Package.package = pL.GetPackage(Package.package.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to Deliver package {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Package_Weight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Buttons.Visibility == Visibility.Hidden)Package_Weight.Style = (Style)this.FindResource("ComboBoxTestAfterCorrectInput");
        }

        private void Package_Priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Buttons.Visibility == Visibility.Hidden) Package_Priority.Style = (Style)this.FindResource("ComboBoxTestAfterCorrectInput");
        }
    }
}
