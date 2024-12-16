﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Api.Models;

namespace ToDoList.Api.Data
{
    public interface ITodoRepository
    {
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<Guid> CreateAsync(TodoItem todo);
        Task UpdateAsync(TodoItem todo);
    }
}
