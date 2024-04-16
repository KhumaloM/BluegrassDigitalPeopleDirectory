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
        public PeopleController(IPeopleDirectoryRepository peopleDirectoryRepository)
        {
            _peopleDirectoryRepository = peopleDirectoryRepository;
        }

        // GET: api/People
        [HttpGet]
        public IActionResult GetPeople()
        {
            var peopleDirectory = _peopleDirectoryRepository.GetPeopleDirectory();
            return Ok(peopleDirectory);
        }
    }
}
