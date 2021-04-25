namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// информация об измеряемых величинах
    /// </summary>
    [Table("Param.Measure")]
    public partial class Measure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Measure()
        {
            Devices = new HashSet<Device>();
        }

        [Key]
        public int id_measure { get; set; }

        /// <summary>
        /// название величины
        /// </summary>
        [Required]
        [StringLength(200)]
        public string name_measure { get; set; }
        /// <summary>
        /// описание
        /// </summary>
        public string spec_measure { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Devices { get; set; }
    }
}
