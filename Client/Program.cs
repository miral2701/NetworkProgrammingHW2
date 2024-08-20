using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectToServer();
            Console.ReadLine();
        }
        static void ConnectToServer()
        {
            TcpClient client=new TcpClient("127.0.0.1",8888);
            Console.WriteLine("Connected to server");

            NetworkStream stream =client.GetStream();
            Console.WriteLine("Enter date -to get date \nEnter time=to get time");
            string request=Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(request);
            stream.Write(buffer, 0, buffer.Length);
          buffer=new byte[1024];

            StringBuilder sb=new StringBuilder();
            int bytes = stream.Read(buffer, 0, buffer.Length);
            sb.Append(Encoding.UTF8.GetString(buffer,0,bytes));
            Console.WriteLine("Response from server->"+sb.ToString());

            client.Close();
        }
    }
}