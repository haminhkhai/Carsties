using AuctionService;
using AuctionService.Data;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//take a look for any class that derive from the profile class and register the mappings
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(x =>
{
    //message outbox config
    //every 10s it's going to look inside outbox to see
    //if there's anything that hasn't been delivered yet once service bus is available
    x.AddEntityFrameworkOutbox<AuctionDbContext>(opt =>
    {
        opt.QueryDelay = TimeSpan.FromSeconds(10);
        opt.UsePostgres();
        opt.UseBusOutbox();
    });

    //faulty handling (example)
    x.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));

    x.UsingRabbitMq(
        (context, cfg) =>
        {
            cfg.ConfigureEndpoints(context);
        }
    );
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        opt.Authority = builder.Configuration["IdentityServiceUrl"];
        opt.RequireHttpsMetadata = false; //identity server is running on http
        opt.TokenValidationParameters.ValidateAudience = false;
        opt.TokenValidationParameters.NameClaimType = "username";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();


//direct http request to the correct API endpoint
app.MapControllers();
try
{
    DbInitializer.InitDb(app);
}
catch (System.Exception e)
{
    System.Console.WriteLine(e);
}
app.Run();
