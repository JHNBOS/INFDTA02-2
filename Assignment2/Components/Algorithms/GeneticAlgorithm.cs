using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment2.Components.Algorithms
{
    public class GeneticAlgorithm
    {
        #region Properties

        private double CrossoverRate { get; set; }
        private double MutationRate { get; set; }
        private bool Elitism { get; set; }
        private int PopulationSize { get; set; }
        private int Iterations { get; set; }
        private Random random;

        #endregion

        #region Constructor

        public GeneticAlgorithm(double crossoverRate, double mutationRate, bool elitism, int populationSize, int iterations)
        {
            this.CrossoverRate = crossoverRate;
            this.MutationRate = mutationRate;
            this.Elitism = elitism;
            this.PopulationSize = populationSize;
            this.Iterations = iterations;
            this.random = new Random();
        }

        #endregion

        public void Run()
        {
            // Get initial population
            var initialPopulation = this.CreatePopulation();

            var currentPopulation = initialPopulation;
            for (int gen = 0; gen < this.Iterations; gen++)
            {
                var nextPopulation = new List<Ind>();

                // Calculate fitness of population
                var fitness = CalculateFitnessOfPopulation(currentPopulation);

                for (int ind = 0; ind < this.PopulationSize; ind++)
                {
                    var parents = this.SelectTwoParents(initialPopulation, fitness);
                    var offspring = this.CrossOver(parents);
                    var mutatedOffspring = new Tuple<Ind, Ind>(this.Mutation(offspring.Item1, this.MutationRate),
                        this.Mutation(offspring.Item2, this.MutationRate));

                    nextPopulation.Add(mutatedOffspring.Item1);
                    nextPopulation.Add(mutatedOffspring.Item2);
                }

                currentPopulation = nextPopulation.ToArray();
            }

            // recompute the fitnesses on the final population and return the best individual
            var fitnesses = Enumerable.Range(0, this.PopulationSize).Select(s => CalculateFitness(currentPopulation[s])).ToArray();
            Console.WriteLine("Genetic algorithm");
            Console.WriteLine("Average fitness: " + fitnesses.Average());
            Console.WriteLine("Best fitness: " + fitnesses.OrderBy(x => x).Last());
        }

        #region Private Methods

        private Ind[] CreatePopulation()
        {
            var individuals = new List<Ind>();
            while (individuals.Count < this.PopulationSize)
            {
                for (int i = 0; i < this.PopulationSize; i++)
                {
                    var individual = this.CreateIndividual();
                    if (individuals.FirstOrDefault(q => q.Binary == individual.Binary) == null)
                    {
                        individuals.Add(individual);
                    }
                }
            }

            return individuals.ToArray();
        }

        private double[] CalculateFitnessOfPopulation(Ind[] population)
        {
            var fitnesses = new List<double>();
            while (fitnesses.Count < this.PopulationSize)
            {
                for (int i = 0; i < this.PopulationSize; i++)
                {
                    var fitness = this.CalculateFitness(population[i]);
                    fitnesses.Add(fitness);
                }
            }

            return fitnesses.ToArray();
        }

        private Ind CreateIndividual()
        {
            string individual = "";

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
            var fitness = Math.Pow(individual.Value(), 2) + 7 * individual.Value();
            return fitness;
        }

        private Tuple<Ind, Ind> SelectTwoParents(Ind[] population, double[] fitnesses)
        {
            Ind parentOne = null;
            Ind parentTwo = null;

            parentOne = this.RouletWheelSelection(population, fitnesses);
            parentTwo = this.RouletWheelSelection(population, fitnesses);

            return new Tuple<Ind, Ind>(parentOne, parentTwo);
        }

        private Ind RouletWheelSelection(Ind[] population, double[] fitnesses)
        {
            var sumOfFitnesses = fitnesses.Sum();
            var probabilities = fitnesses.Select(s => s / sumOfFitnesses).ToList();

            var index = 0;
            var sum = probabilities[0];
            while (random.Next() <= sum)
            {
                index++;
                sum += probabilities[index];
            }

            return population[index];
        }

        private Tuple<Ind, Ind> CrossOver(Tuple<Ind, Ind> parents)
        {
            int positionToSplit = 2;
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

            mutatedIndividual.Binary = new string(binaryDigits);
            return mutatedIndividual;
        }

        #endregion

        #region Tests

        public void TestFitness()
        {
            var individualOne = new Ind("10101");
            var individualTwo = new Ind("11000");

            var resultOne = this.CalculateFitness(individualOne);
            var resultTwo = this.CalculateFitness(individualTwo);

            //-(Math.Pow(21, 2)) + (7 * 21); == 441 + 147 = 558
            //-(Math.Pow(3, 2)) + (7 * 3); == 9 + 21 = 30

            Console.WriteLine("\n\nFITNESS\n-------------------");
            Console.WriteLine("Individual #1:");
            Console.WriteLine("  Expected ==> (-)558");
            Console.WriteLine("  Actual   ==> " + resultOne);
            Console.WriteLine("");
            Console.WriteLine("Individual #2:");
            Console.WriteLine("  Expected ==> (-)30");
            Console.WriteLine("  Actual   ==> " + resultTwo);
        }

        public void TestCrossover()
        {
            var individualOne = new Ind("10101");
            var individualTwo = new Ind("11000");

            //With positionToSplit manually set at 2
            var result = this.CrossOver(new Tuple<Ind, Ind>(individualOne, individualTwo));

            Console.WriteLine("\n\nCROSSOVER\n-------------------");
            Console.WriteLine("Individual #1:");
            Console.WriteLine("  Expected ==> 10000");
            Console.WriteLine("  Actual   ==> " + result.Item1.Binary);
            Console.WriteLine("");
            Console.WriteLine("Individual #2:");
            Console.WriteLine("  Expected ==> 11101");
            Console.WriteLine("  Actual   ==> " + result.Item2.Binary);
        }

        public void TestMutation()
        {
            var individualOne = new Ind("10101");
            var individualTwo = new Ind("11000");

            //With mutation rate of 10000, to eliminate randomness
            var resultOne = this.Mutation(individualOne, 10000);
            var resultTwo = this.Mutation(individualTwo, 10000);

            Console.WriteLine("\n\nMUTATION\n-------------------");
            Console.WriteLine("Individual #1:");
            Console.WriteLine("  Expected ==> 01010");
            Console.WriteLine("  Actual   ==> " + resultOne.Binary);
            Console.WriteLine("");
            Console.WriteLine("Individual #2:");
            Console.WriteLine("  Expected ==> 00111");
            Console.WriteLine("  Actual   ==> " + resultTwo.Binary);
        }

        #endregion
    }
}
