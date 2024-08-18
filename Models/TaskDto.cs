using KanbanBackend.Entities;

namespace KanbanBackend.Models
{
    public class TaskDto
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<SubtaskDto> Subtasks { get; set; }
        public int ColumnId { get; set; }
    }
}
