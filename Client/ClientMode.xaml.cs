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

namespace PLClient
{
    /// <summary>
    /// Interaction logic for ClientMde.xaml
    /// </summary>
    public partial class ClientMde : Page
    {
        BlApi.IBL bL = BlApi.BlFactory.GetBL();
        private ObservableCollection<BO.PackageToList> SentPackages = new ObservableCollection<BO.PackageToList>();
        private ObservableCollection<BO.PackageToList> ReceivePackages = new ObservableCollection<BO.PackageToList>();
        Package Package = new Package();

        public ClientMde()
        {
            InitializeComponent();
            Client_Packages_Sent.DataContext = SentPackages;
            Client_Packages_receive.DataContext = ReceivePackages;
            Priority_Combo.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            Weight_Combo.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

            Package.package = new BO.Package();
            Package.package.TargetClient = new BO.ClientPackage();
            Add_Package.DataContext = Package;
        }


        /// <summary>
        /// Login button as a customer - check whether the ID and cell phone are correct and provide login or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush red = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE92617"));
            if (! SolidColorBrush.Equals(((SolidColorBrush)Login_ID.BorderBrush).Color, red.Color) && ! SolidColorBrush.Equals(((SolidColorBrush)Login_Phone.BorderBrush).Color, red.Color))
            {
                try
                {
                    Model.Client client = new Model.Client();
                    client.client = bL.DisplayClient(int.Parse(Login_ID.Text));
                    if (client.client.Phone != Login_Phone.Text.ToString()) MessageBox.Show($"The cell phone number is incorrect ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        MessageBox.Show($"You have logged in successfully !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Login.Visibility = Visibility.Hidden;
                        Main_Display.Visibility = Visibility.Visible;
                        foreach (var package in bL.GetPackagesSentBySpecificClient(int.Parse(Login_ID.Text))) SentPackages.Add(package);
                        foreach (var package in bL.GetPackagesSentToSpecificClient(int.Parse(Login_ID.Text))) ReceivePackages.Add(package);
                        ClientName.Content = bL.DisplayClient(int.Parse(Login_ID.Text)).Name;
                    }
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Login failed {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            else MessageBox.Show($"Please enter proper input !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        /// <summary>
        /// Delete a customer's package button - if not yet associated with the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Package_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                BO.PackageToList packageToList = new BO.PackageToList();
                packageToList = SentPackages.First(p => p.Id == ((BO.PackageToList)Client_Packages_Sent.SelectedItem).Id);
                bL.DeletePackage(((BO.PackageToList)Client_Packages_Sent.SelectedItem).Id);
                SentPackages.Remove(packageToList);
                MessageBox.Show($"Package successfully deleted !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can not delete package {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Package Pick Button - The customer confirms that a package he sent was collected from him
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pick_up_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.PackageToList packageToList = new BO.PackageToList();
                packageToList = SentPackages.First(p => p.Id == ((BO.PackageToList)Client_Packages_Sent.SelectedItem).Id);
                bL.PickedUpByDrone(bL.DisplayPackage(((BO.PackageToList)Client_Packages_Sent.SelectedItem).Id).DroneOfPackage.Id);
                SentPackages.Remove(packageToList);
                packageToList.Status = BO.PackageStatus.PickedUp;
                SentPackages.Add(packageToList);
                MessageBox.Show($"Thank you for your confirmation. \nThe package was successfully collected !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error collecting package {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Package Delivery Button - The customer confirms that a package he needs to receive has reached him
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delivered_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.PackageToList packageToList = new BO.PackageToList();
                packageToList = ReceivePackages.First(p => p.Id == ((BO.PackageToList)Client_Packages_receive.SelectedItem).Id);
                bL.DeliveredToClient(bL.DisplayPackage(((BO.PackageToList)Client_Packages_receive.SelectedItem).Id).DroneOfPackage.Id);
                ReceivePackages.Remove(packageToList);
                packageToList.Status = BO.PackageStatus.Delivered;
                ReceivePackages.Add(packageToList);
                MessageBox.Show($"Thank you for your confirmation. \nThe package was delivered successfully !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delivery confirmation error {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Add package button - the customer sends a new package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Package_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Package.package.SenderClient = new BO.ClientPackage();
                Package.package.SenderClient.ID = int.Parse(Login_ID.Text);
                int newId = bL.AddPackage(Package.package);

                Package.package = bL.DisplayPackage(newId);
                BO.PackageToList packageToList = new BO.PackageToList() { Id = Package.package.ID, Priority = Package.package.Priority, Receiver = Package.package.TargetClient.Name, Sender = Package.package.SenderClient.Name,
                Status = BO.PackageStatus.Created, Weight = Package.package.Weight} ;
                SentPackages.Add(packageToList);

                Package = new Package();
                Package.package = new BO.Package();
                Package.package.TargetClient = new BO.ClientPackage();
                IDInput.Text = null;
                Priority_Combo.SelectedItem = null;
                Weight_Combo.SelectedItem = null;
                Add_Package.DataContext = Package;

                MessageBox.Show($"The package was successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to add package {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addClient_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
