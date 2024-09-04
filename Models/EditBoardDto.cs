namespace KanbanBackend.Models
{
    public class EditBoardDto
    {
        public int number { get; set; }
        public string Name { get; set; }
        public List<EditColumnDto> Columns { get; set; }
    }
}
