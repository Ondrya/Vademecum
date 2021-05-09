using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.Validators;
using WpfApp.Commands;
using DataLayer;
using System.Windows;
using System.Windows.Input;
using WpfApp.Views;
using WpfApp.Interfaces;

namespace WpfApp.ViewModels
{
    public class LoginViewModel : NotifyDataErrorInfoBase
    {
        private string _userLogin;
        private string _userPassword;
        public DbConnectionSetting Db;

        private string _dbHost;
        private string _dbName;
        private string _dbLogin;
        private string _dbPassword;
        private string _dbRemindMe;

        public string DbHost
        {
            get => _dbHost;
            set 
            { 
                _dbHost = value;
                OnPropertyChanged(nameof(DbHost));
                Db.Host = value;
            }
        }
        public string DbName
        {
            get => _dbName;
            set
            {
                _dbName = value;
                OnPropertyChanged(nameof(DbName));
                Db.Name = value;
            }
        }
        public string DbLogin
        {
            get => _dbLogin;
            set
            {
                _dbLogin = value;
                OnPropertyChanged(nameof(DbLogin));
                Db.Login = value;
            }
        }
        public string DbPassword
        {
            get => _dbPassword;
            set
            {
                _dbPassword = value;
                OnPropertyChanged(nameof(DbPassword));
                Db.Password = value;
            }
        }
        public string DbRemindMe
        {
            get => _dbRemindMe;
            set
            {
                _dbRemindMe = value;
                OnPropertyChanged(nameof(DbRemindMe));
                Db.RemindMe = value;
            }
        }


        public string UserLogin
        {
            get => _userLogin;
            set 
            { 
                _userLogin = value;
                OnPropertyChanged(nameof(UserLogin));
            }
        }
        public string UserPassword
        {
            get => _userPassword;
            set 
            { 
                _userPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }


        #region Команды

        private RelayCommand _loginCommand;
        private RelayCommand _registrationCommand;

        public RelayCommand RegistrationCommand => _registrationCommand ?? (_registrationCommand = new RelayCommand(obj =>
        {
            var registrationWindow = new RegistrationWindow();
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) registrationWindow.Owner = item;
            registrationWindow.Show();
        }, (obj) => Db != null
        ));


        private RelayCommand _loginAsGuestCommand;


        public RelayCommand LoginAsGuestCommand => _loginAsGuestCommand ?? (_loginAsGuestCommand = new RelayCommand(obj =>
        {
            // авторизуем гостя
            DoLogin(AppConfig.GuestLogin, AppConfig.GuestPassword);
        }, (obj) => Db != null));


        public RelayCommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(obj =>
        {
            // авторизуем пользователя
            DoLogin(UserLogin, UserPassword);
        }, (obj) => Db != null && !string.IsNullOrWhiteSpace(UserLogin) && !string.IsNullOrWhiteSpace(UserPassword)));

        internal void DoLogin(string login, string password)
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            var cn = Db.ToString();
            using (var context = new DataContext(cn))
            {
                var user = context.Users.SingleOrDefault(x => x.login == login && x.password == password);
                if (user != null)
                {
                    ((App)Application.Current).CurrentUser = new Models.User()
                    {
                        Login = user.login,
                        Level = (AccessLevel)user.lvl_access
                    };

                    ((App)Application.Current).CurrentSession = new Models.SessionSetting()
                    {
                        CurrentUser = ((App)Application.Current).CurrentUser,
                        SelectedLaType = LaType.Undefined
                    };

                    var selectWindow = new SelectWindow();
                    selectWindow.Show();
                    Mouse.OverrideCursor = null;
                    CloseWindow();
                    
                }
                else
                {
                    Mouse.OverrideCursor = null;
                    MessageBox.Show("Неверный логин/пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void CloseWindow()
        {
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) item.Close();
        }


        #endregion

        public LoginViewModel()
        {
            Db = ((App)Application.Current).CurrentDb;

            DbHost = Db.Host;
            DbName = Db.Name;
            DbLogin = DbLogin;
            DbPassword = Db.Password;
            DbRemindMe = Db.RemindMe;
        }
    }
}
