using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using Hik.Communication.ScsServices.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to connect to server and call methods...");
            Console.ReadLine();

            //Create a client that can call methods of Calculator Service that is running on local computer and 10083 TCP port
            //Since IScsServiceClient is IDisposible, it closes connection at the end of the using block
            using (var client = ScsServiceClientBuilder.CreateClient<ICalculatorService>(new ScsTcpEndPoint("10.110.10.19", 10083)))
            {
                //Connect to the server
                client.Connect();

                //Call a remote method of server
                var division = client.ServiceProxy.Add(9, 3);

                //Write the result to the screen
                Console.WriteLine("Result: " + division);
            }

            Console.WriteLine("Press enter to stop client application");
            Console.ReadLine();
        }
    }

    [ScsService(Version = "3.0")]
    public interface ICalculatorService
    {
        int Add(int number1, int number2);
    }
}
