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
                    .Select(l => l.Split(delimiter)).ToList();

                for (int i = 0; i < lines.Count(); i++)
                {
                    var line = lines[i];

                    if (entries.ElementAtOrDefault(i) == null)
                    {
                        entries.Insert(i, new Vector());
                    }

                    for (int j = 0; j < line.Count(); j++)
                    {
                        var currentItem = line[j];
                        entries[i].AddPoint(int.Parse(currentItem));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                Console.WriteLine("\nAn error occured while parsing the file...");
                Console.ReadKey();

                Environment.Exit(0);
            }

            return entries;
        }
    }
}