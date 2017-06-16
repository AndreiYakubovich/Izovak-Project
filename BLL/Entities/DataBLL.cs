using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassGenerator;

namespace BLL.Entities
{
    public class DataBLL
    {
        
        public ProcessData ProcessData { get; set; }
        public RecipeData RecipeData { get; set; }
        public List<Dictionary<string,object>> ReportDictionary { get; set; }
        
    }
}
