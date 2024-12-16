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

        public Task UpdateAsync(TodoItem todo)
        {
            throw new NotImplementedException();
        }
    }
}
