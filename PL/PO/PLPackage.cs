using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Model
{
    public partial class PL
    {
        BlApi.IBL BL;

       

        public PL()
        {
            this.BL = BlApi.BlFactory.GetBL();
            
            stations = BL.DisplayStationList();
            clients = BL.DisplayClientList();
        }

        public IEnumerable<BO.PackageToList> getPackageList()
        {
            return BL.DisplayPackageList();
        }

        public BO.Package GetPackage(int id)
        {
            
            return BL.DisplayPackage(id);
           
        }

        public void DeletePackage(int id)
        {
            BL.DeletePackage(id);
        }

        public void PickUpPackage(int id)
        {
            BL.PickedUpByDrone(id);
        }

        public void DeliveredToClient(int id)
        {
            BL.DeliveredToClient(id);
        }





















    }


    public static class CopyProperties
    {
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                var propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }
        public static object CopyPropertiesToNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type); // new object of Type
            from.CopyPropertiesTo(to);
            return to;
        }
    }

      public class Ticker : INotifyPropertyChanged
    {
        public Ticker()
        {
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 second updates
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Now"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}



