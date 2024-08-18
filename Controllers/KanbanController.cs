using AutoMapper;
using KanbanBackend.Entities;
using KanbanBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
