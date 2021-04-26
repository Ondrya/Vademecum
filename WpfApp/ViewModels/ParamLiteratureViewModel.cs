using System;
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
    public class ParamLiteratureViewModel : NotifyDataErrorInfoBase, IParamViewModel
    {
        public ParamLiteratureViewModel()
        {
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        public void Fill()
        {
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<Literature>(context.Literatures.ToList());
                NewItem = new Literature();
                Selected = null;
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
        private RelayCommand _addCommand;
        private RelayCommand _updateCommand;
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

        public RelayCommand AddCommand => _addCommand ?? (new RelayCommand(obj =>
        {
            NewItem = new Literature();
            Selected = NewItem;
        }));
        public RelayCommand CreateCommand => _createCommand ?? (new RelayCommand(obj =>
        {
            try
            {
                using (var context = new DataContext(cn))
                {
                    if (FilePath != null) Selected.lit_file = File.ReadAllBytes(FilePath);
                    context.Literatures.Add(Selected);
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
        }, (obj) => ExistInDb()));
        public RelayCommand UpdateCommand => _updateCommand ?? (new RelayCommand(obj =>
        {
            try
            {
                if (FileDel) Selected.lit_file = null;
                if (FilePath != null) Selected.lit_file = File.ReadAllBytes(FilePath);
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


        public bool CheckItem()
        {
            if (string.IsNullOrWhiteSpace(Selected?.lit_name) || Selected?.id_lit > 0) return false;
            return true;
        }

        public bool ExistInDb()
        {
            return Selected != null && Selected?.id_lit > 0;
        }
    }
}
