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
        float probability;
        float functionProbability;

        public Individual(Chromosome chromosome)
        {
            this.chromosome = chromosome;
            UpdateProbability();
        }

        public Chromosome GetChromosome()
        {
            return chromosome;
        }

        public Individual Reproduction(Individual secondParent)
        {
            return new Individual(chromosome.Crossover(secondParent.GetChromosome()));
        }

        public void SetProbability(float probability)
        {
            this.probability = probability;
        }

        public float GetProbability()
        {
            return probability;
        }

        float EvaluateFunctionProbability(float x1, float x2)
        {
            // return  20 + ( x1 * x1 ) + ( x2 * x2 ) - 10 * (float)Math.Cos(2 * Math.PI * x1) - 10 * (float)Math.Cos(2 * Math.PI * x2);
            return ( Math.Abs(x1) ) + ( Math.Abs(x2) );
        }

        public float GetFunctionProbability()
        {
            return functionProbability;
        }

        public void UpdateProbability()
        {
            functionProbability = EvaluateFunctionProbability(chromosome.GetX1(), chromosome.GetX2()); 
        }
    }
}
