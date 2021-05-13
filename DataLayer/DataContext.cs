using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataLayer
{
    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataLayer")
        {
        }

        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<LvlAccess> LvlAccesses { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<BuiltTech> BuiltTeches { get; set; }
        public virtual DbSet<Control> Controls { get; set; }
        public virtual DbSet<DeviceType> DeviceTypes { get; set; }
        public virtual DbSet<Enviroment> Enviroments { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Kind> Kinds { get; set; }
        public virtual DbSet<LAType> LATypes { get; set; }
        public virtual DbSet<Literature> Literatures { get; set; }
        public virtual DbSet<Measure> Measures { get; set; }
        public virtual DbSet<Measure_Dims> Measure_Dims { get; set; }
        public virtual DbSet<Measure_Processing> Measure_Processing { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Sens_Element> Sens_Element { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LvlAccess>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.LvlAccess)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Device>()
                .Property(e => e.dim_weight)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .Property(e => e.dim_unit)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.Enviroments)
                .WithMany(e => e.Devices)
                .Map(m => m.ToTable("DeviceEnvi", "Dev").MapLeftKey("id").MapRightKey("id_envi"));

            modelBuilder.Entity<Device>()
                .HasMany(e => e.Literatures)
                .WithMany(e => e.Devices)
                .Map(m => m.ToTable("DeviceLit", "Dev").MapLeftKey("id").MapRightKey("id_lit"));

            modelBuilder.Entity<Device>()
                .HasMany(e => e.Measures)
                .WithMany(e => e.Devices)
                .Map(m => m.ToTable("DeviceMeasure", "Dev").MapLeftKey("id").MapRightKey("id_measure"));

            modelBuilder.Entity<BuiltTech>()
                .Property(e => e.built_tech)
                .IsUnicode(false);

            modelBuilder.Entity<BuiltTech>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.BuiltTech)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DeviceType>()
                .Property(e => e.device_type)
                .IsUnicode(false);

            modelBuilder.Entity<DeviceType>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.DeviceType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DeviceType>()
                .HasMany(e => e.Types)
                .WithMany(e => e.DeviceTypes)
                .Map(m => m.ToTable("DevTypes", "Param").MapLeftKey("id_device").MapRightKey("id_type"));

            modelBuilder.Entity<Enviroment>()
                .Property(e => e.name_envi)
                .IsUnicode(false);

            modelBuilder.Entity<Function>()
                .Property(e => e.name_func)
                .IsUnicode(false);

            modelBuilder.Entity<Function>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.Function)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Kind>()
                .Property(e => e.name_kind)
                .IsUnicode(false);

            modelBuilder.Entity<Kind>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.Kind)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Kind>()
                .HasMany(e => e.Types)
                .WithMany(e => e.Kinds)
                .Map(m => m.ToTable("TypeKind", "Param").MapLeftKey("id_kind").MapRightKey("id_type"));

            modelBuilder.Entity<LAType>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.LAType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Literature>()
                .Property(e => e.lit_type)
                .IsUnicode(false);

            modelBuilder.Entity<Literature>()
                .Property(e => e.lit_name)
                .IsUnicode(false);

            modelBuilder.Entity<Literature>()
                .Property(e => e.lit_author)
                .IsUnicode(false);

            modelBuilder.Entity<Literature>()
                .Property(e => e.lit_publish)
                .IsUnicode(false);

            modelBuilder.Entity<Literature>()
                .Property(e => e.lit_web)
                .IsUnicode(false);

            modelBuilder.Entity<Measure>()
                .Property(e => e.name_measure)
                .IsUnicode(false);

            modelBuilder.Entity<Measure_Dims>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.Measure_Dims)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Measure_Processing>()
                .Property(e => e.name_measure_proc)
                .IsUnicode(false);

            modelBuilder.Entity<Measure_Processing>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.Measure_Processing)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Producer>()
                .Property(e => e.phone_prod)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.Producer)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Sens_Element>()
                .Property(e => e.name_se)
                .IsUnicode(false);

            modelBuilder.Entity<Sens_Element>()
                .Property(e => e.spec_se)
                .IsUnicode(false);

            modelBuilder.Entity<Sens_Element>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.Sens_Element)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Type>()
                .Property(e => e.name_type)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.Type)
                .WillCascadeOnDelete();
        }
    }
}
