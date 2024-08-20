using AutoMapper;
using KanbanBackend.Entities;
using KanbanBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Route("GetBoardNames")]
        public ActionResult<IEnumerable<SimpleBoardDto>> GetBoardsNames()
        {
            var boards = _dbContext
                .Boards
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

            var boardDto = _mapper.Map<BoardDto>(board);

            if (board == null)
            {
                return NotFound();
            }

            return Ok(boardDto);
        }

        [HttpPost]
        [Route("AddBoard")]
        public ActionResult AddBoard([FromBody] AddBoardDto dto)
        {
            var board = _mapper.Map<Board>(dto);
            _dbContext.Boards.Add(board);
            _dbContext.SaveChanges();

            return Created($"api/kanban/GetBoardById/{board.Id}", null);
        }

        [HttpPost]
        [Route("AddColumn/{boardId}")]
        public ActionResult AddBoard([FromBody] AddColumnDto dto, [FromRoute] int boardId)
        {

            var column = _mapper.Map<Column>(dto);

            column.BoardId = boardId;

            _dbContext.Columns.Add(column);
            _dbContext.SaveChanges();

            return Created($"api/kanban/GetBoardById/{column.BoardId}", null);
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
        public ActionResult EditTask([FromRoute] int id, [FromBody] BoardDto dto)
        {
            var board = _dbContext
                .Boards
                .Include(b => b.Columns)
                .FirstOrDefault(b => b.Id == id);

            if (board == null) return NotFound();

            board.Name = dto.Name;

            var updatedColumns = dto.Columns
                    .Select(c => c.Id)
                    .ToList();

            var columnsToRemove = board.Columns
                    .Where(c => !updatedColumns.Contains(c.Id))
                    .ToList();

            foreach (var column in columnsToRemove)
            {
                _dbContext.Columns.Remove(column);
            }

            foreach (var columnDto in dto.Columns)
            {
                var existingColumn = board.Columns
                    .FirstOrDefault(c => c.Id == columnDto.Id);

                if (existingColumn != null)
                {
                    existingColumn.Name = columnDto.Name;
                }
                else
                {
                    var newColumn = new Column
                    {
                        Name = columnDto.Name,
                        BoardId = board.Id
                    };
                    board.Columns.Add(newColumn);
                }
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

            var updatedSubtasks = dto.Subtasks
                    .Select(s => s.Id)
                    .ToList();

            var subtasksToRemove = task.Subtasks
                    .Where(s => !updatedSubtasks.Contains(s.Id))
                    .ToList();

            foreach (var subtask in subtasksToRemove)
            {
                _dbContext.Subtasks.Remove(subtask);
            }

            foreach (var subtaskDto in dto.Subtasks)
            {
                var existingSubtask = task.Subtasks
                    .FirstOrDefault(s => s.Id == subtaskDto.Id);

                if (existingSubtask != null)
                {
                    existingSubtask.Title = subtaskDto.Title;
                    existingSubtask.Completed = subtaskDto.Completed;
                }
                else
                {
                    var newSubtask = new Subtask
                    {
                        Title = subtaskDto.Title,
                        Completed = subtaskDto.Completed,
                        TaskId = task.Id 
                    };
                    task.Subtasks.Add(newSubtask);
                }
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
