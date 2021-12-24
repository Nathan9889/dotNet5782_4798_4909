using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Station : INotifyPropertyChanged
    {
        private BO.Station _station;
        public BO.Station station
        {
            get { return _station; }
            set
            {
                _station = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Station"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
