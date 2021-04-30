using System;
using System.Collections.Generic;
using System.IO;

namespace Lab5EvrMethod
{
    class MainProgrammsClass
    {
        static void Main(string[] args)
        {
            Int32 countPC = 5;                              //Количество процессоров
            Int32 countTasks = 12;                          //Колчество задач
            Int32 lowRangeTasks = 10;                       //Нижняя граница расписания
            Int32 highRangeTasks = 21;                      //Верхняя граница расписания
            Int32 sizeRange = 255;                          //Размер границы генотипа
            Int32 countIndividualInPopulation = 10;         //Количество особей в популяции
            Int32 countBestIndividual = 11;                 //Количество лучших особей
            Int32 propabilityCrossover = 75;                //Вероятность кроссовера
            Int32 propabilityMutation = 77;                 //Вероятность мутации

            Population population = new Population(countPC, countTasks, lowRangeTasks, highRangeTasks, sizeRange,
                                                   countIndividualInPopulation, countBestIndividual, propabilityCrossover,
                                                   propabilityMutation);

            population.CalculateAnswer();
            Console.WriteLine("Okay");

            Console.ReadKey();
        }
    }
}
