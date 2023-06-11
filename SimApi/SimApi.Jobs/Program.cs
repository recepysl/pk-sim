using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("HangfireConnection");

builder.Services.AddHangfire(configuration => configuration
   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
   .UseSimpleAssemblyNameTypeSerializer()
   .UseRecommendedSerializerSettings()
   .UsePostgreSqlStorage(connection, new PostgreSqlStorageOptions
   {
       TransactionSynchronisationTimeout = TimeSpan.FromMinutes(5),
       InvisibilityTimeout = TimeSpan.FromMinutes(5),
       QueuePollInterval = TimeSpan.FromMinutes(10),
       AllowUnsafeValues = true
   }));


builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
