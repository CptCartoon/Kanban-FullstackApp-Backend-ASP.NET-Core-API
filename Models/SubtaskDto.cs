using KanbanBackend.Entities;
using Task = KanbanBackend.Entities.Task;


namespace KanbanBackend.Models
{
    public class SubtaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Boolean Completed { get; set; } = false;

    }
}
