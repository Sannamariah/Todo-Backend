﻿using Entity;

namespace Infrastructure.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> AddTodo(Todo todo);
        Task<Todo> GetTodo(int id);
        
    }
}
