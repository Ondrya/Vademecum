using System;

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

        public override string ToString()
        {
            return $"data source={Host};initial catalog={Name};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        }


        /// <summary>
        /// Загрузить настройки
        /// </summary>
        internal void Load()
        {
            this.Host = Properties.Settings.Default.ConnectionStringHost;
            this.Name = Properties.Settings.Default.ConnectionStringDbName;
            this.Login = Properties.Settings.Default.ConnectionStringUserName;
            this.Password = Properties.Settings.Default.ConnectionStringUserPassword;
            this.RemindMe = Properties.Settings.Default.ConnectionStringRemindMe;
        }

        internal void Clear()
        {
            this.Host = "";
            this.Name = "";
            this.Login = "";
            this.Password = "";
            this.RemindMe = "false";
        }


        /// <summary>
        /// Сохранить настройки
        /// </summary>
        internal void Save()
        {
            Properties.Settings.Default.ConnectionStringHost = this.Host;
            Properties.Settings.Default.ConnectionStringDbName = this.Name;
            Properties.Settings.Default.ConnectionStringUserName = this.Login;
            Properties.Settings.Default.ConnectionStringUserPassword = this.Password;
            Properties.Settings.Default.ConnectionStringRemindMe = this.RemindMe;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}
