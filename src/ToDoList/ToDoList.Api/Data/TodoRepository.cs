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
        private string _connectionString;

        public TodoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TodoConnectionString")
                ?? throw new ArgumentNullException("Database Connection string is missing");
        }
        public Task<Guid> CreateAsync(TodoItem todo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItem>> QueryAsync(bool? isCompleted, DateTime? createdAfterDate, DateTime? createdBeforeDate, DateTime? completedAfterDate, DateTime? completedBeforeDate, string titleContains, string descriptionContains)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TodoItem todo)
        {
            throw new NotImplementedException();
        }
    }
}
