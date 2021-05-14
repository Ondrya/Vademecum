using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Validators;
using WpfApp.Helpers;
using System.Collections.ObjectModel;
using WpfApp.Commands;
using WpfApp.Views;
using System.Windows;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class SearchViewModel : NotifyDataErrorInfoBase
    {
        public SearchViewModel()
        {
            DeviceCollection = new ObservableCollection<Device>();
            Fill();
        }

        private List<Device> devices;
        private ObservableCollection<Device> _deviceCollection;
        private Device _selectedItem;
        private bool isAdmin;
        private string _searchName;
        private RelayCommand _updateCommand;

        public ObservableCollection<Device> DeviceCollection
        {
            get => _deviceCollection;
            set 
            { 
                _deviceCollection = value;
                OnPropertyChanged(nameof(DeviceCollection));
            }
        }

        public Device SelectedItem
        {
            get { return _selectedItem; }
            set 
            { 
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }


        public RelayCommand UpdateCommand => _updateCommand ?? (_updateCommand = new RelayCommand(obj => UpdateItem(), (obj) => SelectedItem != null));



        public string SearchName
        {
            get => _searchName;
            set 
            { 
                _searchName = value;
                OnPropertyChanged(nameof(SearchName));
                UpdateCollection();
            }
        }

        private void UpdateCollection()
        {
            if (devices == null) return;
            DeviceCollection.Clear();
            foreach (var item in devices)
            {
                if (!string.IsNullOrWhiteSpace(SearchName)) 
                    if (!item.name.CaseInsensitiveContains(SearchName)) continue;

                DeviceCollection.Add(item);
            }
        }


        #region methods

        private void Fill()
        {
            devices = Dict.GetDevices();
            isAdmin = CheckIsAdmin(((App)Application.Current).CurrentUser.Level);
            DeviceCollection = new ObservableCollection<Device>(devices);
        }

        internal void UpdateItem()
        {
            var popUpWindow = new Item(SelectedItem.id);
            popUpWindow.Show();
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) popUpWindow.Owner = item;
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

        #endregion


    }

    public class Search
    {
        public string Name { get; set; }
    }
}
