using BluegrassDigitalPeopleDirectory.Models;

namespace BluegrassDigitalPeopleDirectory.Repositories.Contracts
{
    public interface IPeopleDirectoryRepository
    {
        IQueryable<Person> GetPeopleDirectory(); 
        Task<bool> AddPerson(Person person);
        //void RemovePerson(Person person);
        //void UpdatePerson(Person person);
        Person? GetPersonById(int Id);
        Country? GetPersonCountryById(int Id);
        City? GetPersonCityById(int Id);
        Gender? GetPersonGenderById(int Id);
        PersonProfilePicture? GetPersonProfilePictureById(int Id);
        Task<int> AddPersonProfilePictureAsync(PersonProfilePicture ProfilePic);
    }
}
