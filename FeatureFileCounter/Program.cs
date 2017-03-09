using System;

namespace PickleTestCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var directoryPath = args[0];

                if (args.Length > 1)
                {
                    var branchName = args[1];

                    var testCounter = new TestCounter(directoryPath);
                    testCounter.DiscoverFeatureFiles();
                    testCounter.DiscoverTests();

                    var json = testCounter.ConvertToJson();
                    Logger.LogJson(branchName, json);
                }
                else
                {
                    Console.WriteLine("No branch name given");
                }
            }
            else
            {
                Console.WriteLine("There is no path directory given. Exiting now.");
            }
           
        }
    }
}
