using UchebBirzha.Application;
using UchebBirzha.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddApplication();        
builder.Services.AddInfrastructure(connectionString); 


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();