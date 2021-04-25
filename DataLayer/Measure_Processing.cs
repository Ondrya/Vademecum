namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Таблица Measure_Processing (содержатся записи о том, что устройство делает с измерениями) используется только с приборами, у датчиков остаётся поле NULL
    /// </summary>
    [Table("Param.Measure_Processing")]
    public partial class Measure_Processing
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Measure_Processing()
        {
            Devices = new HashSet<Device>();
        }

        [Key]
        public int id_measure_proc { get; set; }

        /// <summary>
        /// название действия
        /// </summary>
        [StringLength(100)]
        public string name_measure_proc { get; set; }
        /// <summary>
        /// описание действия
        /// </summary>
        public string spec_measure_proc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Devices { get; set; }
    }
}
