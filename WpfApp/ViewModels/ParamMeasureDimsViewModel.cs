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
    public class ParamMeasureDimsViewModel : NotifyDataErrorInfoBase, IParamViewModel, IPublishEventDbEntityEvent
    {
        public ParamMeasureDimsViewModel()
        {
            _eventAggregator = ApplicationService.Instance.EventAggregator;
            cn = ((App)Application.Current).CurrentDb.ToString();
            Fill();
        }

        public void Fill()
        {
            using (var context = new DataContext(cn))
            {
                DataCollection = new ObservableCollection<Measure_Dims>(context.Measure_Dims.ToList());
                NewItem = new Measure_Dims();
                Selected = null;
            }
        }

        private ObservableCollection<Measure_Dims> _dataCollection;
        private Measure_Dims _selected;
        private Measure_Dims _newItem;
        private IEventAggregator _eventAggregator;
        private string cn;
        private RelayCommand _createCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _addCommand;

        public Measure_Dims Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public Measure_Dims NewItem
        {
            get => _newItem;
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
        public ObservableCollection<Measure_Dims> DataCollection
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
                    context.Measure_Dims.Add(NewItem);
                    context.SaveChanges();
                    PublishDbEntityEvent(new DbEntityEventParam(EventType.Create, Dict.ParamMeasureDims, NewItem.id_dim_measure));
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
                    var item = context.Measure_Dims.Find(Selected.id_dim_measure);
                    if (item != null)
                    {
                        context.Measure_Dims.Remove(item);
                        context.SaveChanges();
                        PublishDbEntityEvent(new DbEntityEventParam(EventType.Delete, Dict.ParamMeasureDims, Selected.id_dim_measure));
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
            NewItem = new Measure_Dims();
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
                    PublishDbEntityEvent(new DbEntityEventParam(EventType.Update, Dict.ParamMeasureDims, Selected.id_dim_measure));
                    Fill();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} => {e}", "Не удалось обновить запись!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }, (obj) => ExistInDb()));

        public bool ExistInDb()
        { 
            return Selected != null && Selected?.id_dim_measure > 0; 
        }

        public bool CheckItem()
        {
            if (string.IsNullOrWhiteSpace(Selected?.dim_measure) || Selected?.id_dim_measure > 0) return false;
            return true;
        }

        public void PublishDbEntityEvent(DbEntityEventParam dbEntityEventParam)
        {
            _eventAggregator
              .GetEvent<DbEntityEvent>()
              .Publish(dbEntityEventParam);
        }
    }
}
