using Assignment1.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment1.Components
{
    public class Parser
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

                    for (int j = 0; j < line.Length; j++)
                    {
                        if (entries.FirstOrDefault(q => q.Id == i) == null)
                        {
                            var vector = new Vector();
                            vector.Id = i;
                        }
                        else
                        {
                            var vector = entries.FirstOrDefault(q => q.Id == i);
                            vector.Points.Insert(j, float.Parse(line[j]));
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nAn error occured while parsing the file...");
                Console.ReadKey();

                Environment.Exit(0);
            }

            return entries;
        }
    }
}