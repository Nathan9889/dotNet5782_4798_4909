using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PackageToList
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public BO.WeightCategories Weight { get; set; }
        public BO.Priorities Priority { get; set; }
        public BO.PackageStatus Status { get; set; }


        public override string ToString()
        {
            string result = "";
            result += $"Package Id is {Id},\n";
            result += $"Package Sender Name is {Sender},\n";
            result += $"Package Receiver Name is {Receiver},\n";
            result += $"Package Weight Capacity is {Weight},\n";
            result += $"Package Priorities is {Priority},\n";
            result += $"Package Status is {Status},\n";

            return result;
        }
    }

    //public class PackageToList : INotifyPropertyChanged
    //{

    //    private int _Id;
    //    private string _Sender;
    //    private string _Receiver;
    //    private BO.WeightCategories _Weight;
    //    private BO.Priorities _Priority;
    //    private BO.PackageStatus _Status;

    //    public int Id
    //    {
    //        get { return _Id; }
    //        set
    //        {
    //            _Id = value;
    //            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ID"));
    //        }
    //    }

    //    public string Sender
    //    {
    //        get { return _Sender; }
    //        set
    //        {
    //            _Sender = value;
    //            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Sender"));
    //        }
    //    }

    //    public string Receiver
    //    {
    //        get { return _Receiver; }
    //        set
    //        {
    //            _Receiver = value;
    //            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Receiver"));
    //        }
    //    }

    //    public BO.WeightCategories Weight
    //    {
    //        get { return _Weight; }
    //        set
    //        {
    //            _Weight = value;
    //            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Weight"));
    //        }
    //    }

    //    public BO.Priorities Priority
    //    {
    //        get { return _Priority; }
    //        set
    //        {
    //            _Priority = value;
    //            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Priority"));
    //        }
    //    }

    //    public BO.PackageStatus Status
    //    {
    //        get { return _Status; }
    //        set
    //        {
    //            _Status = value;
    //            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Status"));
    //        }
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public override string ToString()
    //    {
    //        string result = "";
    //        result += $"Package Id is {Id},\n";
    //        result += $"Package Sender Name is {Sender},\n";
    //        result += $"Package Receiver Name is {Receiver},\n";
    //        result += $"Package Weight Capacity is {Weight},\n";
    //        result += $"Package Priorities is {Priority},\n";
    //        result += $"Package Status is {Status},\n";

    //        return result;
    //    }
    //}
}
