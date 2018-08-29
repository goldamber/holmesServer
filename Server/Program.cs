using Server.Service;
using System;
using System.Configuration;
using System.Data.Entity;
using System.ServiceModel;
using static System.Console;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost serviceHost = new ServiceHost(typeof(EngService));
                serviceHost.Open();

                Database.EngContext _context = new Database.EngContext();
                _context.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["engEntDB"].ConnectionString;
                System.Data.Entity.Database.SetInitializer(new Database.DBSeeder());
                _context.Games.Load();

                WriteLine("Service is running!");
                Read();

                serviceHost.Close();
                WriteLine("Service is closed!");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }

            ReadLine();
        }
    }
}