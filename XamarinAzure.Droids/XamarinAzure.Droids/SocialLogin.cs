using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinAzure.Droids
{
    public static class SocialLogin
    {
        private static readonly MobileServiceClient Client = new MobileServiceClient("http://xamarinazuredroid.azurewebsites.net");

        private static async Task<List<AppServiceIdentity>> GetUserData()
        {
            //return await Client.InvokeApiAsync<SocialLoginResult>("/.auth/me", HttpMethod.Get, null);
            var userInfo = await Client.InvokeApiAsync("/.auth/me",System.Net.Http.HttpMethod.Get, null);
            return await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me", HttpMethod.Get, null);
        }

        private static async Task<MobileServiceUser> Authenticate(MobileServiceAuthenticationProvider provider)
        {
            try
            {
#if __IOS__
                    return await Client.LoginAsync(UIKit.UIApplication.SharedApplication.KeyWindow.RootViewController, provider);
#elif WINDOWS_PHONE
                return await Client.LoginAsync(provider);
#else
                return await Client.LoginAsync(Xamarin.Forms.Forms.Context, provider);
#endif
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<List<AppServiceIdentity>> AuthenticateFacebook()
        {
            await Authenticate(MobileServiceAuthenticationProvider.Facebook);
            return await GetUserData();
        }

    }
}
