using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Bear
{
    public class CanClient : MonoBehaviour
    {
        public string url;
        public int port;
        public void Connect()
        {
            // Set up the endpoint for the remote device.
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(url), port);

            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(remoteEndPoint);

        }

        public void Disconnect()
        {

        }

        public void OnDestroy()
        {
            
        }
    }
}