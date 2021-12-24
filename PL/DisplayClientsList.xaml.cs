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
    /// Interaction logic for DisplayClientsList.xaml
    /// </summary>
    public partial class DisplayClientsList : Page
    {

        BlApi.IBL BL;
        Model.PL PL;
        MainWindow MainWindow;
        private ObservableCollection<BO.ClientToList> clients = new ObservableCollection<BO.ClientToList>();

        public DisplayClientsList(MainWindow mainWindow)
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();
            this.PL = new Model.PL();
            this.MainWindow = mainWindow;
            ClientListView.DataContext = clients;
            InitializeList();
        }

        private void InitializeList()
        {
            foreach (var Client in PL.getClientList())
            {
                clients.Add(Client);
            }
        }

        private void Add_New_Client(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = new DisplayClient(MainWindow, PL);
        }

        private void ClientListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ClientListView.SelectedItem != null)
                MainWindow.Frame.Content = new DisplayClient(MainWindow, PL, PL.GetClient(((BO.ClientToList)ClientListView.SelectedItem).Id));
            ClientListView.SelectedItems.Clear();
        }
        private void ExitButton(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
