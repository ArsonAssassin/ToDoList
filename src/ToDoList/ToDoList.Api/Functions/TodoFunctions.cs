using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Api.Data;
using ToDoList.Api.Models;

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
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TodoItem))]
        public async Task<HttpResponseData> GetTodoItem([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/{id}")] HttpRequestData req)
        {
            throw new NotImplementedException();
        }

        [Function("GetAllTodoItems")]
        [OpenApiOperation("GetAllTodoItems")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<TodoItem>))]
        public async Task<HttpResponseData> GetAllTodoItems([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo")] HttpRequestData req)
        {
            throw new NotImplementedException();
        }

        [Function("QueryTodoItems")]
        [OpenApiOperation("QueryTodoItems")]
        [OpenApiParameter("isCompleted", In = ParameterLocation.Query, Required = false, Type = typeof(bool?))]
        [OpenApiParameter("createdAfterDate", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?))]
        [OpenApiParameter("createdBeforeDate", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?))]
        [OpenApiParameter("completedAfterDate", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?))]
        [OpenApiParameter("completedBeforeDate", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?))]
        [OpenApiParameter("titleContains", In = ParameterLocation.Query, Required = false, Type = typeof(string))]
        [OpenApiParameter("descriptionContains", In = ParameterLocation.Query, Required = false, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<TodoItem>))]
        public async Task<HttpResponseData> QueryTodoItems([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/query")] HttpRequestData req)
        {
            throw new NotImplementedException();
        }

        [Function("UpdateTodoItem")]
        [OpenApiOperation("UpdateTodoItem")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid))]
        [OpenApiRequestBody("application/json", typeof(TodoItem))]
        [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
        public async Task<HttpResponseData> UpdateTodoItem([HttpTrigger(AuthorizationLevel.Function, "put", Route = "todo/{id}")] HttpRequestData req)
        {
            throw new NotImplementedException();
        }

    }
}
