using AspProMongoDb.web.DataBase;
using AspProMongoDb.web.Model;
using AspProMongoDb.web.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddRazorPages();
services.AddScoped<MongoDbContext>();
services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
services.AddSingleton<IMongoClient, MongoClient>(provider =>
{
    var settings = provider.GetRequiredService<MongoSettings>();
    return new MongoClient(settings.ConnectionString);
});
services.AddSingleton<MongoSettings>(sp => sp.GetRequiredService<IOptions<MongoSettings>>().Value);
services.AddTransient<IUserServices, UserServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
