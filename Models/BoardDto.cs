using KanbanBackend.Entities;

namespace KanbanBackend.Models
{
    public class BoardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ColumnDto> Columns { get; set; }
    }
}
