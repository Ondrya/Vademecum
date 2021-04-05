using System.Data.Entity;

namespace DataLayer
{
    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext1")
        {
        }

        public DataContext(string cn)
            : base(cn)
        {
        }

        public virtual DbSet<Lvl_Access> Lvl_Access { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Built_Tech> Built_Tech { get; set; }
        public virtual DbSet<Control_Type> Control_Type { get; set; }
        public virtual DbSet<Device_Type> Device_Type { get; set; }
        public virtual DbSet<Enviroment> Enviroments { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Kind> Kinds { get; set; }
        public virtual DbSet<LA_Type> LA_Type { get; set; }
        public virtual DbSet<Literature> Literatures { get; set; }
        public virtual DbSet<Measure> Measures { get; set; }
        public virtual DbSet<Measure_Dims> Measure_Dims { get; set; }
        public virtual DbSet<Measure_Processing> Measure_Processing { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Sens_Element> Sens_Element { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lvl_Access>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Lvl_Access1)
                .HasForeignKey(e => e.lvl_access)
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
                .Map(m => m.ToTable("Device_Envi", "Dev").MapLeftKey("id").MapRightKey("id_envi"));

            modelBuilder.Entity<Device>()
                .HasMany(e => e.Literatures)
                .WithMany(e => e.Devices)
                .Map(m => m.ToTable("Device_Lit", "Dev").MapLeftKey("id").MapRightKey("id_lit"));

            modelBuilder.Entity<Device>()
                .HasMany(e => e.Measures)
                .WithMany(e => e.Devices)
                .Map(m => m.ToTable("Device_Measure", "Dev").MapLeftKey("id").MapRightKey("id_measure"));

            modelBuilder.Entity<Built_Tech>()
                .Property(e => e.built_tech1)
                .IsUnicode(false);

            modelBuilder.Entity<Built_Tech>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.Built_Tech)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Device_Type>()
                .Property(e => e.device_type1)
                .IsUnicode(false);

            modelBuilder.Entity<Device_Type>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.Device_Type)
                .WillCascadeOnDelete();

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
                .Map(m => m.ToTable("Type_Kind", "Param").MapLeftKey("id_kind").MapRightKey("id_type"));

            modelBuilder.Entity<LA_Type>()
                .HasMany(e => e.Devices)
                .WithOptional(e => e.LA_Type)
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
        }
    }
}
