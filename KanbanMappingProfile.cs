using AutoMapper;
using KanbanBackend.Entities;
using KanbanBackend.Models;
using System.Linq;
using Task = KanbanBackend.Entities.Task;

namespace KanbanBackend
{
    public class KanbanMappingProfile : Profile
    {
        public KanbanMappingProfile() {
            CreateMap<Board, BoardDto>();
            CreateMap<Board, SimpleBoardDto>();

            CreateMap<Column, ColumnDto>()
                    .ForMember(dest => dest.TotalTasks, opt => opt.MapFrom(src => src.Tasks.Count()));

            CreateMap<Column, SimpleColumnDto>();
            CreateMap<Column, BoardColumnDto>();

            CreateMap<Task, TaskDto>();
            CreateMap<Task, TaskBoardDto>()
                    .ForMember(dest => dest.TotalSubtasks, opt => opt.MapFrom(src => src.Subtasks.Count))
                    .ForMember(dest => dest.CompletedSubtasks, opt => opt.MapFrom(src => src.Subtasks.Count(s => s.Completed)));

            CreateMap<Task, TaskViewDto>()
                    .ForMember(dest => dest.Columns, opt => opt.MapFrom(src => src.Column.Board.Columns.OrderBy(c => c.Id)))
                    .ForMember(dest => dest.TotalSubtasks, opt => opt.MapFrom(src => src.Subtasks.Count))
                    .ForMember(dest => dest.CompletedSubtasks, opt => opt.MapFrom(src => src.Subtasks.Count(s => s.Completed)));

            CreateMap<Subtask, SubtaskDto>();

            CreateMap<AddBoardDto, Board>()
                    .ForMember(dest => dest.Columns, opt => opt.Ignore());
            CreateMap<EditBoardDto, Board>()
                    .ForMember(dest => dest.Columns, opt => opt.Ignore());
            CreateMap<AddColumnDto, Column>();
            CreateMap<EditColumnDto, Column>();

            CreateMap<AddTaskDto, Task>()
                    .ForMember(dest => dest.Subtasks, opt => opt.Ignore());
            CreateMap<UpdateTaskDto, Task>()
                    .ForMember(dest => dest.Subtasks, opt => opt.Ignore());
            CreateMap<AddSubtaskDto, Subtask>();
        }
    }
}
