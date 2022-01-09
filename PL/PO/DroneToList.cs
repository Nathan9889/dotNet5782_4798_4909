using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace PO
{
    public class DroneToList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }


        private string _Model;
        public string Model
        {
            get { return _Model; }
            set
            {
                _Model = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Model"));
            }
        }


        private double _Battery;
        public double Battery
        {
            get { return _Battery; }
            set
            {
                _Battery = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            }
        }


        private BO.Location _Location;
        public BO.Location Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            }
        }


        private int _PackageID;
        public int PackageID
        {
            get { return _PackageID; }
            set
            {
                _PackageID = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PackageID"));
            }
        }


    }
}
