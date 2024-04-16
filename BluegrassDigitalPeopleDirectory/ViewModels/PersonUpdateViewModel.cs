using BluegrassDigitalPeopleDirectory.Models;

namespace BluegrassDigitalPeopleDirectory.ViewModels
{
    public class PersonUpdateViewModel : Person
    {
        public IFormFile ProfilePictureFile { get; set; }
    }
}
