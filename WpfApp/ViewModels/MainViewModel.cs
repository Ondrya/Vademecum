using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Models;
using WpfApp.Validators;

namespace WpfApp.ViewModels
{
    public class MainViewModel : NotifyDataErrorInfoBase
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

        public MainViewModel()
        {
            CurrentUser = ((App)Application.Current).CurrentUser;
        }
    }
}
