using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataLayer;
using Prism.Events;
using WpfApp.Commands;
using WpfApp.Events;
using WpfApp.Interfaces;
using WpfApp.Validators;

namespace WpfApp.ViewModels
{
    public class ParamEnviromentViewModel : NotifyDataErrorInfoBase, IParamViewModel, IPublishEventDbEntityEvent
    {
        public ParamEnviromentViewModel()
        {
            _eventAggregator = ApplicationService.Instance.EventAggregator;
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        public bool IsAdmin => Helpers.Common.CheckIsAdmin(((App)Application.Current).CurrentUser.Level);

        public void Fill()
        {
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<Enviroment>(context.Enviroments.ToList());
                NewItem = new Enviroment();
                Selected = null;
            }
        }

        private ObservableCollection<Enviroment> _dataCollection;
        private Enviroment _selected;
        private Enviroment _newItem;
        private IEventAggregator _eventAggregator;
        private string cn;
        private RelayCommand _createCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;
        private RelayCommand _updateCommand;

        public Enviroment Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public Enviroment NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
        public ObservableCollection<Enviroment> DataCollection
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
                    context.Enviroments.Add(NewItem);
                    context.SaveChanges();
                    PublishDbEntityEvent(new DbEntityEventParam(EventType.Create, Dict.ParamEnviroment, NewItem.id_envi));
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
                    var item = context.Enviroments.Find(Selected.id_envi);
                    if (item != null)
                    {
                        context.Enviroments.Remove(item);
                        context.SaveChanges();
                        PublishDbEntityEvent(new DbEntityEventParam(EventType.Delete, Dict.ParamEnviroment, Selected.id_envi));
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
            NewItem = new Enviroment();
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
                    PublishDbEntityEvent(new DbEntityEventParam(EventType.Update, Dict.ParamEnviroment, Selected.id_envi));
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
            if (string.IsNullOrWhiteSpace(Selected?.name_envi) || Selected?.id_envi > 0) return false;
            return true;
        }

        public bool ExistInDb()
        {
            return Selected != null && Selected?.id_envi > 0;
        }

        public void PublishDbEntityEvent(DbEntityEventParam dbEntityEventParam)
        {
            _eventAggregator
              .GetEvent<DbEntityEvent>()
              .Publish(dbEntityEventParam);
        }
    }
}
