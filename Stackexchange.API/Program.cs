using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Stackexchange.Application.AutoMapper;
using Stackexchange.Application.TagServices.Commands;
using Stackexchange.Application.TagServices.Queries;
using Stackexchange.Domain.Tags;
using Stackexchange.Infrastructure.Context;
using Stackexchange.Infrastructure.Repositories;

Log.Logger = new LoggerConfiguration().WriteTo.File(Environment.GetEnvironmentVariable("LOG_PATH") ?? "Logs/logs.txt").CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetTagPageQuery)));
builder.Services.AddDbContext<SeDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("SE_DB_CONNSTR")));
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Host.UseSerilog();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var mediator = services.GetRequiredService<IMediator>();
    var command = new PostTagCommand();
    await mediator.Send(command);
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
