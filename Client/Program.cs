using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class Program
{
    static void Main()
    {
        //Socket settings
        Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPAddress serverAdress = IPAddress.Parse("127.0.0.1");
        int serverPort = 8080;
        sck.Connect(new IPEndPoint(serverAdress, serverPort));
        Console.WriteLine("client active");

        //Create to DB Context
        var context = new EntitieDbContext();


        while (true)
        {
                
            byte[] temp2 = new byte[1024];
            int serverBytes = sck.Receive(temp2);

            string serverMessage = Encoding.ASCII.GetString(temp2, 0, serverBytes);
            Console.WriteLine("received response message:" + serverMessage);

            Console.WriteLine("your message:");
            string input = Console.ReadLine();

            byte[] temp = Encoding.UTF8.GetBytes(input);
            sck.Send(temp);

            
            var ent1 = new Entitie() { Name = serverMessage };
            context.Add(ent1);
            context.SaveChanges();
        }

    }
}
