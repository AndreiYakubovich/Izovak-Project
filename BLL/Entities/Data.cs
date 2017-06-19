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
        public CategoriesDictionary RecipeData  { get; set; }
        public List<CategoriesDictionary> ReportData { get; set; }
        
    }

    public class CategoriesDictionary : Dictionary<string, GroupsDictionary>{}

    public class GroupsDictionary : Dictionary<string, PropertyList>{}

    public class PropertyList : List<PropertyClass> { }

}
