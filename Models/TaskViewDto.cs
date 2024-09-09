namespace KanbanBackend.Models
{
    public class TaskViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<SubtaskDto> Subtasks { get; set; }
        public List<SimpleColumnDto> Columns { get; set; }
        public int TotalSubtasks { get; set; }
        public int CompletedSubtasks { get; set; }
        public int ColumnId { get; set; }
    }
}
