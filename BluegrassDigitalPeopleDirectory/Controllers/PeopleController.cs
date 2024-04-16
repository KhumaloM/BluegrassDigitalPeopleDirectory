using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BluegrassDigitalPeopleDirectory.Data;
using BluegrassDigitalPeopleDirectory.Models;
using BluegrassDigitalPeopleDirectory.Repositories.Contracts;

namespace BluegrassDigitalPeopleDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleDirectoryRepository _peopleDirectoryRepository;
        private readonly ILookupRepository _lookupRepository;
        public PeopleController(IPeopleDirectoryRepository peopleDirectoryRepository, ILookupRepository lookupRepository)
        {
            _peopleDirectoryRepository = peopleDirectoryRepository;
            _lookupRepository = lookupRepository;
        }

        // GET: api/People/GetPeople
        [HttpGet]
        [Route("GetPeople")]
        public IActionResult GetPeople()
        {
            var peopleDirectory = _peopleDirectoryRepository.GetPeopleDirectory();
            return Ok(peopleDirectory);
        }

        // GET: api/People/GetCitiesByCountryId
        [HttpGet]
        [Route("GetCitiesByCountryId")]
        public IActionResult GetCitiesByCountryId(int CountryId)
        {
            var citiesByCountry = _lookupRepository.GetCitiesByCountryId(CountryId);
            return Ok(citiesByCountry);
        }
    }
}
