using GrpcServer.Entites;
using GrpcServer.Interceptors;
using GrpcServer.Repository;
using GrpcServer.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<LoggingInterceptor>();
});

builder.Services.AddDbContext<GrpcDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Context")));
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .WithOrigins("http://localhost:3000")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));
builder.Services.AddScoped<IUserRepository,UserRepository>();

var app = builder.Build();
app.UseRouting();
app.UseGrpcWeb();
app.UseCors();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<UserService>()
    .EnableGrpcWeb()
    .RequireCors("CorsPolicy");
});

app.Run();
