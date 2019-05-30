using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using EvenDerech_4_.Models;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace EvenDerech_4_.Controllers
{
    public class FlightController : Controller
    {
        public ActionResult StartScreen() {
            return View();
        }
        public ActionResult LocatePlane(string ip, int port)
        {
            //always close the server if needed before each Action.
            ServerConnect.ServerInstance.closeServer();
            //connect to the server again.
            ServerConnect.ServerInstance.connectToServer(port, ip);
            //get the attributes as of now.
            ServerConnect.ServerInstance.updateAttributes();
            //update the variables in the view.
            ViewBag.Longtitude = ServerConnect.ServerInstance.Lon;
            ViewBag.Latitude = ServerConnect.ServerInstance.Lat;
            return View();
        }

        // GET: Flight
        public ActionResult FlightPath(string ip, int port, int rate)
        {
            //first close if needed and only then connect to it.
            ServerConnect.ServerInstance.closeServer();
            ServerConnect.ServerInstance.connectToServer(port, ip);
            //set the time variable in the view to be what was given as the rate.
            Session["time"] = rate;
            return View();
        }
        // GET: Flight
        public ActionResult SaveFlightData(string ip, int port, int rate, int duration, string path)
        {
            
            ServerConnect.ServerInstance.closeServer();
            ServerConnect.ServerInstance.connectToServer(port, ip);
            FileHandler.GetFileHandlerInstance.FirstWrite = true;
            string absolutePath = AppDomain.CurrentDomain.BaseDirectory + @"\" + path + ".txt";
            
            FileHandler.GetFileHandlerInstance.FilePath = absolutePath;
            //set the time variable in the view to be what was given as the rate.
            Session["time"] = rate;
            Session["duration"] = duration;
            return View();
        }

        [HttpPost]
        public string WriteData()
        {
            //update attributes before sending them to ToXml.
            ServerConnect.ServerInstance.updateAttributes();
            float lat = ServerConnect.ServerInstance.Lat;
            float lon = ServerConnect.ServerInstance.Lon;
            float rudder = ServerConnect.ServerInstance.Rudder;
            float throttle = ServerConnect.ServerInstance.Throttle;
            string data = lat.ToString() + "," + lon.ToString() + "," + rudder.ToString() + "," + throttle.ToString();
            FileHandler.GetFileHandlerInstance.DetailsLine = data;
            FileHandler.GetFileHandlerInstance.SaveDataToFile();
            return ToXml(lat, lon);
        }

        // GET: Flight
        public ActionResult LoadFlightData(string path,int rate)
        {
            return View();
        }

        //Returns the data necessary for flightpath.
        [HttpPost]
        public string GetData()
        {
            //update attributes before sending them to ToXml.
            ServerConnect.ServerInstance.updateAttributes();
            float lat = ServerConnect.ServerInstance.Lat;
            float lon = ServerConnect.ServerInstance.Lon;

            return ToXml(lat, lon);
        }


        //returns a stringbuilder made of the necessary attributes for flightpath.
        private string ToXml(float lat, float lon)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);
            //write them into the string builder.
            writer.WriteStartDocument();
            writer.WriteStartElement("FlightDetails");
            writer.WriteElementString("Longtitude", lon.ToString());
            writer.WriteElementString("Latitude", lat.ToString());
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }
    }
}