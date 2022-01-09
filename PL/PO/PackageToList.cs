using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace PO
{
    public class PackageToList : INotifyPropertyChanged
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


        private string _Sender;
        public string Sender
        {
            get { return _Sender;}
            set
            {
                _Sender = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Sender"));
            }
        }


        private string _Receiver;
        public string Receiver
        {
            get { return _Receiver; }
            set
            {
                _Receiver = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Receiver"));
            }
        }




        private BO.WeightCategories _Weight;
        public BO.WeightCategories Weight
        {
            get { return _Weight; }
            set
            {
                _Weight = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Weight"));
            }
        }



        private BO.Priorities _Priority;
        public BO.Priorities Priority
        {
            get { return _Priority; }
            set
            {
                _Priority = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Priority"));
            }
        }



        private BO.PackageStatus _Status;
        public BO.PackageStatus Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }

    }
}
