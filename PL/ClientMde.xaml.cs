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
    /// Interaction logic for ClientMde.xaml
    /// </summary>
    public partial class ClientMde : Page
    {
        BlApi.IBL bL = BlApi.BlFactory.GetBL();
        private ObservableCollection<BO.PackageToList> SentPackages = new ObservableCollection<BO.PackageToList>();
        private ObservableCollection<BO.PackageToList> ReceivePackages = new ObservableCollection<BO.PackageToList>();
        public ClientMde()
        {
            InitializeComponent();
            Client_Packages_Sent.DataContext = SentPackages;
            Client_Packages_receive.DataContext = ReceivePackages;


        }

        private void Client_Packages_SubmenuOpened(object sender, RoutedEventArgs e)
        {

        }

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
                    }
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Login failed {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            else MessageBox.Show($"Please enter proper input !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
