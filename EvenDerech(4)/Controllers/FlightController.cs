using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace EvenDerech_4_.Controllers
{
    public class FlightController : Controller
    {
        // GET: Flight
        public ActionResult LocatePlane(string ip, int? port)
        {
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