using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;

namespace DalApi
{

    public static class DalFactory
    {

        public static IDAL GetDal(string type)
        {

            switch (type)
            {
                case "List":

                    return DalObject.DalObject.Instance;
                  

                case "xml":
                    return DalXml.Instance;
                    

                default:
                    throw new DO.Exceptions.IDalNotFound("Idal only have List/xml type", type);


            }


        }

    }


}
