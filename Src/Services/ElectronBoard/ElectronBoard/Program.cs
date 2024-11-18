using Carter;
using ElectronBoard.Consuming.VoteAssigment;
using ElectronBoard.Infrastructure.SqlServer.Persistence.Extentions;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<VoteAssignmentConsumer>();
builder.Services.InitialDataBase(builder.Configuration);
#region Validator Behavior Configration
builder.Services
    .AddValidatorsFromAssembly(typeof(Program).Assembly);
#endregion

#region Kafka


#endregion
#region Carter

builder.Services.AddCarter();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

app.Run();
