using PartOne.Helpers;
using System;
using System.Linq;

namespace PartOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileReader = new FileReader();
            var data = fileReader.Parse(',', @"./Data/WineData.csv");

            foreach (var vector in data.Where(q => q.Customer == 1).ToList())
            {
                Console.WriteLine("Customer " + vector.Customer + ":");
                foreach (var offer in vector.Offers)
                {
                    var wine = offer.Key;
                    var accepted = offer.Value;
                    var taken = accepted == 1 ? "taken." : "not taken.";

                    Console.WriteLine("\tWine nr. " + wine + " was " + taken);
                }
            }

            Console.ReadKey();
        }
    }
}
