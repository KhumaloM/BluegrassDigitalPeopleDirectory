
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BluegrassDigitalPeopleDirectory.Models
{
    [Table("ProfilePictures")]
    public class PersonProfilePicture
    {
        //i have seperated this from the mail person model for perfomance reasons once the database grows
        [Key]
        public int Id { get; set; }
        [Required]
        public byte[] ProfilePicture { get; set; }

        [Required]
        public string PictureFormat { get; set; }
    }
}
