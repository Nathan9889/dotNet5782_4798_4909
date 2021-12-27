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
    /// Logique d'interaction pour DisplayClient.xaml
    /// </summary>
    /// 
    public partial class DisplayClient : Page
    {
        private Model.PL pL;
        Client Client = new Client();

        public delegate void Navigation(int id);
        public event Navigation Back;
        public event Navigation PackagePage;
       

        public DisplayClient()
        {
            InitializeComponent();
            this.pL = new Model.PL();
            MainGrid.DataContext = Client;

            Mode.IsChecked = true;
            //**
            Client.client = new BO.Client();
            Client.client.ClientLocation = new BO.Location();
           
            //**

        }

        public DisplayClient(int id)
        {
            
            this.pL =new Model.PL();
            Client.client = pL.GetClient(id);
            InitializeComponent();
            MainGrid.DataContext = Client;

            SenderPackageList.ItemsSource = Client.client.ClientsSender;
            ReceiverPackageList.ItemsSource = Client.client.ClientsReceiver;
        }
      


        private void UpdateName_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.UpdateName(Client.client.ID, Client_Name.Text, "");


                MessageBox.Show("Client Name have been Changed Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Client.client = pL.GetClient(Client.client.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void UpdatePhone_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                pL.UpdateName(Client.client.ID, "", Client_Phone.Text);

                MessageBox.Show("Client Phone have been Changed Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Client.client = pL.GetClient(Client.client.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }







        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Back != null)
                Back(-1);
            this.NavigationService.GoBack();
        }



        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Back != null) Back(-1);
            this.NavigationService.GoBack();
        }

        private void Add_Client_Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                pL.AddClient(Client.client);
                if (Back != null) Back(-1);
                MessageBox.Show($"The Client was successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to add Client {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SenderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.PackageAtClient p = SenderPackageList.SelectedItem as BO.PackageAtClient;
            if (p != null && PackagePage != null && p.Id != 0)
                PackagePage(p.Id);
        }
        private void ReceiverList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.PackageAtClient p = ReceiverPackageList.SelectedItem as BO.PackageAtClient;
            if (PackagePage != null && p.Id != 0)
                PackagePage(p.Id);

        }
    }
}
