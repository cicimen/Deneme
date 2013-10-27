using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Model
{
    //public class Person
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    //    public int SocialSecurityNumber { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }

    //    [Timestamp]
    //    public byte[] RowVersion { get; set; }
    //}


    public class Person
    {
        public Person()
        {
            Address = new Address();
            Info = new PersonalInfo
            {
                Weight = new Measurement(),
                Height = new Measurement()
            };
        }

        public int PersonId { get; set; }
        [ConcurrencyCheck]
        public int SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public PersonalInfo Info { get; set; }

        public List<Lodging> PrimaryContactFor { get; set; }
        public List<Lodging> SecondaryContactFor { get; set; }

        public PersonPhoto Photo { get; set; }

        public List<Reservation> Reservations { get; set; }
    }

    [ComplexType]
    public class Address
    {
        public int AddressId { get; set; }
        [MaxLength(150)]
        [Column("StreetAddress")]
        public string StreetAddress { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Column("State")]
        public string State { get; set; }
        [Column("ZipCode")]
        public string ZipCode { get; set; }
    }

    [ComplexType]
    public class PersonalInfo
    {
        public Measurement Weight { get; set; }
        public Measurement Height { get; set; }
        public string DietryRestrictions { get; set; }
    }
    public class Measurement
    {
        public decimal Reading { get; set; }
        public string Units { get; set; }
    }

}
