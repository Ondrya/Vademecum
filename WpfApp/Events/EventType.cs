using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Events
{
    /// <summary>
    /// тип события в базе (CRUD)
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Создание
        /// </summary>
        Create,
        /// <summary>
        /// Обновление
        /// </summary>
        Update,
        /// <summary>
        /// Удаление
        /// </summary>
        Delete
    }
}
