using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BluegrassDigitalPeopleDirectory.Models
{
    [Table("People")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string? Address{ get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public int? PersonProfilePictureId { get; set; }
        public int GenderId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }

        public virtual PersonProfilePicture? ProfilePicture { get; set; }
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual Gender Gender { get; set; }
    }
}
