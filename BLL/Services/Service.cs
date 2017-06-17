using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        private static List<PropertyClass> GetAllProperties(object entity)
        {
            
            List<PropertyClass> list = new List<PropertyClass>();
            Dictionary<string, PropertyClass> maiDictionary = new Dictionary<string, PropertyClass>();


            foreach (var property in entity.GetType().GetProperties())
            {
                var propertyClass = new PropertyClass()
                {
                    Key = property.Name,
                    Value = property.GetValue(entity)
                };
                IList<CustomAttributeData> customAttributesData = property.GetCustomAttributesData();
                Dictionary<string,object> Attributes = new Dictionary<string, object>();
                if (customAttributesData != null)
                    foreach (var attributeGroups in customAttributesData)
                    {
                       if (attributeGroups.NamedArguments != null)
                            foreach (var atribute in attributeGroups.NamedArguments)
                            {
                                Attributes.Add(atribute.MemberName, atribute.TypedValue);
                            }

                        if (attributeGroups.ConstructorArguments != null)
                            foreach (var atribute in attributeGroups.ConstructorArguments)
                            {
                                Attributes.Add(attributeGroups.AttributeType.Name, atribute.Value);
                            }
                    }
                propertyClass.Attributes = Attributes;
                list.Add(propertyClass);
            }
            return list;
        }


        private RecipeData GetRecipeData(ProcessData process)
        {
            var recipeData = new RecipeData();
         
            recipeData.Recipes = GetAllProperties(process.Recipes[0]);
            recipeData.Chapters = GetChapters(recipeData.Recipes);
           return recipeData;
        }


        private static List<object> GetChapters(List<PropertyClass> recipeData)
        {
            List<object> chapters = new List<object>();
            foreach (var recipe in recipeData)
            {
                if (recipe.Attributes.Count != 0)
                {
                    if (chapters.Contains(recipe.Attributes.First(a => a.Key == "CategoryAttribute").Value) == false)
                    {
                        chapters.Add(recipe.Attributes.First(a => a.Key == "CategoryAttribute").Value);
                    }
                }
            }
            return chapters;
        }


        private static Dictionary<string, object> GetReportData(ProcessData process, int key)
        {
            object report = process.Reports[key];
            return report.GetType().GetProperties().ToDictionary(property => property.Name, property => property.GetValue(report));
        }


        public DataBLL GetProcessData(int Id)
        {
            var data = new DataBLL {ReportDictionary = new List<Dictionary<string, object>>()};
            var process = DALInterface1.GetDataById(Id);
            data.ProcessData = process;
            data.RecipeData = GetRecipeData(process);
            for (var key = 0; key < process.Reports.Count; key++)
            {
                data.ReportDictionary.Add(GetReportData(process, key));
            }
            return data;
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