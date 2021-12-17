using AutoMapper;
using RecipeBook.Data;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Configuration
{
    public class MapperInitializer:Profile
    {
        public MapperInitializer()
        {
            CreateMap<Recipe, RecipeDto>().ReverseMap();
            CreateMap<Recipe, CreateRecipeDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Ingredient, CreateIngredientDto>().ReverseMap();
        }
    }
}
