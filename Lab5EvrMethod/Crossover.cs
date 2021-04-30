using System;
using System.IO;

namespace Lab5EvrMethod
{
    internal class Crossover
    {
        private readonly Int32 probability;
        private static Random rnd = new Random();

        private Int32 rangeDivider;

        public Crossover(Int32 _probability)
        {
            probability = _probability;
        }

        private Individual CrossoverIndividual(Individual indOne, Individual indTwo)
        {
            var individual = indOne.CloneIndividualFull();
            for (int iter = rangeDivider; iter < individual.GetlGenotype.Count; iter++)
                individual.GeneChange(iter, indTwo.GetlGenotype[iter]);
            return individual;
        }

        public (bool flag, Individual itemOne, Individual itemTwo) Crossbreeding(Individual indOne, Individual indTwo)
        {
            if (rnd.Next(0, 100) < probability)
            {
                using (var sw = File.AppendText(@"D:\Example.txt"))
                    sw.WriteLine("Произошло скрещивание особей.\n");
                rangeDivider = rnd.Next(0, indOne.GetlNumber.Count);
                using (var sw = File.AppendText(@"D:\Example.txt"))
                    sw.WriteLine("Позиция делителя - " + rangeDivider.ToString() + "\n");
                return (true, CrossoverIndividual(indOne, indTwo), CrossoverIndividual(indTwo, indOne));
            }
            else
            {
                using (var sw = File.AppendText(@"D:\Example.txt"))
                    sw.WriteLine("Скрещивания не произошло.\n");
                return (false, indOne, indTwo);
            }
        }

    }
}
