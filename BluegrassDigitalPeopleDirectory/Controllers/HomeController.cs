using BluegrassDigitalPeopleDirectory.Models;
using BluegrassDigitalPeopleDirectory.Repositories.Contracts;
using BluegrassDigitalPeopleDirectory.Repositories.Implementations;
using BluegrassDigitalPeopleDirectory.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace BluegrassDigitalPeopleDirectory.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeopleDirectoryRepository _peopleDirectoryRepository;
        public HomeController(IPeopleDirectoryRepository peopleDirectoryRepository)
        {
            _peopleDirectoryRepository = peopleDirectoryRepository;
        }

        public IActionResult Index()
        {
            var directoryPeople = _peopleDirectoryRepository.GetPeopleDirectory(); //get all directory people
            List<SelectListItem> countriesList = new List<SelectListItem>();
            List<SelectListItem> citiesList = new List<SelectListItem>();
            foreach (var person in directoryPeople)
            {
                //these will be used to filter the grid results
                if (!countriesList.Exists(x => x.Value == person.Country.Id.ToString()))
                {
                    countriesList.Add(new SelectListItem { Text = person.Country.Name, Value = person.Country.Id.ToString() });
                }
                if (!citiesList.Exists(x => x.Value == person.City.Id.ToString()))
                {
                    citiesList.Add(new SelectListItem { Text = person.City.Name, Value = person.City.Id.ToString() });
                }
            }
            PeopleDirectorySearchViewModel viewModel = new PeopleDirectorySearchViewModel();
            viewModel.People = directoryPeople;
            viewModel.CitiesList = citiesList;
            viewModel.CountriesList = countriesList;
            return View(viewModel);
        }

        public IActionResult Persondetails(int id)
        {
            var person = _peopleDirectoryRepository.GetPersonById(id); //get person details by Id
            if (person != null)
            {
                var country = _peopleDirectoryRepository.GetPersonCountryById(person.CountryId);
                var city = _peopleDirectoryRepository.GetPersonCityById(person.CityId);
                var gender = _peopleDirectoryRepository.GetPersonGenderById(person.GenderId);
                var profilePicture = _peopleDirectoryRepository.GetPersonProfilePictureById(Convert.ToInt32(person.PersonProfilePictureId));

                string picture = string.Empty;
                string pictureFormat = string.Empty;
                if (profilePicture != null)
                {
                    picture = Convert.ToBase64String(profilePicture.ProfilePicture);
                    pictureFormat = profilePicture.PictureFormat;
                }

                PersonViewModel viewModel = new PersonViewModel { Person = person, Country = country.Name, City = city.Name, Gender = gender.Name, ProfilePicture = picture, ProfilePictureFormat= pictureFormat };
                return View(viewModel);
            }
            return View(new PersonViewModel());
        }

    }
}
