using UnityEngine;
using IngameDebugConsole;
namespace Bear
{
    public class CanClientTestor : MonoBehaviour
    {
        [ConsoleMethod("Connect","Try to Connect with url and ip")]
        public static void Connect(string url,int port)
        {
            CanClient.Instance.Connect(url,port);
        }

        [ConsoleMethod("SendData", "Try to Connect with url and ip")]
        public static void SendData(byte[] data)
        {
            data = new byte[] { 0,0,0,0,0,0,0,0,0,0,0,0,0 };
            CanClient.Instance.SendData(data);
            //Debug.Log(data[0]);
        }
    }
}