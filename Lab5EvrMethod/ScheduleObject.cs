using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Lab5EvrMethod
{
    internal class ScheduleObject
    {
        private Individual individual;
        private Int32 sizeRange;
        private Int32 countProcessor;
        
        public Individual GetIndividual() => individual;

        private List<List<Int32>> schedule;

        public ScheduleObject(Individual _individual, Int32 _sizeRange, Int32 _countPC)
        {
            individual = _individual;
            sizeRange = _sizeRange;
            countProcessor = _countPC;

            individual.eventIndividualChage += BuilderScheduler;

            BuilderScheduler();
        }

        public void BuilderScheduler()
        {
            schedule = new List<List<int>>();
            for (int iter = 0; iter < countProcessor; iter++)
                schedule.Add(new List<Int32>());

            var lNum = individual.GetlNumber;
            var lGen = individual.GetlGenotype;
            for (int iter = 0; iter < lNum.Count; iter++)
            {
                Int32 lenDiap = (Int32)(sizeRange / countProcessor);
                Int32 countDiapInGen = (Int32)(lGen[iter] / lenDiap);
                if (countDiapInGen >= countProcessor)
                    countDiapInGen = countProcessor-1;
                schedule[countDiapInGen].Add(lNum[iter]);
            }
                
        }

        public (Int32 position, Int32 weight) Weight()
        {
            Int32 pos = 0;
            Int32 summ = 0;
            for(int iter = 0; iter < schedule.Count; iter++)
            {
                if(schedule[iter].Sum() > summ)
                {
                    summ = schedule[iter].Sum();
                    pos = iter;
                }
            }
            return (pos, summ);
        }

        public ScheduleObject MinSchedule(ScheduleObject obj)
        {
            using (var sw = File.AppendText(@"D:\Example.txt"))
                sw.WriteLine("Максимальный время особи №1 - {0} / " +
                              "Максимальный время особи №2 - {1}", this.Weight().weight, obj.Weight().weight);
            if (this.Weight().weight <= obj.Weight().weight)
                return this;
            return obj;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Int32 cS = 0;
            foreach (var lItemScheduler in schedule)
            {
                sb.Append(String.Format("p{0,1} | ", cS++));
                foreach (var item in lItemScheduler)
                    sb.Append(String.Format("{0,5}", item));
                sb.Append(String.Format(" | {0,5}\n", lItemScheduler.Sum()));
            }
            return sb.ToString();
        }
    }
}
