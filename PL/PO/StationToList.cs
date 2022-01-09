using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace PO
{
    public class StationToList :INotifyPropertyChanged
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


        private int _AvailableChargingSlots;
        public int AvailableChargingSlots
        {
            get { return _AvailableChargingSlots; }
            set
            {
                _AvailableChargingSlots = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AvailableChargingSlots"));
            }
        }

        private int _BusyChargingSlots;
        public int BusyChargingSlots
        {
            get { return _BusyChargingSlots; }
            set
            {
                _BusyChargingSlots = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("BusyChargingSlots"));
            }
        }

    }
}
