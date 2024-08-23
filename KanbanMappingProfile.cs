﻿using AutoMapper;
using KanbanBackend.Entities;
using KanbanBackend.Models;
using Task = KanbanBackend.Entities.Task;

namespace KanbanBackend
{
    public class KanbanMappingProfile : Profile
    {
        public KanbanMappingProfile() {
            CreateMap<Board, BoardDto>();
            CreateMap<Board, SimpleBoardDto>();

            CreateMap<Column, ColumnDto>();
            CreateMap<Column, SimpleColumnDto>();

            CreateMap<Task, TaskDto>();
            CreateMap<Task, TaskViewDto>()
                    .ForMember(dest => dest.Columns, opt => opt.MapFrom(src => src.Column.Board.Columns));
            CreateMap<Subtask, SubtaskDto>();

            CreateMap<AddBoardDto, Board>();
            CreateMap<AddColumnDto, Column>();

            CreateMap<AddTaskDto, Task>()
                    .ForMember(dest => dest.Subtasks, opt => opt.Ignore());
            CreateMap<UpdateTaskDto, Task>()
                    .ForMember(dest => dest.Subtasks, opt => opt.Ignore());
            CreateMap<AddSubtaskDto, Subtask>();
        }
    }
}
