using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Interfaces;
using ClassGenerator;
using ConsoleApp1.Interfaces;

namespace BLL.Services
{
    public class Service : IService
    {
        public IMainDalInterface DALInterface1
        {
            get => DALInterface2;
            set => DALInterface2 = value;
        }

        public IMainDalInterface DALInterface2 { get; set; }

        public Service(IMainDalInterface dalInterface)
        {
            DALInterface1 = dalInterface;
        }

        private RecipeData GetRecipeData(ProcessData process)
        {
            var recipeData = new RecipeData();
            object Recipe = process.Recipes[0];

           
            List<PropertyClass> list = new List<PropertyClass>();
            Dictionary<string, PropertyClass> maiDictionary = new Dictionary<string, PropertyClass>();

            
            foreach (var property in Recipe.GetType().GetProperties())
            {
                var propertyClass= new PropertyClass();
                propertyClass.Key = property.Name;
                propertyClass.Value = property.GetValue(Recipe);
                var CustomAttributesData = property.GetCustomAttributesData();
                var atributeGroupDictionary = new Dictionary<string, Dictionary<string, object>>();
                foreach (var attribute_groups in CustomAttributesData)
                {
                    var atributeDictionary = new Dictionary<string,object>();
                    if (attribute_groups.NamedArguments.Count == 0)
                    {
                        var value = attribute_groups.ConstructorArguments.First().Value;
                        var key = attribute_groups.ConstructorArguments.First().ArgumentType.ToString();
                        atributeDictionary.Add(key, value);
                     }
                    else
                    {
                        atributeDictionary =
                            attribute_groups.NamedArguments.ToDictionary
                                (attribute => attribute.MemberName, attribute => attribute.TypedValue.Value);
                        }
                    atributeGroupDictionary.Add(attribute_groups.AttributeType.Name, atributeDictionary);
                    }
                propertyClass.Attributes = atributeGroupDictionary;
              
                list.Add(propertyClass);
//                recipes_dict.Add(property.Name, propertyClass);
//                recipes.Add(recipes_dict);
            }
            recipeData.recipes = list;
            return recipeData;
        }

        private Dictionary<string, object> GetReportData(ProcessData process, int key)
        {
            object Report = process.Reports[key];

            var Report_dictionary = new Dictionary<string, object>();

            foreach (var property in Report.GetType().GetProperties())
                Report_dictionary.Add(property.Name, property.GetValue(Report));
            return Report_dictionary;
        }


        public DataBLL GetProcessData(int Id)
        {
            var Data = new DataBLL();
            Data.ReportDictionary = new List<Dictionary<string, object>>();
            var process = DALInterface1.GetDataById(Id);
            Data.ProcessData = process;
            Data.RecipeData = GetRecipeData(process);
            for (var key = 0; key < process.Reports.Count; key++)
            {
                var report = process.Reports[key];
                Data.ReportDictionary.Add(GetReportData(process, key));
            }
            return Data;
        }

        public int GetProcessDataCount()
        {
            var count = DALInterface1.GetCountandDates();

            return count.Count;
        }

        public string GetStuff()
        {
            var process = GetProcessData(1);
            return process.ToString();
        }
    }
}