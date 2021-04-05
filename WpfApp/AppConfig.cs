using System.Configuration;

namespace WpfApp
{
    public static class AppConfig
    {
        public static string GuestLogin = ConfigurationManager.AppSettings.Get("guestLogin");
        public static string GuestPassword = ConfigurationManager.AppSettings.Get("guestPassword");
    }
}
