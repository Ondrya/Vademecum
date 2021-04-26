﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataLayer;
using WpfApp.Commands;
using WpfApp.Validators;

namespace WpfApp.ViewModels
{
    public class ParamTypeViewModel : NotifyDataErrorInfoBase, IParamViewModel
    {
        public ParamTypeViewModel()
        {
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        public void Fill()
        {
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<DataLayer.Type>(context.Types.ToList());
                NewItem = new DataLayer.Type();
                Selected = null;
            }
        }

        private ObservableCollection<DataLayer.Type> _dataCollection;
        private DataLayer.Type _selected;
        private DataLayer.Type _newItem;
        private string cn;
        private RelayCommand _createCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;
        private RelayCommand _updateCommand;

        public DataLayer.Type Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public DataLayer.Type NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
        public ObservableCollection<DataLayer.Type> DataCollection
        {
            get => _dataCollection;
            set
            {
                _dataCollection = value;
                OnPropertyChanged(nameof(DataCollection));
            }
        }

        public RelayCommand CreateCommand => _createCommand ?? (new RelayCommand(obj =>
        {
            try
            {
                using (var context = new DataContext(cn))
                {
                    context.Types.Add(NewItem);
                    context.SaveChanges();
                    Fill();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} => {e}", "Не удалось добавить новую запись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }, (obj) => CheckItem()));

        public RelayCommand DeleteCommand => _deleteCommand ?? (new RelayCommand(obj =>
        {
            try
            {
                using (var context = new DataContext(cn))
                {
                    var item = context.Types.Find(Selected.id_type);
                    if (item != null)
                    {
                        context.Types.Remove(item);
                        context.SaveChanges();
                        Fill();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} => {e}", "Не удалось удалить запись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }, (obj) => ExistInDb()));

        public RelayCommand AddCommand => _addCommand ?? (new RelayCommand(obj =>
        {
            NewItem = new DataLayer.Type();
            Selected = NewItem;
        }));

        public RelayCommand UpdateCommand => _updateCommand ?? (new RelayCommand(obj =>
        {
            try
            {
                using (var context = new DataContext(cn))
                {
                    context.Entry(Selected).State = System.Data.Entity.EntityState.Modified; ;
                    context.SaveChanges();
                    Fill();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} => {e}", "Не удалось обновить запись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }, (obj) => ExistInDb()));

        public bool CheckItem()
        {
            if (string.IsNullOrWhiteSpace(Selected?.name_type) || Selected?.id_type > 0) return false;
            return true;
        }

        public bool ExistInDb()
        {
            return Selected != null && Selected?.id_type > 0;
        }
    }
}
