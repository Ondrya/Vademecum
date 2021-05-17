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
        private RelayCommand _updateCommand;
        private RelayCommand _clearFilterCommand;
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
        private string _searchName;

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


        #region Параметры измерений

        public Measure SelectedMeasure
        {
            get => _selectedMeasure;
            set
            {
                _selectedMeasure = value;
                OnPropertyChanged(nameof(SelectedMeasure));
                UpdateCollection();
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
                UpdateCollection();
            }
        }
        private Measure_Dims _selectedMeasureDim;

        public float? MeasureValueMin
        {
            get => _measureValueMin;
            set
            {
                _measureValueMin = value;
                OnPropertyChanged(nameof(MeasureValueMin));
                UpdateCollection();
            }
        }
        private float? _measureValueMin;

        public float? MeasureValueMax
        {
            get => _measureValueMax;
            set
            {
                _measureValueMax = value;
                OnPropertyChanged(nameof(MeasureValueMax));
                UpdateCollection();
            }
        }
        private float? _measureValueMax;

        public float? ErrorMeasure
        {
            get => _errorMeasure;
            set
            {
                _errorMeasure = value;
                OnPropertyChanged(nameof(ErrorMeasure));
                UpdateCollection();
            }
        }
        private float? _errorMeasure;
        

        #endregion

        #region МГХ
        public float? WeightMin
        {
            get => _weightMin;
            set 
            { 
                _weightMin = value;
                OnPropertyChanged(nameof(WeightMin));
                UpdateCollection();
            }
        }
        private float? _weightMin;

        public float? WeightMax
        {
            get => _weightMax;
            set
            {
                _weightMax = value;
                OnPropertyChanged(nameof(WeightMax));
                UpdateCollection();
            }
        }
        private float? _weightMax;

        public float? LengthMin
        {
            get => _lengthMin;
            set
            {
                _lengthMin = value;
                OnPropertyChanged(nameof(LengthMin));
                UpdateCollection();
            }
        }
        private float? _lengthMin;

        public float? LengthMax
        {
            get => _lengthMax;
            set
            {
                _lengthMax = value;
                OnPropertyChanged(nameof(LengthMax));
                UpdateCollection();
            }
        }
        private float? _lengthMax;

        public float? WidthMin
        {
            get => _widthMin;
            set
            {
                _widthMin = value;
                OnPropertyChanged(nameof(WidthMin));
                UpdateCollection();
            }
        }
        private float? _widthMin;

        public float? WidthMax
        {
            get => _widthMax;
            set
            {
                _widthMax = value;
                OnPropertyChanged(nameof(WidthMax));
                UpdateCollection();
            }
        }
        private float? _widthMax;

        public float? HeightMin
        {
            get => _heightMin;
            set
            {
                _heightMin = value;
                OnPropertyChanged(nameof(HeightMin));
                UpdateCollection();
            }
        }
        private float? _heightMin;

        public float? HeightMax
        {
            get => _heightMax;
            set
            {
                _heightMax = value;
                OnPropertyChanged(nameof(HeightMax));
                UpdateCollection();
            }
        }
        private float? _heightMax;


        public string UnitDim
        {
            get => _unitDim;
            set
            {
                _unitDim = value;
                OnPropertyChanged(nameof(UnitDim));
                UpdateCollection();
            }
        }
        private string _unitDim;

        public string WeightDim
        {
            get => _weightDim;
            set
            {
                _weightDim = value;
                OnPropertyChanged(nameof(WeightDim));
                UpdateCollection();
            }
        }
        private string _weightDim;

        #endregion


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
        public RelayCommand ClearFilterCommand => _clearFilterCommand ?? (_clearFilterCommand = new RelayCommand(obj => ClearFilter()));

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

                if (SelectedMeasure != null)
                    if (item.id_measure != SelectedMeasure.id_measure) continue;
                if (MeasureValueMin != null)
                    if (item.min_measure == null || item.min_measure < MeasureValueMin) continue;
                if (MeasureValueMax != null)
                    if (item.max_measure == null || item.max_measure > MeasureValueMax) continue;
                if (SelectedMeasureDim != null)
                    if (item.id_dim_measure != SelectedMeasureDim.id_dim_measure) continue;

                if (ErrorMeasure != null)
                    if (item.error_measure == null || item.error_measure > ErrorMeasure) continue;

                if (WeightMin != null)
                    if (item.weight == null || item.weight < WeightMin) continue;
                if (WeightMax != null)
                    if (item.weight == null || item.weight > WeightMax) continue;

                if (LengthMin != null)
                    if (item.length == null || item.length < LengthMin) continue;
                if (LengthMax != null)
                    if (item.length == null || item.length > LengthMax) continue;

                if (WidthMin != null)
                    if (item.width == null || item.width < WidthMin) continue;
                if (WidthMax != null)
                    if (item.width == null || item.width > WidthMax) continue;

                if (HeightMin != null)
                    if (item.height == null || item.height < HeightMin) continue;
                if (HeightMax != null)
                    if (item.height == null || item.height > HeightMax) continue;

                //if (string.IsNullOrWhiteSpace(WeightDim))
                //    if (string.IsNullOrWhiteSpace(item.dim_weight) || !item.dim_weight.CaseInsensitiveContains(WeightDim)) continue;
                //if (string.IsNullOrWhiteSpace(UnitDim))
                //    if (string.IsNullOrWhiteSpace(item.dim_unit) || !item.dim_unit.CaseInsensitiveContains(UnitDim)) continue;


                DeviceCollection.Add(item);
            }
        }
        private void ClearFilter()
        {
            SearchName = null;
            SelectedBuiltTech = null;
            SelectedControl = null;
            SelectedDeviceType = null;
            SelectedFunction = null;
            SelectedItem = null;
            SelectedKind = null;
            SelectedMeasure = null;
            SelectedMeasureDim = null;
            SelectedMeasureProcessing = null;
            SelectedProducer = null;
            SelectedSensElement = null;
            SelectedType = null;

            MeasureValueMin = null;
            MeasureValueMax = null;
            ErrorMeasure = null;

            WeightMin = null;
            WeightMax = null;
            WeightDim = null;
            LengthMin = null;
            LengthMax = null;
            WidthMin = null;
            WidthMax = null;
            HeightMin = null;
            HeightMax = null;
            UnitDim = null;
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
