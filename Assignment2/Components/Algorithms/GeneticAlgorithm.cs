﻿using Assignment2.Entities;
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
        private Random random;

        public GeneticAlgorithm(double crossoverRate, double mutationRate, bool elitism, int populationSize, int iterations)
        {
            this.CrossoverRate = crossoverRate;
            this.MutationRate = mutationRate;
            this.Elitism = elitism;
            this.PopulationSize = populationSize;
            this.Iterations = iterations;
            this.random = new Random();
        }

        public void Run()
        {
            for (int iteration = 0; iteration < this.Iterations; iteration++)
            {
                var populationWithFitness = new Dictionary<Ind, double>();
                for (int i = 0; i < this.PopulationSize; i++)
                {
                    var individual = this.CreateIndividual();
                    var fitness = this.CalculateFitness(individual);

                    populationWithFitness.Add(individual, fitness);
                }

                var parents = this.SelectTwoParent(populationWithFitness.Keys.ToArray(), populationWithFitness.Values.ToArray());
                var offspring = this.CrossOver(parents);

                var mutatedChildOne = this.Mutation(offspring.Item1, this.MutationRate);
                var mutatedChildTwo = this.Mutation(offspring.Item2, this.MutationRate);
                var mutatedOffspring = new Tuple<Ind, Ind>(mutatedChildOne, mutatedChildTwo);
            }
        }

        private Ind CreateIndividual()
        {
            var individual = "";

            for (int i = 0; i < 5; i++)
            {
                if (this.random.NextDouble() > 0.5)
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

        private Tuple<Ind, Ind> CrossOver(Tuple<Ind, Ind> parents)
        {
            int positionToSplit = this.random.Next(0, parents.Item1.Binary.Length);
            Ind childOne = new Ind();
            Ind childTwo = new Ind();

            childOne.Binary = parents.Item1.Binary.Substring(0, positionToSplit) + parents.Item2.Binary.Substring(positionToSplit);
            childTwo.Binary = parents.Item2.Binary.Substring(0, positionToSplit) + parents.Item1.Binary.Substring(positionToSplit);

            return new Tuple<Ind, Ind>(childOne, childTwo);
        }

        private Ind Mutation(Ind individual, double mutationRate)
        {
            Ind mutatedIndividual = individual;
            var binaryDigits = mutatedIndividual.Binary.ToCharArray();

            for (int character = 0; character < binaryDigits.Length; character++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    binaryDigits[character] = binaryDigits[character] == '0' ? '1' : '0';
                }
            }

            mutatedIndividual.Binary = binaryDigits.ToString();
            return mutatedIndividual;
        }

    }
}
