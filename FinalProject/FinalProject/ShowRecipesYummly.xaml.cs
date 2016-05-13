using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowRecipesYummly : Page
    {
        List<Model.ResponseYummly> listRecipes = new List<Model.ResponseYummly>();
        int favoriteNumber;

        public ShowRecipesYummly()
        {
            this.InitializeComponent();
            favoriteNumber = 0;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter.GetType() == listRecipes.GetType())
            {
                listRecipes = (List<Model.ResponseYummly>)e.Parameter;
            }
            this.populateRecipes();
            if (Frame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += ShowRecipesYummly_BackRequested;
        }

        private void ShowRecipesYummly_BackRequested(object sender, BackRequestedEventArgs e)
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
            if (listRecipes.Count == 0)
            {
                PivotItem pivotItem = new PivotItem();
                pivotItem.Header = "No Results";
                TextBlock txtTitle = new TextBlock();
                txtTitle.Text = "No Results Found";
                txtTitle.Margin = new Thickness(10, 10, 10, 10);
                pivotItem.Content = txtTitle;
                pvtRecipes.Items.Add(pivotItem);
            }
            foreach (Model.ResponseYummly recipe in listRecipes)
            {
                PivotItem pivotItem = new PivotItem();
                pivotItem.Header = recipe.RecipeName;

                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                Image img = new Image();
                img.Source = new BitmapImage(new Uri(recipe.ImageUrl));
                img.SetValue(Grid.ColumnProperty, 0);
                grid.Children.Add(img);

                
                Image favorite = new Image();
                favorite.Name = "favoriteSymbol"+i.ToString();
                BitmapImage starImage = new BitmapImage();
                starImage.UriSource = new Uri("https://image.freepik.com/free-icon/favorites-star-outlined-symbol_318-69168.png");
                favorite.Source = starImage;
                favorite.HorizontalAlignment = HorizontalAlignment.Right;
                favorite.VerticalAlignment = VerticalAlignment.Top;
                favorite.Width = 15;
                favorite.Height = 15;
                favorite.Tapped += Favorite_Tapped;
                favorite.SetValue(Grid.ColumnProperty, 1);
                i++;
                //grid.Children.Add(favorite);

                StackPanel stk = new StackPanel();
                stk.Name = "recipeStk";
                stk.SetValue(Grid.ColumnProperty, 1);

                stk.Children.Add(favorite);

                TextBlock txtTitle = new TextBlock();
                txtTitle.Text = "Title: " + recipe.RecipeName;
                txtTitle.Margin = new Thickness(10, 10, 10, 10);
                txtTitle.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtTitle);

                TextBlock txtRecipeId = new TextBlock();
                txtRecipeId.Text = "RecipeId: " + recipe.Id;
                txtRecipeId.Margin = new Thickness(10, 10, 10, 10);
                txtRecipeId.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtRecipeId);

                TextBlock txtIngredientsList = new TextBlock();
                txtIngredientsList.Text = "Ingredients: " + recipe.Ingredients.Replace("\",","\n");
                txtIngredientsList.Margin = new Thickness(10, 10, 10, 10);
                txtIngredientsList.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtIngredientsList);

                TextBlock txtSocialRank = new TextBlock();
                txtSocialRank.Text = "Social Rank: " + recipe.Rating.ToString();
                txtSocialRank.Margin = new Thickness(10, 10, 10, 10);
                txtSocialRank.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtSocialRank);

                TextBlock txtPublisher = new TextBlock();
                txtPublisher.Text = "Publisher: " + recipe.SourceDisplayName;
                txtPublisher.Margin = new Thickness(10, 10, 10, 10);
                txtPublisher.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtPublisher);

                TextBlock txtTotalTime = new TextBlock();
                txtTotalTime.Text = "Total Time (seconds): " + recipe.TotalTime.ToString();
                txtTotalTime.Margin = new Thickness(10, 10, 10, 10);
                txtTotalTime.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtTotalTime);

                TextBlock txtCourse = new TextBlock();
                txtCourse.Text = "Courses: " + recipe.Course;
                txtCourse.Margin = new Thickness(10, 10, 10, 10);
                txtCourse.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtCourse);

                TextBlock txtCusine = new TextBlock();
                txtCusine.Text = "Cuisine: " + recipe.Cuisine;
                txtCusine.Margin = new Thickness(10, 10, 10, 10);
                txtCusine.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtCusine);

                TextBlock txtFlavors = new TextBlock();
                txtFlavors.Text = "Flavors: " + recipe.Flavors.Replace(",\"", "\n");
                txtFlavors.Margin = new Thickness(10, 10, 10, 10);
                txtFlavors.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtFlavors);

                grid.Children.Add(stk);

                pivotItem.Content = grid;
                pvtRecipes.Items.Add(pivotItem);
            }
        }

        private async void Favorite_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var favoriteImage = (Image)this.FindName("favoriteSymbol"+pvtRecipes.SelectedIndex.ToString());
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
            await this.saveFavorite();
        }

        private async Task saveFavorite()
        {
            int value;
            ApplicationDataContainer roamingSettings;
            try
            {
                // to get roaming settings.....
                roamingSettings =
                    ApplicationData.Current.RoamingSettings;
                value = (int)roamingSettings.Values["FavoriteYummly"];
                favoriteNumber = value;
            }
            catch (Exception exRoaming)
            {
                // if the value key is not set, then the exception will cause
                // this code to execute, which just sets the value to 0 (default)
                string errMsg = exRoaming.Message;
                favoriteNumber = 0;
            } // end inner try for roaming settings
            Model.ResponseYummly recipe = listRecipes[pvtRecipes.SelectedIndex];
            StorageFolder storageFolder = ApplicationData.Current.RoamingFolder;
            StorageFile sampleFile;
            string fileText = "";
            try
            {
                sampleFile = await storageFolder.GetFileAsync("Yummly"+favoriteNumber+".txt");
                fileText = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);

            }
            catch (Exception myE)
            {
                string message = myE.Message;
                sampleFile = await storageFolder.CreateFileAsync(("Yummly" + favoriteNumber + ".txt"));
            }
            String objectRecipe = "";
            objectRecipe += recipe.ImageUrl + "\n";
            objectRecipe += recipe.SourceDisplayName + "\n";
            objectRecipe += recipe.Ingredients + "\n";
            objectRecipe += recipe.Id + "\n";
            objectRecipe += recipe.RecipeName + "\n";
            objectRecipe += recipe.TotalTime + "\n";
            objectRecipe += recipe.Course + "\n";
            objectRecipe += recipe.Cuisine + "\n";
            objectRecipe += recipe.Flavors + "\n";
            objectRecipe += recipe.Rating + "\n";
        // file open, now write to it using the writeTextAsync
         await Windows.Storage.FileIO.WriteTextAsync(sampleFile, objectRecipe);
        favoriteNumber++;
            roamingSettings =
                ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["FavoriteYummly"] = favoriteNumber;
        }
    }
}
