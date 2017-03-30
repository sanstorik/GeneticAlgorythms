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
        public const float MIN_X = -5.12f;
        public const float MAX_X = 5.12f;

        Population population;
        Individual[] individuals;
        float step;

        private GeneticAlgorythm() { }

        public void Solve()
        {
            population = new Population();
            individuals = new Individual[INDIVIDUALS_COUNT];

            EvaluteFunctionStep();
            CreateIndividuals();
            InitializeInitialPopulation();
        }

        public float GetFunctionStep()
        {
            return step;
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

        void EvaluteFunctionStep()
        {
            float length = Math.Abs(MIN_X) + Math.Abs(MAX_X);
            step = length / (float)Math.Sqrt(INDIVIDUALS_COUNT);
        }

        void InitializeInitialPopulation()
        {
            Random rand = new Random();
            int randomIndividualIndex;

            for (int i = 0; i < INITIAL_POPULATION_COUNT; i++)
            {
                randomIndividualIndex = rand.Next(0, INDIVIDUALS_COUNT - 1);
                population.AddIndividual(individuals[ randomIndividualIndex ]);
                Console.WriteLine(individuals[randomIndividualIndex].GetChromosome().GetX1() + " " + individuals[randomIndividualIndex].GetChromosome().GetX2());
            }
        }

        public static GeneticAlgorythm GetInstance()
        {
            if (instance == null)
                instance = new GeneticAlgorythm();
            return instance;
        }
    }
}
