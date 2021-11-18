using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL;

namespace BL
{
    public partial class BL : IBL.IBL
    {


        public Package GetPackage(int id)
        {
            Package package = default;
            try
            {
                IDAL.DO.Package dalPackage = dal.PackageById(id);

            }
            catch (IDAL.DO.Exceptions.IDException pex)
            {
                throw new IBL.BO.Exceptions.BLPackageException($"Package ID {id} not found",pex);
            }

            return package;
        }

        //public IDAL.DO.Package GetUrgentPackage()
        //{
        //    IDAL.DO.Package dalPackage = dal.PackageList().FirstOrDefault(x => x.Priority == IDAL.DO.Priorities.Urgent);
        //    if (dalPackage.Priority == IDAL.DO.Priorities.Urgent)
        //        return dalPackage;
        //    return new IDAL.DO.Package();
        //}

        public void AddPackage(Package package)
        {

            try
            {
                if (package.ID < 0) throw new IBL.BO.Exceptions.PackageIdException("Package Id cannot be negative", package.ID);
            }
            catch(IBL.BO.Exceptions.PackageIdException ex)
            {
                //if (ex.Message == "Package Id cannot be negative") { throw; }
            }

            if (!dal.ClientsList().Any(client => client.ID == package.ReceiverClient.ID)) throw new Exceptions.IdNotFoundException("Receiver Id not found", package.ReceiverClient.ID);
            if (!dal.ClientsList().Any(client => client.ID == package.SenderClient.ID)) throw new Exceptions.IdNotFoundException("Sender Id not found", package.ReceiverClient.ID);

            IDAL.DO.Package dalPackage = new IDAL.DO.Package();

            dalPackage.ID = package.ID;
            dalPackage.SenderId = package.SenderClient.ID;
            dalPackage.TargetId = package.ReceiverClient.ID;
            dalPackage.Priority = (IDAL.DO.Priorities)package.Priority;
            dalPackage.Weight = (IDAL.DO.WeightCategories)package.Weight;
            dalPackage.PickedUp = DateTime.MinValue;
            dalPackage.Associated = DateTime.MinValue;
            dalPackage.Delivered = DateTime.MinValue;
            dalPackage.Created = DateTime.Now;
            dalPackage.DroneId = 0;
            try
            {
                dal.AddPackage(dalPackage);
            }
            catch(IDAL.DO.Exceptions.IDException ex )
            {
                throw new Exceptions.IDException("Package ID already exists", ex, dalPackage.ID);
            }

        }

       

        public void packageToDrone( int Droneid)
        {
            //IDAL.DO.Drone dalDrone = dal.DroneById(id); //no exception

            DroneToList drone = DroneList.First(x => x.ID == Droneid);
            if(!(drone.Status == DroneStatus.Available))
            {
                throw new Exceptions.DroneTaken("Drone is not Available");// new except
            }



            int droneWeight = (int)(drone.MaxWeight);

            IEnumerable<IDAL.DO.Package> dalPackageList = dal.PackageList().Where(x => ((int)(x.Weight) <= droneWeight));
            

            IDAL.DO.Package dalPackage = dalPackageList.Find(x => x.Priority == IDAL.DO.Priorities.Urgent);
           
            if(dalPackage == null)
            {


            }


            double minBattery = batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, )
            minBattery += batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, );
            minBattery += batteryConsumption(drone.DroneLocation.Latitude, drone.DroneLocation.Longitude, );

            if (drone.Status == DroneStatus.Available)
            {
                if (drone.Battery >= minBattery)
                {
                    drone.Status = DroneStatus.Shipping; // לברר
                    
                    //dalPackage.DroneId = drone.ID;

                }







            }







        }



        
    }
}
