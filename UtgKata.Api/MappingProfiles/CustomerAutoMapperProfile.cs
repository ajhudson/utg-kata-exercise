// <copyright file="CustomerAutoMapperProfile.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.MappingProfiles
{
    using AutoMapper;
    using UtgKata.Api.Models;
    using UtgKata.Data.Models;

    /// <summary>
    /// The automapper profile for customers.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class CustomerAutoMapperProfile : Profile
    {
        /// <summary>Initializes a new instance of the <see cref="CustomerAutoMapperProfile" /> class.</summary>
        public CustomerAutoMapperProfile()
        {
            this.CreateMap<Customer, CustomerViewModel>().ReverseMap();
        }
    }
}
