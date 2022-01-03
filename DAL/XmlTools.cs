using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace xml
{
    class XMLTools
    {
        private static string dirPath = @"..\..\..\..\Data\";
     
        static XMLTools()
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }
        //public XMLTools() { }

        public static void SetStationListToFile(IEnumerable<DO.Station> stations, string stationPath)
        {
            XElement rootElemStations;

            var v = from station in stations
                    select new XElement("student",
                                                    new XElement("id", station.ID),
                                                    new XElement("name", station.Name),
                                                    new XElement("Latitude", station.Latitude),
                                                    new XElement("Longitude", station.Longitude),
                                                    new XElement("ChargeSlots", station.ChargeSlots)
                                                    
                                                );
            rootElemStations = new XElement("students", v);

            rootElemStations.Save(dirPath+stationPath);
        }


        public static void SaveListToXmlElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dirPath + filePath);
            }
            catch (Exception ex)
            {
                throw new DO.Exceptions.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }



        public static XElement LoadListFromXmlElement(string filePath)
        {
            try
            {
                if (File.Exists(dirPath + filePath))
                {
                    return XElement.Load(dirPath + filePath);
                }
                else
                {
                    XElement rootElem = new XElement(dirPath + filePath);
                    rootElem.Save(dirPath + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DO.Exceptions.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }





    }
}
