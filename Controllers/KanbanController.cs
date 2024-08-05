using KanbanBackend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers
{

    [Route("api/kanban")]
    public class KanbanController : ControllerBase
    {
        private readonly KanbanDbContext _dbContext;
        public KanbanController(KanbanDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Board>> GetAll()
        {
            var boards = _dbContext
                .Boards
                .ToList();

            return Ok(boards);
        }
    }
}
