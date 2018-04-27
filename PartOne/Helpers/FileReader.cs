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

                        var newCustomer = new Vector();
                        newCustomer.Id = customer;

                        if (isNumber)
                        {
                            var point = new Point();
                            point.X = wineCount;
                            point.Y = offer;

                            if (!entries.Any(q => q.Id == customer))
                            {
                                newCustomer.Points.Add(point);
                                entries.Add(newCustomer);
                            }
                            else
                            {
                                entries.FirstOrDefault(q => q.Id == customer).Points.Add(point);
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