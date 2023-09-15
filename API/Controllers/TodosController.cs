using Entity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }


    }

    //[HttpGet]
    //public async Task<AcceptedAtActionResult<Todo> GetTodo(int id)>
    //{
    //        var todo = await _todoRepository.GetTodo(id);
    //        if (todo == null)
    //        {
    //        return NotFoundObjectResult();
    //        }

    //}

    //[HttpPost]
    //public async Task<ActionResult<Todo> PostTodo(TodoDto todo)>
    //{
    //    var todo = _mapper.Map<Todo>(TodoDto);
    //}
}
