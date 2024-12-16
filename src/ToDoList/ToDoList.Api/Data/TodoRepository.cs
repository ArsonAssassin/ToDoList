using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Api.Models;

namespace ToDoList.Api.Data
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(IConfiguration configuration, TodoDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAsync(TodoItem todo)
        {
            todo.Id = Guid.NewGuid();
            todo.CreationTime = DateTime.UtcNow;
            todo.IsCompleted = false;
            todo.CompletionTime = null;

            await _context.TodoItems.AddAsync(todo);
            await _context.SaveChangesAsync();

            return todo.Id;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems
                .OrderByDescending(t => t.CreationTime)
                .ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(Guid id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> QueryAsync(bool? isCompleted, DateTime? createdAfterDate, DateTime? createdBeforeDate, DateTime? completedAfterDate, DateTime? completedBeforeDate, string titleContains, string descriptionContains)
        {
            var query = _context.TodoItems.AsQueryable();

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            if (createdAfterDate.HasValue)
                query = query.Where(t => t.CreationTime >= createdAfterDate.Value);

            if (createdBeforeDate.HasValue)
                query = query.Where(t => t.CreationTime <= createdBeforeDate.Value);

            if (completedAfterDate.HasValue)
                query = query.Where(t => t.CompletionTime >= completedAfterDate.Value);

            if (completedBeforeDate.HasValue)
                query = query.Where(t => t.CompletionTime <= completedBeforeDate.Value);

            if (!string.IsNullOrEmpty(titleContains))
                query = query.Where(t => t.Title.Contains(titleContains));

            if (!string.IsNullOrEmpty(descriptionContains))
                query = query.Where(t => t.Description.Contains(descriptionContains));

            return await query
                .OrderByDescending(t => t.CreationTime)
                .ToListAsync();
        }

        public async Task UpdateAsync(TodoItem todo)
        {
            var existing = await _context.TodoItems.FindAsync(todo.Id)
                ?? throw new KeyNotFoundException($"Todo item with ID {todo.Id} not found");

            existing.Title = todo.Title;
            existing.Description = todo.Description;
            existing.IsCompleted = todo.IsCompleted;
            existing.CompletionTime = todo.CompletionTime;

            await _context.SaveChangesAsync();
        }    
    }
}
