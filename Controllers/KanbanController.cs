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

            if(board == null)
            {
                return NotFound();
            }

            return Ok(boardDto);
        }

        [HttpPost]
        [Route("AddBoard")]
        public ActionResult AddBoard([FromBody]AddBoardDto dto)
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
    }
}
