using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Task.Application.Entities;
using Task.Application.Features.Commands.CreateTask;

namespace Task.Application.AutoMapperProfiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<ToDoTask, CreateTaskCommand>().ReverseMap();
        }
    }
}