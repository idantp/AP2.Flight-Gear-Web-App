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
        // GET: Flight
        public ActionResult LocatePlane(string ip, int? port)
        {
            ServerConnect server = new ServerConnect();
            server.connectToServer(5402, "127.0.0.1");
            ViewBag.Longtitude = server.Lon;
            ViewBag.Latitude = server.Lat;
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