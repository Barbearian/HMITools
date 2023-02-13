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
        public bool ConnectOnAwake;
        private Socket socket;
        private static CanClient _instance;
        public static CanClient Instance {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("CanClient").AddComponent<CanClient>();
                    DontDestroyOnLoad(_instance);

                }
                return _instance;

            }
        }

        private void Awake()
        { 
            if (ConnectOnAwake)
                Connect();
        }

        public void Connect()
        {
            if (socket != null) {
                Disconnect();
            }

            // Set up the endpoint for the remote device.
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(url), port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(remoteEndPoint);

            

        }

        public void Connect(string url, int port)
        {
            this.url = url;
            this.port = port;
            Connect();
        }

        public void SendData(byte[] data){
            socket.Send(data);
        }

        public void Disconnect()
        {
            Disconnect(socket);
        }

        private void Disconnect(Socket socket) {
            socket?.Shutdown(SocketShutdown.Both);
            socket?.Close();
        }

        public void OnDestroy()
        {
            Disconnect();
        }
    }
}