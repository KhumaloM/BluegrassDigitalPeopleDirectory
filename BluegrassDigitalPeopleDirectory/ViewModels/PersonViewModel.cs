using BluegrassDigitalPeopleDirectory.Models;

namespace BluegrassDigitalPeopleDirectory.ViewModels
{
    public class PersonViewModel
    {
        public Person Person { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfilePictureFormat { get; set; }
    }
}
