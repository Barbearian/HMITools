using UnityEngine;
using IngameDebugConsole;
namespace Bear
{
    public class CanClientTestor : MonoBehaviour
    {
        public static CanClientTestor manager;
        public CanData[] data;

        public void Awake()
        {
            manager = this;
        }

        [ConsoleMethod("Connect","Try to Connect with url and ip")]
        public static void Connect(string url,int port)
        {
            CanClient.Instance.Connect(url,port);
        }

        [ConsoleMethod("SendData", "Try to send custom data")]
        public static void SendData(byte[] data)
        {
            data = new byte[] { 0,0,0,0,0,0,0,0,0,0,0,0,0 };
            CanClient.Instance.SendData(data);
            //Debug.Log(data[0]);
        }

        [ConsoleMethod("SendCustomData", "Try to send custom data")]

        public static void SendCustomData(int index) {

            var Data = manager.data;
            var sentData = Data[index].data;
            //sentData = new byte[]{136,0,0,0,1,1,2,3,4,5,6,7,9 };
            CanClient.Instance.SendData(sentData);
        }

        
    }

    [System.Serializable]
    public struct CanData {
        public byte[] data;
    }

    
}