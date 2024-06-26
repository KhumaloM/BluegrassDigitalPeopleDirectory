﻿using BluegrassDigitalPeopleDirectory.Models;

namespace BluegrassDigitalPeopleDirectory.Repositories.Contracts
{
    public interface IPeopleDirectoryRepository
    {
        IQueryable<Person> GetPeopleDirectory(); 
        Task<bool> AddPerson(Person person);
        void DeletePerson(Person person);
        Task<bool> UpdatePerson(Person person);
        bool UpdatePersonProfilePicture(PersonProfilePicture personProfilePicture);
        Person? GetPersonById(int Id);
        Country? GetPersonCountryById(int Id);
        City? GetPersonCityById(int Id);
        Gender? GetPersonGenderById(int Id);
        PersonProfilePicture? GetPersonProfilePictureById(int Id);
        Task<int> AddPersonProfilePictureAsync(PersonProfilePicture ProfilePic);
        Task SendPeopleupdateEmailAsync(string EmailBody);
    }
}
