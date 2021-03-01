using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashCode2021.Aggregators;
using HashCode2021.Helpers;
using HashCode2021.Models;
using HashCode2021.Optimizers;
using HashCode2021.Processors;

namespace HashCode2021
{
    class Program
    {
        private const string A_File = "a.txt";
        private const string B_File = "b.txt";
        private const string C_File = "c.txt";
        private const string D_File = "d.txt";
        private const string E_File = "e.txt";
        private const string F_File = "f.txt";

        private static List<string> InputFiles = new List<string>
        {
            A_File,
            B_File,
            C_File,
            D_File,
            E_File,
            F_File
        };

        private static readonly List<IProcessor> AllProcessors = new List<IProcessor>
        {
            new SlightlySmartProcessor(),
            new RandomProcessor(),

        };

        private static readonly Dictionary<string, IProcessor> ProcessorByFile = new Dictionary<string, IProcessor>
        {
            { A_File, new RandomProcessor() },
            { B_File, new SlightlySmartProcessor() },
            { C_File, new SlightlySmartProcessor() },
            { D_File, new SlightlySmartProcessor() },
            { E_File, new SlightlySmartProcessor() },
            { F_File, new SlightlySmartProcessor() },
        };

        static async Task Main(string[] args)
        {
            Console.WriteLine("Program started...\n");

            foreach (var inputFile in InputFiles)
            {
                var inputModel = await InputHelper.InitAsync(inputFile);
                var insightsModel = new InsightsModel(inputModel);
                var outputTasks = AllProcessors
                    .Select(async processor => await processor.ProcessAsync(inputModel, insightsModel))
                    .ToList();

                await Task.WhenAll(outputTasks);

                var outputs = outputTasks.Select(task => task.Result).ToList();
                
                IAggregator aggregator = new FirstPrevailsAndApplyAverageAggregator();

                var finalOutput = await aggregator.AggregateAsync(outputs);
                
                IOptimizer optimizer = new IntersectionsWithoutTrafficOptimizer();
                
                //await optimizer.OptimizeAsync(inputModel, insightsModel, finalOutput);

                await OutputHelper.PrintAsync($"{inputFile}.out", finalOutput);

                Console.WriteLine("\nProgram finished!");
            }
        }
    }
}
