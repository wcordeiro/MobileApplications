using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Model;
using System.Diagnostics;
using Windows.Web.Http;
using System.Runtime.Serialization.Json;
using System.IO;
using Windows.Data.Json;

namespace FinalProject.Bussiness
{
    class Responsef2f
    {
        private const String countKey = "count";
        private const String recipesKey = "recipes";

        private const String appKey = "c8c358aba94fa326ea40fcc2c6731591";
        private String stringUri = "http://food2fork.com/api/search?key="+appKey;
        private FinalProject.Model.Responsef2f responseModel;

        public string StringUri
        {
            get
            {
                return stringUri;
            }

            set
            {
                stringUri = value;
            }
        }

        public Responsef2f()
        {
            responseModel = new Model.Responsef2f();
        }

        public List<ResponseRecipef2f> parseResponse(String response)
        {
            JsonObject jsonObject = JsonObject.Parse(response);
            responseModel.Count = Convert.ToInt32(jsonObject.GetNamedNumber(countKey, 0));
            List<ResponseRecipef2f> recipeList = new List<ResponseRecipef2f>();
            foreach (IJsonValue jsonValues in jsonObject.GetNamedArray(recipesKey, new JsonArray()))
            {
                JsonObject jsonValue = JsonObject.Parse(jsonValues.ToString());
                ResponseRecipef2f recipe = new ResponseRecipef2f();
                recipe.Publisher = jsonValue.GetNamedString("publisher", "");
                recipe.F2fUrl = jsonValue.GetNamedString("f2f_url", "");
                recipe.Title = jsonValue.GetNamedString("title", "");
                recipe.SourceUrl = jsonValue.GetNamedString("source_url", "");
                recipe.RecipeId = jsonValue.GetNamedString("recipe_id", "");
                recipe.ImageUrl = jsonValue.GetNamedString("image_url", "");
                recipe.SocialRank = jsonValue.GetNamedNumber("social_rank", 0);
                recipe.PublisherUrl = jsonValue.GetNamedString("publisher_url", "");
                recipeList.Add(recipe);
            }
            responseModel.Recipe = recipeList;
            return recipeList;
        }

        public async Task<String> getData(int page)
        {
            var httpClient = new HttpClient();
            var uri = new Uri(StringUri+"&page="+page.ToString());

            HttpResponseMessage result = await httpClient.GetAsync(uri);

            return result.Content.ToString();
        }
    }



}
