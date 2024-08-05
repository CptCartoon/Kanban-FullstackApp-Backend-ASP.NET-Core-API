namespace KanbanBackend.Entities
{
    public class Subtask
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public Boolean Completed { get; set; } = false;
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
