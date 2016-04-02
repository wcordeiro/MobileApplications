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

        private void Searchblck_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
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
