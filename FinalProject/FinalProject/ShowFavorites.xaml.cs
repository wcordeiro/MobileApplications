using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ShowFavorites : Page
    {
        List<Model.ResponseYummly> listRecipes = new List<Model.ResponseYummly>();
        List<Model.Recipef2f> listf2fRecipe = new List<Model.Recipef2f>();

        public ShowFavorites()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<Object> list = (List<Object>) e.Parameter;
            listRecipes = (List<Model.ResponseYummly>)list[0];
            listf2fRecipe = (List<Model.Recipef2f>)list[1];
            this.populateRecipes();
            if (Frame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += ShowFavorites_BackRequested; ;
        }

        private void ShowFavorites_BackRequested(object sender, BackRequestedEventArgs e)
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
            if(listRecipes.Count == 0 && listf2fRecipe.Count == 0)
            {
                PivotItem pivotItem = new PivotItem();
                pivotItem.Header ="Favorites";
                TextBlock txtTitle = new TextBlock();
                txtTitle.Text = "No Favorites at this Point";
                txtTitle.Margin = new Thickness(10, 10, 10, 10);
                txtTitle.SetValue(Grid.ColumnProperty, 1);
                pivotItem.Content = txtTitle;
                pvtFavorites.Items.Add(pivotItem);
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

                StackPanel stk = new StackPanel();
                stk.Name = "recipeStk";
                stk.SetValue(Grid.ColumnProperty, 1);

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
                txtIngredientsList.Text = "Ingredients: " + recipe.Ingredients.Replace("\",", "\n");
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
                pvtFavorites.Items.Add(pivotItem);
            }
            foreach (Model.Recipef2f recipe in listf2fRecipe)
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

                StackPanel stk = new StackPanel();
                stk.SetValue(Grid.ColumnProperty, 1);

                TextBlock txtTitle = new TextBlock();
                txtTitle.Text = "Title: " + recipe.Title;
                txtTitle.Margin = new Thickness(10, 10, 10, 10);
                txtTitle.SetValue(Grid.ColumnProperty, 1);
                stk.Children.Add(txtTitle);

                TextBlock txtRecipeId = new TextBlock();
                txtRecipeId.Text = "RecipeId: " + recipe.RecipeId;
                txtRecipeId.Margin = new Thickness(10, 10, 10, 10);
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
                pvtFavorites.Items.Add(pivotItem);
            }
        }
    }
}
