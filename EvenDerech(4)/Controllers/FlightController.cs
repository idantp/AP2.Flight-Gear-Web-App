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
        public ActionResult StartScreen() {
            return View();
        }
        public ActionResult LocatePlane(string ip, int port)
        {   
            ServerConnect.ServerInstance.connectToServer(port, ip);
            ViewBag.Longtitude = ServerConnect.ServerInstance.Lon;
            ViewBag.Latitude = ServerConnect.ServerInstance.Lat;
            //todo
            ServerConnect.ServerInstance.closeServer();
            return View();
        }
        // GET: Flight
        public ActionResult FlightPath(string ip, int port, int rate)
        {
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

        //[HttpPost]
        //public string GetData()
        //{
        //    //todo singleton
        //    return ToXml(server);


        //}

        //private string ToXml(ServerConnect server)
        //{

        //}
    }
}