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
        
        private ObservableCollection<BO.ClientToList> clients = new ObservableCollection<BO.ClientToList>();

        public delegate void ClientPage(int id);
        public event ClientPage AddClik;
        public event ClientPage DoubleClik;

        public DisplayClientsList()
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();
            this.PL = new Model.PL();
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
            AddClik(-1);
        }

        private void ClientListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ClientListView.SelectedItem != null) DoubleClik(((BO.ClientToList)ClientListView.SelectedItem).Id);
            ClientListView.SelectedItems.Clear();
        }



        public void RefreshList(int t)
        {
            var p = PL.getClientList();
            clients.Clear();
            foreach (var client in p) clients.Add(client);
        }
    }
}
