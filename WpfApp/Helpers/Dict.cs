using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataLayer;

namespace WpfApp.Helpers
{
    public class Dict
    {
        /// <summary>
        /// Полуить список типов устройств
        /// </summary>
        /// <returns></returns>
        public static List<DeviceType> GetDevices()
        {
            var cn = ((App)Application.Current).CurrentDb.ToString();
            using (var context = new DataContext(cn))
            {
                return context.DeviceTypes.ToList();
            }
        }
    }
}
