using Catalog.Api.Context;
using Catalog.Api.Listener;
using Microsoft.EntityFrameworkCore;
using Plain.RabbitMQ;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogContext>(options => options.UseSqlite(connectionString));
builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@localhost:5672"));
builder.Services.AddSingleton<IPublisher>(p => new Publisher(p.GetService<IConnectionProvider>(), "catalog_exchange", ExchangeType.Topic));

builder.Services.AddSingleton<ISubscriber>(s => new Subscriber(s.GetService<IConnectionProvider>(),
    "order_exchange",
    "order_response_queue",
    "order_created_routingkey",
    ExchangeType.Topic
    ));

builder.Services.AddHostedService<OrderCreatedListener>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
