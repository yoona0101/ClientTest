using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;
using Server;


namespace App
{
    internal class appClientServerClass
    {
        static void Main(string[] args)
        {
            ServerClass server = new ServerClass();
            server.StartAppServer();
            //server.CreateFile();
            //server.CreateFileDate();
            ClientClass client = new ClientClass();
            client.OpenFlags();


        }
    }
}
