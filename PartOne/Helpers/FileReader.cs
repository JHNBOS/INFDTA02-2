using PartOne.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PartOne.Helpers
{
    public class FileReader
    {
        public List<Vector> Parse(char delimiter, string path)
        {
            var entries = new List<Vector>();

            try
            {
                var lines = File.ReadAllLines(path)
                    .Select(l => l.Split(delimiter).ToList());

                var wineCount = 1;
                foreach (var line in lines)
                {
                    var customer = 1;
                    var list = new List<int>();

                    foreach (var number in line)
                    {
                        int offer;
                        var isNumber = int.TryParse(number, out offer);
                        if (isNumber)
                        {
                            var vector = new Vector();
                            vector.Customer = customer;
                            vector.Offers.Add(wineCount, offer);

                            if (!entries.Any(q => q.Customer == vector.Customer))
                            {
                                entries.Add(vector);
                            }
                            else
                            {
                                entries.FirstOrDefault(q => q.Customer == vector.Customer).Offers.Add(wineCount, offer);
                            }
                            customer++;
                        }
                    }

                    wineCount++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nInvalid file path...");
                Debug.WriteLine(ex);
                Console.ReadKey();
                System.Environment.Exit(0);
            }

            return entries;
        }
    }
}