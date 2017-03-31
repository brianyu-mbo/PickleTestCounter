using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickleTestCounter
{
    public class FeatureFileEntry
    {
        public string Name { get; set; }

        public int NumberOfTests { get; set; }

        public string BranchName { get; set; }

        public string LastUpdated { get; set; }
    }
}
