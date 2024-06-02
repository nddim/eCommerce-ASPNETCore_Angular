﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Helpers.Auth
{
    public class AutorizacijaSwaggerHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "my-auth-token",
                In = ParameterLocation.Header,
                Description = "upisati token preuzet iz autentikacijacontrollera"
            });
        }
    }
    public class RefreshTokenHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "refreshtoken",
                In = ParameterLocation.Header,
                Description = "refreshtoken"
            });
        }
    }
}
