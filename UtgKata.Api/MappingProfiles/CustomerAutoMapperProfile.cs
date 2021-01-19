using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtgKata.Api.Models;
using UtgKata.Data.Models;

namespace UtgKata.Api.MappingProfiles
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            this.CreateMap<Customer, CustomerViewModel>().ReverseMap();
        }
    }
}
