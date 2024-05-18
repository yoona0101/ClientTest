using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Server
{
    public class ServerClass
    {
        /// <summary>
        /// Запуск серверной части программы
        /// </summary>
        /// <returns></returns>
        public async Task StartAppServer()
        {
            await Task.Run(delegate
            {
                Load();
            });
        }
        public string text;
        /// <summary>
        /// Получение данных от клиента
        /// </summary>
        public void Load()
        {
            Console.OutputEncoding = Encoding.GetEncoding(866);
            Console.WriteLine("Однопоточный сервер запущен");
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8888);
            Socket sock = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sock.Bind(ipEndPoint);
                sock.Listen(10);
                while (true)
                {
                    Console.WriteLine($"Слушаем, порт {ipEndPoint}");
                    Socket s = sock.Accept();
                    string data = null;
                    byte[] bytes = new byte[1024];
                    int bytesCount = s.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesCount);
                    Console.Write("Данные от клиента: " + data + "\n\n");
                    StreamWriter streamWriter = new StreamWriter("data.txt", append: true);
                    streamWriter.WriteLine("Данные от клиента: " + data + "\n Текущее время: " + DateTime.Now.Hour + " " + DateTime.Now.Minute);
                    string reply = "Размер: " + data.Length.ToString() + " символов";
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    s.Send(msg);
                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Соединение завершено.");
                        break;
                    }
                    s.Shutdown(SocketShutdown.Both);
                    s.Close();
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }


            //public void CreateFile()
            //{
            //    StreamWriter streamWriter = new StreamWriter("data.txt", append: true);
            //    streamWriter.WriteLine("Данные от клиента: " + text + "\n Текущее время: " + DateTime.Now.Hour + " " + DateTime.Now.Minute);

            //    streamWriter.Close();
            //}
            //public void CreateFileDate()
            //{
            //    //string filename = $"messager_{DateTime.Now.ToString("yyyyMMdd")}.txt";
            //    StreamWriter streamWriter = new StreamWriter($"messager_{DateTime.Now.ToString("yyyyMMdd")}.txt", append: true);

            //    streamWriter.WriteLine("Данные от клиента: " + text + "\n Текущее время: " + DateTime.Now.Hour + " " + DateTime.Now.Minute);

            //    streamWriter.Close();

            //}
        }
    }
}