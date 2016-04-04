using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;
using FinalProject.Model;
using System.Net.Http.Headers;

namespace FinalProject.Bussiness
{
    class ResponseYummly
    {
        private const String appId = "b6199d25";
        private const String appKey = "3612c18e13a9b1ec9a0a55a3b946d85f";
        private const String stringUri = "http://api.yummly.com/v1/api/recipes?_app_id="+appId+"&_app_key="+appKey;

        public async Task<List<Model.ResponseYummly>> getRecipes(String query)
        {
            var httpClient = new HttpClient();
            var uri = new Uri(stringUri + query);
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("X-Yummly-App-ID", appId);
          //  httpClient.DefaultRequestHeaders.Add("Authorization", "X-Yummly-App-ID:"  + appId);
          //  httpClient.DefaultRequestHeaders.Add("Authorization", "X-Yummly-App-Key:" + appKey);
            HttpResponseMessage result = await httpClient.GetAsync(uri);

            List<Model.ResponseYummly> listRecipes =  new List<Model.ResponseYummly>();

            JsonObject jsonObject = JsonObject.Parse(result.Content.ToString());
            foreach(IJsonValue jsonValue in jsonObject.GetNamedArray("matches", new JsonArray()))
            {
                try {
                    JsonObject jsonRecipe = JsonObject.Parse(jsonValue.ToString());
                    Model.ResponseYummly recipe = new Model.ResponseYummly();
                    JsonObject jsonUrl = jsonRecipe.GetNamedObject("imageUrlsBySize", new JsonObject());
                    recipe.ImageUrl = jsonUrl.GetNamedString("90", "");
                    recipe.SourceDisplayName = jsonRecipe.GetNamedString("sourceDisplayName", "");
                    JsonArray ingredientsArray = jsonRecipe.GetNamedArray("ingredients", new JsonArray());
                    recipe.Ingredients = ingredientsArray.ToString();
                    recipe.Id = jsonRecipe.GetNamedString("id", "");
                    recipe.RecipeName = jsonRecipe.GetNamedString("recipeName", "");
                    recipe.TotalTime = jsonRecipe.GetNamedNumber("totalTimeInSeconds", 0);
                    JsonObject jsonAttributes = jsonRecipe.GetNamedObject("attributes", new JsonObject());
                    JsonArray courseArray = jsonAttributes.GetNamedArray("course", new JsonArray());
                    recipe.Course = courseArray.ToString();
                    JsonArray cuisineArray = jsonAttributes.GetNamedArray("cuisine", new JsonArray());
                    recipe.Cuisine = cuisineArray.ToString();
                    IJsonValue value;
                    if (jsonRecipe.TryGetValue("flavors", out value))
                    {
                        // JsonObject flavourObject = jsonRecipe.GetNamedObject("flavors", new JsonObject());
                        recipe.Flavors = value.ToString();
                    }
                    recipe.Rating = jsonRecipe.GetNamedNumber("rating", 0);
                    listRecipes.Add(recipe);
                }
                catch
                {
                    continue;
                }
            }

            return listRecipes;
        }
    }
}
