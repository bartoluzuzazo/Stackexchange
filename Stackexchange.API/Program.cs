using Microsoft.EntityFrameworkCore;
using Stackexchange.Application.AutoMapper;
using Stackexchange.Application.TagServices.Queries;
using Stackexchange.Domain.Tags;
using Stackexchange.Infrastructure.Context;
using Stackexchange.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetTagQuery)));
builder.Services.AddDbContext<SeDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("SE_DB_CONNSTR")));
builder.Services.AddScoped<ITagRepository, TagRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
