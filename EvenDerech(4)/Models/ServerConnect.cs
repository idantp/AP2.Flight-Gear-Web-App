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
            getThrottle = "get /controls/flight/throttle\r\n";
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

        private float getDetail(string coordType, NetworkStream stream, BinaryReader reader)
        {
            if (connected)
            {
                Byte[] bytesToWrite = Encoding.ASCII.GetBytes(coordType);
                stream.Write(bytesToWrite, 0, bytesToWrite.Length);
                string line = "";
                char c;
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
                    //todo
                    else { return 0; }
                }
                catch
                {
                    //todo
                    return 0;
                }

            }
            return 0;
        }


        public void connectToServer(int portNumber, string ipAddress)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
            this.tcpClient = new TcpClient();
            while (!this.tcpClient.Connected)
            {
                try { tcpClient.Connect(endPoint); }
                catch (Exception) { }
            }
            this.connected = true;
            using (NetworkStream stream = tcpClient.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                this.lon = getDetail(getLon, stream, reader);
                this.lat = getDetail(getLat, stream, reader);
                // Todo: does not work !
                Rudder = getDetail(getRudder, stream, reader);
                Throttle = getDetail(getThrottle, stream, reader);
            }

        }
        public void closeServer() {
            if (connected) {
                tcpClient.Close();
                connected = false;
                //todo do we need to set the singleton to null?
            }
        }
    }
}