using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5EvrMethod
{
    class Generation
    {
        private readonly Int32 countIndividual;
        private Int32 sizeRange;
        private Int32 countPC;

        private Crossover cS;
        private static Random rnd = new Random();

        private List<Individual> lIndividual;
        public List<Individual> Getlindividual() => lIndividual;

        public Generation(Int32 _countIndividual, Int32 _sizeRange, 
                          Int32 _countPC, Int32 _propability)
        {
            countIndividual = _countIndividual;
            sizeRange = _sizeRange;
            countPC = _countPC;
            lIndividual = new List<Individual>();
            cS = new Crossover(_propability);
        }

        internal void AddIndividual(Individual individual)
        {
            if (lIndividual.Count < countIndividual)
                lIndividual.Add(individual);
        }

        public void FillingGeneration(Generation generation)
        {
            var lGenIndividual = generation.Getlindividual();

            for (int iter = lIndividual.Count; iter < countIndividual; iter++)
            {
                Int32 randPos = rnd.Next(0, countIndividual);
                while(randPos == iter)
                    randPos = rnd.Next(0, countIndividual);

                using (var sw = File.AppendText(@"D:\Example.txt"))
                {
                    ScheduleObject sch = new ScheduleObject(lGenIndividual[iter], sizeRange, countPC);
                    int w1 = sch.Weight().weight;
                    ScheduleObject sch2 = new ScheduleObject(lGenIndividual[randPos], sizeRange, countPC);
                    int w2 = sch2.Weight().weight;
                    sw.WriteLine("Берём {0}( вес - {1}) и {2} (вес - {3}) особи\n", iter, w1, randPos, w2);
                }
                    

                var resultCrossbreeding = cS.Crossbreeding(lGenIndividual[iter], lGenIndividual[randPos]);  
                if (resultCrossbreeding.flag)
                {
                    ScheduleObject schOne = new ScheduleObject(lGenIndividual[iter], sizeRange, countPC);

                    ScheduleObject schTwo = new ScheduleObject(resultCrossbreeding.itemOne, sizeRange, countPC);
                    using (var sw = File.AppendText(@"D:\Example.txt"))
                        sw.WriteLine("Первая особь после кроссинговера:\n" + resultCrossbreeding.itemOne.ToString());
                    using (var sw = File.AppendText(@"D:\Example.txt"))
                        sw.WriteLine("Расписание на её основе:\n" + schTwo.ToString() + "\n" + schTwo.Weight().weight.ToString() + "\n");
                    if (resultCrossbreeding.itemOne.GeneMutation())
                    {
                        using (var sw = File.AppendText(@"D:\Example.txt"))
                            sw.WriteLine("Мутировавшая первая особь:\n" + resultCrossbreeding.itemOne.ToString());
                        using (var sw = File.AppendText(@"D:\Example.txt"))
                        {
                            ScheduleObject sch = new ScheduleObject(resultCrossbreeding.itemOne, sizeRange, countPC);
                            sw.WriteLine("Расписание на её основе:\n" + schTwo.ToString() + "\n" + sch.Weight().weight.ToString() + "\n");
                        }
                    }
                    else
                    {
                        using (var sw = File.AppendText(@"D:\Example.txt"))
                            sw.WriteLine("Первый ребёнок не мутировал.\n");
                    }

                    ScheduleObject schThree = new ScheduleObject(resultCrossbreeding.itemTwo, sizeRange, countPC);
                    using (var sw = File.AppendText(@"D:\Example.txt"))
                        sw.WriteLine("Вторая особь после кроссинговера:\n" + resultCrossbreeding.itemTwo.ToString());
                    using (var sw = File.AppendText(@"D:\Example.txt"))
                        sw.WriteLine("Расписание на её основе:\n" + schThree.ToString() + "\n" + schThree.Weight().weight.ToString() + "\n");
                    if (resultCrossbreeding.itemTwo.GeneMutation())
                    {
                        using (var sw = File.AppendText(@"D:\Example.txt"))
                            sw.WriteLine("Мутировавшая вторая особь:\n" + resultCrossbreeding.itemTwo.ToString());
                        using (var sw = File.AppendText(@"D:\Example.txt"))
                        {
                            ScheduleObject sch = new ScheduleObject(resultCrossbreeding.itemOne, sizeRange, countPC);
                            sw.WriteLine("Расписание на её основе:\n" + schThree.ToString() + sch.Weight().weight.ToString() + "\n");
                        }
                    }
                    else
                    {
                        using (var sw = File.AppendText(@"D:\Example.txt"))
                            sw.WriteLine("Второй ребёнок не мутировал.\n");
                    }

                    var resEqual = schOne.MinSchedule(schTwo).MinSchedule(schThree);
                    lIndividual.Add(resEqual.GetIndividual());
                    using (var sw = File.AppendText(@"D:\Example.txt"))
                        sw.WriteLine("В новое поколение добавлена следующая особь: ");
                    using (var sw = File.AppendText(@"D:\Example.txt"))
                        sw.WriteLine(resEqual.GetIndividual().ToString());
                }
                else
                {
                    using (var sw = File.AppendText(@"D:\Example.txt"))
                        sw.WriteLine("Родители не скрестились.");
                    var buffInv = lGenIndividual[iter].CloneIndividualFull();
                    using (var sw = File.AppendText(@"D:\Example.txt"))
                        sw.WriteLine("Пытаемся мутировать родителя.\n");
                    if (buffInv.GeneMutation())
                    {
                        using (var sw = File.AppendText(@"D:\Example.txt"))
                            sw.WriteLine("Мутировали родителя:\n" + buffInv.ToString());
                        ScheduleObject schOne = new ScheduleObject(lGenIndividual[iter], sizeRange, countPC);
                        ScheduleObject schTwo = new ScheduleObject(buffInv, sizeRange, countPC);

                        using (var sw = File.AppendText(@"D:\Example.txt"))
                            sw.WriteLine("Расписание на основе мутированного родителя:\n" + schTwo.ToString());
                        var item = schOne.MinSchedule(schTwo);
                        using (var sw = File.AppendText(@"D:\Example.txt"))
                            sw.WriteLine("В популяцию попадает данная особь:\n" + item.GetIndividual().ToString());
                        lIndividual.Add(item.GetIndividual());
                    }
                    else
                    {
                        using (var sw = File.AppendText(@"D:\Example.txt"))
                            sw.WriteLine("Мутации родителя не произошло.\n");
                        lIndividual.Add(lGenIndividual[iter]);
                    }
                }     
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in lIndividual)
            {
                ScheduleObject sch = new ScheduleObject(item, sizeRange, countPC);
                sb.Append(item.ToString() + "\n" + sch.Weight().weight.ToString() + "\n");
            }
            return sb.ToString();
        }

    }
}
