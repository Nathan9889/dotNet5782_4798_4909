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


}



