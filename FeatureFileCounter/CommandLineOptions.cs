using CommandLine;

namespace PickleTestCounter
{
    public class CommandLineOptions
    {
        [Option('i', "input", Required = true,
            HelpText = "Input directory to search for feature files.")]
        public string InputPath { get; set; }

        [Option('b', "branch name", Required = true,
            HelpText = "Branch name to store log")]
        public string BranchName { get; set; }

        [Option('o', "Output directory", Required = true,
            HelpText = "Output directory for the log files")]
        public string OutputPath { get; set; }
    }
}
