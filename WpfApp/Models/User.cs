using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Helpers;

namespace WpfApp.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Уровень доступа
        /// </summary>
        public AccessLevel Level { get; set; }

        public override string ToString()
        {
            return $"User: {Login}, AccessLevel {Level.GetDescription()}";
        }
    }
}
