using Microsoft.WindowsAzure.MobileServices;
using ResenhaFilmesApp.Authentication;
using ResenhaFilmesApp.ExtensionMethods;
using ResenhaFilmesApp.Helpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ResenhaFilmesApp.Services
{
    public class AzureService
    {
        static readonly string AppUrl = "https://gsociallogin.azurewebsites.net";

        public MobileServiceClient Client { get; set; } = null;

        public static bool UseAuth { get; set; } = false;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            if (Settings.AuthToken.IsNotNullOrWhiteSpace() && Settings.UserId.IsNotNullOrWhiteSpace())
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
            }
        }
        public async Task<bool> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ops!", "Não foi possível efetuar o login", "OK");
                });

                return false;
            }
            else
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;
            }

            return true;
        }
    }
}
