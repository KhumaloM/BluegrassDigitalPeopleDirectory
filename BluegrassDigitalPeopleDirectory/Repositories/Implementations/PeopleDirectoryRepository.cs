using BluegrassDigitalPeopleDirectory.Data;
using BluegrassDigitalPeopleDirectory.Models;
using BluegrassDigitalPeopleDirectory.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace BluegrassDigitalPeopleDirectory.Repositories.Implementations
{
    public class PeopleDirectoryRepository : IPeopleDirectoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public PeopleDirectoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Person> GetPeopleDirectory()
        {
            return _dbContext.People.Include(p => p.City).Include(p => p.Country).Include(p => p.Gender).Include(p => p.ProfilePicture);
        }

        public Person? GetPersonById(int Id)
        {
            return _dbContext.People.Where(x => x.Id == Id).FirstOrDefault();
        }

        public async Task<bool> AddPerson(Person person)
        {
            _dbContext.Add(person);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public City? GetPersonCityById(int Id)
        {
            return _dbContext.Cities.Where(x => x.Id == Id).FirstOrDefault();
        }

        public Country? GetPersonCountryById(int Id)
        {
            return _dbContext.Countries.Where(x => x.Id == Id).FirstOrDefault();
        }

        public Gender? GetPersonGenderById(int Id)
        {
            return _dbContext.Genders.Where(x => x.Id == Id).FirstOrDefault();
        }

        public PersonProfilePicture? GetPersonProfilePictureById(int Id)
        {
            return _dbContext.ProfilePictures.Where(x => x.Id == Id).FirstOrDefault();
        }

        public async Task<int> AddPersonProfilePictureAsync(PersonProfilePicture ProfilePic)
        {
            _dbContext.Add(ProfilePic);
            await _dbContext.SaveChangesAsync();
            return ProfilePic.Id;
        }

        public bool UpdatePerson(Person person)
        {
            _dbContext.Update(person);
            _dbContext.ChangeTracker.DetectChanges();
            var getChanges = _dbContext.ChangeTracker.DebugView.LongView; //use change tracker to track the changes made and send email to mark@bluegrassdigital.com

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdatePersonProfilePicture(PersonProfilePicture personProfilePicture)
        {
            _dbContext.Update(personProfilePicture);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
