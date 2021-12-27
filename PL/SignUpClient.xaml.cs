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

using Newtonsoft.Json;

namespace PL
{
    /// <summary>
    /// Interaction logic for SignUpClient.xaml
    /// </summary>
    /// 
    public partial class SignUpClient : Window
    {
        Model.Client client = new Model.Client();
        BlApi.IBL bL = BlApi.BlFactory.GetBL();
        public SignUpClient()
        {
            client.client = new BO.Client();
            client.client.ClientLocation = new BO.Location();
            InitializeComponent();
            DataContext = client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bL.AddClient(client.client);
                MessageBox.Show($"Client added successfully !", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to add Client {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
