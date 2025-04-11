using CapstoneTravelBlog.Data;
using CapstoneTravelBlog.Models.Account;
using CapstoneTravelBlog.Services;
using CapstoneTravelBlog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Syster", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(a => a.File("Log/Log_txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Async(a => a.Console())
    .CreateLogger();


    Log.Information("Starting application.....");

    var builder = WebApplication.CreateBuilder(args);
   
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomersManager API", Version = "v1" });

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Inserisci il token JWT nel formato: Bearer {token}",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };

        c.AddSecurityDefinition("Bearer", securityScheme);
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { securityScheme, new string[] {} }
        });
    });

    builder.Services.Configure<Jwt>(builder.Configuration.GetSection(nameof(Jwt)));

    builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount =
       builder.Configuration.GetValue<bool>("Identity:RequireConfirmedAccount");
        options.Password.RequiredLength =
            builder.Configuration.GetValue<int>("Identity:RequiredLength");
        options.Password.RequireDigit =
            builder.Configuration.GetValue<bool>("Identity:RequireDigit");
        options.Password.RequireLowercase =
            builder.Configuration.GetValue<bool>("Identity:RequireLowercase");
        options.Password.RequireNonAlphanumeric =
            builder.Configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");
        options.Password.RequireUppercase =
            builder.Configuration.GetValue<bool>("Identity:RequireUppercase");

    })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters()
           {
               ValidateIssuer = true,

               ValidateAudience = true,

               ValidateLifetime = true,

               ValidateIssuerSigningKey = true,

               ValidIssuer = builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("Issuer"),

               ValidAudience = builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("Audience"),

               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection(nameof(Jwt)).GetValue<string>("SecurityKey")))
           };
       });


    builder.Services.AddScoped<UserManager<ApplicationUser>>();
    builder.Services.AddScoped<SignInManager<ApplicationUser>>();
    builder.Services.AddScoped<RoleManager<ApplicationRole>>();
    builder.Services.AddScoped<ViaggioService>();
    builder.Services.AddScoped<BlogPostService>();
    builder.Services.AddScoped<FraseUtileService>();
    builder.Services.AddScoped<GiornoViaggioService>();
    builder.Services.AddScoped<PrenotazioneService>();
    builder.Services.AddControllers();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );


    builder.Services.AddCors();

    builder.Host.UseSerilog();

    var app = builder.Build();



// Configure the HTTP request pipeline.
app.Environment.IsDevelopment();
        app.UseSwagger();
        app.UseSwaggerUI();
   

    app.UseHttpsRedirection();

    app.UseCors(c =>
        c.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

