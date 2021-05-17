using WpfApp.Models;

namespace WpfApp.Helpers
{
    internal class Common
    {
        internal static bool CheckIsAdmin(AccessLevel level)
        {
            switch (level)
            {
                case AccessLevel.Administrator:
                    return true;
                case AccessLevel.Developer:
                    return true;
                case AccessLevel.Student:
                case AccessLevel.Guest:
                default:
                    return false;
            }
        }
    }
}
