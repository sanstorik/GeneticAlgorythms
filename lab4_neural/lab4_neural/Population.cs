using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_neural
{
    class Population
    {
        HashSet<Individual> population;

        public Population()
        {
            population = new HashSet<Individual>();
        }

        public void AddToPopulation(Individual individual)
        {
            population.Add(individual);
        }
    }
}
