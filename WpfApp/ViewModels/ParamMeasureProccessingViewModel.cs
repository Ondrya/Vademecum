using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DataLayer;
using WpfApp.Commands;
using WpfApp.Validators;

namespace WpfApp.ViewModels
{
    public class ParamMeasureProccessingViewModel : NotifyDataErrorInfoBase, IParamViewModel
    {
        public ParamMeasureProccessingViewModel()
        {
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        public void Fill()
        {
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<Measure_Processing>(context.Measure_Processing.ToList());
                NewItem = new Measure_Processing();
                Selected = null;
            }
        }

        private ObservableCollection<Measure_Processing> _dataCollection;
        private Measure_Processing _selected;
        private Measure_Processing _newItem;
        private string cn;
        private RelayCommand _addCommand;
        private RelayCommand _createCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _deleteCommand;

        public Measure_Processing Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public Measure_Processing NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
        public ObservableCollection<Measure_Processing> DataCollection
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
            NewItem = new Measure_Processing();
            Selected = NewItem;
        }));

        public RelayCommand CreateCommand => _createCommand ?? (new RelayCommand(obj =>
        {
            try
            {
                using (var context = new DataContext(cn))
                {
                    context.Measure_Processing.Add(Selected);
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
        public bool ExistInDb()
        {
            return Selected != null && Selected?.id_measure_proc > 0;
        }

        public RelayCommand DeleteCommand => _deleteCommand ?? (new RelayCommand(obj =>
        {
            try
            {
                using (var context = new DataContext(cn))
                {
                    var item = context.Measure_Processing.Find(Selected.id_measure_proc);
                    if (item != null)
                    {
                        context.Measure_Processing.Remove(item);
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

        public bool CheckItem()
        {
            if (string.IsNullOrWhiteSpace(Selected?.name_measure_proc) || Selected?.id_measure_proc > 0) return false;
            return true;
        }
    }
}
