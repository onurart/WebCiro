using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCiro.Application.Features.AppFeatures.CompanyFeatures.Commands.CreateCompany;
using WebCiro.Domain.AppEntities;

namespace WebCiro.Persistance.Mapping
{
    public  class MappingProfile: Profile
    {
        public MappingProfile()
        {

            CreateMap<CreateCompanyRequest, Company>().ReverseMap();
        }

    }
}
