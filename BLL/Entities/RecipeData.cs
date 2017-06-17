using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class RecipeData
    {
/*
        public Dictionary<string, List<Dictionary<string, AtributesClass>>> recipes { get; set; }*/
        public List<PropertyClass> Recipes { get; set; }

        public List<object> Chapters { get; set; }
    }
}
