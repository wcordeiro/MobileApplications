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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowRecipies : Page
    {
        List<Model.ResponseRecipef2f> recipeList;

        public ShowRecipies()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Bussiness.Responsef2f response = new Bussiness.Responsef2f();
            
            recipeList = response.parseResponse(e.Parameter.ToString());
        }

        private void populateRecipes()
        {
            PivotItem pvt;
            for (int i = 0; i < e.Result.menu.Length; i++)
            {
                pvt = new PivotItem();
                pvt.Header = e.Result.menu[i].name.ToLower();
                var stack = new StackPanel();
                pvt.Content = stack;
                pvtRecipes.Items.Add(pvt);
                pvt = null;
            }
        }

        private void pvtRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
