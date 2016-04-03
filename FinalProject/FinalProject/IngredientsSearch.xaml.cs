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
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IngredientsSearch : Page
    {

        private int numberTxtBox;
        private Boolean flagColumn;
        private int lastRow;
        private int numIngredients;

        public IngredientsSearch()
        {
            this.InitializeComponent();
            numberTxtBox = 5;
            flagColumn = false;
            lastRow = 1;
            numIngredients = 0;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Frame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += IngredientsSearch_BackRequested;
        }

        private void IngredientsSearch_BackRequested(object sender, BackRequestedEventArgs e)
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
            newTxtBox.Name = "Ingredient"+ numIngredients;
            newTxtBox.Margin = new Thickness(10, 0, 0, 0);
            if (!flagColumn)
            {
                newTxtBox.SetValue(Grid.RowProperty, addImage.GetValue(Grid.RowProperty));
                newTxtBox.SetValue(Grid.ColumnProperty, 0);
                IngredientsGrid.RowDefinitions.Add(new RowDefinition());
                addImage.SetValue(Grid.RowProperty, ((int)addImage.GetValue(Grid.RowProperty) + 1));
            }
            else
            {
                newTxtBox.SetValue(Grid.RowProperty, lastRow);
                newTxtBox.SetValue(Grid.ColumnProperty, 1);
                lastRow++;
            }
            IngredientsGrid.Children.Remove(addImage);
            IngredientsGrid.UpdateLayout();
            IngredientsGrid.Children.Add(newTxtBox);
            IngredientsGrid.UpdateLayout();
            IngredientsGrid.Children.Add(addImage);
            IngredientsGrid.UpdateLayout();
            IngredientsPanel.UpdateLayout();
            numberTxtBox++;
            if(numberTxtBox % 4 == 0)
            {
                flagColumn = !flagColumn;
            }
           // IngredientsPanel.Children.Add(addImage);
        }

        private async void Searchblck_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string query = "&q=";
            for(int i = 0; i <= numIngredients; i++)
            {
                var myTextBox = (TextBox)this.FindName("Ingredient" + i.ToString());
                if(myTextBox.Text != null)
                {
                    if (i == numIngredients)
                        query += myTextBox.Text;
                    else
                        query += myTextBox.Text + ",";
                }
            }
            Bussiness.Responsef2f response = new Bussiness.Responsef2f();
            response.StringUri = response.StringUri + query;
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
