using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Validators;
using System.Runtime.CompilerServices;
using WpfApp.Commands;
using WpfApp.Models;
using System.Collections.ObjectModel;

namespace WpfApp.ViewModels
{
    public class ItemViewModel : NotifyDataErrorInfoBase
    {
        public ItemViewModel()
        {
            DeviceTypeCollection = new ObservableCollection<DeviceType>();
        }

        private Device _currentDevice;
        private Producer _currentDeviceProducer;
        private int _currentDeviceId;
        private string cn;
        private LaType LaType;
        private RelayCommand _saveCommand;
        private DeviceType _selectedDeviceType;
        private ObservableCollection<DeviceType> _deviceTypeCollection;
        private string _buttonName;

        public string ButtonName
        {
            get => _buttonName;
            set 
            { 
                _buttonName = value;
                OnPropertyChanged(nameof(ButtonName));
            }
        }


        public ObservableCollection<DeviceType> DeviceTypeCollection
        {
            get => _deviceTypeCollection;
            set 
            { 
                _deviceTypeCollection = value;
                OnPropertyChanged(nameof(DeviceTypeCollection));
            }
        }


        public DeviceType SelectedDeviceType
        {
            get => _selectedDeviceType;
            set 
            { 
                _selectedDeviceType = value;
                OnPropertyChanged(nameof(SelectedDeviceType));
                CurrentDevice.id_type = value?.id_device;
            }
        }
        public Device CurrentDevice
        {
            get => _currentDevice;
            set 
            {
                _currentDevice = value;
                OnPropertyChanged(nameof(CurrentDevice));
                SelectedDeviceType = DeviceTypeCollection.FirstOrDefault(x => x.id_device == value?.id_type);
            }
        }
        public Producer CurrentDeviceProducer
        {
            get => _currentDeviceProducer;
            set 
            { 
                _currentDeviceProducer = value;
                OnPropertyChanged(nameof(CurrentDeviceProducer));
            }
        }
        public int CurrentDeviceId
        {
            get => _currentDeviceId;
            set 
            { 
                _currentDeviceId = value;
                OnPropertyChanged(nameof(CurrentDeviceId));
                Fill();
            }
        }

        /// <summary>
        /// Загрузить данные
        /// </summary>
        private void Fill()
        {
            cn = ((App)Application.Current).CurrentDb.ToString();
            LaType = ((App)Application.Current).CurrentSession.SelectedLaType;
            DeviceTypeCollection = new ObservableCollection<DeviceType>(Helpers.Dict.GetDevices());
            ButtonName = CurrentDeviceId == 0 ? "Создать" : "Сохранить";

            using (var context = new DataContext(cn))
            {

                CurrentDevice = CurrentDeviceId == 0 ? new Device() { id_LA = (int)LaType } : context.Devices.Find(CurrentDeviceId);

                // данные для вкладки ПРОИЗВОДИТЕЛЬ
                CurrentDeviceProducer = CurrentDevice == null ? new Producer() : context.Producers.Find(CurrentDevice.Producer);
            }
        }

        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(obj =>
        {
            if (CurrentDeviceId == 0)
            {
                DeviceCreate();
            }
            else
            {
                DeviceUpdate();
            }
        }, (obj) => CanSave()));

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(CurrentDevice?.name) && SelectedDeviceType != null;
        }

        private void DeviceUpdate()
        {
            throw new NotImplementedException();
        }

        private void DeviceCreate()
        {
            using (var context = new DataContext(cn))
            {
                try
                {
                    CurrentDevice.id_type = SelectedDeviceType?.id_device;
                    context.Devices.Add(CurrentDevice);
                    context.SaveChanges();
                    CurrentDeviceId = CurrentDevice.id;
                    MessageBox.Show($"id: {CurrentDevice.id}", "Устройство сохранено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                
            }
        }
    }
}
