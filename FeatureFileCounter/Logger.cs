using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PickleTestCounter
{
    public static class Logger
    {
        /// <summary>
        /// In the log directory, given a log, create a directory with the branch name and write a log into it
        /// </summary>
        /// <param name="branchName"></param>
        /// <param name="json"></param>
        /// <param name="outputPath"></param>
        public static void LogJson(string branchName, string json, string outputPath)
        {
            var cleanBranchName =
                branchName.Replace(Path.DirectorySeparatorChar, '.').Replace(Path.AltDirectorySeparatorChar, '.');

            // Create a directory for the logs
            var directory = Path.Combine(@"PickleCounter\BranchLogs\", cleanBranchName);
            var path = Path.Combine(outputPath, directory);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Appends timestamp.txt
            var timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            path = Path.Combine(path, timeStamp + ".txt");

            if (!File.Exists(path))
            {
                WriteLog(json, path);
            }
            else
            {
                Console.WriteLine($"Somehow the {path} already exists...");
            }
        }

        /// <summary>
        /// Update the master json file
        /// </summary>
        /// <param name="json"></param>
        /// <param name="outputPath"></param>
        public static void UpdateMasterJson(string json, string outputPath)
        {
            var directory = @"PickleCounter\BranchLogs\";
            var path = Path.Combine(outputPath, directory, "MasterJSON.txt");

            Console.WriteLine(json);

            if (!File.Exists(path))
            {
                WriteLog(json, path);
            }
            else
            {
                var dictionary = ReadJson(path);

                var test = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
                foreach (var key in test.Keys)
                {
                    int count;
                    if (dictionary.TryGetValue(key, out count))
                    {
                        if (count < test[key])
                        {
                            Console.WriteLine("Yeah... it's bigger.");
                            dictionary[key] = test[key];
                        }
                    }
                    else
                    {
                        dictionary.Add(key, test[key]);
                    }
                }
            }
        }

        /// <summary>
        /// Writes the log given a string and path
        /// </summary>
        /// <param name="json"></param>
        /// <param name="path"></param>
        private static void WriteLog(string json, string path)
        {
            using (var sw = new StreamWriter(path))
            {
                sw.Write(json);
                sw.Close();
            }
        }

        private static Dictionary<string, int> ReadJson(string path)
        {
            using (var sr = new StreamReader(path))
            {
                var stringJson = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, int>>(stringJson);
            }
        }
    }
}
