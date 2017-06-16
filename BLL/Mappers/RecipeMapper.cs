using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using ClassGenerator.AbstractClasses;

namespace BLL.Mappers
{
    public static class RecipeMapper
    {

        public static RecipeBLL ToBLLRecipe(this AbstractRecipe model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            
        }

    }
}
