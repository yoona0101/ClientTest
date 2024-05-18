using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Client
{
    public class ClientClass
    {

        /// <summary>
        /// Открытие порта
        /// </summary>
        public void OpenFlags()
        {
            Console.OutputEncoding = Encoding.GetEncoding(866);
            try
            {
                Communicate("localhost", 8888);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Ввод сообщения 
        /// </summary>
        /// <param name="hostname">IP-адрес</param>
        /// <param name="port">номер порта</param>
        private static void Communicate(string hostname, int port)
        {
            byte[] bytes = new byte[1024];
            IPHostEntry ipHost = Dns.GetHostEntry(hostname);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEndPoint);
            Console.Write("Введите сообщение: ");
            string message = Console.ReadLine();
            Console.WriteLine($"Подключаемся к порту {socket.RemoteEndPoint.ToString()} ");
            byte[] data = Encoding.UTF8.GetBytes(message);
            int bytesSent = socket.Send(data);
            int bytesRec = socket.Receive(bytes);
            Console.WriteLine($"\n Ответ сервера:\n {Encoding.UTF8.GetString(bytes, 0, bytesRec)} \n \n ");
            if (message.IndexOf("<The End>") == -1)
            {
                Communicate(hostname, port);
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

        }

    }

}

