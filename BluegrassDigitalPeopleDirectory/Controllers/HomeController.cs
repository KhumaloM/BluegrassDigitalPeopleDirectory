using BluegrassDigitalPeopleDirectory.Models;
using BluegrassDigitalPeopleDirectory.Repositories.Contracts;
using BluegrassDigitalPeopleDirectory.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
            return View(directoryPeople);
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
