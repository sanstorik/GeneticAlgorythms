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
        const float PERCENT_OF_POPULATION_LIVES = 0.45f;

        public Population()
        {
            population = new List<Individual>();
        }

        public Population(Individual[] individuals)
        {
            population = new List<Individual>();
            for (int i = 0; i < individuals.Length; i++)
                population.Add(individuals[i]);
        }

        public void AddIndividual(Individual individual)
        {
            population.Add(individual);
        }

        public void UpdateAllIndividualProbabilities()
        {
            for (int i = 0; i < population.Count; i++)
                population[i].UpdateProbability();
        }

        public Individual GetIndividual(int index)
        {
            if (index < 0)
                return null;

            return population[index];
        }

        public int GetPopulationCapacity()
        {
            return population.Count;
        }

        public Individual[] GetPopulation()
        {
            return population.ToArray();
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
