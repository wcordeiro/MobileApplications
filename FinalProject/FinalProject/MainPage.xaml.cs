using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FinalProject.Bussiness;
using Windows.ApplicationModel.Activation;
using System.Diagnostics;
using Windows.Storage;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        List<Model.ResponseYummly> listRecipe;
        List<Model.Recipef2f> listf2fRecipe;

        public MainPage()
        {
            this.InitializeComponent();
            listRecipe = new List<Model.ResponseYummly>();
            listf2fRecipe = new List<Model.Recipef2f>();
        }

        private void InfoImage_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Ingredientstxtblck_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(IngredientsSearch));
        }

        private void Moretxtblck_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdvancedSearch));
        }

        private async void Recipiestxtblck_Tapped(object sender, TappedRoutedEventArgs e)
        {

            Responsef2f response = new Responsef2f();
            Bussiness.Recipef2f responseRecipe = new Bussiness.Recipef2f();
            String responseString = await response.getData(1);
            List<Model.ResponseRecipef2f> recipeList;
            List<Model.Recipef2f> recipeFinalList = new List<Model.Recipef2f>();
            recipeList = response.parseResponse(responseString);
            foreach (Model.ResponseRecipef2f recipe in recipeList)
            {
                Model.Recipef2f recipeModel = new Model.Recipef2f();
                recipeModel = await responseRecipe.getRecipe(recipe.RecipeId);
                recipeFinalList.Add(recipeModel);
            }
            Frame.Navigate(typeof(ShowRecipies), recipeFinalList);
        }

        private async void FavoriteImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int value;
            int yummlyFavorites;
            ApplicationDataContainer roamingSettings;
            try
            {
                // to get roaming settings.....
                roamingSettings =
                    ApplicationData.Current.RoamingSettings;
                value = (int)roamingSettings.Values["FavoriteYummly"];
                yummlyFavorites = value;
            }
            catch (Exception exRoaming)
            {
                // if the value key is not set, then the exception will cause
                // this code to execute, which just sets the value to 0 (default)
                string errMsg = exRoaming.Message;
                yummlyFavorites = 0;
            } // end inner try for roaming settings
            if(yummlyFavorites != 0){
               await this.readYummlyFiles(yummlyFavorites);
            }
            int f2fFavorites;
            try
            {
                // to get roaming settings.....
                roamingSettings =
                    ApplicationData.Current.RoamingSettings;
                value = (int)roamingSettings.Values["Favoritef2f"];
                f2fFavorites = value;
            }
            catch (Exception exRoaming)
            {
                // if the value key is not set, then the exception will cause
                // this code to execute, which just sets the value to 0 (default)
                string errMsg = exRoaming.Message;
                f2fFavorites = 0;
            } // end inner try for roaming settings
            if (yummlyFavorites != 0)
            {
                await this.readf2fFiles(f2fFavorites);
            }
            List<Object> list = new List<Object>();
            list.Add(listRecipe);
            list.Add(listf2fRecipe);
            Frame.Navigate(typeof(ShowFavorites), list);
        }

        private async Task<int> readf2fFiles(int f2fFavorites)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            // List<Model.ResponseYummly> listRecipe = new List<Model.ResponseYummly>();

            StorageFile sampleFile;
            for (int i = 0; i < f2fFavorites; i++)
            {
                string fileText = "";
                try
                {
                    sampleFile = await storageFolder.GetFileAsync("f2f" + i + ".txt");
                    fileText = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

                }
                catch (Exception myE)
                {
                    string message = myE.Message;
                }
                Model.Recipef2f recipe = new Model.Recipef2f();
                String[] tokens = fileText.Split('\n');
                recipe.ImageUrl = tokens[0]; // URL of the image
                recipe.SourceUrl = tokens[1]; ; // Original Url of the recipe on the publisher's site
                recipe.RecipeId = tokens[2]; ; // Id used for follow-up request
                recipe.F2fUrl = tokens[3]; ; // Url of the recipe on Food2Fork.com
                recipe.Title = tokens[4]; ; // Title of the recipe
                recipe.Publisher = tokens[5]; ; // Name of the Publisher
                recipe.PublisherUrl = tokens[6]; ; // Base url of the publisher
                recipe.SocialRank = Double.Parse(tokens[7]); ; // The Social Ranking of the Recipe (As determined by f2f Ranking Algorithm)
                recipe.IngredientsList = tokens[8]; ;
                listf2fRecipe.Add(recipe);
            }
            return 1;
        }

        private async Task<int> readYummlyFiles(int yummlyFavorites)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
           // List<Model.ResponseYummly> listRecipe = new List<Model.ResponseYummly>();
            
            StorageFile sampleFile;
            for (int i = 0; i < yummlyFavorites; i++)
            {
                string fileText = "";
                try
                {
                    sampleFile = await storageFolder.GetFileAsync("Yummly" + i + ".txt");
                    fileText = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

                }
                catch (Exception myE)
                {
                    string message = myE.Message;
                }
                Model.ResponseYummly recipe = new Model.ResponseYummly();
                String[] tokens = fileText.Split('\n');
                recipe.ImageUrl = tokens[0];
                recipe.SourceDisplayName = tokens[1];
                recipe.Ingredients = tokens[3];
                recipe.Id = tokens[3];
                recipe.RecipeName = tokens[4];
                recipe.TotalTime = Double.Parse(tokens[5]);
                recipe.Course = tokens[6];
                recipe.Cuisine = tokens[7];
                recipe.Flavors = tokens[8];
                recipe.Rating = Double.Parse(tokens[9]);
                listRecipe.Add(recipe);
            }
            return 1;
        }
    }
}
