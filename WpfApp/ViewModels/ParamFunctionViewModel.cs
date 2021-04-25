using System;
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
    public class ParamFunctionViewModel : NotifyDataErrorInfoBase
    {
        public ParamFunctionViewModel()
        {
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        private void Fill()
        {
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<Function>(context.Functions.ToList());
                NewItem = new Function();
                Selected = null;
            }
        }

        private ObservableCollection<Function> _dataCollection;
        private Function _selected;
        private Function _newItem;
        private string cn;
        private RelayCommand _addCommand;
        private RelayCommand _createCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _deleteCommand;

        public Function Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public Function NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
        public ObservableCollection<Function> DataCollection
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
            NewItem = new Function();
            Selected = NewItem;
        }));

        public RelayCommand CreateCommand => _createCommand ?? (new RelayCommand(obj =>
        {
            try
            {
                using (var context = new DataContext(cn))
                {
                    context.Functions.Add(Selected);
                    context.SaveChanges();
                    Fill();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} => {e}", "Не удалось добавить новую запись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }, (obj) => CheckItem()));

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

        /// <summary>
        /// Объект not null и есть в базе (id>0)
        /// </summary>
        /// <returns></returns>
        private bool ExistInDb()
        {
            return Selected != null && Selected?.id_func > 0;
        }

        public RelayCommand DeleteCommand => _deleteCommand ?? (new RelayCommand(obj =>
        {
            try
            {
                using (var context = new DataContext(cn))
                {
                    var item = context.Functions.Find(Selected.id_func);
                    if (item != null)
                    {
                        context.Functions.Remove(item);
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

        private bool CheckItem()
        {
            if (string.IsNullOrWhiteSpace(Selected?.name_func) || Selected?.id_func>0) return false;
            return true;
        }
    }
}
