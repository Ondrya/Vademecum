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
    public class ParamSensElementViewModel : NotifyDataErrorInfoBase, IParamViewModel
    {
        public ParamSensElementViewModel()
        {
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        public void Fill()
        {
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<Sens_Element>(context.Sens_Element.ToList());
                NewItem = new Sens_Element();
                Selected = null;
            }
        }

        private ObservableCollection<Sens_Element> _dataCollection;
        private Sens_Element _selected;
        private Sens_Element _newItem;
        private string cn;
        private RelayCommand _createCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;
        private RelayCommand _updateCommand;

        public Sens_Element Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public Sens_Element NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
        public ObservableCollection<Sens_Element> DataCollection
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
                    context.Sens_Element.Add(NewItem);
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
                    var item = context.Sens_Element.Find(Selected.id_se);
                    if (item != null)
                    {
                        context.Sens_Element.Remove(item);
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
            NewItem = new Sens_Element();
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
            if (string.IsNullOrWhiteSpace(Selected?.name_se) || Selected?.id_se > 0) return false;
            return true;
        }

        public bool ExistInDb()
        {
            return Selected != null && Selected?.id_se > 0;
        }
    }
}
