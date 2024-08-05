namespace KanbanBackend.Entities
{
    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Task> Tasks { get; set; }
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
    }
}
