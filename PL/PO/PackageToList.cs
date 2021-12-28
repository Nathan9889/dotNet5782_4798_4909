using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PackageToList : INotifyPropertyChanged
    {
        private BO.PackageToList _packageToList;
        public BO.PackageToList packageToList
        {
            get { return _packageToList; }
            set
            {
                _packageToList = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PackageToList"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
