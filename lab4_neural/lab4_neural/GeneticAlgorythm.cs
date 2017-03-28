using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_neural
{
    class GeneticAlgorythm
    {
        const int INDIVIDUALS_COUNT = 65536;
        const int INITIAL_POPULATION_COUNT = 100;
        const float MIN_X = -5.12f;
        const float MAX_X = 5.12f;

        Population population;
        Individual[] individuals;
        float step;

        public GeneticAlgorythm()
        {
            population = new Population();
            individuals = new Individual[INDIVIDUALS_COUNT];

            EvaluteFunctionStep();
            CreateIndividuals();
            InitializeInitialPopulation();
        }

        void CreateIndividuals()
        {
            float xValue = 0;
            for (int i = 0; i < INDIVIDUALS_COUNT; i++)
            {
                individuals[i] = new Individual(new Chromosome(i));
                individuals[i].EvaluateFunctionProbability(xValue, xValue);
                xValue += step;
            }

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
            step = length / INDIVIDUALS_COUNT;
        }

        void InitializeInitialPopulation()
        {
            for (int i = 0; i < INITIAL_POPULATION_COUNT; i++)
                population.AddIndividual(individuals[i]);
        }
    }
}
