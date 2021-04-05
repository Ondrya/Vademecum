using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public enum AccessLevel
    {
        /// <summary>
        /// Администратр - доступ на чтение и запись всех данных
        /// </summary>
        [Description("Администратор")]
        Administrator = 1,
        /// <summary>
        /// Разработчик - чтение, запись, правка, ЗАКРЫТ ДОСТУП к персональным данным
        /// </summary>
        [Description("Разработчик")]
        Developer = 2,
        /// <summary>
        /// Студент - ...
        /// </summary>
        [Description("Студент")]
        Student = 3,
        /// <summary>
        /// Гость - только чтение
        /// </summary>
        [Description("Гость")]
        Guest = 4
    }
}
