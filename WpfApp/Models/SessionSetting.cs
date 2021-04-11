using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    /// <summary>
    /// Параметры текущего сеанса работы
    /// </summary>
    public class SessionSetting
    {
        public User CurrentUser { get; set; }
        public LaType SelectedLaType { get; set; }
    }
}
