using BluegrassDigitalPeopleDirectory.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BluegrassDigitalPeopleDirectory.ViewModels
{
    public class PeopleDirectorySearchViewModel
    {
        public IQueryable<Person> People { get; set; }
        public List<SelectListItem> CountriesList { get; set; }
        public List<SelectListItem> CitiesList { get; set; }
    }
}
