using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Bear
{
    public class CanClient : MonoBehaviour
    {
        public string url;
        public int port;
        public bool ConnectOnAwake;
        public bool Connected;
        private Socket socket;
        private Task readingTask;
        

        private static CanClient _instance;
        private byte[] buffer = new byte[13];
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

            set {
                _instance = value;
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

            Instance = this;

            // Set up the endpoint for the remote device.
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(url), port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var awaiter = socket.ConnectAsync(remoteEndPoint).GetAwaiter();
            awaiter.OnCompleted(OnConnected);


        }

        private void OnConnected()
        {
            Debug.Log("Connected");
            readingTask = Task.Run(ReceiveLoop);
        }

        private void ReceiveLoop()
        {
            try {
                while (socket.Connected)
                {
                    int read = socket.Receive(buffer);
                    OnReceiveData(buffer);
                }
                Debug.Log("End Receiving");
            }
            catch (Exception e) {
                Debug.Log(e);
            }

        }

        private void OnReceiveData(byte[] data) {
            StringBuilder sb = new StringBuilder();
            sb.Append("Received: ");
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i] + " ");
            }
            Debug.Log(sb);

        //    socket.Send(data);
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