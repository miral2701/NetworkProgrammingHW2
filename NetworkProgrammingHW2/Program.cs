using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkProgrammingHW2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ipAddress, 8888);
            server.Start();
            Console.WriteLine("Server started");
            while(true)
            {
                TcpClient client= server.AcceptTcpClient();
                Console.WriteLine("Client connected");
                NetworkStream stream = client.GetStream();
                byte[] buffer =new byte[1024];
                int bytesRead =stream.Read(buffer, 0, buffer.Length);
                string request=Encoding.UTF8.GetString(buffer,0,bytesRead);
                Console.WriteLine("Request from client->"+request);

                string response = String.Empty;
                if (request.Equals("date", StringComparison.OrdinalIgnoreCase))
                {
                    response=DateTime.Now.ToShortDateString();
                }
                else if (request.Equals("time", StringComparison.OrdinalIgnoreCase))
                {
                    response = DateTime.Now.ToShortTimeString();

                }
                else
                {
                    Console.WriteLine("Error");
                }
                byte[] data=Encoding.UTF8.GetBytes(response);
                stream.Write(data, 0, data.Length);
                client.Close();
            }
        }
    }
}