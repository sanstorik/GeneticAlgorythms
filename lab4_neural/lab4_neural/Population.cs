using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_neural
{
    class Population
    {
        List<Individual> population;
        const float PERCENT_OF_POPULATION_LIVES = 0.5f;

        public Population()
        {
            population = new List<Individual>();
        }

        public void AddIndividual(Individual individual)
        {
            population.Add(individual);
        }

        public Individual GetIndividual(int index)
        {
            return population[index];
        }

        public Individual[] GetBestIndividuals()
        {
            int countOfIndividualsInNextPopulation = (int)( population.Count * PERCENT_OF_POPULATION_LIVES );
            Individual[] survivedIndividuals = new Individual[countOfIndividualsInNextPopulation];

            Comparison<Individual> comp = (x, y) =>  y.GetProbability().CompareTo(x.GetProbability());

            population.Sort(comp);
            for (int i = 0; i < countOfIndividualsInNextPopulation; i++)
                survivedIndividuals[i] = population[i];

            return survivedIndividuals;
        }
    }
}
