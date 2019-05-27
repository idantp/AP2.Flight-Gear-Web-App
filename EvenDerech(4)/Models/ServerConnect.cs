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
        bool connected;
        string getLon;
        string getLat;
        Nullable<float> lon;
        Nullable<float> lat;

        public ServerConnect()
        {
            lat = lon = 0;
            connected = false;
            getLat = "get /position/latitude-deg\r\n";
            getLon = "get /position/longitude-deg\r\n";
        }

        //Longtitude property
        public Nullable<float> Lon
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
        public Nullable<float> Lat
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

        private Nullable<float> getCoordinate(string coordType, NetworkStream stream, BinaryReader reader)
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
                    else { return null; }
                }
                catch
                {
                    return null;
                }

            }
            return null;
        }


        public void connectToServer(int portNumber, string ipAddress)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
            this.tcpClient = new TcpClient();
            tcpClient.Connect(endPoint);
            this.connected = true;
            using (NetworkStream stream = tcpClient.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                this.lon = getCoordinate(getLon, stream, reader);
                this.lat = getCoordinate(getLat, stream, reader);
            }

        }
        //hope this works.
        public void closeServer() {
            if (connected) {
                tcpClient.Close();
                connected = false;
            }
        }
    }
}