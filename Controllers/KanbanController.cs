using AutoMapper;
using KanbanBackend.Entities;
using KanbanBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Task = KanbanBackend.Entities.Task;

namespace KanbanBackend.Controllers
{

    [Route("api/kanban")]
    public class KanbanController : ControllerBase
    {
        private readonly KanbanDbContext _dbContext;
        private readonly IMapper _mapper;
        public KanbanController(KanbanDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("GetBoardsNames")]
        public ActionResult<IEnumerable<SimpleBoardDto>> GetBoardsNames()
        {
            var boards = _dbContext
                .Boards
                .Include(b => b.Columns)
                .ToList();

            var simpleBoardsDto = _mapper.Map<List<SimpleBoardDto>>(boards);

            return Ok(simpleBoardsDto);
        }

        [HttpGet]
        [Route("GetAllBoards")]
        public ActionResult<IEnumerable<BoardDto>> GetAllBoards()
        {
            var boards = _dbContext
                .Boards
                .Include(b => b.Columns)
                    .ThenInclude(c => c.Tasks)
                        .ThenInclude(t => t.Subtasks)
                .ToList();

            var boardsDto = _mapper.Map<List<BoardDto>>(boards);

            return Ok(boardsDto);
        }

        [HttpGet]
        [Route("GetBoardById/{id}")]
        public ActionResult<BoardDto> GetBoardById([FromRoute] int id)
        {
            var board = _dbContext
                .Boards
                .Include(b => b.Columns)
                    .ThenInclude(c => c.Tasks)
                        .ThenInclude(t => t.Subtasks)
                .FirstOrDefault(b => b.Id == id);

            if(board == null)
            {
                return NotFound();
            }

            var boardDto = _mapper.Map<BoardDto>(board);

            return Ok(boardDto);
        }

        [HttpGet]
        [Route("GetColumnsByBoard/{id}")]
        public ActionResult<IEnumerable<BoardColumnDto>> GetColumnsByBoard([FromRoute] int id)
        {
            var columns = _dbContext.Columns
                .Where(c => c.BoardId == id)
                .ToList();

            var columnDto = _mapper.Map<List<BoardColumnDto>>(columns);

            if (columns == null)
            {
                return NotFound();
            }

            return Ok(columnDto);
        }

        [HttpGet]
        [Route("GetTaskViewById/{id}")]
        public ActionResult<IEnumerable<TaskViewDto>> GetTaskViewById([FromRoute] int id)
        {
            var taskView = _dbContext.Tasks
                     .Include(t => t.Subtasks)
                     .Include(t => t.Column)
                     .ThenInclude(c => c.Board)
                            .ThenInclude(b => b.Columns)
                     .FirstOrDefault(t => t.Id == id);

            var taskViewDto = _mapper.Map<TaskViewDto>(taskView);

            if (taskView == null)
            {
                return NotFound();
            }

            return Ok(taskViewDto);
        }

        [HttpPost]
        [Route("AddBoard")]
        public ActionResult AddBoard([FromBody] AddBoardDto dto)
        {
            var board = _mapper.Map<Board>(dto);

            _dbContext.Boards.Add(board);
            _dbContext.SaveChanges();

            if(dto.Columns != null && dto.Columns.Any() ) {

                foreach(var columnDto in dto.Columns)
                {
                    var column = _mapper.Map<Column>(columnDto);
                    column.BoardId = board.Id;
                    _dbContext.Columns.Add(column);
                }
                _dbContext.SaveChanges();
            }

            return Created($"api/kanban/GetBoardById/{board.Id}", null);
        }

        [HttpPost]
        [Route("AddColumns/{boardId}")]
        public ActionResult AddColumns([FromBody] List<AddColumnDto> dto, [FromRoute] int boardId)
        {

       

            foreach(var columnDto in dto)
            {
                var column = _mapper.Map<Column>(columnDto);

                column.BoardId = boardId;

                _dbContext.Columns.Add(column);
            }
            _dbContext.SaveChanges();


            return Created($"Created", null);
        }

        [HttpPost]
        [Route("AddTask/{columnId}")]
        public ActionResult AddBoard([FromBody] AddTaskDto dto, [FromRoute] int columnId)
        {

            var task = _mapper.Map<Task>(dto);

            task.ColumnId = columnId;

            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();

            if (dto.Subtasks != null && dto.Subtasks.Any())
            {
                foreach (var subtaskDto in dto.Subtasks)
                {
                    var subtask = _mapper.Map<Subtask>(subtaskDto);
                    subtask.TaskId = task.Id;
                    _dbContext.Subtasks.Add(subtask);
                }
                _dbContext.SaveChanges();
            }

            return Created($"Added", null);
        }


        [HttpPut]
        [Route("EditBoard/{id}")]
        public ActionResult EditBoard([FromRoute] int id, [FromBody] EditBoardDto dto)
        {
            var board = _dbContext
                .Boards
                .Include(b => b.Columns)
                .FirstOrDefault(b => b.Id == id);

            if (board == null) return NotFound();

            board.Name = dto.Name;

            var existingColumns = board.Columns
                    .ToList();

            var updatedColumns = dto.Columns
                    .ToList();

            var columnsToRemove = board.Columns
                    .Where(c => !updatedColumns.Any(item => item.Id == c.Id))
                    .ToList();

            var columnsToAdd = dto.Columns
                    .Where(c => !existingColumns.Any(item => item.Id == c.Id))
                    .ToList();

            var columnsToEdit = dto.Columns
                    .Where(c => existingColumns.Any(item => item.Id == c.Id))
                    .ToList();


            foreach (var columntEdit in columnsToEdit)
            {
                var existingColumn = board.Columns
                    .FirstOrDefault(c => c.Id == columntEdit.Id);

                if(existingColumn != null)
                {
                    existingColumn.Name = columntEdit.Name;
                }
            }

            foreach (var columnAdd in columnsToAdd)
            {   
                    var newColumn = new Column
                    {
                        Name = columnAdd.Name,
                        BoardId = id
                    };
                    board.Columns.Add(newColumn);
            }

            foreach (var column in columnsToRemove)
            {
                _dbContext.Columns.Remove(column);
            }

            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("EditTask/{id}")]
        public ActionResult EditTask([FromRoute] int id, [FromBody] UpdateTaskDto dto)
        {
            var task = _dbContext
                .Tasks
                .Include(t => t.Subtasks)
                .FirstOrDefault(t => t.Id == id);

            if (task == null) return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.ColumnId = dto.ColumnId;

            var existingSubtasks = task.Subtasks.ToList();

            var updatedSubtasks = dto.Subtasks.ToList();

            var subtasksToRemove = task.Subtasks
                    .Where(s => !updatedSubtasks.Any(item => item.Id == s.Id))
                    .ToList();

            var subtasksToAdd = dto.Subtasks
                    .Where(s => !existingSubtasks.Any(item => item.Id == s.Id))
                    .ToList();

            var subtasksToEdit = dto.Subtasks
                    .Where(s => existingSubtasks.Any(item => item.Id == s.Id))
                    .ToList();  

            foreach ( var subtaskEdit in subtasksToEdit )
            {
                var existingSubtask = task.Subtasks.FirstOrDefault(s => s.Id == subtaskEdit.Id);

                if (existingSubtask != null)
                {
                    existingSubtask.Title = subtaskEdit.Title;
                }
            }

            foreach(var subtaskAdd in subtasksToAdd)
            {
                var newSubtask = new Subtask
                {
                    Title = subtaskAdd.Title,
                    Completed = subtaskAdd.Completed,
                    TaskId = task.Id,
                };

                task.Subtasks.Add(newSubtask);
            }

            foreach (var subtask in subtasksToRemove)
            {
                _dbContext.Subtasks.Remove(subtask);
            }

            _dbContext.SaveChanges();

            return Ok();
         }

        [HttpPatch]
        [Route("ChangeTaskColumn/{id}")]
        public ActionResult ChangeTaskColumn([FromRoute] int id, [FromBody] ChangeTaskColumnDto dto)
        {
            var task = _dbContext
                .Tasks
                .FirstOrDefault(s => s.Id == id);

            if (task != null)
            {
                task.ColumnId = dto.ColumnId;
            }

            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPatch]
        [Route("ChangeSubtaskStatus/{id}")]
        public ActionResult ChangeSubtaskStatus([FromRoute] int id, [FromBody] ChangeSubtaskStatus dto)
        {
            var subtask = _dbContext
                .Subtasks
                .FirstOrDefault(s => s.Id == id);

            if (subtask != null)
            {
                subtask.Completed = dto.Completed;
            }

            _dbContext.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        [Route("DeleteBoard/{id}")]
        public ActionResult DeleteBoard([FromRoute] int id)
        {
            var board = _dbContext
                .Boards
                .FirstOrDefault(b => b.Id == id);

            if (board == null) return NotFound();

            _dbContext.Boards.Remove(board);
            _dbContext.SaveChanges();

            return NoContent();
        }



        [HttpDelete]
        [Route("DeleteTask/{id}")]
        public ActionResult DeleteTask([FromRoute] int id)
        {
            var task = _dbContext
                .Tasks
                .FirstOrDefault(t => t.Id == id);

            if (task == null) return NotFound();

            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
