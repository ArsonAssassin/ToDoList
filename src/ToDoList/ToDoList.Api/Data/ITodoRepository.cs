using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Api.Models;

namespace ToDoList.Api.Data
{
    public interface ITodoRepository
    {
        /// <summary>
        /// Retrieves a specific todo item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the todo item.</param>
        /// <returns>The todo item if found, null otherwise.</returns>
        Task<TodoItem?> GetByIdAsync(Guid id);
        /// <summary>
        /// Retrieves all todo items.
        /// </summary>
        /// <returns>A collection of all todo items.</returns>
        Task<IEnumerable<TodoItem>> GetAllAsync();
        /// <summary>
        /// Creates a new todo item.
        /// </summary>
        /// <param name="todo">The todo item to create.</param>
        /// <returns>The unique identifier of the created todo item.</returns>
        Task<Guid> CreateAsync(TodoItem todo);
        /// <summary>
        /// Updates an existing todo item.
        /// </summary>
        /// <param name="todo">The todo item with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the todo item is not found.</exception>
        Task UpdateAsync(TodoItem todo);
        /// <summary>
        /// Queries todo items based on various filter criteria.
        /// </summary>
        /// <param name="isCompleted">Optional filter for completion status.</param>
        /// <param name="createdAfterDate">Optional filter for items created after this date.</param>
        /// <param name="createdBeforeDate">Optional filter for items created before this date.</param>
        /// <param name="completedAfterDate">Optional filter for items completed after this date.</param>
        /// <param name="completedBeforeDate">Optional filter for items completed before this date.</param>
        /// <param name="titleContains">Optional filter for title text containment.</param>
        /// <param name="descriptionContains">Optional filter for description text containment.</param>
        /// <returns>A filtered collection of todo items.</returns>
        Task<IEnumerable<TodoItem>> QueryAsync(bool? isCompleted, DateTime? createdAfterDate, DateTime? createdBeforeDate, DateTime? completedAfterDate,
                                               DateTime? completedBeforeDate, string? titleContains, string? descriptionContains);
    }
}
