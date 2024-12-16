using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
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

        [Function("GetTodoItem")]
        [OpenApiOperation("GetTodoItem")]
        public async Task<HttpResponseData> GetTodoItem([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/{id}")] HttpRequestData req)
        {
            throw new NotImplementedException();
        }

        [Function("GetAllTodoItems")]
        [OpenApiOperation("GetAllTodoItems")]
        public async Task<HttpResponseData> GetAllTodoItems([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo")] HttpRequestData req)
        {
            throw new NotImplementedException();
        }

        [Function("QueryTodoItems")]
        [OpenApiOperation("QueryTodoItems")]
        public async Task<HttpResponseData> QueryTodoItems([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/query")] HttpRequestData req)
        {
            throw new NotImplementedException();
        }

        [Function("UpdateTodoItem")]
        [OpenApiOperation("UpdateTodoItem")]
        public async Task<HttpResponseData> UpdateTodoItem([HttpTrigger(AuthorizationLevel.Function, "put", Route = "todo/{id}")] HttpRequestData req)
        {
            throw new NotImplementedException();
        }

    }
}
