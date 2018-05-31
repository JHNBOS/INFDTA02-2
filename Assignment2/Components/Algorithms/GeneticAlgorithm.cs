using Assignment2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Run()
        {
            var populationWithFitness = new Dictionary<Ind, double>();
            for (int i = 0; i < this.PopulationSize; i++)
            {
                var individual = this.CreateIndividual();
                var fitness = this.CalculateFitness(individual);

                populationWithFitness.Add(individual, fitness);
            }

            var parents = this.SelectTwoParent(populationWithFitness.Keys.ToArray(), populationWithFitness.Values.ToArray());
            this.CrossOver(parents);
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

        private double CalculateFitness(Ind individual)
        {
            var fitness = -(Math.Pow(individual.Value(), 2) + (7 * individual.Value()));
            return fitness;
        }

        private Tuple<Ind, Ind> SelectTwoParent(Ind[] population, double[] fitnesses)
        {
            // Get highest fitness (parent one)
            var fitnessParentOne = fitnesses.Max();
            var indexParentOne = Array.IndexOf(fitnesses, fitnessParentOne);
            var parentOne = population[indexParentOne];

            // Get second highest fitness (parent two)
            var fitnessParentTwo = fitnesses.Skip(indexParentOne).ToArray().Max();
            var indexParentTwo = Array.IndexOf(fitnesses, fitnessParentTwo);
            var parentTwo = population[indexParentTwo];

            return new Tuple<Ind, Ind>(parentOne, parentTwo);
        }

        private void CrossOver(Tuple<Ind, Ind> parents)
        {

        }
    }
}
