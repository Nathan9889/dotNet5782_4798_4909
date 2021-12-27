using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Drone : INotifyPropertyChanged
    {
        private BO.Drone _drone;
        public BO.Drone drone
        {
            get { return _drone; }
            set
            {
                _drone = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Drone"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
