using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.Validators;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    public class LoginViewModel : NotifyDataErrorInfoBase
    {
        private string _userLogin;
        private string _userPassword;
        private DbConnectionSetting _db;

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
        public DbConnectionSetting Db
        {
            get => _db;
            set {
                _db = value;
                OnPropertyChanged(nameof(Db));
            }
        }


        #region Команды

        private RelayCommand _loginCommand;
        private RelayCommand _registrationCommand;

        public RelayCommand RegistrationCommand => _registrationCommand ?? (_registrationCommand = new RelayCommand(obj =>
        {
            // проверить действительно ли это новый пользователь - открыть окно регистрации нового пользователя
            //throw new NotImplementedException();
        }
        ));

        public RelayCommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(obj =>
        {
           //throw new NotImplementedException();
        }, (obj) => ValidateLoginPassword(obj.ToString() == "user") 
        ));

        private bool ValidateLoginPassword(bool isUser)
        {
            // если гость - разрешить авторизацию
            if (!isUser) return true;
            // проверить логин пароль и тип подключения
            //throw new NotImplementedException();
            return false;
        }


        #endregion

        public LoginViewModel()
        {

        }
    }
}
