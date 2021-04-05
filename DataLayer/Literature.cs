namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Param.Literature")]
    public partial class Literature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Literature()
        {
            Devices = new HashSet<Device>();
        }

        [Key]
        public int id_lit { get; set; }

        [StringLength(100)]
        public string lit_type { get; set; }

        [StringLength(500)]
        public string lit_name { get; set; }

        [StringLength(200)]
        public string lit_author { get; set; }

        public int? lit_date { get; set; }

        [StringLength(200)]
        public string lit_publish { get; set; }

        public string lit_web { get; set; }

        public byte[] lit_file { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Devices { get; set; }
    }
}
