using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BLL.Entities;
using BLL.Interfaces;
using ClassGenerator;
using DAL.Interfaces;

namespace BLL.Services
{
    public class Service : IService
    {

        private IMainDalInterface DalInterface { get;}

        public Service(IMainDalInterface dalInterface)
        {
            DalInterface = dalInterface;
        }

        private static PropertyList GetAllProperties(object entity)
        {

            PropertyList list = new PropertyList();
           foreach (var property in entity.GetType().GetProperties())
            {
                var propertyClass = new PropertyClass()
                {
                    Name = property.Name,
                    Value = property.GetValue(entity)
                };
                IList<CustomAttributeData> customAttributesData = property.GetCustomAttributesData();
                Dictionary<string, object> Attributes = new Dictionary<string, object>();
                if (customAttributesData != null)
                    foreach (var attributeGroups in customAttributesData)
                    {
                       foreach (var atribute in attributeGroups.NamedArguments)
                        {
                               Attributes.Add(atribute.MemberName, atribute.TypedValue);
                        }

                        foreach (var atribute in attributeGroups.ConstructorArguments)
                        {
                            Attributes.Add(attributeGroups.AttributeType.Name, atribute.Value);
                        }
                    }
                propertyClass.Attributes = Attributes;
                if(propertyClass.Attributes.Count != 0 && propertyClass.Attributes.First(a=>a.Key== "BrowsableAttribute").Value.Equals(true))
                list.Add(propertyClass);
            }
            return list;
        }


        private static CategoriesDictionary GetSortedProperties(PropertyList list)
        {
            CategoriesDictionary sortedProperies = new CategoriesDictionary();
            
            Dictionary<string, List<string>> Categories = GetCategoriesAndGroups(list);
            foreach (var category in Categories)
            {
                GroupsDictionary groups = new GroupsDictionary();
                foreach (var @group in category.Value)
                {
                    PropertyList propertyList = new PropertyList();
                    foreach (var property in list)
                    {
                        if (property.Attributes.Count != 0 &&
                            (property.Attributes.First(a => a.Key == "CategoryAttribute").Value.ToString() == category.Key && 
                            property.Attributes.First(a => a.Key == "GroupName").Value.ToString() == @group))
                            propertyList.Add(property);
                    }
                    groups.Add(@group, propertyList);
                }

                sortedProperies.Add(category.Key,groups);
            }
            return sortedProperies;
        }

        private static Dictionary<string, List<string>> GetCategoriesAndGroups(PropertyList list)
        {
            Dictionary<string, List<string>> CategoriesAndGroups = new Dictionary<string, List<string>>();
            List<object> categories = new List<object>();

            foreach (var recipe in list)
            {
                if (recipe.Attributes.Count != 0)
                {
                    if (categories.Contains(recipe.Attributes.First(a => a.Key == "CategoryAttribute").Value) == false)
                        categories.Add(recipe.Attributes.First(a => a.Key == "CategoryAttribute").Value);
                }
            }
            foreach (var category in categories)
            {
                List<string> groups = new List<string>();
                foreach (var recipe in list)
                {
                    if (recipe.Attributes.Count != 0 &&
                        (recipe.Attributes.First(a => a.Key == "CategoryAttribute").Value.ToString() == category.ToString() &&
                         (recipe.Attributes.ContainsKey("GroupName") && 
                         groups.Contains(recipe.Attributes.First(a => a.Key == "GroupName").Value.ToString()) == false)))
                        groups.Add(recipe.Attributes.First(a => a.Key == "GroupName").Value
                            .ToString());
                }
                CategoriesAndGroups.Add(category.ToString(), groups);
            }

            return CategoriesAndGroups;
        }


        private static List<CategoriesDictionary> GetReportData(ProcessData process)
        {
            return (from report in process.Reports let catdict = new CategoriesDictionary() select GetSortedProperties(GetAllProperties(report))).ToList();
        }
        

        private static CategoriesDictionary GetRecipeData(ProcessData process)
        {
            CategoriesDictionary data = GetSortedProperties(GetAllProperties(process.Recipes[0]));
            return data;
        }


        public List<DataBLL> GetProcessData()
        {
            List<DataBLL> ListofProcessData = new List<DataBLL>();
            
            List<ProcessData> listOfProcess = DalInterface.GetData();
            foreach (var process in listOfProcess)
            {
                var data = new DataBLL();
                data.ProcessData = process;
                if(process.Recipes.Count!=0)
                    data.RecipeData = GetRecipeData(process);
                if (process.Reports.Count != 0)
                    data.ReportData = GetReportData(process);
                ListofProcessData.Add(data);
            }
            return ListofProcessData;
        }

     
        public string GetStuff()
        {
            var process = GetProcessData();
            return process.ToString();
        }
    }
}