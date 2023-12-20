using System.Text.Json.Serialization;
using CadastroNF.Data.Context;
using CadastroNF.Data.Repositories;
using CadastroNF.Data.Repositories.Interfaces;
using CadastroNF.Services;
using CadastroNF.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CadastroNF;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupIocContainer(this IServiceCollection services, string? dbConnectionString)
    {
        services.AddApiSetup();
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConnectionString));
        services.AddRepositories();
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IClienteRepository, ClienteRepository>();
        services.AddTransient<IFornecedorRepository, FornecedorRepository>();
        services.AddTransient<IProdutoRepository, ProdutoRepository>();
        services.AddTransient<INotaFiscalRepository, NotaFiscalRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IClienteService, ClienteService>();
        services.AddTransient<IFornecedorService, FornecedorService>();
        services.AddTransient<IProdutoService, ProdutoService>();
        services.AddTransient<INotaFiscalService, NotaFiscalService>();

        return services;
    }

    private static IServiceCollection AddApiSetup(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}