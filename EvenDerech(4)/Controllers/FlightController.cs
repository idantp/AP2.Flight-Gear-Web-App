using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using EvenDerech_4_.Models;

namespace EvenDerech_4_.Controllers
{
    public class FlightController : Controller
    {
        //GET: Flight
        private float normLon(float lon)
        {
            return (lon + 180) / 360 * 100;
        }
        private float normLat(float lat)
        {
            return (lat + 90) / 180 * 100;
        }
        public ActionResult StartScreen() {
            return View();
        }
        public ActionResult LocatePlane(string ip, int port)
        {   
            ServerConnect server = new ServerConnect();
            server.connectToServer(port, ip);
            ViewBag.Longtitude = server.Lon;
            ViewBag.Latitude = server.Lat;
            ViewBag.lat1 = normLat(server.Lat);
            ViewBag.lon1 = normLon(server.Lon);
            server.closeServer();
            return View();
        }
        // GET: Flight
        public ActionResult FlightPath(string ip, int? port,int? rate)
        {
            return View();
        }
        // GET: Flight
        public ActionResult SaveFlightData(string ip, int? port, int? rate,int? duration,string path)
        {
            return View();
        }
        // GET: Flight
        public ActionResult LoadFlightData(string path,int? rate)
        {
            return View();
        }
    }
}