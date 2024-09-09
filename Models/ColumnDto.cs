using KanbanBackend.Entities;

namespace KanbanBackend.Models
{
    public class ColumnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TaskBoardDto> Tasks { get; set; }
        public int TotalTasks { get; set; }

    }
}
