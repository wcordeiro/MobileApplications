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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void InfoImage_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void SettingImage_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Ingredientstxtblck_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(IngredientsSearch));
        }

        private void Moretxtblck_Tapped(object sender, TappedRoutedEventArgs e)
        {

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
    }
}
