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
        private Device _selectedItem;
        private bool isAdmin;
        private string _searchName;
        private RelayCommand _updateCommand;
        private ObservableCollection<Device> _deviceCollection;
        private ObservableCollection<DataLayer.Type> _typeCollection;
        private ObservableCollection<Producer> _producerCollection;
        private ObservableCollection<Enviroment> _enviromentCollection;
        private ObservableCollection<DeviceType> _deviceTypeCollection;
        private ObservableCollection<Function> _functionCollection;
        private ObservableCollection<Sens_Element> _sensElementCollection;
        private ObservableCollection<Kind> _kindCollection;
        private ObservableCollection<Control> _controlCollection;
        private ObservableCollection<Measure_Processing> _measureProcessingCollection;
        private ObservableCollection<BuiltTech> _builtTechCollection;
        private ObservableCollection<Measure> _measureCollection;
        private ObservableCollection<Measure_Dims> _measureDimCollection;
        private ObservableCollection<Literature> _literatureCollection;

        public ObservableCollection<Device> DeviceCollection
        {
            get => _deviceCollection;
            set 
            { 
                _deviceCollection = value;
                OnPropertyChanged(nameof(DeviceCollection));
            }
        }
        public ObservableCollection<Producer> ProducerCollection
        {
            get => _producerCollection;
            set
            {
                _producerCollection = value;
                OnPropertyChanged(nameof(ProducerCollection));
            }
        }
        public ObservableCollection<Enviroment> EnviromentCollection
        {
            get => _enviromentCollection;
            set
            {
                _enviromentCollection = value;
                OnPropertyChanged(nameof(EnviromentCollection));
            }
        }
        public ObservableCollection<DeviceType> DeviceTypeCollection
        {
            get => _deviceTypeCollection;
            set
            {
                _deviceTypeCollection = value;
                OnPropertyChanged(nameof(DeviceTypeCollection));
            }
        }
        public ObservableCollection<DataLayer.Type> TypeCollection
        {
            get => _typeCollection;
            set
            {
                _typeCollection = value;
                OnPropertyChanged(nameof(TypeCollection));
            }
        }
        public ObservableCollection<Function> FunctionCollection
        {
            get => _functionCollection;
            set
            {
                _functionCollection = value;
                OnPropertyChanged(nameof(FunctionCollection));
            }
        }
        public ObservableCollection<Sens_Element> SensElementCollection
        {
            get => _sensElementCollection;
            set
            {
                _sensElementCollection = value;
                OnPropertyChanged(nameof(SensElementCollection));
            }
        }
        public ObservableCollection<Kind> KindCollection
        {
            get => _kindCollection;
            set
            {
                _kindCollection = value;
                OnPropertyChanged(nameof(KindCollection));
            }
        }
        public ObservableCollection<Control> ControlCollection
        {
            get => _controlCollection;
            set
            {
                _controlCollection = value;
                OnPropertyChanged(nameof(ControlCollection));
            }
        }
        public ObservableCollection<Measure_Processing> MeasureProcessingCollection
        {
            get => _measureProcessingCollection;
            set
            {
                _measureProcessingCollection = value;
                OnPropertyChanged(nameof(MeasureProcessingCollection));
            }
        }
        public ObservableCollection<BuiltTech> BuiltTechCollection
        {
            get => _builtTechCollection;
            set
            {
                _builtTechCollection = value;
                OnPropertyChanged(nameof(BuiltTechCollection));
            }
        }
        public ObservableCollection<Measure> MeasureCollection
        {
            get => _measureCollection;
            set
            {
                _measureCollection = value;
                OnPropertyChanged(nameof(MeasureCollection));
            }
        }
        public ObservableCollection<Measure_Dims> MeasureDimCollection
        {
            get => _measureDimCollection;
            set
            {
                _measureDimCollection = value;
                OnPropertyChanged(nameof(MeasureDimCollection));
            }
        }
        public ObservableCollection<Literature> LiteratureCollection
        {
            get => _literatureCollection;
            set
            {
                _literatureCollection = value;
                OnPropertyChanged(nameof(LiteratureCollection));
            }
        }
        
        public Producer SelectedProducer
        {
            get => _selectedProducer;
            set
            {
                _selectedProducer = value;
                OnPropertyChanged(nameof(SelectedProducer));
            }
        }
        private Producer _selectedProducer;
        
        public DeviceType SelectedDeviceType
        {
            get => _selectedDeviceType;
            set
            {
                _selectedDeviceType = value;
                OnPropertyChanged(nameof(SelectedDeviceType));
            }
        }
        private DeviceType _selectedDeviceType;

        public DataLayer.Type SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
                UpdateCollection();
            }
        }
        private DataLayer.Type _selectedType;

        public Function SelectedFunction
        {
            get => _selectedFunction;
            set
            {
                _selectedFunction = value;
                OnPropertyChanged(nameof(SelectedFunction));
                UpdateCollection();
            }
        }
        private Function _selectedFunction;

        public Sens_Element SelectedSensElement
        {
            get => _selectedSensElement;
            set
            {
                _selectedSensElement = value;
                OnPropertyChanged(nameof(SelectedSensElement));
                UpdateCollection();
            }
        }
        private Sens_Element _selectedSensElement;

        public Kind SelectedKind
        {
            get => _selectedKind;
            set
            {
                _selectedKind = value;
                OnPropertyChanged(nameof(SelectedKind));
                UpdateCollection();
            }
        }
        private Kind _selectedKind;

        public Control SelectedControl
        {
            get => _selectedControl;
            set
            {
                _selectedControl = value;
                OnPropertyChanged(nameof(SelectedControl));
                UpdateCollection();
            }
        }
        private Control _selectedControl;

        public Measure_Processing SelectedMeasureProcessing
        {
            get => _selectedMeasureProcessing;
            set
            {
                _selectedMeasureProcessing = value;
                OnPropertyChanged(nameof(SelectedMeasureProcessing));
                UpdateCollection();
            }
        }
        private Measure_Processing _selectedMeasureProcessing;

        public BuiltTech SelectedBuiltTech
        {
            get => _selectedBuiltTech;
            set
            {
                _selectedBuiltTech = value;
                OnPropertyChanged(nameof(SelectedBuiltTech));
                UpdateCollection();
            }
        }
        private BuiltTech _selectedBuiltTech;

        public Measure SelectedMeasure
        {
            get => _selectedMeasure;
            set
            {
                _selectedMeasure = value;
                OnPropertyChanged(nameof(SelectedMeasure));
            }
        }
        private Measure _selectedMeasure;

        public Measure_Dims SelectedMeasureDim
        {
            get => _selectedMeasureDim;
            set
            {
                _selectedMeasureDim = value;
                OnPropertyChanged(nameof(SelectedMeasureDim));
            }
        }
        private Measure_Dims _selectedMeasureDim;

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
                if (SelectedType != null)
                    if (item.id_type != SelectedType.id_type) continue;
                if (SelectedFunction != null)
                    if (item.id_func != SelectedFunction.id_func) continue;
                if (SelectedSensElement != null)
                    if (item.id_se != SelectedSensElement.id_se) continue;
                if (SelectedKind != null)
                    if (item.id_kind != SelectedKind.id_kind) continue;
                if (SelectedControl != null)
                    if (item.id_control != SelectedControl.id_control) continue;
                if (SelectedMeasureProcessing != null)
                    if (item.id_measure_proc != SelectedMeasureProcessing.id_measure_proc) continue;
                if (SelectedBuiltTech != null)
                    if (item.id_built_tech != SelectedBuiltTech.id_built_tech) continue;

                
                DeviceCollection.Add(item);
            }
        }


        #region methods

        private void Fill()
        {
            devices = Dict.GetDevices();
            isAdmin = CheckIsAdmin(((App)Application.Current).CurrentUser.Level);
            DeviceCollection = new ObservableCollection<Device>(devices);

            DeviceTypeCollection = new ObservableCollection<DeviceType>(Helpers.Dict.GetDeviceTypes());
            DeviceTypeCollection.Add(null);
            TypeCollection = new ObservableCollection<DataLayer.Type>(Helpers.Dict.GetTypes());
            TypeCollection.Add(null);
            FunctionCollection = new ObservableCollection<Function>(Helpers.Dict.GetFunctions());
            FunctionCollection.Add(null);
            SensElementCollection = new ObservableCollection<Sens_Element>(Helpers.Dict.GetSensElements());
            SensElementCollection.Add(null);
            KindCollection = new ObservableCollection<Kind>(Helpers.Dict.GetKinds());
            KindCollection.Add(null);
            ControlCollection = new ObservableCollection<Control>(Helpers.Dict.GetControls());
            ControlCollection.Add(null);
            MeasureProcessingCollection = new ObservableCollection<Measure_Processing>(Helpers.Dict.GetMeasureProcessing());
            MeasureProcessingCollection.Add(null);
            BuiltTechCollection = new ObservableCollection<BuiltTech>(Helpers.Dict.GetBuiltTeches());
            BuiltTechCollection.Add(null);
            MeasureCollection = new ObservableCollection<Measure>(Helpers.Dict.GetMeasures());
            MeasureCollection.Add(null);
            MeasureDimCollection = new ObservableCollection<Measure_Dims>(Helpers.Dict.GetMeasureDims());
            MeasureDimCollection.Add(null);
            EnviromentCollection = new ObservableCollection<Enviroment>(Helpers.Dict.GetEnviroments());
            EnviromentCollection.Add(null);
            ProducerCollection = new ObservableCollection<Producer>(Helpers.Dict.GetProdusers());
            ProducerCollection.Add(null);
            LiteratureCollection = new ObservableCollection<Literature>(Helpers.Dict.GetLiterature());
            LiteratureCollection.Add(null);
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
