using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BL
{
    public partial class BL :IBL.IBL
    {

        //public Client GetClient(int id)
        //{
        //    Client client = default;
        //    try
        //    {
        //        IDAL.DO.Client dalClient = dal.ClientById(id);

        //    }
        //    catch (IDAL.DO.Exceptions.IDException ClientEx)
        //    {
        //        throw new IBL.BO.Exceptions.BLClientException($"Client ID {id} not found", ClientEx);
        //    }

        //    return client;
        //}

        IDAL.DO.Station NearestStationToClient(int ClientID) //  חישוב התחנה הקרובה ללקוח עם עמדות טעינה פנויות
        {
            IDAL.DO.Station tempStation = new IDAL.DO.Station();
            double distance = int.MaxValue;
            if (dal.StationWithCharging().Count() == 0) throw new IBL.BO.Exceptions.SendingDroneToCharging("There are no charging slots available at any station",0); // אם אין עמדות טעינה פנויות באף תחנה

            foreach (var station in dal.StationWithCharging())
            {
                double tempDistance = DalObject.DalObject.distance(dal.ClientById(ClientID).Latitude, dal.ClientById(ClientID).Longitude, station.Latitude, station.Longitude);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    tempStation.Latitude = station.Latitude;
                    tempStation.Longitude = station.Longitude;
                }

            }

            return tempStation;
        }


        public void AddClient(Client client)
        {
            try
            {
                if (client.ID < 0) throw new IBL.BO.Exceptions.IDException("Client ID cannot be negative", client.ID);
                if (!dal.ClientsList().Any(x => x.ID == client.ID)) throw new IBL.BO.Exceptions.IDException("Client ID not found", client.ID);
            }
            
            //catch (IBL.BO.Exceptions.IDException ex)
            //{
            //    if (ex.Message == "Station ID can not be negative") { throw; }
            //    else if (ex.Message == "Station ID not found") { throw; }

            //}

            IDAL.DO.Client dalClient = new IDAL.DO.Client();

            if (client.ID < 100000000 && client.ID > 1000000000)
                throw new Exceptions.IDException("Id not valid", client.ID);
            if (client.Phone.Length != 11)
                throw new Exceptions.IDException("Phone number not valid");

            dalClient.ID = client.ID;
            dalClient.Name = client.Name;
            dalClient.Phone = client.Phone;
            if (((client.ClientLocation.Latitude <= 31.73) && (client.ClientLocation.Latitude >= 31.83)) ||
                ((client.ClientLocation.Longitude <= 35.16) && (client.ClientLocation.Longitude >= 35.26)))
            {
                throw new Exceptions.LocationOutOfRange("Client Location entered is out of shipping range", client.ID);
            }
            dalClient.Latitude = client.ClientLocation.Latitude;
            dalClient.Longitude = client.ClientLocation.Longitude;

            try
            {
                dal.AddClient(dalClient);
            }
            catch (IDAL.DO.Exceptions.IDException ex)
            {
                throw new Exceptions.IDException("Client with this ID already exists", ex, dalClient.ID);
            }
        }

        public void UpdateClient(int id, string name, string phone)
        {
            IDAL.DO.Client dalClient;
            if (!dal.ClientsList().Any(x => x.ID == id))
                throw new IBL.BO.Exceptions.IDException("Client ID not found", id);
            dalClient= dal.ClientById(id);

            IDAL.DO.Client clientTemp = dalClient;

            if (name != "")
                clientTemp.Name = name;

            clientTemp.Phone = phone;

            dal.DeleteClient(dalClient);
            dal.AddClient(clientTemp);

        }

        public IEnumerable<IDAL.DO.Client> RecivedCustomerList()
        {
            List<IDAL.DO.Client> receivedClient = new List<IDAL.DO.Client>();
            foreach (var client in dal.ClientsList())
            {
                if (dal.PackageList().Any(x => x.TargetId == client.ID && x.Delivered != DateTime.MinValue))
                    receivedClient.Add(client);
            }

            return receivedClient;
        }


        public Client DisplayClient(int id)
        {
            if (!dal.ClientsList().Any(x => x.ID == id))
                throw new IBL.BO.Exceptions.IDException("Client ID not found", id);

            IDAL.DO.Client dalClient = dal.ClientsList().First(x => x.ID == id);

            Client client = new Client();
            client.ID = dalClient.ID;
            client.Name = dalClient.Name;
            client.Phone = dalClient.Phone;
            Location location = new Location(); //check
            client.ClientLocation.Latitude = dalClient.Latitude;
            client.ClientLocation.Longitude = dalClient.Longitude;



            foreach (var item in dal.droneChargesList())
            {
                PackageAtClient packageAtClient = new PackageAtClient();


              

            }


        }


    }
}
