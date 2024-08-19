namespace KanbanBackend.Models
{
    public class AddSubtaskDto
    {
        public string Title { get; set; }
        public Boolean Completed { get; set; } = false;

    }
}
