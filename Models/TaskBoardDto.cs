namespace KanbanBackend.Models
{
    public class TaskBoardDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ColumnId { get; set; }

        public int TotalSubtasks { get; set; }
        public int CompletedSubtasks {  get; set; }
    }
}
