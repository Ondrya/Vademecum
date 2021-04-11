using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Validators;

namespace WpfApp.ViewModels
{
    public class RegistrationViewModel : NotifyDataErrorInfoBase
    {
        public RegistrationViewModel()
        {
            UserLogin = "";
            TempPassword = "";
            TempPasswordConfirm = "";
            LastResult = true;
        }

        private string _userLogin;
        private string _tempPassword;
        private string _tempPasswordConfirm;
        private string _userLastName;
        private string _userFirstName;
        private string _userFatherName;
        private bool _lastResult;

        public bool LastResult
        {
            get => _lastResult;
            set 
            { 
                _lastResult = value;
                OnPropertyChanged(nameof(LastResult));
            }
        }

        public string TempPassword
        {
            get => _tempPassword;
            set 
            { 
                _tempPassword = value;
                OnPropertyChanged(nameof(TempPassword));
                ValidateCustomErrors(nameof(TempPassword));
                LastResult = true;
            }
        }
        public string TempPasswordConfirm
        {
            get => _tempPasswordConfirm;
            set
            {
                _tempPasswordConfirm = value;
                OnPropertyChanged(nameof(TempPasswordConfirm));
                ValidateCustomErrors(nameof(TempPasswordConfirm));
                LastResult = true;
            }
        }
        public string UserLogin
        {
            get => _userLogin;
            set 
            { 
                _userLogin = value;
                OnPropertyChanged(nameof(UserLogin));
                ValidateCustomErrors(nameof(UserLogin));
                LastResult = true;
            }
        }
        public string UserLastName
        {
            get => _userLastName;
            set 
            { 
                _userLastName = value;
                OnPropertyChanged(nameof(UserLastName));
            }
        }
        public string UserFirstName
        {
            get => _userFirstName;
            set 
            { 
                _userFirstName = value;
                OnPropertyChanged(nameof(UserFirstName));
            }
        }
        public string UserFatherName
        {
            get => _userFatherName;
            set 
            {
                _userFatherName = value;
                OnPropertyChanged(nameof(UserFatherName));
            }
        }


        private RelayCommand _closeCommand;

        private RelayCommand _registrationCommand;

        public RelayCommand RegistrationCommand => _registrationCommand ?? (new RelayCommand(obj => 
        {
            LastResult = TryCreateUser();

            var msg = LastResult ? "Новый пользователь создан" : "Новый пользователь НЕ создан";
            MessageBox.Show(
                msg,
                "Результат сохранения",
                MessageBoxButton.OK,
                LastResult ? MessageBoxImage.Information : MessageBoxImage.Warning);
            if (LastResult) CloseWindow();
        },(obj) => !HasErrors && LastResult));

        private bool TryCreateUser()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            var cn = ((App)Application.Current).CurrentDb.ToString();
            using (var context = new DataContext(cn))
            {
                var userExist = context.Users.SingleOrDefault(x => x.login == UserLogin);
                Mouse.OverrideCursor = null;
                if (userExist != null) return false;

                try
                {
                    var newUser = new DataLayer.User()
                    {
                        login = UserLogin,
                        password = TempPassword,
                        name = UserFirstName,
                        surname = UserLastName,
                        patronymic = UserFatherName,
                        lvl_access = (int)AccessLevel.Student
                    };

                    var user = context.Users.Add(newUser);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Mouse.OverrideCursor = null;
                    return false;
                }
                Mouse.OverrideCursor = null;
                return true;
            }
        }

        public RelayCommand CloseCommand => _closeCommand ?? (_closeCommand = new RelayCommand(obj =>
        {
            var answer = MessageBox.Show(
                "Отменить регистрацию нового пользователя?", 
                "Требуется подтверждение", 
                MessageBoxButton.OKCancel, 
                MessageBoxImage.Question);
            if (answer == MessageBoxResult.Cancel) return;

            CloseWindow();
        }));

        private void CloseWindow()
        {
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) item.Close();
        }

        private void ValidateCustomErrors(string propertyName)
        {
            ClearErrors(propertyName);
            var errors = ValidateProperty(propertyName);
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
        }

        private IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(UserLogin):
                    if (string.IsNullOrWhiteSpace(UserLogin)) yield return "Логин не может быть пустым";
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    using (var context = new DataContext(((App)Application.Current).CurrentDb.ToString()))
                    {
                        var user = context.Users.FirstOrDefault(x => x.login == UserLogin);
                        Mouse.OverrideCursor = null;
                        if (user!=null) yield return "Пользователь с таким логином уже существует";
                    }
                    Mouse.OverrideCursor = null;
                    break;
                case nameof(TempPassword):
                    if (string.IsNullOrWhiteSpace(TempPassword)) yield return "Пароль не может быть пустым";
                    if (!string.IsNullOrWhiteSpace(TempPasswordConfirm) && TempPassword != TempPasswordConfirm)
                        yield return "Пароль и пароль подтверждения не совпадают!";
                    break;
                case nameof(TempPasswordConfirm):
                    if (string.IsNullOrWhiteSpace(TempPasswordConfirm)) yield return "Пароль не может быть пустым";
                    if (!string.IsNullOrWhiteSpace(TempPasswordConfirm) && TempPassword != TempPasswordConfirm)
                        yield return "Пароль и пароль подтверждения не совпадают!";
                    break;
                default:
                    MessageBox.Show(
                        $"Для поля {propertyName} не найден валидатор, хотя валидация запущена.", 
                        "Не найден обработчик проверки", 
                        MessageBoxButton.OK, 
                        MessageBoxImage.Warning);
                    break;
            }
        }
    }
}
