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
        public void addPackage(Package package)
        {

            try
            {
                if (package.ID < 0) throw new IBL.BO.Exceptions.PackageIdException("Package Id cannot be negative", package.ID);
            }
            catch(IBL.BO.Exceptions.PackageIdException ex)
            {
                if (ex.Message == "Package Id cannot be negative") { throw; }
            }

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

    }

}
