using BluegrassDigitalPeopleDirectory.Data;
using BluegrassDigitalPeopleDirectory.Models;
using BluegrassDigitalPeopleDirectory.Repositories.Contracts;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using System;

namespace BluegrassDigitalPeopleDirectory.Repositories.Implementations
{
    public class PeopleDirectoryRepository : IPeopleDirectoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MailSettings _mailSettings;
        public PeopleDirectoryRepository(ApplicationDbContext dbContext, IOptions<MailSettings> mailSettings)
        {
            _dbContext = dbContext;
            _mailSettings = mailSettings.Value;
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

        public async Task<bool> UpdatePerson(Person person)
        {
            _dbContext.Update(person);
            _dbContext.ChangeTracker.DetectChanges();
            var getChanges = _dbContext.ChangeTracker.DebugView.LongView; //use change tracker to track the changes made and send email to mark@bluegrassdigital.com
            await SendPeopleupdateEmailAsync(getChanges);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool UpdatePersonProfilePicture(PersonProfilePicture personProfilePicture)
        {
            _dbContext.Update(personProfilePicture);
            return _dbContext.SaveChanges() > 0;
        }

        public async Task SendPeopleupdateEmailAsync(string mailBody)
        {
            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = "mark@bluegrassdigital.com";
            mailRequest.Subject = "An update has been made to the People Directory. Below are the changes";
            mailRequest.Body = mailBody;


            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public void DeletePerson(Person person)
        {
            _dbContext.People.Remove(person);
            _dbContext.SaveChanges();
    }
    }
}
