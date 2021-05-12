using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DataLayer;

namespace WpfApp.Helpers
{
    /// <summary>
    /// Получить коллекции-списки сущностей базы данных
    /// </summary>
    public class Dict
    {
        private static string cn = ((App)Application.Current).CurrentDb.ToString();

        /// <summary>
        /// Полуить список типов устройств
        /// </summary>
        /// <returns></returns>
        public static List<DeviceType> GetDeviceTypes() => new DataContext(cn).DeviceTypes.ToList();
        public static List<DataLayer.Type> GetTypes() => new DataContext(cn).Types.ToList();
        public static List<Function> GetFunctions() => new DataContext(cn).Functions.ToList();
        public static List<Sens_Element> GetSensElements() => new DataContext(cn).Sens_Element.ToList();
        public static List<Kind> GetKinds() => new DataContext(cn).Kinds.ToList();
        public static List<Control> GetControls() => new DataContext(cn).Controls.ToList();
        public static List<Measure_Processing> GetMeasureProcessing() => new DataContext(cn).Measure_Processing.ToList();
        public static List<BuiltTech> GetBuiltTeches() => new DataContext(cn).BuiltTeches.ToList();
        public static List<Measure> GetMeasures() => new DataContext(cn).Measures.ToList();
        public static List<Measure_Dims> GetMeasureDims() => new DataContext(cn).Measure_Dims.ToList();
        public static List<Enviroment> GetEnviroments() => new DataContext(cn).Enviroments.ToList();
        public static List<Producer> GetProdusers() => new DataContext(cn).Producers.ToList();
        public static List<Literature> GetLiterature() => new DataContext(cn).Literatures.ToList();

        public static List<int> GetDeviceEnviroments(int currentDeviceId) => new DataContext(cn).Devices.Find(currentDeviceId).Enviroments.Select(x => x.id_envi).ToList();
    }
}
