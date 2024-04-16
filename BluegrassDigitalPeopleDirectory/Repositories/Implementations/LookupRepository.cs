using BluegrassDigitalPeopleDirectory.Data;
using BluegrassDigitalPeopleDirectory.Models;
using BluegrassDigitalPeopleDirectory.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BluegrassDigitalPeopleDirectory.Repositories.Implementations
{
    public class LookupRepository: ILookupRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LookupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IList<Country> GetAllCountries()
        {
            return _dbContext.Countries.ToList();
        }

        public IList<City> GetAllCities()
        {
            return _dbContext.Cities.ToList();
        }

        public IList<Gender> GetAllGenders()
        {
            return _dbContext.Genders.ToList();
        }

        public IList<City> GetCitiesByCountryId(int CountryId)
        {
            return _dbContext.Cities.Where(x => x.CountryId == CountryId).ToList();
        }
    }
}
