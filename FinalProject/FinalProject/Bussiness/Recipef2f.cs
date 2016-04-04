using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Model;
using Windows.Web.Http;
using Windows.Data.Json;

namespace FinalProject.Bussiness
{
    class Recipef2f
    {
        private const String appKey = "c8c358aba94fa326ea40fcc2c6731591";
        private const String stringUri = "http://food2fork.com/api/get?key=" + appKey + "&rId=";

        public async Task<Model.Recipef2f> getRecipe(String recipeId)
        {
            var httpClient = new HttpClient();
            var uri = new Uri(stringUri+recipeId);

            HttpResponseMessage result = await httpClient.GetAsync(uri);

            Model.Recipef2f recipe = new Model.Recipef2f();

            JsonObject jsonObject = JsonObject.Parse(result.Content.ToString());

            JsonObject jsonValue = jsonObject.GetNamedObject("recipe", new JsonObject());
            
            recipe.Publisher = jsonValue.GetNamedString("publisher", "");
            recipe.F2fUrl = jsonValue.GetNamedString("f2f_url", "");
            JsonArray ingredientsArray = jsonValue.GetNamedArray("ingredients");
            foreach(JsonValue ingredient in ingredientsArray)
            {
                recipe.IngredientsList += ingredient.ToString() + " \n ";
            } 
            recipe.SourceUrl = jsonValue.GetNamedString("source_url", "");
            recipe.ImageUrl = jsonValue.GetNamedString("image_url", "");
            recipe.SocialRank = jsonValue.GetNamedNumber("social_rank", 0);
            recipe.PublisherUrl = jsonValue.GetNamedString("publisher_url", "");
            recipe.Title = jsonValue.GetNamedString("title", "");
            recipe.RecipeId = recipeId;

            return recipe;
        }
    }
}
