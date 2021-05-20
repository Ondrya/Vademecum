using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Validators;
using WpfApp.Views;

namespace WpfApp.ViewModels
{
    public class SelectViewModel : NotifyDataErrorInfoBase
    {
        private User _currentUser;


        public User CurrentUser
        {
            get => _currentUser;
            set 
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        public SelectViewModel()
        {
            CurrentUser = ((App)Application.Current).CurrentUser;
        }

        private RelayCommand _logOutCommand;
        private RelayCommand _helpCommand;
        private RelayCommand _setLaTypeCommand;

        public RelayCommand SetLaTypeCommand => _setLaTypeCommand ?? (_setLaTypeCommand = new RelayCommand(obj =>
        {
            var laType = (LaType)obj;
            ((App)Application.Current).CurrentSession.SelectedLaType = laType;
            var mainWindow = new MainWindow();
            mainWindow.Show();
            CloseWindow();
        }));
        public RelayCommand HelpCommand => _helpCommand ?? (_helpCommand = new RelayCommand(obj =>
        {
            string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
             + "\\Resources\\Documents\\Help.docx";
            System.Diagnostics.Process.Start(path);
        }));
        public RelayCommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new RelayCommand(obj =>
        {
            var answer = MessageBox.Show("Зайти под другим пользователем?", "Требуется подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Cancel) return;

            ((App)Application.Current).CurrentUser = null;
            ((App)Application.Current).CurrentSession = null;

            var loginWindow = new LoginWindow();
            loginWindow.Show();

            CloseWindow();
        }));


        private void CloseWindow()
        {
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) item.Close();
        }

    }
}
