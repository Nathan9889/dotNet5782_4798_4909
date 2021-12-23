using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Package : INotifyPropertyChanged
    {
        private BO.Package _package;
        public BO.Package package
        {
            get { return _package; }
            set
            {
                _package = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Package"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
