using DapperUnitOfWork.Common.Data.Factories.Implementation;
using DapperUnitOfWork.Common.Data.Factories.Interfaces;
using DapperUnitOfWork.Common.Data.IOptions;
using DapperUnitOfWork.Data.Context.Implementation;
using DapperUnitOfWork.Data.Context.Interfaces;
using DapperUnitOfWork.Data.Repositories.Implementation;
using DapperUnitOfWork.Data.Repositories.Interfaces;
using DapperUnitOfWork.Data.UnitOfWork.Implementation;
using DapperUnitOfWork.Data.UnitOfWork.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var sqlServerOptions = new SqlServerOptions();
configuration.Bind(SqlServerOptions.SectionName, sqlServerOptions);
services.AddSingleton(Options.Create(sqlServerOptions));

services.Configure<SqlServerOptions>(configuration.GetSection(SqlServerOptions.SectionName));
services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
services.AddTransient<IPersonDbContext, PersonDbContext>();
services.AddTransient<IAddressRepository, AddressRepository>();
services.AddTransient<IPersonDbContextSession, PersonDbContextSession>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();