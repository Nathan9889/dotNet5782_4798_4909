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

        static IEnumerable<BO.ClientToList> clients = new List<BO.ClientToList>();

     

        public IEnumerable<BO.ClientToList> getClientList()
        {
            return BL.DisplayClientList();
        }

        public BO.Client GetClient(int id)
        {
            return BL.DisplayClient(id);
        }

        public void DeleteClient(int id)
        {
            BL.DeleteClient(id);
        }
        public void UpdateName(int id, string name, string phone)
        {
            BL.UpdateClient(id, name, "");
        }
        public void UpdatePhone(int id, string name, string phone)
        {
            BL.UpdateClient(id, "", phone);
        }

        public void AddClient(BO.Client client)
        {
            BL.AddClient(client);

        }
    }


    //public static class CopyProperties
    //{
    //    public static void CopyPropertiesTo<T, S>(this S from, T to)
    //    {
    //        foreach (PropertyInfo propTo in to.GetType().GetProperties())
    //        {
    //            var propFrom = typeof(S).GetProperty(propTo.Name);
    //            if (propFrom == null)
    //                continue;
    //            var value = propFrom.GetValue(from, null);
    //            if (value is ValueType || value is string)
    //                propTo.SetValue(to, value);
    //        }
    //    }
    //    public static object CopyPropertiesToNew<S>(this S from, Type type)
    //    {
    //        object to = Activator.CreateInstance(type); // new object of Type
    //        from.CopyPropertiesTo(to);
    //        return to;
    //    }
    //}



}



