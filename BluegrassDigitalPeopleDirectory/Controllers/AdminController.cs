using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BluegrassDigitalPeopleDirectory.Data;
using BluegrassDigitalPeopleDirectory.Models;
using BluegrassDigitalPeopleDirectory.Repositories.Contracts;
using BluegrassDigitalPeopleDirectory.Repositories.Implementations;
using BluegrassDigitalPeopleDirectory.ViewModels;

namespace BluegrassDigitalPeopleDirectory.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPeopleDirectoryRepository _peopleDirectoryRepository;
        private readonly ILookupRepository _lookupRepository;
        public AdminController(IPeopleDirectoryRepository peopleDirectoryRepository, ILookupRepository lookupRepository)
        {
            _peopleDirectoryRepository = peopleDirectoryRepository;
            _lookupRepository = lookupRepository;
        }

        // GET: Admin
        public IActionResult Index()
        {
            var directoryPeople = _peopleDirectoryRepository.GetPeopleDirectory();
            return View(directoryPeople);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_lookupRepository.GetAllCities(), "Id", "Name");
            ViewData["CountryId"] = new SelectList(_lookupRepository.GetAllCountries(), "Id", "Name");
            ViewData["GenderId"] = new SelectList(_lookupRepository.GetAllGenders(), "Id", "Name");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Address,MobileNumber,EmailAddress,PersonProfilePictureId,GenderId,CountryId,CityId,ProfilePictureFile")] PersonUpdateViewModel personViewModel)
        {
            if (personViewModel.Name != string.Empty && personViewModel.Surname != string.Empty)
            {
                //first process profile pic, we are saving it in a different table so that it does not affect the perfomance as the app scales
                int? profilePicId = null;
                if (personViewModel.ProfilePictureFile != null)
                {
                    var memoryStream = new MemoryStream();
                    personViewModel.ProfilePictureFile.CopyTo(memoryStream);
                    PersonProfilePicture picture = new PersonProfilePicture();
                    picture.ProfilePicture = memoryStream.ToArray();
                    picture.PictureFormat = personViewModel.ProfilePictureFile.ContentType;
                    profilePicId = await _peopleDirectoryRepository.AddPersonProfilePictureAsync(picture);
                }
                var person = new Person();
                person.Name = personViewModel.Name;
                person.Surname = personViewModel.Surname;
                person.GenderId = personViewModel.GenderId;
                person.Address = personViewModel.Address;
                person.MobileNumber = personViewModel.MobileNumber;
                person.EmailAddress = personViewModel.EmailAddress;
                person.PersonProfilePictureId = profilePicId;
                person.CountryId = personViewModel.CountryId;
                person.CityId = personViewModel.CityId;
                await _peopleDirectoryRepository.AddPerson(person);
            }
            return RedirectToAction("Index", "Admin");
        }

        // GET: Admin/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _peopleDirectoryRepository.GetPersonById(Convert.ToInt32(id));
            if (person == null)
            {
                return NotFound();
            }
            string profilePic = string.Empty;
            string pictureFormat = string.Empty;
            if (person.PersonProfilePictureId != null)
            {
                var profilePicture = _peopleDirectoryRepository.GetPersonProfilePictureById(Convert.ToInt32(person.PersonProfilePictureId));
                if (profilePicture != null)
                {
                    profilePic = Convert.ToBase64String(profilePicture.ProfilePicture);
                    pictureFormat = profilePicture.PictureFormat;
                }
            }
            ViewData["CityId"] = new SelectList(_lookupRepository.GetAllCities(), "Id", "Name");
            ViewData["CountryId"] = new SelectList(_lookupRepository.GetAllCountries(), "Id", "Name");
            ViewData["GenderId"] = new SelectList(_lookupRepository.GetAllGenders(), "Id", "Name");
            ViewData["ProfilePicture"] = profilePic;
            ViewData["pictureFormat"] = pictureFormat;

            //map Person to PersonUpdateViewModel
            return View(person);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Address,MobileNumber,EmailAddress,PersonProfilePictureId,GenderId,CountryId,CityId")] Person person)
        //{
        //    if (id != person.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(person);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PersonExists(person.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", person.CityId);
        //    ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", person.CountryId);
        //    ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", person.GenderId);
        //    ViewData["PersonProfilePictureId"] = new SelectList(_context.ProfilePictures, "Id", "Id", person.PersonProfilePictureId);
        //    return View(person);
        //}

        //// GET: Admin/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var person = await _context.People
        //        .Include(p => p.City)
        //        .Include(p => p.Country)
        //        .Include(p => p.Gender)
        //        .Include(p => p.ProfilePicture)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(person);
        //}

        //// POST: Admin/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var person = await _context.People.FindAsync(id);
        //    if (person != null)
        //    {
        //        _context.People.Remove(person);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PersonExists(int id)
        //{
        //    return _context.People.Any(e => e.Id == id);
        //}
    }
}
