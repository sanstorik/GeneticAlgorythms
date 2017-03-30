﻿using System;

namespace lab4_neural
{
    class Chromosome
    {
        char[] genes;
        Random rand;

        public Chromosome(params char[] genes)
        {
            this.genes = genes;
            rand = new Random();
        }

        public Chromosome(int chromosomeValue)
        {
            genes = new char[16];
            rand = new Random();
            char[] tempGenes = Convert.ToString(chromosomeValue, 2).ToCharArray();

            int lengthDiff = genes.Length - tempGenes.Length;
            CompleteGenesWithZeroes(lengthDiff);

            for (int i = lengthDiff, j = 0; i < genes.Length; i++, j++)
                genes[i] = tempGenes[j];
        }

        public Chromosome Mutate()
        {
            int randomGene = rand.Next(0, genes.Length);
            char[] mutatedGenes = genes;
            mutatedGenes[randomGene] = genes[randomGene] == '0' ? '1' : '0';

            return new Chromosome(genes);
        }

        public Chromosome Crossover(Chromosome secondParent)
        {
            int indexOfCrossover = rand.Next(1, genes.Length);

            char[] crossedGenes = genes;
            for (int geneIndex = indexOfCrossover; geneIndex < genes.Length; geneIndex++)
                crossedGenes[geneIndex] = secondParent.genes[geneIndex];

            return new Chromosome(crossedGenes);
        }

        public float GetX1()
        {
            return GetGenesValue(0, 8);
        }

        public float GetX2()
        {
            return GetGenesValue(8, genes.Length);
        }

        float GetGenesValue(int from, int to)
        {
            string genesValue = string.Empty;
            for (int i = from; i < to; i++)
                genesValue += genes[i];

            return GeneticAlgorythm.MIN_X + ( Convert.ToInt32(genesValue, 2) * GeneticAlgorythm.GetInstance().GetFunctionStep() );
        }

        void CompleteGenesWithZeroes(int lengthDiff)
        {
            if (lengthDiff > 0)
                for (int i = 0; i < lengthDiff; i++)
                    genes[i] = '0';
        }

        public override string ToString()
        {
            string value = string.Empty;

            for (int i = 0; i < genes.Length; i++)
            {
                if (i == genes.Length / 2)
                    value += " ";

                value += genes[i];
            }

            return value;
        }
    }
}
