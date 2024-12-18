﻿using Azure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
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
        /// <summary>
        /// Creates a new todo item.
        /// </summary>
        /// <param name="req">The HTTP request containing the todo item to create.</param>
        /// <returns>
        /// 201 Created with the newly created todo item and its location
        /// 400 Bad Request if the request body is invalid
        /// 500 Internal Server Error if an unexpected error occurs
        /// </returns>
        /// <remarks>
        /// The request body should contain:
        /// - title (optional): The title of the todo item
        /// - description (optional): A detailed description of the todo item
        /// 
        /// CreationTime will be automatically set to the current UTC time
        /// IsCompleted will be set to false by default
        /// </remarks>
        [Function("CreateTodoItem")]
        [OpenApiOperation("CreateTodoItem", tags: new[] { "Todo" }, Summary = "Create a new todo item", Description = "Creates a new Todo item in the database using the information provided")]
        [OpenApiRequestBody("application/json", typeof(TodoItem))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Guid))]
        [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
        [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseData> CreateTodoItem([HttpTrigger(AuthorizationLevel.Function, "post", Route = "todo")] HttpRequestData req)
        {
            var response = req.CreateResponse();
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var newTodo = JsonSerializer.Deserialize<TodoItem>(requestBody);

                if (newTodo == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    await response.WriteAsJsonAsync(new { error = "Invalid request body" });
                    return response;
                }
                newTodo.Id = Guid.NewGuid();

                await _todoRepository.CreateAsync(newTodo);

                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(newTodo.Id);
                return response;
            }
            catch
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
        /// <summary>
        /// Gets a specific todo item by its unique identifier.
        /// </summary>
        /// <param name="req">The HTTP request.</param>
        /// <param name="id">The unique identifier of the todo item.</param>
        /// <returns>
        /// 200 OK with the todo item if found
        /// 404 Not Found if the item doesn't exist
        /// 400 Bad Request if the ID format is invalid
        /// 500 Internal Server Error if an unexpected error occurs
        /// </returns>
        [Function("GetTodoItem")]
        [OpenApiOperation("GetTodoItem", tags: new[] { "Todo" }, Summary = "Get a specific todo item", Description = "Retrieves a todo item by its unique identifier")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TodoItem))]
        [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
        [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
        [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseData> GetTodoItem([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/{id}")] HttpRequestData req, string id)
        {
            var response = req.CreateResponse();
            try
            {
                if (!Guid.TryParse(id, out Guid todoId))
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    await response.WriteAsJsonAsync(new { error = "Invalid ID format" });
                    return response;
                }
                var todo = await _todoRepository.GetByIdAsync(todoId);
                if (todo == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(todo);
                return response;
            }
            catch
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
        /// <summary>
        /// Gets all todo items without any filtering.
        /// </summary>
        /// <param name="req">The HTTP request.</param>
        /// <returns>200 OK with a list of all todo items</returns>
        [Function("GetAllTodoItems")]
        [OpenApiOperation("GetAllTodoItems", tags: new[] { "Todo" }, Summary = "Get all todo items", Description = "Retrieves all todo items in the database")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<TodoItem>))]
        [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseData> GetAllTodoItems([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo")] HttpRequestData req)
        {
            var response = req.CreateResponse();

            try
            {
                var todos = await _todoRepository.GetAllAsync();
                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(todos);
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
        /// <summary>
        /// Queries todo items using various filter criteria.
        /// </summary>
        /// <param name="req">The HTTP request.</param>
        /// <returns>200 OK with the filtered list of todo items</returns>
        /// <remarks>
        /// Supported query parameters:
        /// - isCompleted: Filter by completion status
        /// - createdAfterDate: Filter items created after this date
        /// - createdBeforeDate: Filter items created before this date
        /// - completedAfterDate: Filter items completed after this date
        /// - completedBeforeDate: Filter items completed before this date
        /// - titleContains: Filter items where title contains this text
        /// - descriptionContains: Filter items where description contains this text
        /// </remarks>
        [Function("QueryTodoItems")]
        [OpenApiOperation("QueryTodoItems", tags: new[] { "Todo" }, Summary = "Query the database for a filtered list of Todo Items", Description = "Retrieves a list of all Todo items filtered by provided parameters")]
        [OpenApiParameter("isCompleted", In = ParameterLocation.Query, Required = false, Type = typeof(bool?))]
        [OpenApiParameter("createdAfterDate", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?))]
        [OpenApiParameter("createdBeforeDate", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?))]
        [OpenApiParameter("completedAfterDate", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?))]
        [OpenApiParameter("completedBeforeDate", In = ParameterLocation.Query, Required = false, Type = typeof(DateTime?))]
        [OpenApiParameter("titleContains", In = ParameterLocation.Query, Required = false, Type = typeof(string))]
        [OpenApiParameter("descriptionContains", In = ParameterLocation.Query, Required = false, Type = typeof(string))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<TodoItem>))]
        [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
        [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseData> QueryTodoItems([HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/query")] HttpRequestData req)
        {
            var response = req.CreateResponse();

            try
            {
                var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
                bool? isCompleted = !string.IsNullOrEmpty(query["isCompleted"])
                ? bool.Parse(query["isCompleted"])
                : null;

                DateTime? createdAfterDate = !string.IsNullOrEmpty(query["createdAfterDate"])
                    ? DateTime.Parse(query["createdAfterDate"])
                    : null;

                DateTime? createdBeforeDate = !string.IsNullOrEmpty(query["createdBeforeDate"])
                    ? DateTime.Parse(query["createdBeforeDate"])
                    : null;

                DateTime? completedAfterDate = !string.IsNullOrEmpty(query["completedAfterDate"])
                    ? DateTime.Parse(query["completedAfterDate"])
                    : null;

                DateTime? completedBeforeDate = !string.IsNullOrEmpty(query["completedBeforeDate"])
                    ? DateTime.Parse(query["completedBeforeDate"])
                    : null;

                string? titleContains = query["titleContains"];
                string? descriptionContains = query["descriptionContains"];
                var todos = await _todoRepository.QueryAsync(
                isCompleted,
                createdAfterDate,
                createdBeforeDate,
                completedAfterDate,
                completedBeforeDate,
                titleContains,
                descriptionContains);

                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(todos);
                return response;
            }
            catch (FormatException ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteAsJsonAsync(new { error = "Invalid date format" });
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                await response.WriteAsJsonAsync(new { error = "An error occurred while processing your request" });
                return response;
            }
        }
        /// <summary>
        /// Updates an existing todo item.
        /// </summary>
        /// <param name="req">The HTTP request containing the updated todo item.</param>
        /// <param name="id">The unique identifier of the todo item to update.</param>
        /// <returns>
        /// 200 OK with the updated todo item if successful
        /// 404 Not Found if the item doesn't exist
        /// 400 Bad Request if the ID format is invalid or request body is invalid
        /// 500 Internal Server Error if an unexpected error occurs
        /// </returns>
        [Function("UpdateTodoItem")]
        [OpenApiOperation("UpdateTodoItem", tags: new[] { "Todo" }, Summary = "Update a Todo item", Description = "Updates a specified Todo item with provided values")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid))]
        [OpenApiRequestBody("application/json", typeof(TodoItem))]
        [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
        [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
        [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseData> UpdateTodoItem([HttpTrigger(AuthorizationLevel.Function, "put", Route = "todo/{id}")] HttpRequestData req, string id)
        {
            var response = req.CreateResponse();

            try
            {
                if (!Guid.TryParse(id, out Guid todoId))
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    await response.WriteAsJsonAsync(new { error = "Invalid ID format" });
                    return response;
                }

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var updatedTodo = JsonSerializer.Deserialize<TodoItem>(requestBody);

                if (updatedTodo == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    await response.WriteAsJsonAsync(new { error = "Invalid request body" });
                    return response;
                }

                updatedTodo.Id = todoId;
                await _todoRepository.UpdateAsync(updatedTodo);

                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(updatedTodo);
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

    }
}
