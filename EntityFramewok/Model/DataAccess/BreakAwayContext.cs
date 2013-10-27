using System.Data.Entity;
using Model;

using System.Data.Entity.ModelConfiguration;

namespace DataAccess
{
    public class BreakAwayContext : DbContext
    {
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Person> People { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Destination>().Property(d => d.Name).IsRequired();
        //    modelBuilder.Entity<Destination>().Property(d => d.Description).HasMaxLength(500);
        //    modelBuilder.Entity<Destination>().Property(d => d.Photo).HasColumnType("image");
        //    modelBuilder.Entity<Lodging>().Property(l => l.Name).IsRequired().HasMaxLength(200);
        //    //If you are wondering where HasMinLength is, there is no Fluent configuration
        //    //for minimum length, as it is not a facet of a database column.
        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new DestinationConfiguration());
        //    modelBuilder.Configurations.Add(new LodgingConfiguration());
        //}

        //modelBuilder.Entity<Trip>().HasKey(t => t.Identifier);

        //modelBuilder.ComplexType<Address>();

        //modelBuilder.ComplexType<Address>().Property(p => p.StreetAddress).HasMaxLength(150);

        //modelBuilder.Entity<Destination>().HasMany(d => d.Lodgings).WithOptional(l => l.Destination);

        //modelBuilder.Entity<InternetSpecial>().HasRequired(s => s.Accommodation).WithMany(l => l.InternetSpecials).HasForeignKey(s => s.AccommodationId);


        //modelBuilder.Entity<Lodging>().HasOptional(l => l.PrimaryContact).WithMany(p => p.PrimaryContactFor);
        //modelBuilder.Entity< Lodging >().HasOptional(l => l.SecondaryContact).WithMany(p => p.SecondaryContactFor);

        //modelBuilder.Entity<PersonPhoto>().HasRequired(p => p.PhotoOf).WithOptional(p => p.Photo);

        //modelBuilder.Entity<PersonPhoto>().HasRequired(p => p.PhotoOf).WithRequiredDependent(p => p.Photo);

        //modelBuilder.Entity<Destination>().ToTable("Locations", "baga");

        //modelBuilder.ComplexType<Address>().Property(p => p.StreetAddress).HasColumnName("StreetAddress");
        //bu şekilde yaparsan sadece persondaki adres bilgileri için geçerli oluyor.
        //modelBuilder.Entity<Person>().Property(p => p.Address.StreetAddress).HasColumnName("StreetAddress");
    }

    
    public class AddressConfiguration :ComplexTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            Property(a => a.StreetAddress).HasMaxLength(150).HasColumnName("StreetAddress");
            Property(a => a.StreetAddress).HasColumnName("StreetAddress");
        }
    }

    public class TripConfiguration : EntityTypeConfiguration<Trip>
    {
        public TripConfiguration()
        {
            HasKey(t => t.Identifier);
            //this.HasKey(t => t.Identifier);
        }
    }


    public class DestinationConfiguration : EntityTypeConfiguration<Destination>
    {
        public DestinationConfiguration()
        {           
            //Property(d => d.Name).IsRequired();
            Property(d => d.Description).HasMaxLength(500);
            Property(d => d.Photo).HasColumnType("image");
            

            //HasMany(d => d.Lodgings).WithOptional(l => l.Destination);
            //HasMany(d => d.Lodgings).WithRequired(l => l.Destination);

            //Property(d => d.Name).IsRequired().HasColumnName("LocationName");
            //Property(d => d.DestinationId).HasColumnName("LocationID");

            Map(m =>
            {
                m.Properties(d => new { d.Name, d.Country, d.Description });
                m.ToTable("Locations");
            });
            Map(m =>
            {
                m.Properties(d => new { d.Photo });
                m.ToTable("LocationPhotos");
            });

        }
    }
    public class LodgingConfiguration : EntityTypeConfiguration<Lodging>
    {
        public LodgingConfiguration()
        {
            Property(l => l.Name).IsRequired().HasMaxLength(200);

            Property(l => l.MilesFromNearestAirport).HasPrecision(8, 1);


            //HasRequired(l => l.Destination).WithMany(d => d.Lodgings).WillCascadeOnDelete(false);

        }
    }

    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Property(p => p.SocialSecurityNumber).IsConcurrencyToken();
            Property(p => p.Address.StreetAddress).HasColumnName("StreetAddress");
        }
    }



//    The DatabaseGeneratedOption can be configured on a particular property. You can append
//this configuration to the HasKey you applied earlier, for example:
//modelBuilder.Entity<Trip>()
//.HasKey(t => t.Identifier)
//.Property(t => t.Identifier)
//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//Or you can create a separate statement:
//modelBuilder.Entity<Person>()
//.HasKey(p => t.SocialSecurityNumber);
//modelBuilder.Entity<Person>()
//.Property(p => p.SocialSecurityNumber)
//.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

//    With DbModelBuilder, you configure the Property like this:
//modelBuilder.Entity<Person>()
//.Property(p => p.RowVersion).IsRowVersion();
//Inside an EntityTypeConfiguration<T> class the configuration looks like:
//Property(p => p.RowVersion).IsRowVersion();
    
    //Property(l => l.Owner).IsUnicode(false);



//    public class ReservationConfiguration :
//EntityTypeConfiguration<Reservation>
//{
//}

//Notice that we’ve done nothing more than declare the class. There’s no code in it. This
//is enough to allow you to add the class to the DbModelBuilder configurations, which
//will ensure that Reservation is included in the model and maps to the database table,
//Reservations:

//modelBuilder.Configurations.Add(new ReservationConfiguration());

//Now that you’ve seen the conventional behavior that causes Code First to include a
//class in its model, let’s look at how to configure the model to exclude a class.


    //modelBuilder.Ignore<MyInMemoryOnlyClass>();


//    modelBuilder.Entity<Lodging>().Map(m =>
//{
//m.ToTable("Lodgings");
//}).Map<Resort>(m =>
//{
//m.ToTable("Resorts");
//});

}