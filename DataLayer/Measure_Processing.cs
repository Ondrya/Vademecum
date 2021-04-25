namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ������� Measure_Processing (���������� ������ � ���, ��� ���������� ������ � �����������) ������������ ������ � ���������, � �������� ������� ���� NULL
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
        /// �������� ��������
        /// </summary>
        [StringLength(100)]
        public string name_measure_proc { get; set; }
        /// <summary>
        /// �������� ��������
        /// </summary>
        public string spec_measure_proc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Devices { get; set; }
    }
}
