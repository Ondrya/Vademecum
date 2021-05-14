using DataLayer;
using System;
using System.Linq;
using System.Windows;
using WpfApp.Validators;
using WpfApp.Commands;
using WpfApp.Models;
using System.Collections.ObjectModel;
using WpfApp.Views;
using Prism.Events;
using WpfApp.Events;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;
using WpfApp.Converters;
using System.Collections.Generic;

namespace WpfApp.ViewModels
{
    public class ItemViewModel : NotifyDataErrorInfoBase
    {
        public ItemViewModel()
        {
            _eventAggregator = ApplicationService.Instance.EventAggregator;
            // подписываемся на сообщения
            _eventAggregator
                .GetEvent<DbEntityEvent>()
                .Subscribe(HandleParamEvent);

            DeviceTypeCollection = new ObservableCollection<DeviceType>();
            TypeCollection = new ObservableCollection<DataLayer.Type>();
            FunctionCollection = new ObservableCollection<Function>();
            SensElementCollection = new ObservableCollection<Sens_Element>();
            KindCollection = new ObservableCollection<Kind>();
            ControlCollection = new ObservableCollection<Control>();
            MeasureProcessingCollection = new ObservableCollection<Measure_Processing>();
            BuiltTechCollection = new ObservableCollection<BuiltTech>();
            MeasureCollection = new ObservableCollection<Measure>();
            MeasureDimCollection = new ObservableCollection<Measure_Dims>();
            EnviromentCollection = new ObservableCollection<EnviromentViewModel>();
            LiteratureCollection = new ObservableCollection<LiteratureViewModel>();
        }

        IEventAggregator _eventAggregator;
        private Device _currentDevice;
        private int _currentDeviceId;
        private string cn;
        private LaType LaType;
        private RelayCommand _saveCommand;
        private RelayCommand _openParamWindowCommand;
        private RelayCommand _uploadImageCommand;
        private RelayCommand _showImageCommand;
        private RelayCommand _deleteImageCommand;

        private ObservableCollection<DeviceType> _deviceTypeCollection;
        private ObservableCollection<DataLayer.Type> _typeCollection;
        private ObservableCollection<Function> _functionCollection;
        private ObservableCollection<Sens_Element> _sensElementCollection;
        private ObservableCollection<Kind> _kindCollection;
        private ObservableCollection<Control> _controlCollection;
        private ObservableCollection<Measure_Processing> _measureProcessingCollection;
        private ObservableCollection<BuiltTech> _builtTechCollection;
        private ObservableCollection<Measure> _measureCollection;
        private ObservableCollection<Measure_Dims> _measureDimCollection;
        private ObservableCollection<EnviromentViewModel> _enviromentCollection;
        private ObservableCollection<LiteratureViewModel> _literatureCollection;
        private string _buttonName;

        // при сохранении присваивается id и данные перезагружаются, поэтому Collections перезагружаются и обнуляются


        private bool _deviceCreated;

        public bool DeviceCreated
        {
            get => _deviceCreated;
            set 
            { 
                _deviceCreated = value;
                OnPropertyChanged(nameof(DeviceCreated));
            }
        }



        private byte[] _rawImageDataSchema;
        public byte[] RawImageDataSchema
        {
            get { return _rawImageDataSchema; }
            set
            {
                _rawImageDataSchema = value;
                OnPropertyChanged(nameof(RawImageDataSchema));
                CurrentDevice.schema = value;
            }
        }


        private byte[] _rawImageDataView;
        public byte[] RawImageDataView
        {
            get { return _rawImageDataView; }
            set
            {
                _rawImageDataView = value;
                OnPropertyChanged(nameof(RawImageDataView));
                CurrentDevice.view = value;
            }
        }

        public DeviceType SelectedDeviceType
        {
            get => _selectedDeviceType;
            set 
            { 
                _selectedDeviceType = value;
                OnPropertyChanged(nameof(SelectedDeviceType));
                CurrentDevice.id_device = value?.id_device;
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
                CurrentDevice.id_type = value?.id_type;
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
                CurrentDevice.id_func = value?.id_func;
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
                CurrentDevice.id_se = value?.id_se;
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
                CurrentDevice.id_kind = value?.id_kind;
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
                CurrentDevice.id_control = value?.id_control;
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
                CurrentDevice.id_measure_proc = value?.id_measure_proc;
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
                CurrentDevice.id_built_tech = value?.id_built_tech;
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
                CurrentDevice.id_dim_measure = value?.id_dim_measure;
            }
        }
        private Measure_Dims _selectedMeasureDim;


        private ObservableCollection<Producer> _producerCollection;

        public ObservableCollection<Producer> ProducerCollection
        {
            get => _producerCollection;
            set 
            { 
                _producerCollection = value;
                OnPropertyChanged(nameof(ProducerCollection));
            }
        }
        public ObservableCollection<EnviromentViewModel> EnviromentCollection
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
        public ObservableCollection<LiteratureViewModel> LiteratureCollection
        {
            get => _literatureCollection;
            set 
            { 
                _literatureCollection = value;
                OnPropertyChanged(nameof(LiteratureCollection));
            }
        }


        public string ButtonName
        {
            get => _buttonName;
            set
            {
                _buttonName = value;
                OnPropertyChanged(nameof(ButtonName));
            }
        }
        
        public Device CurrentDevice
        {
            get => _currentDevice;
            set 
            {
                _currentDevice = value;
                OnPropertyChanged(nameof(CurrentDevice));

                SelectedDeviceType = DeviceTypeCollection.FirstOrDefault(x => x.id_device == value?.id_type);
                SelectedType = TypeCollection.FirstOrDefault(x => x.id_type == value?.id_type);
                SelectedFunction = FunctionCollection.FirstOrDefault(x => x.id_func == value?.id_func);
                SelectedSensElement = SensElementCollection.FirstOrDefault(x => x.id_se == value?.id_se);
                SelectedKind = KindCollection.FirstOrDefault(x => x.id_kind == value?.id_kind);
                SelectedControl = ControlCollection.FirstOrDefault(x => x.id_control == value?.id_control);
                SelectedMeasureProcessing = MeasureProcessingCollection.FirstOrDefault(x => x.id_measure_proc == value?.id_measure_proc);
                SelectedBuiltTech = BuiltTechCollection.FirstOrDefault(x => x.id_built_tech == value?.id_built_tech);
                SelectedMeasureDim = MeasureDimCollection.FirstOrDefault(x => x.id_dim_measure == value?.id_dim_measure);
                SelectedProducer = ProducerCollection.FirstOrDefault(x => x.id_prod == value?.id_prod);

                // было в связанную таблицу Device_Measurе - изменю на прямое добавление как пункты выше(удалю таблицу-связь, добавлю id_measure в главную таблицу Device)
                //SelectedMeasure = MeasureCollection.FirstOrDefault(x => x.id_measure == value?.id_measure);
                var measureIds = Helpers.Dict.GetDeviceMeasures(CurrentDeviceId)?.ToList();
                if (measureIds != null && measureIds.Count>0)
                {
                    var measureId = measureIds[0];
                    SelectedMeasure = MeasureCollection.FirstOrDefault(x => x.id_measure == measureId);
                }
                    

                    
                        

                if (CurrentDeviceId > 0)
                {
                    var enviIds = Helpers.Dict.GetDeviceEnviroments(CurrentDeviceId).ToList();
                    var litIds = Helpers.Dict.GetDeviceLiteratures(CurrentDeviceId).ToList();

                    if (enviIds != null) foreach (var enviId in enviIds)
                        {
                            foreach (var envi in EnviromentCollection)
                            {
                                if (envi.id_envi == enviId) envi.IsSelected = true;
                            }
                        }
                    if (litIds != null) foreach (var litId in litIds)
                        {
                            foreach (var lit in LiteratureCollection)
                            {
                                if (lit.id_lit == litId) lit.IsSelected = true;
                            }
                        }
                }
            }
        }
        public Producer SelectedProducer
        {
            get => _selectedProducer;
            set 
            { 
                _selectedProducer = value;
                OnPropertyChanged(nameof(SelectedProducer));
                CurrentDevice.id_prod = value?.id_prod;
            }
        }
        private Producer _selectedProducer;

        public int CurrentDeviceId
        {
            get => _currentDeviceId;
            set 
            { 
                _currentDeviceId = value;
                OnPropertyChanged(nameof(CurrentDeviceId));
                DeviceCreated = value > 0;
                Fill();
            }
        }

        /// <summary>
        /// Загрузить данные
        /// </summary>
        private void Fill()
        {
            cn = ((App)Application.Current).CurrentDb.ToString();
            LaType = ((App)Application.Current).CurrentSession.SelectedLaType;
            ButtonName = CurrentDeviceId == 0 ? "Создать" : "Сохранить";

            DeviceTypeCollection = new ObservableCollection<DeviceType>(Helpers.Dict.GetDeviceTypes());
            TypeCollection = new ObservableCollection<DataLayer.Type>(Helpers.Dict.GetTypes());
            FunctionCollection = new ObservableCollection<Function>(Helpers.Dict.GetFunctions());
            SensElementCollection = new ObservableCollection<Sens_Element>(Helpers.Dict.GetSensElements());
            KindCollection = new ObservableCollection<Kind>(Helpers.Dict.GetKinds());
            ControlCollection = new ObservableCollection<Control>(Helpers.Dict.GetControls());
            MeasureProcessingCollection = new ObservableCollection<Measure_Processing>(Helpers.Dict.GetMeasureProcessing());
            BuiltTechCollection = new ObservableCollection<BuiltTech>(Helpers.Dict.GetBuiltTeches());
            MeasureCollection = new ObservableCollection<Measure>(Helpers.Dict.GetMeasures());
            MeasureDimCollection = new ObservableCollection<Measure_Dims>(Helpers.Dict.GetMeasureDims());
            EnviromentCollection = new ObservableCollection<EnviromentViewModel>(Helpers.Dict.GetEnviroments().Select(x => new EnviromentViewModel(x, false)));
            ProducerCollection = new ObservableCollection<Producer>(Helpers.Dict.GetProdusers());
            LiteratureCollection = new ObservableCollection<LiteratureViewModel>(Helpers.Dict.GetLiterature().Select(x => new LiteratureViewModel(x, false)));

            using (var context = new DataContext(cn))
            {
                CurrentDevice = CurrentDeviceId == 0 ? new Device() { id_LA = (int)LaType } : context.Devices.Find(CurrentDeviceId);
            }

            //CurrentDeviceProducer = CurrentDevice == null ? new Producer() : ProducerCollection.First(x => x.id_prod == CurrentDevice.id_prod);
            RawImageDataSchema = CurrentDevice.schema;
            RawImageDataView = CurrentDevice.view;
        }

        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(obj =>
        {
            if (CurrentDeviceId == 0)
            {
                DeviceCreate();
            }
            else
            {
                DeviceUpdate();
            }
        }, (obj) => CanSave()));
        public RelayCommand OpenParamWindowCommand => _openParamWindowCommand ?? (_openParamWindowCommand = 
            new RelayCommand( obj => {
                var windowName = obj.ToString();
                switch (windowName)
                {
                    // Тип 
                    case "ParamType":
                        var ParamTypeWindow = new ParamType();
                        ParamTypeWindow.Show();
                        break;
                    //Назначение
                    case "ParamFunction":
                        var ParamFunctionWindow = new ParamFunction();
                        ParamFunctionWindow.Show();
                        break;
                    //Чувствительный элемент
                    case "ParamSensElement":
                        var ParamSensElementWindow = new ParamSensElement();
                        ParamSensElementWindow.Show();
                        break;
                    //Принцип действия
                    case "ParamKind":
                        var ParamKindWindow = new ParamKind();
                        ParamKindWindow.Show();
                        break;
                    //Метод управления
                    case "ParamControlType":
                        var ParamControlTypeWindow = new ParamControlType();
                        ParamControlTypeWindow.Show();
                        break;
                    //Возможные операции
                    case "ParamMeasureProccessing":
                        var ParamMeasureProccessingWindow = new ParamMeasureProccessing();
                        ParamMeasureProccessingWindow.Show();
                        break;
                    //Технология изготовления
                    case "ParamBuiltTech":
                        var ParamBuiltTechWindow = new ParamBuiltTech();
                        ParamBuiltTechWindow.Show();
                        break;
                    //Измеряемая величина
                    case "ParamMeasure":
                        var ParamMeasureWindow = new ParamMeasure();
                        ParamMeasureWindow.Show();
                        break;
                    //Единицы измерений
                    case "ParamMeasureDims":
                        var ParamMeasureDimsWindow = new ParamMeasureDims();
                        ParamMeasureDimsWindow.Show();
                        break;
                    default:
                        MessageBox.Show("Такого справочника нет", "Элемент не найден", MessageBoxButton.OK, MessageBoxImage.Warning);
                        throw new ArgumentException();
                        break;
                }
            }));
        public RelayCommand UploadImageCommand => _uploadImageCommand ?? (_uploadImageCommand = new RelayCommand(obj => 
        {
            var target = obj.ToString();
            switch (target)
            {
                case "Schema":
                    RawImageDataSchema = SelectImage();
                    break;
                case "View":
                    RawImageDataView = SelectImage();
                    break;
                default:
                    break;
            }
        }));

        public RelayCommand DeleteImageCommand => _deleteImageCommand ?? (_deleteImageCommand = new RelayCommand(obj =>
        {
            var target = obj.ToString();
            switch (target)
            {
                case "Schema":
                    RawImageDataSchema = null;
                    break;
                case "View":
                    RawImageDataView = null;
                    break;
                default:
                    break;
            }
        }, (obj) => CanDelete(obj.ToString())));

        private bool CanDelete(string target)
        {
            switch (target)
            {
                case "Schema":
                    return RawImageDataSchema != null;
                case "View":
                    return RawImageDataView != null;
                default:
                    return false;
            }
        }

        public RelayCommand ShowImageCommand => 
            _showImageCommand ?? (_showImageCommand = new RelayCommand(obj => 
            {
                var target = obj.ToString();
                switch (target)
                {
                    case "Schema":
                        ShowImage(RawImageDataSchema);
                        break;
                    case "View":
                        ShowImage(RawImageDataView);
                        break;
                    default:
                        break;
                }
            }, (obj) => CanShowImage(obj.ToString())));

        private bool CanShowImage(string target)
        {
            switch (target)
            {
                case "Schema":
                    return RawImageDataSchema != null;
                case "View":
                    return RawImageDataView != null;
                default:
                    return false;
            }
        }

        private void ShowImage(byte[] rawImageDataSchema)
        {
            var showImageWindow = new PopUpImage(rawImageDataSchema);
            showImageWindow.Show();
        }

        private byte[] SelectImage()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Filter = "Images (*.jpg,*.png)|*.jpg;*.png|All Files(*.*)|*.*"
                //Filter = "Images (*.jpg)|*.jpg;|All Files(*.*)|*.*"
            };

            if (dialog.ShowDialog() != true) { return null; }

            return File.ReadAllBytes(dialog.FileName);
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(CurrentDevice?.name) && SelectedDeviceType != null;
        }

        private void DeviceUpdate()
        {
            using (var context = new DataContext(cn))
            {
                try
                {
                    context.Entry(CurrentDevice).State = System.Data.Entity.EntityState.Modified; ;
                    context.SaveChanges();
#if !DEBUG
                    CloseWindow();
#endif
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Обновление остновных параметров");
                }

            }
            using (var context = new DataContext(cn))
            {
                try
                {
                    var selectedEnvies = EnviromentCollection.Where(e => e.IsSelected).Select(e => e.id_envi).ToList();
                    var envies = context.Enviroments.Where(x => selectedEnvies.Contains(x.id_envi)).ToList(); // возмжные среды
                    var device = context.Devices.Find(CurrentDeviceId);
                    var deviceEnvies = device.Enviroments.ToList();

                    foreach (var item in deviceEnvies) device.Enviroments.Remove(item);
                    foreach (var item in envies) device.Enviroments.Add(item);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Обновление списка сред");
                }
            }

            using (var context = new DataContext(cn))
            {
                try
                {
                    var selectedLits = LiteratureCollection.Where(e => e.IsSelected).Select(e => e.id_lit);
                    var lits = context.Literatures.Where(x => selectedLits.Contains(x.id_lit)).ToList(); // список литературы
                    var device = context.Devices.Find(CurrentDeviceId);
                    var deviceLits = device.Literatures.ToList();

                    foreach (var item in deviceLits) device.Literatures.Remove(item);
                    foreach (var item in lits) device.Literatures.Add(item);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Обновление списка литературы");
                }
            }

            using (var context = new DataContext(cn))
            {
                if (SelectedMeasure == null) return;
                try
                {
                    var device = context.Devices.Find(CurrentDeviceId);
                    var measures= context.Measures.First(x => SelectedMeasure.id_measure==x.id_measure); // список измерений
                    var deviceMeasures = device.Measures.ToList();

                    foreach (var item in deviceMeasures) device.Measures.Remove(item);
                    device.Measures.Add(measures);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Обновление списка сред");
                }

            }

            MessageBox.Show($"Устройство ОБНОВЛЕНО - id: {CurrentDevice.id}", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeviceCreate()
        {
            using (var context = new DataContext(cn))
            {
                try
                {
                    CurrentDevice.id_type = SelectedDeviceType?.id_device;
                    context.Devices.Add(CurrentDevice);
                    context.SaveChanges();
                    CurrentDeviceId = CurrentDevice.id;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                
            }

            MessageBox.Show($"id: {CurrentDevice.id}", "Устройство сохранено", MessageBoxButton.OK, MessageBoxImage.Information);
#if !DEBUG
            CloseWindow();
#endif
        }

        private void CloseWindow()
        {
            foreach (Window item in Application.Current.Windows)
                if (item.DataContext == this) item.Close();
        }

        /// <summary>
        /// Обработка входящего сообщения от словарей
        /// </summary>
        /// <param name="paramEvent"></param>
        private void HandleParamEvent(DbEntityEventParam msg)
        {
            // обработать параметр - обновить нужную Collection - add, remove, update на основе Dict
            // обрабатываем только добавление, в остальных случаях выводим сообщение, что данные изменились и, возможно,  
            // требуется перезагрузка формы

            switch (msg.Crud)
            {
                case EventType.Create:
                    using (var context = new DataContext(cn))
                    {
                        switch (msg.Item)
                        {
                            case Dict.ParamType:
                                TypeCollection.Add(context.Types.Find(msg.EntityId));
                                break;
                            case Dict.ParamFunction:
                                FunctionCollection.Add(context.Functions.Find(msg.EntityId));
                                break;
                            case Dict.ParamSensElement:
                                SensElementCollection.Add(context.Sens_Element.Find(msg.EntityId));
                                break;
                            case Dict.ParamKind:
                                KindCollection.Add(context.Kinds.Find(msg.EntityId));
                                break;
                            case Dict.ParamControlType:
                                ControlCollection.Add(context.Controls.Find(msg.EntityId));
                                break;
                            case Dict.ParamMeasureProccessing:
                                MeasureProcessingCollection.Add(context.Measure_Processing.Find(msg.EntityId));
                                break;
                            case Dict.ParamBuiltTech:
                                BuiltTechCollection.Add(context.BuiltTeches.Find(msg.EntityId));
                                break;
                            case Dict.ParamMeasure:
                                MeasureCollection.Add(context.Measures.Find(msg.EntityId));
                                break;
                            case Dict.ParamMeasureDims:
                                MeasureDimCollection.Add(context.Measure_Dims.Find(msg.EntityId));
                                break;
                            case Dict.ParamProducer:
                                ProducerCollection.Add(context.Producers.Find(msg.EntityId));
                                break;
                            case Dict.ParamLiterature:
                                LiteratureCollection.Add(new LiteratureViewModel(context.Literatures.Find(msg.EntityId), false));
                                break;
                            case Dict.ParamEnviroment:
                                EnviromentCollection.Add(new EnviromentViewModel(context.Enviroments.Find(msg.EntityId), false));
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    var converter = new EnumDescriptionConverter();
                    MessageBox.Show($"Данные справочника {converter.GetEnumDescription(msg.Item)} обновлены, возможно требуется перезагрузить форму");
                    break;
            }
        }
    }
}
