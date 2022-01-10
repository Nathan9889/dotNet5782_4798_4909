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

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayClientsList.xaml
    /// </summary>
    public partial class DisplayClientsList : Page
    {
        BlApi.IBL BL;
        Model.PL PL;
        
        //private ObservableCollection<BO.ClientToList> clients = new ObservableCollection<BO.ClientToList>();

        public delegate void ClientPage(int id);
        public event ClientPage AddClik;
        public event ClientPage DoubleClik;


        /// <summary>
        /// ctor to display client list and initlialize datacontext and observable list
        /// </summary>
        public DisplayClientsList()
        {
            InitializeComponent();
            this.BL = BlApi.BlFactory.GetBL();
            this.PL = new Model.PL();
            ClientListView.DataContext =ObservableList.clients;
            //InitializeList();
        }

        ///// <summary>
        ///// init observable list
        ///// </summary>
        //private void InitializeList()
        //{
        //    foreach (var Client in PL.getClientList())
        //    {
        //        clients.Add(Client);
        //    }
        //}

        /// <summary>
        /// add new client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_New_Client(object sender, RoutedEventArgs e)
        {
            AddClik(-1);
        }


        /// <summary>
        /// opening the selected client page from the list 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ClientListView.SelectedItem != null) DoubleClik(((PO.ClientToList)ClientListView.SelectedItem).Id);
            ClientListView.SelectedItems.Clear();
        }


        ///// <summary>
        ///// refreshing the list of clients
        ///// </summary>
        ///// <param name="t"></param>
        //public void RefreshList(int t)
        //{
        //    var p = PL.getClientList();
        //    clients.Clear();
        //    foreach (var client in p) clients.Add(client);
        //}
    }
}
