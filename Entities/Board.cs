namespace KanbanBackend.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Column> Columns { get; set; }
    }
}
