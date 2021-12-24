using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Client : INotifyPropertyChanged
    {
        private BO.Client _client;
        public BO.Client client
        {
            get { return _client; }
            set
            {
                _client = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Client"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
