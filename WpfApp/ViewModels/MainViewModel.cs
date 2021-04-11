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

        private SessionSetting _currentSession;

        public SessionSetting CurrentSession
        {
            get => _currentSession;
            set 
            { 
                _currentSession = value;
                OnPropertyChanged(nameof(CurrentSession));
            }
        }


        public MainViewModel()
        {
            CurrentSession = ((App)Application.Current).CurrentSession;
        }
    }
}
