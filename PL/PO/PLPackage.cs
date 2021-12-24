using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class PL
    {
        BlApi.IBL BL;

       

        public PL()
        {
            this.BL = BlApi.BlFactory.GetBL();
            packages = BL.DisplayPackageList();
            stations = BL.DisplayStationList();
            clients = BL.DisplayClientList();
        }

        public IEnumerable<BO.PackageToList> getPackageList()
        {
            return new List<BO.PackageToList>();
        }

        public Package GetPackage(int id)
        {
            Package package = new Package();
            package.package = BL.DisplayPackage(id);
            return package;
        }

        public void DeletePackage(int id)
        {
            BL.DeletePackage(id);
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



}



