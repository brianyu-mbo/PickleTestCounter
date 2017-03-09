using System;
using System.IO;

namespace PickleTestCounter
{
    public static class Logger
    {
        public static void LogJson(string branchName, string json)
        {
            var directory = Path.Combine(@"..\..\Logs\", branchName);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var timeStamp = DateTime.Now.ToString("yyyy-mm-dd-HH:mm:ss");
            var path = Path.Combine(directory, timeStamp);
            path = Path.ChangeExtension(path, ".json");

            if (!File.Exists(path))
            {
                using (var sw = new StreamWriter(path))
                {
                    sw.Write(json);
                    sw.Close();
                }
            }
            else
            {
                Console.WriteLine($"Somehow the {path} already exists...");
            }
        }
    }
}
