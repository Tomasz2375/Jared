﻿using Blazored.LocalStorage;
using Jared.Application.Validators.Task;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jared.Presentation;

public static class DepedencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);
        services.AddBlazoredLocalStorage();
        services.AddScoped<TaskRootDtoValidator>();
        services.AddScoped<TaskDetailsDtoValidator>();

        return services;
    }
}
