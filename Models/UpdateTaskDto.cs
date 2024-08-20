namespace KanbanBackend.Models
{
    public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<SubtaskDto> Subtasks { get; set; }
        public int ColumnId { get; set; }
    }
}
