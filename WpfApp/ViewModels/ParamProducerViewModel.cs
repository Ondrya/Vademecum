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
    public class ParamProducerViewModel : NotifyDataErrorInfoBase, IParamViewModel, IPublishEventDbEntityEvent
    {
        public ParamProducerViewModel()
        {
            _eventAggregator = ApplicationService.Instance.EventAggregator;
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        public void Fill()
        {
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<Producer>(context.Producers.ToList());
                NewItem = new Producer();
                Selected = null;
            }
        }

        private ObservableCollection<Producer> _dataCollection;
        private Producer _selected;
        private Producer _newItem;
        private IEventAggregator _eventAggregator;
        private string cn;
        private RelayCommand _createCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _addCommand;
        private RelayCommand _updateCommand;

        public Producer Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public Producer NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
        public ObservableCollection<Producer> DataCollection
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
                    context.Producers.Add(NewItem);
                    context.SaveChanges();
                    PublishDbEntityEvent(new DbEntityEventParam(EventType.Create, Dict.ParamProducer, NewItem.id_prod));
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
                    var item = context.Producers.Find(Selected.id_prod);
                    if (item != null)
                    {
                        context.Producers.Remove(item);
                        context.SaveChanges();
                        PublishDbEntityEvent(new DbEntityEventParam(EventType.Delete, Dict.ParamProducer, Selected.id_prod));
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
            NewItem = new Producer();
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
                    PublishDbEntityEvent(new DbEntityEventParam(EventType.Update, Dict.ParamProducer, Selected.id_prod));
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
            if (string.IsNullOrWhiteSpace(Selected?.name_prod) || Selected?.id_prod > 0) return false;
            return true;
        }

        public bool ExistInDb()
        {
            return Selected != null && Selected?.id_prod > 0;
        }

        public void PublishDbEntityEvent(DbEntityEventParam dbEntityEventParam)
        {
            _eventAggregator
              .GetEvent<DbEntityEvent>()
              .Publish(dbEntityEventParam);
        }
    }
}
