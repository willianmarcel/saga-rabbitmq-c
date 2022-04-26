using Microsoft.EntityFrameworkCore;
using Ordering.Api.Context;
using Ordering.Api.Listeners;
using Plain.RabbitMQ;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderingContext>(options => options.UseSqlite(connectionString));
builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@localhost:5672"));

builder.Services.AddSingleton<IPublisher>(p => new Publisher(p.GetService<IConnectionProvider>(),
                "order_exchange", // exchange name
                ExchangeType.Topic));

builder.Services.AddSingleton<ISubscriber>(s => new Subscriber(s.GetService<IConnectionProvider>(),
    "catalog_exchange", // Exchange name
    "catalog_response_queue", //queue name
    "catalog_response_routingkey", // routing key
    ExchangeType.Topic));

builder.Services.AddHostedService<CatalogResponseListener>();

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
