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

        public Population()
        {
            population = new List<Individual>();
        }

        public void AddIndividual(Individual individual)
        {
            population.Add(individual);
        }
    }
}
