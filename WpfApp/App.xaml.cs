using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Models;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        public User CurrentUser { get; set; }
        public DbConnectionSetting CurrentDb { get; set; }

        #endregion

        /// <summary>
        /// Обработчик неперехваченных исключений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                "Неожиданная ошибка. Обратитесь к администратору"
                + Environment.NewLine + e.Exception.Message
                + Environment.NewLine + e.Exception, "Неожиданная ошибка");

            e.Handled = true;
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CurrentDb = LoadCurrentDbSetting();
        }

        private DbConnectionSetting LoadCurrentDbSetting()
        {
            var res = new DbConnectionSetting();
            res.Load();
            return res;
        }
    }
}
