using Business.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoonBoost
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

        private void OnNewActivityClicked(object sender, EventArgs e)
        {

        }

        private void OnNewPlanClicked(object sender, EventArgs e)
        {

        }

        private void OnSelectedPost(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}
