using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Api.Data;

namespace ToDoList.Api.Functions
{
    public class TodoFunctions
    {
        private ITodoRepository _todoRepository;

        public TodoFunctions(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
    }
}
