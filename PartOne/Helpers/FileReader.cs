using PartOne.Models;
using System;
using System.Collections.Generic;
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
                    .Select(l => l.Split(delimiter)).ToList();

                for (int i = 0; i < lines.Count(); i++)
                {
                    for (int j = 0; j < lines[i].Count(); j++)
                    {
                        var item = lines[i];
                        if (entries.ElementAtOrDefault(j) == null)
                        {
                            entries.Add(new Vector());
                        }

                        entries[j].AddPoint(int.Parse(item[j]));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nInvalid file path...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            return entries;
        }
    }
}