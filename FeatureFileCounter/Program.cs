using System;
using CommandLine;

namespace PickleTestCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(options =>
                {
                    var inputPath = options.InputPath;
                    var branchName = options.BranchName;
                    var outputPath = options.OutputPath;

                    var testCounter = new TestCounter(inputPath);
                    testCounter.DiscoverFeatureFiles();
                    testCounter.DiscoverTests();

                    var json = testCounter.ConvertToJson();
                    Console.WriteLine(json);
                    Logger.LogJson(branchName, json, outputPath);

                    var testJson = @"{ ""appointmentQPos"": 954 }";
                    Logger.UpdateMasterJson(testJson, outputPath);
                });
        }
    }
}
