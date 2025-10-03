﻿using Microsoft.Extensions.DependencyInjection;
using Supers.Application.UseCases.SuperHerois.Cadastro;
using Supers.Application.Utils.AutoMapper;
using Microsoft.Extensions.Configuration;
using Supers.Application.UseCases.SuperPoderes.ObterTodos;
using Supers.Application.UseCases.SuperHerois.ObterTodos;
using Supers.Application.UseCases.SuperHerois.Obter;
using Supers.Application.UseCases.SuperHerois.Atualizar;

namespace Supers.Application
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration) 
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(AutoMapping));
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<ICadastroDeSupersUseCase, CadastroDeSupersUseCase>();
            services.AddScoped<IObterTodosOsSupersUseCase, ObterTodosOsSupersUseCase>();
            services.AddScoped<IObterTodosOsPoderesUseCase, ObterTodosOsPoderesUseCase>();
            services.AddScoped<IObterSuperUseCase, ObterSuperUseCase>();
            services.AddScoped<IAtualizarSuperUseCase, AtualizarSuperUseCase>();
        }
    }
}