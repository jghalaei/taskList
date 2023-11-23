using AutoMapper;
using Task.Application.Features.Commands.CreateTask;
using Task.Application.Features.Commands.EditTaskCommand;
using Task.Application.ViewModels;
using Task.Core.Entities;

namespace Task.Application.AutoMapperProfiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<TodoTask, CreateTaskCommand>().ReverseMap();
            CreateMap<TodoTask,EditTaskCommand>().ReverseMap();
            CreateMap<TodoTask, TaskViewModel>().ForMember("DueDate",opt=>opt.MapFrom(src=>src.DueDate.ToLocalTime())).ReverseMap();
            CreateMap<TaskHistory, TaskHistoryViewModel>().ForMember("Date", opt => opt.MapFrom(src => src.CreatedAt)).ReverseMap();
        }
    }
}