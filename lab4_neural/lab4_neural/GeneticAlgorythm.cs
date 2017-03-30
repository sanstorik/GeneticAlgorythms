using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_neural
{
    class GeneticAlgorythm
    {
        static GeneticAlgorythm instance;
        const int INDIVIDUALS_COUNT = 65536;
        const int INITIAL_POPULATION_COUNT = 100;
        const float REPROPUCTION_PROBABILITY = 0.25f;
        const float GENE_MUTATION_PROBABILITY = 0.015f;
        public const float MIN_X = -5.12f;
        public const float MAX_X = 5.12f;
        Random rand;

        Population population;
        Individual[] individuals;
        float step;

        private GeneticAlgorythm() { rand = new Random(); }

        public void Solve()
        {
            population = new Population();
            individuals = new Individual[INDIVIDUALS_COUNT];

            EvaluteFunctionStep();
            CreateIndividuals();
            InitializeInitialPopulation();

            SelectBestIndividualsAndReproductThem();
        }

        public float GetFunctionStep()
        {
            return step;
        }

        void EvaluteFunctionStep()
        {
            float length = Math.Abs(MIN_X) + Math.Abs(MAX_X);
            step = length / (float)Math.Sqrt(INDIVIDUALS_COUNT);
        }

        void CreateIndividuals()
        {
            for (int i = 0; i < INDIVIDUALS_COUNT; i++)
                individuals[i] = new Individual(new Chromosome(i));

            SetProbabilityForIndividuals();
        }

        void SetProbabilityForIndividuals()
        {
            float probabilitySum = 0;
            for (int i = 0; i < INDIVIDUALS_COUNT; i++)
                probabilitySum += individuals[i].GetFunctionProbability();

            for (int i = 0; i < INDIVIDUALS_COUNT; i++)
                individuals[i].SetProbability(individuals[i].GetFunctionProbability() / probabilitySum);
        }


        void InitializeInitialPopulation()
        {
            int randomIndividualIndex;

            for (int i = 0; i < INITIAL_POPULATION_COUNT; i++)
            {
                randomIndividualIndex = rand.Next(0, INDIVIDUALS_COUNT - 1);
                population.AddIndividual(individuals[ randomIndividualIndex ]);
            }
        }

        void SelectBestIndividualsAndReproductThem()
        {
            Population nextGeneration = new Population(population.GetBestIndividuals());

            ReproductWithSex(nextGeneration);
            ReproductWithMutation(nextGeneration);

            population = nextGeneration;

            foreach(var individ in population.GetPopulation())
            {
                Console.WriteLine(individ.GetFunctionProbability());
            }

            Console.WriteLine(population.GetPopulationCapacity());
        }

        void ReproductWithSex(Population nextGeneration)
        {
            var bestIndividuals = nextGeneration.GetPopulation();
            Queue<Individual> childs = new Queue<Individual>();
            Individual firstParent;
            Individual secondParent;

            for (int i = 0; i < bestIndividuals.Length * 2; i++)
            {
                if (rand.NextDouble() <= REPROPUCTION_PROBABILITY)
                {
                    firstParent = GetRandomIndividual(bestIndividuals);
                    secondParent = GetTheClosestIndividual(firstParent, bestIndividuals);

                    childs.Enqueue(firstParent.Reproduction(secondParent));
                }
            }

            while (childs.Count > 0)
                nextGeneration.AddIndividual(childs.Dequeue());
        }

        void ReproductWithMutation(Population nextGeneration)
        {
            Queue<Individual> mutatedIndividuals = new Queue<Individual>();

            for (int i=0; i < nextGeneration.GetPopulationCapacity(); i++)
            {
                // 16 - is count of genes
                if (rand.NextDouble() <= GENE_MUTATION_PROBABILITY * 16)
                    mutatedIndividuals.Enqueue(
                        new Individual(nextGeneration.GetIndividual(i).GetChromosome().Mutate()));
            }

            while (mutatedIndividuals.Count > 0)
                nextGeneration.AddIndividual(mutatedIndividuals.Dequeue());
        }

        Individual GetRandomIndividual(Individual[] individuals)
        {
            return individuals[rand.Next(0, individuals.Length)];
        }

        Individual GetTheClosestIndividual(Individual firstParent, Individual[] population)
        {
            float probability = firstParent.GetProbability();
            Individual closest = population.FirstOrDefault(
                (secondParent) => Math.Abs(secondParent.GetProbability() - firstParent.GetProbability()) <= 0.1f);

            if (closest == null)
                closest = population[0];

            return closest;
        }

        public static GeneticAlgorythm GetInstance()
        {
            if (instance == null)
                instance = new GeneticAlgorythm();
            return instance;
        }
    }
}
