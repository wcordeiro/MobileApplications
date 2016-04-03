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
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdvancedSearch : Page
    {
        private int numIngredients;
        private int numExIngredients;

        public AdvancedSearch()
        {
            this.InitializeComponent();
            numIngredients = 0;
            numExIngredients = 0;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Frame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += AdvancedSearch_BackRequested;
        }

        private void AdvancedSearch_BackRequested(object sender, BackRequestedEventArgs e)
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

        private void addImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            numIngredients++;
            TextBox newTxtBox = new TextBox();
            newTxtBox.Name = "AIngredient" + numIngredients;
            newTxtBox.Margin = new Thickness(10, 0, 0, 0);
            newTxtBox.SetValue(Grid.RowProperty, addImage.GetValue(Grid.RowProperty));
            newTxtBox.SetValue(Grid.ColumnProperty, 0);
            IngredientsGrid.RowDefinitions.Add(new RowDefinition());
            addImage.SetValue(Grid.RowProperty, ((int)addImage.GetValue(Grid.RowProperty) + 1));
            IngredientsGrid.Children.Remove(addImage);
            IngredientsGrid.UpdateLayout();
            IngredientsGrid.Children.Add(newTxtBox);
            IngredientsGrid.UpdateLayout();
            IngredientsGrid.Children.Add(addImage);
            IngredientsGrid.UpdateLayout();
            IngredientsPanel.UpdateLayout();
        }

        private async void Searchblck_Tapped(object sender, TappedRoutedEventArgs e)
        {
            String query = "";
            query += "&q=" + DishtobeSearch.Text.Replace(" ","+");
            query += this.getAllowedIngredients();
            query += this.getExcludedIndredients();
            query += this.getAllergies();
            query += this.getDiet();
            query += this.getCuisine();
            query += this.getCourse();
            query += this.getFlavours();
            ResponseYummly response = new ResponseYummly();
            List<Model.ResponseYummly> listRecipes = new List<Model.ResponseYummly>();
            
            listRecipes = await response.getRecipes(query);
            Frame.Navigate(typeof(ShowRecipesYummly), listRecipes);
        }

        private string getFlavours()
        {
            String query = "";
            query += this.getSweetFlavour();
            query += this.getMeatyFlavour();
            query += this.getSourFlavour();
            query += this.getBitterFlavour();
            query += this.getPiquantFlavour();
            return query;
        }

        private string getPiquantFlavour()
        {
            String query = "";
            if (piquant1.IsChecked.Equals(true))
            {
                query += "&flavor.piquant.min=0&flavor.piquant.max=0.2";
            }
            else if (piquant2.IsChecked.Equals(true))
            {
                query += "&flavor.piquant.min=0.2&flavor.piquant.max=0.4";
            }
            else if (piquant3.IsChecked.Equals(true))
            {
                query += "&flavor.piquant.min=0.4&flavor.piquant.max=0.6";
            }
            else if (piquant4.IsChecked.Equals(true))
            {
                query += "&flavor.piquant.min=0.6&flavor.piquant.max=0.8";
            }
            else if (piquant5.IsChecked.Equals(true))
            {
                query += "&flavor.piquant.min=0.8&flavor.piquant.max=1";
            }
            return query;
        }

        private string getBitterFlavour()
        {
            String query = "";
            if (bitter1.IsChecked.Equals(true))
            {
                query += "&flavor.bitter.min=0&flavor.bitter.max=0.2";
            }
            else if (bitter2.IsChecked.Equals(true))
            {
                query += "&flavor.bitter.min=0.2&flavor.bitter.max=0.4";
            }
            else if (bitter3.IsChecked.Equals(true))
            {
                query += "&flavor.bitter.min=0.4&flavor.bitter.max=0.6";
            }
            else if (bitter4.IsChecked.Equals(true))
            {
                query += "&flavor.bitter.min=0.6&flavor.bitter.max=0.8";
            }
            else if (bitter5.IsChecked.Equals(true))
            {
                query += "&flavor.bitter.min=0.8&flavor.bitter.max=1";
            }
            return query;
        }

        private string getSourFlavour()
        {
            String query = "";
            if (sour1.IsChecked.Equals(true))
            {
                query += "&flavor.sour.min=0&flavor.sour.max=0.2";
            }
            else if (sour2.IsChecked.Equals(true))
            {
                query += "&flavor.sour.min=0.2&flavor.sour.max=0.4";
            }
            else if (sour3.IsChecked.Equals(true))
            {
                query += "&flavor.sour.min=0.4&flavor.sour.max=0.6";
            }
            else if (sour4.IsChecked.Equals(true))
            {
                query += "&flavor.sour.min=0.6&flavor.sour.max=0.8";
            }
            else if (sour5.IsChecked.Equals(true))
            {
                query += "&flavor.sour.min=0.8&flavor.sour.max=1";
            }
            return query;
        }

        private string getMeatyFlavour()
        {
            String query = "";
            if (meaty1.IsChecked.Equals(true))
            {
                query += "&flavor.meaty.min=0&flavor.meaty.max=0.2";
            }
            else if (meaty2.IsChecked.Equals(true))
            {
                query += "&flavor.meaty.min=0.2&flavor.meaty.max=0.4";
            }
            else if (meaty3.IsChecked.Equals(true))
            {
                query += "&flavor.meaty.min=0.4&flavor.meaty.max=0.6";
            }
            else if (meaty4.IsChecked.Equals(true))
            {
                query += "&flavor.meaty.min=0.6&flavor.meaty.max=0.8";
            }
            else if (meaty5.IsChecked.Equals(true))
            {
                query += "&flavor.meaty.min=0.8&flavor.meaty.max=1";
            }
            return query;
        }

        private string getSweetFlavour()
        {
            String query = "";
            if (sweet1.IsChecked.Equals(true))
            {
                query += "&flavor.sweet.min=0&flavor.sweet.max=0.2";
            }
            else if (sweet2.IsChecked.Equals(true))
            {
                query += "&flavor.sweet.min=0.2&flavor.sweet.max=0.4";
            }
            else if(sweet3.IsChecked.Equals(true))
            {
                query += "&flavor.sweet.min=0.4&flavor.sweet.max=0.6";
            }
            else if (sweet4.IsChecked.Equals(true))
            {
                query += "&flavor.sweet.min=0.6&flavor.sweet.max=0.8";
            }
            else if (sweet5.IsChecked.Equals(true))
            {
                query += "&flavor.sweet.min=0.8&flavor.sweet.max=1";
            }
            return query;
        }

        private string getCourse()
        {
            String query = "";
            if (course1.IsChecked.Equals(true))
            {
                query += "&allowedCourse[]=course^course-Main Dishes";
            }
            if (course2.IsChecked.Equals(true))
            {
                query += "&allowedCourse[]=course^course-Desserts";
            }
            if (course3.IsChecked.Equals(true))
            {
                query += "&allowedCourse[]=course^course-Breakfast and Brunch";
            }
            if (course4.IsChecked.Equals(true))
            {
                query += "&allowedCourse[]=course^course-Salads";
            }
            if (course5.IsChecked.Equals(true))
            {
                query += "&allowedCourse[]=course^course-Beverages";
            }
            if (course6.IsChecked.Equals(true))
            {
                query += "&allowedCourse[]=course^course-Salads";
            }
            return query;
        }

        private string getCuisine()
        {
            String query = "";
            if (cusine1.IsChecked.Equals(true))
            {
                query += "&allowedCuisine[]=cuisine^cuisine-american";
            }
            if (cusine2.IsChecked.Equals(true))
            {
                query += "&allowedCuisine[]=cuisine^cuisine-italian";
            }
            if (cusine3.IsChecked.Equals(true))
            {
                query += "&allowedCuisine[]=cuisine^cuisine-irish";
            }
            if (cusine4.IsChecked.Equals(true))
            {
                query += "&allowedCuisine[]=cuisine^cuisine-greek";
            }
            if (cusine5.IsChecked.Equals(true))
            {
                query += "&allowedCuisine[]=cuisine^cuisine-portuguese";
            }
            if (cusine6.IsChecked.Equals(true))
            {
                query += "&allowedCuisine[]=cuisine^cuisine-french";
            }
            return query;
        }

        private string getDiet()
        {
            String query = "";
            if (diet1.IsChecked.Equals(true))
            {
                query += "&allowedDiet[]=386^Vegan";
            }
            if (diet2.IsChecked.Equals(true))
            {
                query += "&allowedDiet[]=387^Lacto-ovo-vegetarian";
            }
            if (diet3.IsChecked.Equals(true))
            {
                query += "&allowedDiet[]=388^Lactovegetarian";
            }
            if (diet4.IsChecked.Equals(true))
            {
                query += "&allowedDiet[]=389^Ovovegetarian";
            }
            if (diet5.IsChecked.Equals(true))
            {
                query += "&allowedDiet[]=390^Pescetarian";
            }
            return query;
        }

        private string getAllergies()
        {
            String query = "";
            if (allergy1.IsChecked.Equals(true))
            {
                query += "&allowedAllergy[]=396^Dairy-Free";
            }
            if (allergy2.IsChecked.Equals(true))
            {
                query += "&allowedAllergy[]=393^Gluten-Free";
            }
            if (allergy3.IsChecked.Equals(true))
            {
                query += "&allowedAllergy[]=394^Peanut-Free";
            }
            if (allergy4.IsChecked.Equals(true))
            {
                query += "&allowedAllergy[]=397^Egg-Free";
            }
            return query;
        }

        private string getExcludedIndredients()
        {
            String query = "";
            for (int i = 1; i <= numExIngredients; i++)
            {
                var myTextBox = (TextBox)this.FindName("EIngredient" + i.ToString());
                if (myTextBox.Text != null)
                {
                    query += "&excludedIngredient[]=" + myTextBox.Text;
                }
            }
            return query;
        }

        private string getAllowedIngredients()
        {
            String query = "";
            for(int i = 1; i <= numIngredients; i++)
            {
                var myTextBox = (TextBox)this.FindName("AIngredient" + i.ToString());
                if (myTextBox.Text != null)
                {
                    query += "&allowedIngredient[]=" + myTextBox.Text;
                }
            }
            return query;
        }

        private void addImageEx_Tapped(object sender, TappedRoutedEventArgs e)
        {
            numExIngredients++;
            TextBox newTxtBox = new TextBox();
            newTxtBox.Name = "EIngredient" + numExIngredients;
            newTxtBox.Margin = new Thickness(10, 0, 0, 0);
            newTxtBox.SetValue(Grid.RowProperty, addImageEx.GetValue(Grid.RowProperty));
            newTxtBox.SetValue(Grid.ColumnProperty, 1);
            IngredientsGrid.RowDefinitions.Add(new RowDefinition());
            addImageEx.SetValue(Grid.RowProperty, ((int)addImageEx.GetValue(Grid.RowProperty) + 1));
            IngredientsGrid.Children.Remove(addImageEx);
            IngredientsGrid.UpdateLayout();
            IngredientsGrid.Children.Add(newTxtBox);
            IngredientsGrid.UpdateLayout();
            IngredientsGrid.Children.Add(addImageEx);
            IngredientsGrid.UpdateLayout();
            IngredientsPanel.UpdateLayout();
        }
    }
}
