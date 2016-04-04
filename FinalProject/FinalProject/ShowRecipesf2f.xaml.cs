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
using FinalProject.Model;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Core;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowRecipies : Page
    {

        List<Model.Recipef2f> recipeFinalList = new List<Model.Recipef2f>();
        private int page;
        int favoriteNumber;

        public ShowRecipies()
        {
            this.InitializeComponent();    
            page = 1;
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter.GetType() == recipeFinalList.GetType())
            {
                recipeFinalList = (List<Model.Recipef2f>) e.Parameter;
            }
            this.populateRecipes();
            if (Frame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += ShowRecipies_BackRequested;
        }

        private void ShowRecipies_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }

        }

        private void populateRecipes()
        {
            // PivotItem pvt;
            int i = 0;
            if(recipeFinalList.Count == 0)
            {
                PivotItem pivotItem = new PivotItem();
                pivotItem.Header = "No Results";
                TextBlock txtTitle = new TextBlock();
                txtTitle.Text = "No Results Found";
                txtTitle.Margin = new Thickness(10, 10, 10, 10);
                pivotItem.Content = txtTitle;
                pvtRecipes.Items.Add(pivotItem);
            }
            foreach(Model.Recipef2f recipe in recipeFinalList)
            {
                PivotItem pivotItem = new PivotItem();
                pivotItem.Header = recipe.Title;

                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                Image img = new Image();
                img.Source = new BitmapImage(new Uri(recipe.ImageUrl));
                img.SetValue(Grid.ColumnProperty, 0);
                grid.Children.Add(img);

                Image favorite = new Image();
                favorite.Name = "favoriteSymbol" + i.ToString();
                BitmapImage starImage = new BitmapImage();
                starImage.UriSource = new Uri("https://image.freepik.com/free-icon/favorites-star-outlined-symbol_318-69168.png");
                favorite.Source = starImage;
                favorite.HorizontalAlignment = HorizontalAlignment.Right;
                favorite.VerticalAlignment = VerticalAlignment.Top;
                favorite.Width = 15;
                favorite.Height = 15;
                favorite.Tapped += Favorite_Tapped; ;
                favorite.SetValue(Grid.ColumnProperty, 1);
                i++;

                StackPanel stk = new StackPanel();
                stk.SetValue(Grid.ColumnProperty, 1);

                stk.Name = "recipeStk";

                stk.Children.Add(favorite);

                TextBlock txtTitle= new TextBlock();
                txtTitle.Text = "Title: " + recipe.Title;
                txtTitle.Margin = new Thickness(10, 10, 10, 10);
                txtTitle.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtTitle);

                TextBlock txtRecipeId = new TextBlock();
                txtRecipeId.Text = "RecipeId: " + recipe.RecipeId;
                txtRecipeId.Margin = new Thickness(10,10,10,10);
                txtRecipeId.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtRecipeId);

                TextBlock txtIngredientsList = new TextBlock();
                txtIngredientsList.Text = "Ingredients: " + recipe.IngredientsList;
                txtIngredientsList.Margin = new Thickness(10, 10, 10, 10);
                txtIngredientsList.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtIngredientsList);

                TextBlock txtSocialRank = new TextBlock();
                txtSocialRank.Text = "Social Rank: " + recipe.SocialRank.ToString();
                txtSocialRank.Margin = new Thickness(10, 10, 10, 10);
                txtSocialRank.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtSocialRank);

                TextBlock txtPublisher = new TextBlock();
                txtPublisher.Text = "Publisher: " + recipe.Publisher;
                txtPublisher.Margin = new Thickness(10, 10, 10, 10);
                txtPublisher.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtPublisher);

                TextBlock txtPublisherUrl = new TextBlock();
                txtPublisherUrl.Text = "Publisher URL: " + recipe.PublisherUrl;
                txtPublisherUrl.Margin = new Thickness(10, 10, 10, 10);
                txtPublisherUrl.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtPublisherUrl);

                TextBlock txtSourceUrl = new TextBlock();
                txtSourceUrl.Text = "Source URL: " + recipe.SourceUrl;
                txtSourceUrl.Margin = new Thickness(10, 10, 10, 10);
                txtSourceUrl.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtSourceUrl);

                TextBlock txtF2fUrl = new TextBlock();
                txtF2fUrl.Text = "F2F URL: " + recipe.F2fUrl;
                txtF2fUrl.Margin = new Thickness(10, 10, 10, 10);
                txtF2fUrl.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtF2fUrl);

                grid.Children.Add(stk);

                pivotItem.Content = grid;
                pvtRecipes.Items.Add(pivotItem);
            }
        }

        private void Favorite_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var favoriteImage = (Image)this.FindName("favoriteSymbol" + pvtRecipes.SelectedIndex.ToString());
            BitmapImage starImage = new BitmapImage();
            starImage.UriSource = new Uri("http://images.all-free-download.com/images/graphiclarge/favorite_icon_55521.jpg");
            favoriteImage.Source = starImage;
            favoriteImage.HorizontalAlignment = HorizontalAlignment.Right;
            favoriteImage.VerticalAlignment = VerticalAlignment.Top;
            favoriteImage.Width = 15;
            favoriteImage.Height = 15;
            favoriteImage.SetValue(Grid.ColumnProperty, 1);
            var stk = (StackPanel)this.FindName("recipeStk");
            stk.UpdateLayout();
            this.saveFavorite();
        }

        private async void saveFavorite()
        {
            int value;
            ApplicationDataContainer roamingSettings;
            try
            {
                // to get roaming settings.....
                roamingSettings =
                    ApplicationData.Current.RoamingSettings;
                value = (int)roamingSettings.Values["Favoritef2f"];
                favoriteNumber = value;
            }
            catch (Exception exRoaming)
            {
                // if the value key is not set, then the exception will cause
                // this code to execute, which just sets the value to 0 (default)
                string errMsg = exRoaming.Message;
                favoriteNumber = 0;
            } // end inner try for roaming settings
            Model.Recipef2f recipe = recipeFinalList[pvtRecipes.SelectedIndex];
            StorageFolder storageFolder = ApplicationData.Current.RoamingFolder;
            StorageFile sampleFile;
            string fileText = "";
            try
            {
                sampleFile = await storageFolder.GetFileAsync("f2f" + favoriteNumber + ".txt");
                fileText = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

            }
            catch (Exception myE)
            {
                string message = myE.Message;
                sampleFile = await storageFolder.CreateFileAsync(("f2f" + favoriteNumber + ".txt"));
            }
            String objectRecipe = "";
            objectRecipe += recipe.ImageUrl + "\n"; // URL of the image
            objectRecipe += recipe.SourceUrl + "\n"; // Original Url of the recipe on the publisher's site
            objectRecipe += recipe.RecipeId + "\n"; // Id used for follow-up request
            objectRecipe += recipe.F2fUrl + "\n"; // Url of the recipe on Food2Fork.com
            objectRecipe += recipe.Title + "\n"; // Title of the recipe
            objectRecipe += recipe.Publisher + "\n"; // Name of the Publisher
            objectRecipe += recipe.PublisherUrl + "\n"; // Base url of the publisher
            objectRecipe += recipe.SocialRank + "\n"; // The Social Ranking of the Recipe (As determined by f2f Ranking Algorithm)
            objectRecipe += recipe.IngredientsList + "\n"; 

        await Windows.Storage.FileIO.WriteTextAsync(sampleFile, objectRecipe);
            favoriteNumber++;
            roamingSettings =
                ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["Favoritef2f"] = favoriteNumber;
        }

        private async void pvtRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((pvtRecipes.SelectedIndex+27) % 10 == 0)
            {
                page++;
                Bussiness.Responsef2f response = new Bussiness.Responsef2f();
                Bussiness.Recipef2f responseRecipe = new Bussiness.Recipef2f();
                String responseString = await response.getData(page);
                List<Model.ResponseRecipef2f> recipeList;
                //List<Model.Recipef2f> recipeFinalList = new List<Model.Recipef2f>();
                recipeList = response.parseResponse(responseString);
                foreach (Model.ResponseRecipef2f recipe in recipeList)
                {
                    Model.Recipef2f recipeModel = new Model.Recipef2f();
                    recipeModel = await responseRecipe.getRecipe(recipe.RecipeId);
                    recipeFinalList.Add(recipeModel);
                }
                this.populateRecipes();
            }
        }
    }

}
