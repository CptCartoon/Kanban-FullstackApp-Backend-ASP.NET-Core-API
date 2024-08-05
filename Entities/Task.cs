namespace KanbanBackend.Entities
{
    public class Task
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual List<Subtask> Subtasks  { get; set; }
        public int ColumnId { get; set; }
        public virtual Column Column { get; set; }
    }
}
