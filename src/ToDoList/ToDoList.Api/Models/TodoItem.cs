using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Api.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletionTime { get; set; }
    }
}
