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

        DalXml()
        {
            DalObject.DataSource.Initialize();
            xml.XMLTools.SetStationListToFile(DalObject.DataSource.StationList, stationPath);
        }

       





        public void AddClient(Client client)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(Drone drone)
        {
            throw new NotImplementedException();
        }

        public int AddPackage(Package package)
        {
            throw new NotImplementedException();
        }

        public void AddStation(Station station)
        {
           
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);
            XElement stationElem = (from d in stationRootElem.Elements()
                                    where int.Parse(d.Element("ID").Value) == station.ID
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


        public Station ChargingStation(int stationID)
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


        public Client ClientById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> ClientsFilter(Predicate<Client> match)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> ClientsList()
        {
            throw new NotImplementedException();
        }

        public void DeleteClient(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteDrone(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteDroneCharge(DroneCharge droneCharge)
        {
            throw new NotImplementedException();
        }

        public void DeletePackage(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int id)
        {
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);
            XElement stationElem = (from d in stationRootElem.Elements()
                                    where int.Parse(d.Element("ID").Value) == id
                                    select d).FirstOrDefault();
            if (stationElem == null) throw new Exceptions.IDException("A Station ID Not Found", id);

            stationElem.Remove();
            XMLTools.SaveListToXmlElement(stationRootElem, stationPath);
        }

        public void DeliveredToClient(Package package)
        {
            throw new NotImplementedException();
        }

        public Drone DroneById(int id)
        {
            throw new NotImplementedException();
        }

        public void DroneCharge(Drone drone, int stationID)
        {
            throw new NotImplementedException();
        }

        public DroneCharge DroneChargeByIdDrone(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> DroneChargeFilter(Predicate<DroneCharge> match)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> droneChargesList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> DronedFilter(Predicate<Drone> match)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> DroneList()
        {
            throw new NotImplementedException();
        }

        public void FinishCharging(DroneCharge droneCharge)
        {
            throw new NotImplementedException();
        }

        public Package PackageById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Package> PackageList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Package> PackagesFilter(Predicate<Package> match)
        {
            throw new NotImplementedException();
        }

        public void packageToDrone(Package package, int DroneID)
        {
            throw new NotImplementedException();
        }

        public void PickedUpByDrone(Package package)
        {
            throw new NotImplementedException();
        }

        public double[] PowerConsumptionByDrone()
        {
            throw new NotImplementedException();
        }

        public Station StationById(int id)
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

        public IEnumerable<Station> StationsFilter(Predicate<Station> match)
        {
            List<Station> stations = new List<Station>();
            XElement stationRootElem = XMLTools.LoadListFromXmlElement(stationPath);

            //stations = (from s in stationRootElem.Elements()
                        
                        //let   new Station()
                        //{
                        //    ID = Convert.ToInt32(s.Element("ID").Value),
                        //    Name = s.Element("Name").Value.ToString(),
                        //    Latitude = Convert.ToDouble(s.Element("Latitude").Value),
                        //    Longitude = Convert.ToDouble(s.Element("Longitude").Value),
                        //    ChargeSlots = Convert.ToInt32(s.Element("ChargeSlots").Value)
                        //}).ToList();

            return stations;
        }

        public IEnumerable<Station> StationsList()
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


    }
}
