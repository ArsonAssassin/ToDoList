using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ToDoList.Api.Services.OpenApi
{
    public class OpenApiSettings : DefaultOpenApiConfigurationOptions
    {
        public override OpenApiInfo Info { get; set; } = new OpenApiInfo
        {
            Title = "Todo List API",
            Version = "1.0.0",
            Description = @"
                This API serves data for managing Todo Lists as a backend for TodoListUI.
            "
           
        };        

    }
}
