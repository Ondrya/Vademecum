using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DataLayer;
using WpfApp.Models;
using DataLayer;

namespace WpfApp.Helpers
{
    /// <summary>
    /// Получить коллекции-списки сущностей базы данных
    /// </summary>
    public class Dict
    {
        /// <summary>
        /// Получить строку соединения
        /// </summary>
        /// <returns></returns>
        public static string GetCn()
        {
            return ((App)Application.Current).CurrentDb.ToString();
        }


        /// <summary>
        /// Текущий тип ЛА
        /// </summary>
        /// <returns></returns>
        public static int GetSelectedLaType()
        {
            return (int)((App)Application.Current).CurrentSession.SelectedLaType;
        }

        /// <summary>
        /// Полуить список типов устройств
        /// </summary>
        /// <returns></returns>
        public static List<DeviceType> GetDeviceTypes() => new DataContext(GetCn()).DeviceTypes.ToList();
        public static List<DataLayer.Type> GetTypes() => new DataContext(GetCn()).Types.ToList();
        public static List<Function> GetFunctions() => new DataContext(GetCn()).Functions.ToList();
        public static List<Sens_Element> GetSensElements() => new DataContext(GetCn()).Sens_Element.ToList();
        public static List<Kind> GetKinds() => new DataContext(GetCn()).Kinds.ToList();
        public static List<Control> GetControls() => new DataContext(GetCn()).Controls.ToList();
        public static List<Measure_Processing> GetMeasureProcessing() => new DataContext(GetCn()).Measure_Processing.ToList();
        public static List<BuiltTech> GetBuiltTeches() => new DataContext(GetCn()).BuiltTeches.ToList();
        public static List<Measure> GetMeasures() => new DataContext(GetCn()).Measures.ToList();
        public static List<Measure_Dims> GetMeasureDims() => new DataContext(GetCn()).Measure_Dims.ToList();
        public static List<Enviroment> GetEnviroments() => new DataContext(GetCn()).Enviroments.ToList();
       
        public static List<Producer> GetProdusers() => new DataContext(GetCn()).Producers.ToList();
        public static List<Literature> GetLiterature() => new DataContext(GetCn()).Literatures.ToList();

        public static List<int> GetDeviceEnviroments(int currentDeviceId) => new DataContext(GetCn()).Devices.Find(currentDeviceId)?.Enviroments.Select(x => x.id_envi).ToList();
        public static List<int> GetDeviceLiteratures(int currentDeviceId) => new DataContext(GetCn()).Devices.Find(currentDeviceId)?.Literatures.Select(x => x.id_lit).ToList();
        //public static List<int> GetDeviceMeasures(int currentDeviceId) => new DataContext(cn).Devices.Find(currentDeviceId)?.Measures.Select(x => x.id_measure).ToList();

        public static List<Device> GetDevices()
        {
            var laType = GetSelectedLaType();
            return new DataContext(GetCn()).Devices.Where(x => x.id_LA == laType).ToList();
        }
    }
}
