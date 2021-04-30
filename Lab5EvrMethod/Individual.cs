using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab5EvrMethod
{
    internal class Individual: ICloneable
    {
        internal delegate void IndividualChange();
        internal IndividualChange eventIndividualChage;

        private static Random rnd = new Random();

        private Int32 sizeRange; //Нижняя и верхняя граница заполнения 
        private Int32 countGenom; //Количество 
        private Int32 propability;
        private Int32 countBin = 0;

        public Int32 GetSizeRange() => sizeRange;
        public Int32 GetCountGenom() => countGenom;

        private List<Int32> lNumber = new List<Int32>();
        private List<Int32> lGenotype = new List<Int32>();

        public List<Int32> GetlNumber => lNumber;
        public List<Int32> GetlGenotype => lGenotype;

        public Individual(Int32 _sizeRange, Int32 _countGenom, Int32 _propability, List<Int32> _lNumber, List<Int32> _lGenotype)
        {
            sizeRange = _sizeRange;
            countGenom = _countGenom;
            propability = _propability;
            lNumber = _lNumber;
            lGenotype = _lGenotype;

            ConsiderCountBin();
        }

        public Individual(Int32 _sizeRange, Int32 _countGenom, Int32 _propability, List<Int32> _lNumber)
        { 
            sizeRange = _sizeRange;
            countGenom = _countGenom;
            propability = _propability;
            lNumber = _lNumber;

            ConsiderCountBin();
            GeneratingListGenomes(sizeRange);
        }

        private void ConsiderCountBin()
        {
            while (1 << countBin < sizeRange)
                countBin++;
            if ((1 << countBin) - sizeRange > 1)
                --countBin;
        }

        private void GeneratingListGenomes(Int32 _sizeRange)
        {
            for (int iter = 0; iter < countGenom; iter++)
                lGenotype.Add(rnd.Next(0, _sizeRange));
        }

        internal void GeneChange(Int32 gene, Int32 value)
        {
            if (gene < 0 || gene >= lGenotype.Count)
                throw new IndexOutOfRangeException();
            lGenotype[gene] = value;
            eventIndividualChage?.Invoke();
        }

        internal bool GeneMutation()
        {
            if (rnd.Next(0, 100) < propability)
            {
                Int32 genMutation = rnd.Next(0, lGenotype.Count);
                Int32 posMutation = rnd.Next(0, 8);
                Int32 buffValue = lGenotype[genMutation];
                lGenotype[genMutation] = lGenotype[genMutation] ^ (1 << posMutation);
                using (var sw = File.AppendText(@"D:\Example.txt"))
                    sw.WriteLine(String.Format("Мутация произошла в {0} гене в {1} позиции. " +
                                                "Из {2} получили {3}", genMutation, posMutation,
                                                 buffValue, lGenotype[genMutation]));
                eventIndividualChage?.Invoke();
                return true;
            }
            else
            {
                using (var sw = File.AppendText(@"D:\Example.txt"))
                    sw.WriteLine("Мутации не произошло.");
                return false;
            }
        }

        public Individual CloneIndividualNumber()
        {
            return new Individual(sizeRange, countGenom, propability, new List<Int32>(lNumber));
        }

        public Individual CloneIndividualFull()
        {
            return new Individual(sizeRange, countGenom, propability, new List<Int32>(lNumber), new List<Int32>(lGenotype));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int item = 0; item < lNumber.Count; item++)
                sb.Append(String.Format("{0,5}", lNumber[item]) + " -> " + String.Format("{0,5}", lGenotype[item]) + "\n");
            return sb.ToString();
        }
    }
}
