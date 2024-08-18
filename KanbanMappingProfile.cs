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
            CreateMap<Task, TaskDto>();
            CreateMap<Subtask, SubtaskDto>();
        }
    }
}