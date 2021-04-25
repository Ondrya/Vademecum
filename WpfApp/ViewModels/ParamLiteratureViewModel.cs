﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataLayer;
using WpfApp.Commands;
using WpfApp.Helpers;
using WpfApp.Validators;

namespace WpfApp.ViewModels
{
    public class ParamLiteratureViewModel : NotifyDataErrorInfoBase
    {
        public ParamLiteratureViewModel()
        {
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        private void Fill()
        {
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<Literature>(context.Literatures.ToList());
                NewItem = new Literature();
                FilePath = null;
                FileDel = false;
            }
        }

        private ObservableCollection<Literature> _dataCollection;
        private Literature _selected;
        private Literature _newItem;
        private string cn;
        private RelayCommand _createCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _selectFileCommand;
        private RelayCommand _deleteFileCommand;
        private string _filePath;
        private bool _fileDel;

        public string FilePath
        {
            get => _filePath;
            set 
            { 
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }
        public bool FileDel
        {
            get => _fileDel;
            set
            {
                _fileDel = value;
                OnPropertyChanged(nameof(FileDel));
            }
        }
        

        public Literature Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public Literature NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
        public ObservableCollection<Literature> DataCollection
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
                    if (FilePath != null) NewItem.lit_file = File.ReadAllBytes(FilePath);
                    context.Literatures.Add(NewItem);
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
                    var item = context.Literatures.Find(Selected.id_lit);
                    if (item != null)
                    {
                        context.Literatures.Remove(item);
                        context.SaveChanges();
                        Fill();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} => {e}", "Не удалось удалить запись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }, (obj) => Selected != null));

        public RelayCommand SelectFileCommand => _selectFileCommand ?? (new RelayCommand(obj =>
        {
            Functions.FileDialog("pdf files (*.pdf)|*.pdf|All files (*.*)|*.*", "pdf", "Выберите файл");
            FilePath = Functions.way_file;
        }));

        public RelayCommand DeleteFileCommand => _deleteFileCommand ?? (new RelayCommand(obj =>
        {
            FileDel = true;
            FilePath = null;
        }));


        private bool CheckItem()
        {
            if (string.IsNullOrWhiteSpace(NewItem.lit_name)) return false;
            return true;
        }
    }
}