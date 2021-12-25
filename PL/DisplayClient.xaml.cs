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

        private MainWindow mainWindow;
        private Model.PL pL;
        Client Client;
        public delegate void Navigation();
        public event Navigation Back;
        //
        BlApi.IBL BL;
        //

        public DisplayClient()
        {
            InitializeComponent();
           
            this.pL = new Model.PL();

            
            this.BL = BlApi.BlFactory.GetBL();
            
        }

        public DisplayClient(int id)
        {
            InitializeComponent();
           
            this.pL =new Model.PL();
            this.Client = pL.GetClient(id);
            MainGrid.DataContext = Client;
        }


        //*******************************************************************************
        private void UpdateName_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                //BL.UpdateClient();

                MessageBox.Show("Drone sent to charge", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //InitializeDisplayDrone(selectedDrone.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.NavigationService.GoBack();
        }

        private void Update_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                //BL.UpdateClient();

                MessageBox.Show("Drone sent to charge", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //InitializeDisplayDrone(selectedDrone.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.NavigationService.GoBack();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        //***********************************************************************************
    }
}
