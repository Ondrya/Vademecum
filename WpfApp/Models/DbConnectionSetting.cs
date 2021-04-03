namespace WpfApp.Models
{
    /// <summary>
    /// Настройки подключения к базе данных
    /// </summary>
    public class DbConnectionSetting
    {
        public string Host { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RemindMe { get; set; }
    }
}
