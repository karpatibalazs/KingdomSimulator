using KingdomSim.Models;
using System;

namespace KingdomSim
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Kingdom kingdom = FileParser.ParseFromFile("input.txt");

                kingdom.RunFullSimulation();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Kritikus hiba történt: {e.Message}");
            }

            Console.ReadLine();
        }
    }
}