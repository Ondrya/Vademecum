namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// возможные единицы измерения, чтобы не возникало конфликтов при написании одной величины разными способами, к примеру градусы и °C, с другими также
    /// </summary>
    [Table("Param.Measure_Dims")]
    public partial class Measure_Dims
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Measure_Dims()
        {
            Devices = new HashSet<Device>();
        }

        [Key]
        public int id_dim_measure { get; set; }
        /// <summary>
        /// обозначение единицы измерения
        /// </summary>
        [StringLength(50)]
        public string dim_measure { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Devices { get; set; }
    }
}
