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


        private IDAL.DO.Station NearestStationToClient(int ClientID) //  חישוב התחנה הקרובה ללקוח עם עמדות טעינה פנויות
        {
            IDAL.DO.Station tempStation = new IDAL.DO.Station();
            double distance = int.MaxValue;
            if (dal.StationWithCharging().Count() == 0) throw new IBL.BO.Exceptions.SendingDroneToCharging("There are no charging slots available at any station", 0); // אם אין עמדות טעינה פנויות באף תחנה

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
         
             if (client.ID < 0) throw new IBL.BO.Exceptions.IDException("Client ID cannot be negative", client.ID);
             if (!dal.ClientsList().Any(x => x.ID == client.ID)) throw new IBL.BO.Exceptions.IDException("Client ID not found", client.ID);
            

            IDAL.DO.Client dalClient = new IDAL.DO.Client();

            if (client.ID < 100000000 && client.ID > 1000000000)
                throw new Exceptions.IDException("Id not valid", client.ID);

            correctPhone(client.Phone); // בודק תקינות מספר פלאפון

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
            correctPhone(phone); // בודק תקינות מספר פלאפון

            dalClient = dal.ClientById(id);

            IDAL.DO.Client clientTemp = dalClient;

            if (name != "")
                clientTemp.Name = name;
            if (phone != "")
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

            location.Longitude = dalClient.Longitude;
            location.Latitude = dalClient.Latitude;
            client.ClientLocation = location;



            //List<PackageAtClient> senderClientList = new List<PackageAtClient>();

            foreach (var item in dal.PackageList())
            {

                if (item.SenderId == dalClient.ID)
                {
                    PackageAtClient packageAtClient = new PackageAtClient();

                    packageAtClient.Id = item.ID;
                    packageAtClient.Weight = (WeightCategories)item.Weight;
                    packageAtClient.Priority = (Priorities)item.Priority;
                    //if (item.Created != DateTime.MinValue)
                    packageAtClient.Status = PackageStatus.Created; //?

                    ClientPackage clientPackage = new ClientPackage();
                    clientPackage.ID = item.SenderId;
                    //clientPackage.Name;//???????

                    packageAtClient.Source_Destination = clientPackage;

                    // senderClientList.Add(packageAtClient);
                    client.ClientsSender.Add(packageAtClient);
                }
            }



            //List <PackageAtClient> receiverClientList = new List<PackageAtClient>();

            foreach (var item in dal.PackageList())
            {

                if (item.TargetId == dalClient.ID)
                {
                    PackageAtClient packageAtClient = new PackageAtClient();

                    packageAtClient.Id = item.ID;
                    packageAtClient.Weight = (WeightCategories)item.Weight;
                    packageAtClient.Priority = (Priorities)item.Priority;
                    packageAtClient.Status = PackageStatus.Delivered;///????

                    ClientPackage clientPackage = new ClientPackage();

                    clientPackage.ID = item.TargetId;
                   // clientPackage.Name;//?????

                    packageAtClient.Source_Destination = clientPackage;

                    // receiverClientList.Add(packageAtClient);
                    client.ClientsReceiver.Add(packageAtClient);
                }
            }

            //client.ClientsSender = senderClientList;
            //client.ClientsReceiver = receiverClientList;


            return client;

        }

        private void correctPhone(string phone) // בןדק תקינות מספר פלאפון
        {
            string[] nums = { "052", "053", "054", "055", "056", "057", "058" };

            if (phone.Length != 10) throw new IBL.BO.Exceptions.PhoneExceptional("The cell phone number is incorrect", phone);

            phone = phone.Substring(0, 3);
            if (!nums.Any(x => x == phone)) throw new IBL.BO.Exceptions.PhoneExceptional("The cell phone number is incorrect", phone);
        }



    }


    
}
