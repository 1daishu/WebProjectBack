using Microsoft.EntityFrameworkCore;
using Pod.Data;
using Pod.IServices;
using Pod.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Pod.MapperProfile.Mapper).Assembly);
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyString")));

builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICommentService, CommentService>();
const string myAllowSpecificOrigins = "MyPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyPolicy");


app.UseAuthorization();

app.MapControllers();

app.Run();
