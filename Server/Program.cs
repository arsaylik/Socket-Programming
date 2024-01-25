using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class Server
{
    public static void Main()
    {
        //Socket settings
        Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPAddress serverAdress = IPAddress.Parse("127.0.0.1");
        int serverPort = 8080;
        sck.Bind(new IPEndPoint(serverAdress, serverPort));

        sck.Listen(5000);
        Console.WriteLine("server active");

        Socket clientSocket = sck.Accept();

        //Create to DB Context
        var context = new Entite2DbContext();
        context.Database.EnsureCreated();

        while (true)
        {
            Console.WriteLine("your message:");
            string input = Console.ReadLine();

            byte[] temp = Encoding.UTF8.GetBytes(input);
            clientSocket.Send(temp);

            byte[] temp2 = new byte[1024];
            int clientBytes = clientSocket.Receive(temp2);

            string clientMessage = Encoding.UTF8.GetString(temp2, 0, clientBytes);
            Console.WriteLine("received data from message:" + clientMessage);

            var ent2 = new ServerMessage() { Message = clientMessage };
            context.Add(ent2);
            context.SaveChanges();
        }
    }
}

