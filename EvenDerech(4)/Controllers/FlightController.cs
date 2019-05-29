﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using EvenDerech_4_.Models;
using System.Text;
using System.Xml;

namespace EvenDerech_4_.Controllers
{
    public class FlightController : Controller
    {
        public ActionResult StartScreen() {
            return View();
        }
        public ActionResult LocatePlane(string ip, int port)
        {
            ServerConnect.ServerInstance.closeServer();
            ServerConnect.ServerInstance.connectToServer(port, ip);
            ServerConnect.ServerInstance.updateAttributes();
            ViewBag.Longtitude = ServerConnect.ServerInstance.Lon;
            ViewBag.Latitude = ServerConnect.ServerInstance.Lat;
            return View();
        }
        // GET: Flight
        public ActionResult FlightPath(string ip, int port, int rate)
        {
            ServerConnect.ServerInstance.closeServer();
            ServerConnect.ServerInstance.connectToServer(port, ip);
            Session["time"] = rate;
            return View();
        }
        // GET: Flight
        public ActionResult SaveFlightData(string ip, int port, int rate,int duration, string path)
        {
            return View();
        }
        // GET: Flight
        public ActionResult LoadFlightData(string path,int? rate)
        {
            return View();
        }

        //Idan
        [HttpPost]
        public string GetData()
        {
            //if (ServerConnect.ServerInstance.)
            ServerConnect.ServerInstance.updateAttributes();
            float lat = ServerConnect.ServerInstance.Lat;
            float lon = ServerConnect.ServerInstance.Lon;

            return ToXml(lat, lon);
        }

        //Idan
        private string ToXml(float lat, float lon)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("LongtitudeLatitude");
            writer.WriteElementString("Longtitude", lon.ToString());
            writer.WriteElementString("Latitude", lat.ToString());
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }
    }
}