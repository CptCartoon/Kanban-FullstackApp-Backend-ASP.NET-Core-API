namespace KanbanBackend.Models
{
    public class AddBoardDto
    {
        public string Name { get; set; }
        public List<AddColumnDto> Columns { get; set; }
    }
}
