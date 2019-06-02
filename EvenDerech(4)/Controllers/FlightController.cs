using System.Web.Mvc;
using System.Net;
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
            System.Net.IPAddress IP = null;
            //check which kind of argument we received.
            bool check = IPAddress.TryParse(ip,out IP);
            if (check == true)
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
            return (RedirectToAction("LoadFlightData", new {path = ip, rate = port }));            
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
            string absolutePath = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + path + ".txt";
            
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
                Session["time"] = rate;
                string absolutePath = System.AppDomain.CurrentDomain.BaseDirectory + @"\" + path + ".txt";
                FileHandler.GetFileHandlerInstance.FilePath = absolutePath;
                FileHandler.GetFileHandlerInstance.DetailsRead = FileHandler.GetFileHandlerInstance.ReadData();
                FileHandler.GetFileHandlerInstance.ArrayIndex = 0;
                return View();
        }

        [HttpPost]
        public string DrawFromSaved()
        {
            FileHandler handler = FileHandler.GetFileHandlerInstance;
            int maxLines = handler.DetailsRead.Length;
            if (handler.ArrayIndex < maxLines)
            {   
                string[] line = handler.DetailsRead[handler.ArrayIndex].Split(',');
                handler.ArrayIndex = handler.ArrayIndex + 1;
                float lat, lon;
                // if one of the paramters in invalid - finish the reading.
                if(!float.TryParse(line[0], out lat) || !float.TryParse(line[1], out lon))
                {
                    return ToXml(200, 200);
                }
                return ToXml(lat, lon);
            }
            return ToXml(200,200);

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