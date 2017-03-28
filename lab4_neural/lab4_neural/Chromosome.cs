using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_neural
{
    class Chromosome
    {
        int[] genes;

        public Chromosome(params int[] genes)
        {
            this.genes = genes;
        }

        public int GetChromosomeValue()
        {
            string genesValue = string.Empty;

            foreach (var gene in genes)
                genesValue += gene;

            return Convert.ToInt32(genesValue, 2);
        }
    }
}
