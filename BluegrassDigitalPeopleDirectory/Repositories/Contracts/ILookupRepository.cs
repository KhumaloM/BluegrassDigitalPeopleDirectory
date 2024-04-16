using BluegrassDigitalPeopleDirectory.Models;

namespace BluegrassDigitalPeopleDirectory.Repositories.Contracts
{
    public interface ILookupRepository
    {
        IList<Country> GetAllCountries();
        IList<City> GetAllCities();
        IList<Gender> GetAllGenders();
    }
}
