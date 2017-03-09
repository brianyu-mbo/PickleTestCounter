using System.Collections.Generic;
using System.Linq;

namespace PickleTestCounter
{
    class FeatureFile
    {
        private readonly string _filePath;
        private string[] _lines;
        /// <summary>
        /// Name of the widget
        /// </summary>
        public IEnumerable<string> Categories { get; set; } 
        

        public FeatureFile(string filePath)
        {
            _filePath = filePath;
        }

        public void Setup()
        {
            _lines = System.IO.File.ReadAllLines(_filePath);
            SetupCategoryTags();
        }

        /// <summary>
        /// Gets all category tags.
        /// Assumes they are on the first line.
        /// </summary>
        public void SetupCategoryTags()
        {
            var categoryLine = _lines[0].Replace(" ", "");
            var categories = categoryLine.Split('@');
            var cleanCategories = categories.Where(category => category.Length > 1).ToList();
            
            Categories = cleanCategories;
        }

        /// <summary>
        /// Gets the Widget Category tag.
        /// Assumes that the category contains a ':' character.
        /// </summary>
        /// <returns></returns>
        public string GetWidgetCategoryTag()
        {
            return Categories.FirstOrDefault(category => category.Contains(':'));
        }

        /// <summary>
        /// Returns the number of scenarios within a feature file.
        /// Checks for keyword 'Scenario:'.
        /// Also checks that it isn't a comment (starts with a #).
        /// </summary>
        /// <returns></returns>
        public int GetNumScenario()
        {
            return _lines.Count(line => line.Trim().IndexOf('#') != 0 && line.Contains("Scenario:"));
        } 
    }
}
