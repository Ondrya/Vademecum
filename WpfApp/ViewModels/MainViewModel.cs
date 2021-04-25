using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class MainViewModel : NotifyDataErrorInfoBase
    {
        public MainViewModel()
        {
            CurrentSession = ((App)Application.Current).CurrentSession;
            ShowDevices = true;
            ShowSensors = true;
            isAdmin = CheckIsAdmin(((App)Application.Current).CurrentUser.Level);
            Fill();
        }

        private bool CheckIsAdmin(AccessLevel level)
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

        private void Fill()
        {
            // todo получить данные из базы данных
            _items = new List<DeviceSensorLookUp>();
            DataGridCollection = new ObservableCollection<DeviceSensorLookUp>(_items.Where(t => t.IsSensor == ShowSensors || t.IsDevice == _showDevices));
        }

        private SessionSetting _currentSession;
        private bool _showSensors;
        private bool _showDevices;
        private ObservableCollection<DeviceSensorLookUp> _dataGridCollection;
        private DeviceSensorLookUp _selectedItem;
        private List<DeviceSensorLookUp> _items;
        private bool isAdmin;

        public bool ShowDevices
        {
            get => _showDevices;
            set
            {
                _showDevices = value;
                OnPropertyChanged(nameof(ShowDevices));
            }
        }
        public bool ShowSensors
        {
            get => _showSensors;
            set
            {
                _showSensors = value;
                OnPropertyChanged(nameof(ShowSensors));
            }
        }
        public ObservableCollection<DeviceSensorLookUp> DataGridCollection
        {
            get => _dataGridCollection;
            set 
            { 
                _dataGridCollection = value;
                OnPropertyChanged(nameof(DataGridCollection));
            }
        }
        public DeviceSensorLookUp SelectedItem
        {
            get => _selectedItem;
            set 
            { 
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }




        public SessionSetting CurrentSession
        {
            get => _currentSession;
            set
            {
                _currentSession = value;
                OnPropertyChanged(nameof(CurrentSession));
            }
        }


        #region Команды

        private RelayCommand _filterSensorCommand;
        private RelayCommand _filterDeviceCommand;
        private RelayCommand _logOutCommand;
        private RelayCommand _createCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _showAdminWindowCommand;

        private RelayCommand _parametersCommand;

        public RelayCommand ParametersCommand => _parametersCommand ?? (_parametersCommand = new RelayCommand(obj =>
        {
            var popUpWindow = new ParametersWindow();
            popUpWindow.Show();
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) popUpWindow.Owner = item;
        }));


        public RelayCommand UpdateCommand => _updateCommand ?? (_updateCommand = new RelayCommand(obj =>
        {
            var popUpWindow = new Item();
            popUpWindow.Show();
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) popUpWindow.Owner = item;
        }, (obj) => SelectedItem != null));
        public RelayCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(obj =>
        {
            var res = MessageBox.Show("Удалить выбранный элемент?", "Требуется подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.OK) DeleteItem(SelectedItem);
        }, (obj) => SelectedItem != null));
        public RelayCommand CreateCommand => _createCommand ?? (_createCommand = new RelayCommand(obj =>
        {
            var popUpWindow = new Item();
            popUpWindow.Show();
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) popUpWindow.Owner = item;
        }));

        private void DeleteItem(DeviceSensorLookUp selectedItem)
        {
            throw new NotImplementedException();
        }




        public RelayCommand ShowAdminWindowCommand => _showAdminWindowCommand ?? (_showAdminWindowCommand = new RelayCommand(obj =>
        {
            var popUpWindow = new Administration();
            popUpWindow.Show();
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) popUpWindow.Owner = item;
        }, (obj) => isAdmin));





        public RelayCommand FilterSensorCommand => _filterSensorCommand ?? (_filterSensorCommand = new RelayCommand(obj =>
        {
            ShowSensors = !ShowDevices;
            // отвильтровать датчики
            FilterDataGrid();
        }));

        public RelayCommand FilterDeviceCommand => _filterDeviceCommand ?? (_filterDeviceCommand = new RelayCommand(obj =>
        {
            ShowDevices = !ShowDevices;
            // отвильтровать устройства
            FilterDataGrid();
        }));

        private void FilterDataGrid()
        {
            DataGridCollection = new ObservableCollection<DeviceSensorLookUp>(_items.Where(t => t.IsSensor == ShowSensors || t.IsDevice == _showDevices));
        }

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


        #endregion



    }

    public class DeviceSensorLookUp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Vendor { get; set; }
        public bool IsSensor { get; set; }
        public bool IsDevice { get; set; }
    }
}
