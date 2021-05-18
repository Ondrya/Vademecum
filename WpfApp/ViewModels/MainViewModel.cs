using DataLayer;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfApp.Commands;
using WpfApp.Events;
using WpfApp.Helpers;
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
            isAdmin = Common.CheckIsAdmin(((App)Application.Current).CurrentUser.Level);
            Fill();
        }

        public string GoToItemName => isAdmin ? "Редактировать" : "Просмотр";

        private string cn;
        private int laType;

        private void Fill()
        {
            HeaderList = "Список датчиков и устройств";
            cn = ((App)Application.Current).CurrentDb.ToString();
            using (var context = new DataContext(cn))
            {
                var result = Helpers.Dict.GetDevices().OrderByDescending(x => x.id);//context.Devices.ToList();

                _items = new List<DeviceSensorLookUp>();

                foreach (var x in result)
                {
                    _items.Add(new DeviceSensorLookUp()
                    {
                        Id = x.id,
                        Name = x?.name,
                        DeviceTypeId = x?.id_type,
                        DeviceTypeName = GetDeviceType(x?.id_type),
                        Vendor = x?.Producer?.name_prod,
                        IsSensor = x?.id_type == 1,
                        IsDevice = x?.id_type == 2
                    });
                }

                //DataGridCollection = new ObservableCollection<DeviceSensorLookUp>(_items.Where(t => t.IsSensor == ShowSensors || t.IsDevice == _showDevices));
                DataGridCollection = new ObservableCollection<DeviceSensorLookUp>(_items);
            }
            
        }

        private string GetDeviceType(int? id_type)
        {
            if (id_type == null) return "";
            if (id_type == 1) return "Датчик";
            if (id_type == 2) return "Прибор";
            return "";
        }

        private SessionSetting _currentSession;
        private bool _showSensors;
        private bool _showDevices;
        private ObservableCollection<DeviceSensorLookUp> _dataGridCollection;
        private DeviceSensorLookUp _selectedItem;
        private List<DeviceSensorLookUp> _items;
        private bool isAdmin;
        private string _headerList;

        public string HeaderList
        {
            get => _headerList;
            set 
            { 
                _headerList = value;
                OnPropertyChanged(nameof(HeaderList));
            }
        }
        public bool ShowDevices
        {
            get => _showDevices;
            set
            {
                _showDevices = value;
                OnPropertyChanged(nameof(ShowDevices));
                if (value) HeaderList = "Список устройств";
            }
        }
        public bool ShowSensors
        {
            get => _showSensors;
            set
            {
                _showSensors = value;
                OnPropertyChanged(nameof(ShowSensors));
                if (value) HeaderList = "Список датчиков";
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
        private RelayCommand _refreshCommand;
        private RelayCommand _showAdminWindowCommand;
        private RelayCommand _parametersCommand;
        private RelayCommand _openSearchWindowCommand;

        public RelayCommand OpenSearchWindowCommand => 
            _openSearchWindowCommand ?? (_openSearchWindowCommand = new RelayCommand(obj => 
            {
                var popUpWindow = new SearchWindow();
                popUpWindow.Show();
            }));
        public RelayCommand ParametersCommand => _parametersCommand ?? (_parametersCommand = new RelayCommand(obj =>
        {
            var popUpWindow = new ParametersWindow();
            popUpWindow.Show();
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) popUpWindow.Owner = item;
        }));
        public RelayCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(obj => { Fill(); }));
        public RelayCommand UpdateCommand => _updateCommand ?? (_updateCommand = new RelayCommand(obj =>
        {
            UpdateItem();
        }, (obj) => SelectedItem != null));

        internal void UpdateItem()
        {
            var popUpWindow = new Item(SelectedItem.Id);
            popUpWindow.Show();
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) popUpWindow.Owner = item;
        }

        public RelayCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(obj =>
        {
            var res = MessageBox.Show("Удалить выбранный элемент?", "Требуется подтверждение.", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes) DeleteItem(SelectedItem);
        }, (obj) => SelectedItem != null && isAdmin));
        public RelayCommand CreateCommand => _createCommand ?? (_createCommand = new RelayCommand(obj =>
        {
            var popUpWindow = new Item();
            popUpWindow.Show();
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) popUpWindow.Owner = item;
        }, (obj) => isAdmin));

        private void DeleteItem(DeviceSensorLookUp selectedItem)
        {
            try
            {
                using (var context = new DataContext(cn))
                {
                    var item = context.Devices.Find(selectedItem.Id);
                    if (item != null)
                    {
                        context.Devices.Remove(item);
                        context.SaveChanges();
                        DataGridCollection.Remove(selectedItem);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
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
            ShowDevices = false;
            ShowSensors = true; ;
            // отвильтровать датчики
            FilterDataGrid();
        }));
        public RelayCommand FilterDeviceCommand => _filterDeviceCommand ?? (_filterDeviceCommand = new RelayCommand(obj =>
        {
            ShowSensors = false;
            ShowDevices = true;
            // отвильтровать устройства
            FilterDataGrid();
        }));

        private void FilterDataGrid()
        {
            DataGridCollection = new ObservableCollection<DeviceSensorLookUp>(_items.Where(t => t.IsSensor == ShowSensors || t.IsDevice == _showDevices));
            //DataGridCollection = new ObservableCollection<Device>(_items.Where(t => t.IsSensor == ShowSensors || t.IsDevice == _showDevices));
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
        public int? DeviceTypeId { get; set; }
        public string DeviceTypeName { get; set; }
        public string Vendor { get; set; }
        public bool IsSensor { get; set; }
        public bool IsDevice { get; set; }
    }
}
