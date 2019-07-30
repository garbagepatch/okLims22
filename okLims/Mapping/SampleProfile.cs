using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Model;
using okLims.Models;

namespace okLims.Mapping
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<RequestModel, Request>()
                .ForMember(d => d.Author, o => o.MapFrom(s => new Employee {Id = s.AuthorId, Name = s.AuthorName}))
                .ForMember(d => d.Manager, o => o.MapFrom(s => s.ManagerId.HasValue ? new Employee {Id = s.ManagerId.Value, Name = s.ManagerName} : null))
                ;
        }
    }
}
