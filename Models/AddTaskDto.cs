using KanbanBackend.Entities;

namespace KanbanBackend.Models
{
    public class AddTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AddSubtaskDto> Subtasks { get; set; }
        public int OrderIndex { get; set; }

    }
}
