using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace BL
{
    public partial class BL : IBL.IBL
    {

        public Station GetStation(int id)
        {
            Station station = default;
            try
            {
                IDAL.DO.Station dalStation = dal.StationById(id);

            }
            catch (IDAL.DO.Exceptions.IDException SationEx)
            {
                throw new IBL.BO.Exceptions.BLStationException($"Sation ID {id} not found", SationEx);
            }

            return station;
        }


        // public List<StationToList> StationList;
        public void AddStation(Station station)
        {
           
        }

    }


}
