using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_neural
{
    class Individual
    {
        Chromosome chromosome;

        public Individual(Chromosome chromosome)
        {

        }

        public Chromosome GetChromosome()
        {
            return chromosome;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return this == null;

            Individual ind = (Individual)obj;
            return ind.chromosome.GetChromosomeValue() == chromosome.GetChromosomeValue();
        }

        public override int GetHashCode()
        {
            return chromosome.GetChromosomeValue();
        }
    }
}
