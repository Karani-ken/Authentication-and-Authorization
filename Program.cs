using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Extensions;
using Authentication_and_Authorization.Services;
using Authentication_and_Authorization.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
//services
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IProductInterface, ProductService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//authentication
builder.AddAppAuthentication();

//Adding Authorization options

builder.addAuthorizationExtension();

builder.AddSwaggenGenExtension();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();
//Migration
//check if there any pending migrations
//add to database(update-database)
app.ApplyMigration();
app.Run();


