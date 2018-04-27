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
        public Dictionary<int, List<Point>> Parse(char delimiter, string path)
        {
            var entries = new Dictionary<int, List<Point>>();

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
                            var point = new Point();
                            point.X = wineCount;
                            point.Y = offer;

                            if (!entries.Any(q => q.Key == customer))
                            {
                                entries.Add(customer, new List<Point>() { point });
                            }
                            else
                            {
                                entries.FirstOrDefault(q => q.Key == customer).Value.Add(point);
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