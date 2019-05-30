using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace EvenDerech_4_.Models
{
    public class ServerConnect
    {
        private TcpClient tcpClient;
        NetworkStream stream;
        BinaryReader reader;
        bool connected;
        string getLon;
        string getLat;
        string getRudder;
        string getThrottle;
        float lon;
        float lat;
        float rudder;
        float throttle;

        private ServerConnect()
        {
            lat = lon = 0;
            rudder = 0;
            throttle = 0;
            connected = false;
            getLat = "get /position/latitude-deg\r\n";
            getLon = "get /position/longitude-deg\r\n";
            getRudder = "get /controls/flight/rudder\r\n";
            getThrottle = "get /controls/engines/current-engine/throttle\r\n";
        }

        //Singleton
        private static ServerConnect serverInstance = null;
        public static ServerConnect ServerInstance {
            get {
                if (serverInstance == null) {
                    serverInstance = new ServerConnect();
                }
                return serverInstance;
            }
        }

        public float Rudder
        {
            get => rudder;
            set => rudder = value;
        }


        public float Throttle
        {
            get => throttle;
            set => throttle = value;
        }

        //Longtitude property
        public float Lon
        {
            set
            {
                this.lon = value;
            }
            get
            {
                return this.lon;
            }
        }
        //Latitude property
        public float Lat
        {
            set
            {
                this.lat = value;
            }
            get
            {
                return this.lat;
            }
        }

        //Gets a specific path and requests its value from the server and returns it.
        private float getDetail(string coordType, NetworkStream stream, BinaryReader reader)
        {
            if (connected)
            {
                //request from server
                Byte[] bytesToWrite = Encoding.ASCII.GetBytes(coordType);
                stream.Write(bytesToWrite, 0, bytesToWrite.Length);
                string line = "";
                char c;
                //read the value from what was returned by the server.
                try
                {
                    while ((c = reader.ReadChar()) != '\n')
                    {
                        line += c;
                    }
                    if (line != "")
                    {
                        string[] temp = line.Split('\'');
                        return float.Parse(temp[1]);
                    }
                    else { return 0; }
                }
                catch
                {
                    return 0;
                }

            }
            return 0;
        }

        //update the attributes (lon, lat, rudder and throttle). Reads from the server each time and updates the memebers.
        public void updateAttributes() {
                this.lon = getDetail(getLon, stream, reader);
                this.lat = getDetail(getLat, stream, reader);
                Rudder = getDetail(getRudder, stream, reader);
                Throttle = getDetail(getThrottle, stream, reader);
        }

        //Connects to the server in order to read and write to it.
        public void connectToServer(int portNumber, string ipAddress)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
            this.tcpClient = new TcpClient();
            //attempt to connect and loop until connected.
            while (!connected)
            {
                try {
                    //once connected, get the stream and reader and update connected member to true.
                    tcpClient.Connect(endPoint);
                    this.stream = tcpClient.GetStream();
                    this.reader = new BinaryReader(stream);
                    this.connected = true;
                }
                catch (Exception) { }
            }

        }
        //takes care of when we disconnect from the server, to close everything we opened.
        public void closeServer() {
            if (connected) {
                stream.Close();
                reader.Close();
                tcpClient.Close();
                connected = false;
            }
        }
    }
}