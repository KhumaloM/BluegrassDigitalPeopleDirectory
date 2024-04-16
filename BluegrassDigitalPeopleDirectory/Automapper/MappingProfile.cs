using AutoMapper;
using BluegrassDigitalPeopleDirectory.Models;
using BluegrassDigitalPeopleDirectory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BluegrassDigitalPeopleDirectory.automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonUpdateViewModel>();
        }
    }
}
