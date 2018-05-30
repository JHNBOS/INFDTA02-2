using Assignment2.Entities;
using System;

namespace Assignment2.Components.Algorithms
{
    public class GeneticAlgorithm
    {
        private double CrossoverRate { get; set; }
        private double MutationRate { get; set; }
        private bool Elitism { get; set; }
        private int PopulationSize { get; set; }
        private int Iterations { get; set; }

        public GeneticAlgorithm(double crossoverRate, double mutationRate, bool elitism, int populationSize, int iterations)
        {
            this.CrossoverRate = crossoverRate;
            this.MutationRate = mutationRate;
            this.Elitism = elitism;
            this.PopulationSize = populationSize;
            this.Iterations = iterations;
        }

        private Ind CreateIndividual()
        {
            var random = new Random();
            var individual = "";

            for (int i = 0; i < 5; i++)
            {
                if (random.NextDouble() > 0.5)
                {
                    individual += 1;
                }
                else
                {
                    individual += 0;
                }
            }

            return new Ind(individual);
        }

    }
}
