namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dev.Device")]
    public partial class Device
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Device()
        {
            Enviroments = new HashSet<Enviroment>();
            Literatures = new HashSet<Literature>();
            Measures = new HashSet<Measure>();
        }

        public int id { get; set; }

        [StringLength(150)]
        public string name { get; set; }

        public int? id_LA { get; set; }

        public int? id_device { get; set; }

        public int? id_kind { get; set; }

        public int? id_se { get; set; }

        public int? id_func { get; set; }

        public int? id_control { get; set; }

        public int? id_measure_proc { get; set; }

        public int? id_prod { get; set; }

        public float? min_measure { get; set; }

        public float? max_measure { get; set; }

        public int? id_dim_measure { get; set; }

        public float? error_measure { get; set; }

        public float? weight { get; set; }

        [StringLength(50)]
        public string dim_weight { get; set; }

        public float? width { get; set; }

        public float? length { get; set; }

        public float? height { get; set; }

        [StringLength(50)]
        public string dim_unit { get; set; }

        public int? id_built_tech { get; set; }

        public float? min_temp { get; set; }

        public float? max_temp { get; set; }

        [StringLength(50)]
        public string temp_unit { get; set; }

        [Column(TypeName = "image")]
        public byte[] view { get; set; }

        [Column(TypeName = "image")]
        public byte[] schema { get; set; }

        public virtual Control_Type Control_Type { get; set; }

        public virtual Device_Type Device_Type { get; set; }

        public virtual Measure_Processing Measure_Processing { get; set; }

        public virtual Producer Producer { get; set; }

        public virtual Sens_Element Sens_Element { get; set; }

        public virtual Built_Tech Built_Tech { get; set; }

        public virtual Function Function { get; set; }

        public virtual Kind Kind { get; set; }

        public virtual LA_Type LA_Type { get; set; }

        public virtual Measure_Dims Measure_Dims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enviroment> Enviroments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Literature> Literatures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Measure> Measures { get; set; }
    }
}
