using BiddingService;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("bids", false));

    x.UsingRabbitMq(
        (context, cfg) =>
        {
            cfg.Host(
                builder.Configuration["RabbitMq:Host"],
                "/",
                host =>
                {
                    host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
                    host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
                }
            );
            cfg.ConfigureEndpoints(context);
        }
    );
});

//register authentication with jwt
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        System.Console.WriteLine("--Enter auction " + builder.Configuration["IdentityServiceUrl"]);
        opt.Authority = builder.Configuration["IdentityServiceUrl"];
        opt.RequireHttpsMetadata = false; //identity server is running on http
        opt.TokenValidationParameters.ValidateAudience = false;
        opt.TokenValidationParameters.NameClaimType = "username";
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//add a background service
builder.Services.AddHostedService<CheckAuctionFinished>();
//add grpc client
builder.Services.AddScoped<GrpcAuctionClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

await DB.InitAsync("BidDb", MongoClientSettings
    .FromConnectionString(builder.Configuration.GetConnectionString("BidDbConnection")));

app.Run();
