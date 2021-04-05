namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Param.Producer")]
    public partial class Producer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producer()
        {
            Devices = new HashSet<Device>();
        }

        [Key]
        public int id_prod { get; set; }

        [StringLength(200)]
        public string name_prod { get; set; }

        [StringLength(300)]
        public string address_prod { get; set; }

        [StringLength(100)]
        public string phone_prod { get; set; }

        [StringLength(100)]
        public string web_prod { get; set; }

        [StringLength(100)]
        public string email_prod { get; set; }

        public string spec_prod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Devices { get; set; }
    }
}
