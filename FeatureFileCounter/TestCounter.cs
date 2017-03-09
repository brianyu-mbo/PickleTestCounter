using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PickleTestCounter;

namespace PickleTestCounter
{
    class TestCounter
    {
        private readonly string _directoryPath;

        private IList<FeatureFile> _featureFiles; 

        public IDictionary<string, int> CategoryTestCountDictionary { get; set; } = new SortedDictionary<string, int>();

        public TestCounter(string path)
        {
            if (Directory.Exists(path))
            {
                _directoryPath = path;
            }
            else
            {
                throw new Exception($"Path: {path} not found.");
            }
        }
        
        public void DiscoverFeatureFiles()
        {
            var myFiles =
                Directory.GetFiles(_directoryPath, "*.*", SearchOption.AllDirectories)
                    .Where(file =>
                    {
                        var extension = Path.GetExtension(file);
                        return extension != null && extension.Equals(".feature");
                    });

            _featureFiles = new List<FeatureFile>();

            foreach (var file in myFiles)
            {
                var featureFile = new FeatureFile(file);
                _featureFiles.Add(featureFile);
            }
        }

        public void DiscoverTests()
        {
            foreach (var featureFile in _featureFiles)
            {
                featureFile.Setup();
                foreach (var category in featureFile.Categories)
                {
                    var numTests = featureFile.GetNumScenario();

                    if (CategoryTestCountDictionary.ContainsKey(category))
                    {
                        CategoryTestCountDictionary[category] += numTests;
                    }
                    else
                    {
                        CategoryTestCountDictionary[category] = numTests;
                    }
                }
            }
        }

        public void PrintDictionary()
        {
            foreach (var key in CategoryTestCountDictionary.Keys)
            {
                Console.WriteLine($"Key: {key}  Value: {CategoryTestCountDictionary[key]}");
            }
        }

        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(CategoryTestCountDictionary, Formatting.Indented);
        }
    }
}
