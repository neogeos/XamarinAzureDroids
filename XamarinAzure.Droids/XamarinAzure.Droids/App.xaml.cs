using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinAzure.Droids
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            var welcomeLabel = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var fbButton = new Button
            {
                Text = "Facebook",
                TextColor = Color.FromHex("#fff"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.FromHex("#3b5998")
            };

            fbButton.Clicked += async (sender, args) =>
            {
                var user = await SocialLogin.AuthenticateFacebook();
                var name = user.FirstOrDefault().UserClaims.FirstOrDefault(c => c.Type.EndsWith("name")).Value;
                var userid = user.FirstOrDefault().UserId;
                welcomeLabel.Text = string.Format("Welcome {0}!, this is your username {1}", name, userid);
            };

            //MainPage = new XamarinAzure.Droids.MainPage();

            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        fbButton,
                        welcomeLabel
                    }
                }
            };
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
