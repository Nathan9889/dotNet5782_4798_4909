using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace PO
{
    public class ClientToList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }


        private int _sentAndDeliveredPackage;
        public int sentAndDeliveredPackage
        {
            get { return _sentAndDeliveredPackage; }
            set
            {
                _sentAndDeliveredPackage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("sentAndDeliveredPackage"));
            }
        }




        private int _sentAndUndeliveredPackage;
        public int sentAndUndeliveredPackage
        {
            get { return _sentAndUndeliveredPackage; }
            set
            {
                _sentAndUndeliveredPackage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("sentAndUndeliveredPackage"));
            }
        }

        private int _ReceivedAndDeliveredPackage;
        public int ReceivedAndDeliveredPackage
        {
            get { return _ReceivedAndDeliveredPackage; }
            set
            {
                _ReceivedAndDeliveredPackage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ReceivedAndDeliveredPackage"));
            }
        }


        private int _ReceivedAndUnDeliveredPackage;
        public int ReceivedAndUnDeliveredPackage
        {
            get { return _ReceivedAndUnDeliveredPackage; }
            set
            {
                _ReceivedAndUnDeliveredPackage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ReceivedAndUnDeliveredPackage"));
            }
        }


    }
}
