using System.ComponentModel;

namespace WpfApp.Models
{
    /// <summary>
    /// Тип летательного оппарата
    /// </summary>
    public enum LaType
    {
        /// <summary>
        /// Не определён
        /// </summary>
        [Description("Не определён")]
        Undefined = 0,
        /// <summary>
        /// Авиационный
        /// </summary>
        [Description("Авиационный")]
        Aero = 1,
        /// <summary>
        /// Космический
        /// </summary>
        [Description("Космический")]
        Space = 2,
        /// <summary>
        /// Универсальный
        /// </summary>
        [Description("Универсальный")]
        Universal = 3
    }
}
