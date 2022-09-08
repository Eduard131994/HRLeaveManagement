using HRLeaveManagement.Api.Extensions;
using HRLeaveManagement.Application;
using HRLeaveManagement.Identity;
using HRLeaveManagement.Infrastructure;
using HRLeaveManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDoc();
builder.Services.AddCors(o =>
{
    o.AddPolicy(
        "CorsPloicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPloicy");
app.MapControllers();

app.Run();
