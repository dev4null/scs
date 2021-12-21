using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a service application that runs on 10083 TCP port
            var serviceApplication = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(10083));

            //Create a CalculatorService and add it to service application
            serviceApplication.AddService<ICalculatorService, CalculatorService>(new CalculatorService());
            serviceApplication.ClientConnected += ServiceApplication_ClientConnected;
            serviceApplication.ClientDisconnected += ServiceApplication_ClientDisconnected; ;

            //Start service application
            serviceApplication.Start();

            //Console.WriteLine(serviceApplication.)

            Console.WriteLine($"Calculator service is started. {Process.GetCurrentProcess().Id} Press enter to stop...");
            Console.ReadLine();

            //Stop service application
            serviceApplication.Stop();
        }

        private static void ServiceApplication_ClientDisconnected(object sender, ServiceClientEventArgs e)
        {
            Console.WriteLine(e.Client.ClientId);
            //throw new NotImplementedException();
        }

        private static void ServiceApplication_ClientConnected(object sender, ServiceClientEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }

    public class CalculatorService : ScsService, ICalculatorService
    {
        public int Add(int number1, int number2)
        {
            return number1 + number2;
        }
    }
    [ScsService(Version = "3.0")]
    public interface ICalculatorService
    {
        int Add(int number1, int number2);
    }
}
