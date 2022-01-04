using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using xml;

namespace DalApi
{
    class DalXml : IDAL
    {
        static readonly IDAL instance = new DalXml();
        internal static IDAL Instance { get { return instance; } }
        static DalXml() { }

        private string stationPath = "Stations.xml";
        readonly string clientPath = "Clients.xml";
        readonly string dronePath = "Drones.xml";
        readonly string packagePath = "Packages.xml";
        readonly string droneChargePath = "DroneCharge.xml";
        readonly string configPath = "Config.xml";


        DalXml()
        {
            DalObject.DataSource.Initialize();
            xml.XMLTools.SetStationListToFile(DalObject.DataSource.StationList, stationPath);
            xml.XMLTools.SaveListToXMLSerializer(DalObject.DataSource.ClientList, clientPath);
            xml.XMLTools.SaveListToXMLSerializer(DalObject.DataSource.DroneList, dronePath);
            xml.XMLTools.SaveListToXMLSerializer(DalObject.DataSource.PackageList, packagePath);
            xml.XMLTools.SaveListToXMLSerializer(DalObject.DataSource.droneCharge, droneChargePath);


            xml.XMLTools.Config(configPath);
        }




        public void AddClient(Client client)
        {

            List<Client> clientList = XMLTools.LoadListFromXMLSerializer<DO.Client>(clientPath);
            if(clientList.Any(c=>c.ID == client.ID)) throw new Exceptions.IDException("A client ID Not Found", client.ID);

            clientList.Add(client);
            XMLTools.SaveListToXMLSerializer(clientList, clientPath);
        }

        public void AddDrone(Drone drone)
        {
            List<Drone> dronesList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (dronesList.Any(c => c.ID == drone.ID)) throw new Exceptions.IDException("A client ID Not Found", drone.ID);

            dronesList.Add(drone);
            XMLTools.SaveListToXMLSerializer(dronesList, dronePath);
        }

        public int AddPackage(Package package)
        {
            List<Package> PackagesList = XMLTools.LoadListFromXMLSerializer<DO.Package>(packagePath);
            if (PackagesList.Any(c => c.ID == package.ID)) throw new Exceptions.IDException("A client ID Not Found", package.ID);

            package.ID = getPackageID();

            PackagesList.Add(package);
            XMLTools.SaveListToXMLSerializer(PackagesList, packagePath);

            throw new Exception("test");
        }



        #region XmlElement
        public void AddStation(Station station) // נבדק ועובד
        {
           
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);
            XElement stationElem = (from d in stationRootElem.Elements()
                                    where Convert.ToInt32(d.Element("ID").Value) == station.ID
                                    select d).FirstOrDefault();

            if (stationElem == null)
            {
                XElement newStation = new XElement("Station",
                    new XElement("ID", station.ID),
                    new XElement("Name", station.Name),
                    new XElement("Latitude", station.Latitude),
                    new XElement("Longitude", station.Longitude),
                    new XElement("ChargeSlots", station.ChargeSlots)
                    );

                stationRootElem.Add(newStation);
                XMLTools.SaveListToXmlElement(stationRootElem, stationPath);
            }
            else
                throw new Exceptions.IDException("A Station ID already exists", station.ID);

        }



        public Station StationById(int id) // נבדק ועובד
        {
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);
            XElement stationElem = (from s in stationRootElem.Elements()
                                    where Convert.ToInt32(s.Element("ID").Value) == id
                                    select s
                                    ).FirstOrDefault();

            if (stationElem == null) throw new Exceptions.IDException("A Station ID Not Found", id);

            Station station = new Station()
            {
                ID = Convert.ToInt32(stationElem.Element("ID").Value),
                Name = stationElem.Element("Name").Value.ToString(),
                Latitude = Convert.ToDouble(stationElem.Element("Latitude").Value),
                Longitude = Convert.ToDouble(stationElem.Element("Longitude").Value),
                ChargeSlots = Convert.ToInt32(stationElem.Element("ChargeSlots").Value)
            };

            return station;
        }

        public IEnumerable<Station> StationsFilter(Predicate<Station> match) // עובד
        {
            List<Station> stations = new List<Station>();
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);

            stations = (from s in stationRootElem.Elements()
                        select new Station()
                        {
                            ID = Convert.ToInt32(s.Element("ID").Value),
                            Name = s.Element("Name").Value.ToString(),
                            Latitude = Convert.ToDouble(s.Element("Latitude").Value),
                            Longitude = Convert.ToDouble(s.Element("Longitude").Value),
                            ChargeSlots = Convert.ToInt32(s.Element("ChargeSlots").Value)
                        }).ToList();

            return stations.FindAll(match);
        }

        public IEnumerable<Station> StationsList() // עובד
        {
            List<Station> stations = new List<Station>();
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);

            stations = (from s in stationRootElem.Elements()
                        select new Station()
                        {
                            ID = Convert.ToInt32(s.Element("ID").Value),
                            Name = s.Element("Name").Value.ToString(),
                            Latitude = Convert.ToDouble(s.Element("Latitude").Value),
                            Longitude = Convert.ToDouble(s.Element("Longitude").Value),
                            ChargeSlots = Convert.ToInt32(s.Element("ChargeSlots").Value)
                        }).ToList();

            return stations;
        }

        private void finishChargingStation(int stationID) //
        {
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);
            XElement stationElem = (from d in stationRootElem.Elements()
                                    where int.Parse(d.Element("ID").Value) == stationID
                                    select d).FirstOrDefault();

            if (stationElem == null) throw new Exceptions.IDException("A Station ID Not Found", stationID);

            stationElem.Element("ChargeSlots").Value = (int.Parse(stationElem.Element("ChargeSlots").Value) + 1).ToString();

            XMLTools.SaveListToXmlElement(stationRootElem, stationPath);
        }


        public Station ChargingStation(int stationID) // עובד
        {
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);
            XElement stationElem = (from d in stationRootElem.Elements()
                                    where int.Parse(d.Element("ID").Value) == stationID
                                    select d).FirstOrDefault();

            if (stationElem == null) throw new Exceptions.IDException("A Station ID Not Found", stationID);

            stationElem.Element("ChargeSlots").Value = (int.Parse(stationElem.Element("ChargeSlots").Value)-1).ToString();

            XMLTools.SaveListToXmlElement(stationRootElem, stationPath);
            return StationById(Convert.ToInt32( stationElem.Element("ID").Value));
        }

        public void DeleteStation(int id) // עובד
        {
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);
            XElement stationElem = (from d in stationRootElem.Elements()
                                    where int.Parse(d.Element("ID").Value) == id
                                    select d).FirstOrDefault();
            if (stationElem == null) throw new Exceptions.IDException("A Station ID Not Found", id);

            stationElem.Remove();
            XMLTools.SaveListToXmlElement(stationRootElem, stationPath);
        }



        public double[] PowerConsumptionByDrone()//
        {
            XElement configRootElem = XMLTools.LoadListFromXmlElement(configPath);

            double[] arr = new double[5];
            arr[0] = Convert.ToInt32(configRootElem.Element("PowerAvailableDrone").Value);
            arr[1] = Convert.ToInt32(configRootElem.Element("PowerLightDrone").Value);
            arr[2] = Convert.ToInt32(configRootElem.Element("PowerMediumDrone").Value);
            arr[3] = Convert.ToInt32(configRootElem.Element("PowerHeavyDrone").Value);
            arr[4] = Convert.ToInt32(configRootElem.Element("ChargeRate").Value);
            return arr;
        }

        private int getPackageID()//
        {
            XElement configRootElem = XMLTools.LoadListFromXmlElement(configPath);
            XElement stationElem = configRootElem.Element("PackageId");

            int id = Convert.ToInt32(stationElem.Value);

            stationElem.Value = (Convert.ToInt32(stationElem.Value) + 1).ToString();

            XMLTools.SaveListToXmlElement(configRootElem, configPath);
            return id;
        }
        #endregion











        public Client ClientById(int id)
        {
            var ClientList = XMLTools.LoadListFromXMLSerializer<DO.Client>(clientPath);

            if (!ClientList.Any(c => c.ID == id))
                throw new Exceptions.IDException("client not found", id);

            Client client = ClientList.Find(c => c.ID == id);

            return client;
        }

        public IEnumerable<Client> ClientsFilter(Predicate<Client> match)
        {
            var ClientList = XMLTools.LoadListFromXMLSerializer<DO.Client>(clientPath);

            return ClientList.FindAll(match);

        }

        public IEnumerable<Client> ClientsList()
        {
            return XMLTools.LoadListFromXMLSerializer<DO.Client>(clientPath);
        }

        public void DeleteClient(int id)
        {
            var ClientList = XMLTools.LoadListFromXMLSerializer<DO.Client>(clientPath);
            

            if (!ClientList.Any(c => c.ID == id))
                throw new Exceptions.IDException("client not found", id);

            Client client = ClientList.Find(c => c.ID == id);

            ClientList.Remove(client);

            XMLTools.SaveListToXMLSerializer(ClientList, clientPath);

        }

        public void DeleteDrone(int id) //
        {
            var droneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);

            if (!droneList.Any(d => d.ID == id))
                throw new Exceptions.IDException("Drine not found", id);

            Drone drone = droneList.Find(c => c.ID == id);
            droneList.Remove(drone);
            XMLTools.SaveListToXMLSerializer(droneList, clientPath);
        }

        public void DeleteDroneCharge(DroneCharge droneCharge) //
        {
            List<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(droneChargePath);
            if (!droneChargeList.Any(x => x.DroneId == droneCharge.DroneId)) { throw new DO.Exceptions.IDException("id to remove not found", droneCharge.DroneId); }
            droneChargeList.Remove(droneCharge);
            XMLTools.SaveListToXMLSerializer(droneChargeList, droneChargePath);
        }


      

      

        public Drone DroneById(int id)//
        {
            List<Drone> droneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);

            if (!droneList.Any(d => d.ID == id))
                throw new Exceptions.IDException("Drone not found", id);

            Drone drone = droneList.Find(d => d.ID == id);
            return drone;
        }

        public void DroneCharge(Drone drone, int stationID)//
        {
            List<Drone> droneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);

            if (!droneList.Any(d => d.ID == drone.ID))
                throw new Exceptions.IDException("Drone not found", drone.ID);

            List<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(droneChargePath);

            Station station = ChargingStation(stationID); // The station that the user choose

            DroneCharge droneCharg = new DroneCharge() // Initialization of a new instance for DroneCharge
            {
                DroneId = drone.ID,
                StationId = stationID,
                ChargingStartTime = DateTime.Now
            };
            
            droneChargeList.Add(droneCharg); // Add the instance to the list
            XMLTools.SaveListToXMLSerializer(droneChargeList, droneChargePath);
        }

        public DroneCharge DroneChargeByIdDrone(int id)//
        {
            List<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(droneChargePath);

            if (!droneChargeList.Any(d => d.DroneId == id))
                throw new Exceptions.IDException("Drone not found in Chargeing", id);

            DroneCharge drone = droneChargeList.Find(d => d.DroneId == id);
            return drone;
        }

        public IEnumerable<DroneCharge> DroneChargeFilter(Predicate<DroneCharge> match)//
        {
            List<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(droneChargePath);
            return droneChargeList.FindAll(match);
        }

        public IEnumerable<DroneCharge> droneChargesList()//
        {
            return XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(droneChargePath);
        }

        public IEnumerable<Drone> DronedFilter(Predicate<Drone> match)//
        {
            List<Drone> droneChargeList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            return droneChargeList.FindAll(match);
        }

        public IEnumerable<Drone> DroneList()//
        {
            return XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
        }


        public IEnumerable<Package> PackageList()
        {
            return XMLTools.LoadListFromXMLSerializer<DO.Package>(packagePath);
        }

        public IEnumerable<Package> PackagesFilter(Predicate<Package> match)
        {
            var PackageList = XMLTools.LoadListFromXMLSerializer<DO.Package>(packagePath);

            return PackageList.FindAll(match);
        }


        public void FinishCharging(DroneCharge droneCharge)//
        {
            List<DroneCharge> droneList = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(dronePath);
            if (!droneList.Any(d => d.DroneId == droneCharge.DroneId))
                throw new Exceptions.IDException("Drone not found", droneCharge.DroneId);

            List<DroneCharge> droneChargeList = XMLTools.LoadListFromXMLSerializer<DO.DroneCharge>(droneChargePath);
            Drone drone = DroneById(droneCharge.DroneId);

            finishChargingStation(droneCharge.StationId);

            droneChargeList.Remove(droneCharge); // Deleting the instance from the list
            XMLTools.SaveListToXMLSerializer(droneChargeList, droneChargePath);
        }

        public void packageToDrone(Package package, int DroneID)
        {
            List<Package> packagesList = XMLTools.LoadListFromXMLSerializer<DO.Package>(packagePath);
            if (!packagesList.Any(d => d.ID == package.ID))
                throw new Exceptions.IDException("Package not found", package.ID);

            List<Drone> droneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dronePath);
            if (!droneList.Any(d => d.ID == DroneID))
                throw new Exceptions.IDException("Drone not found", DroneID);


            Package packageTemp = package;
            packageTemp.DroneId = DroneID; //  Updates in temp of  package
            packageTemp.Associated = DateTime.Now;//  package

            packagesList.Add(packageTemp); // Add temp to list and delete old
            packagesList.Remove(package);

            XMLTools.SaveListToXMLSerializer(packagesList, packagePath);
        }

        public void PickedUpByDrone(Package package)//
        {
            List<Package> packagesList = XMLTools.LoadListFromXMLSerializer<DO.Package>(packagePath);
            if (!packagesList.Any(d => d.ID == package.ID))
                throw new Exceptions.IDException("Package not found", package.ID);

            Package packageTemp = package;
            packageTemp.PickedUp = DateTime.Now; // Updates in temp of drone
            packagesList.Add(packageTemp); // Add temp to list and delete old
            packagesList.Remove(package);

            XMLTools.SaveListToXMLSerializer(packagesList, packagePath);
        }

      
      



        public void DeletePackage(int id)
        {
            var PackageList = XMLTools.LoadListFromXMLSerializer<DO.Package>(packagePath);


            if (!PackageList.Any(p => p.ID == id))
                throw new Exceptions.IDException("Package not found", id);

            Package package = PackageList.Find(p => p.ID == id);

            PackageList.Remove(package);

            XMLTools.SaveListToXMLSerializer(PackageList, packagePath);
        }


        public Package PackageById(int id)
        {
            var PackageList = XMLTools.LoadListFromXMLSerializer<DO.Package>(packagePath);

            if (!PackageList.Any(p => p.ID == id))
                throw new Exceptions.IDException("Package not found", id);
            Package package = PackageList.Find(p => p.ID == id);
            return package;
        }

      

        public void DeliveredToClient(Package package)
        {
            List<DO.Package> PackageList = XMLTools.LoadListFromXMLSerializer<DO.Package>(packagePath);

            if (!PackageList.Any(p => p.ID == package.ID))
                throw new Exceptions.IDException("Package not found", package.ID);

            var myPackage = PackageList.First(x => x.ID == package.ID);

            var index = PackageList.IndexOf(package);

            package.Delivered = DateTime.Now;

            PackageList[index] = package;

            XMLTools.SaveListToXMLSerializer(PackageList, packagePath);

        }

    }
}
