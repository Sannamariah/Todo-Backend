using Entity;

namespace Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoContext _context;

    public TodoRepository(TodoContext context)
    {
        _context = context;
    }

    public async Task<Todo> AddTodo(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task<Todo> GetTodo(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

  
}