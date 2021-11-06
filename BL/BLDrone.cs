using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL;

namespace IBL
{
    public partial class BL : IBL
    {
        public List<DroneToList> DroneList;
        public IDAL.IDAL dal;
        public double PowerVacantDrone ;
        public double PowerLightDrone;
        public double PowerMediumDrone ;
        public double PowerHeavyDrone ;
        public double ChargeRate;

        BL()
        { 
            dal = new DalObject.DalObject();

            PowerVacantDrone = (dal.PowerConsumptionByDrone())[0];
            PowerLightDrone = (dal.PowerConsumptionByDrone())[1];
            PowerMediumDrone = (dal.PowerConsumptionByDrone())[2];
            PowerHeavyDrone = (dal.PowerConsumptionByDrone())[3];
            ChargeRate = (dal.PowerConsumptionByDrone())[4];


        }
    }





}
