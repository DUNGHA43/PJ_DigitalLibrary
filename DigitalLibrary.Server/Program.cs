using DigitalLibrary.Server.Data.DatabaseContext;
using DigitalLibrary.Server.Data.UnitOfWork;
using DigitalLibrary.Server.Services.Interface;
using DigitalLibrary.Server.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Net.payOS;

var builder = WebApplication.CreateBuilder(args);

// CORS Configuration
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7236")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Thêm dòng này nếu sử dụng credentials
        });
});


builder.Services.AddDbContext<DbDigitalLibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("Secret");

if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("JWT Secret is missing in configuration.");
}

var keyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (!string.IsNullOrEmpty(accessToken))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Token expired or invalid" });
                return context.Response.WriteAsync(result);
            }
        };
    });


var payOsSettings = builder.Configuration.GetSection("PayOSSettings");

PayOS payOS = new PayOS(
    payOsSettings.GetValue<string>("PAYOS_CLIENT_ID") ?? throw new Exception("PAYOS_CLIENT_ID is missing"),
    payOsSettings.GetValue<string>("PAYOS_API_KEY") ?? throw new Exception("PAYOS_API_KEY is missing"),
    payOsSettings.GetValue<string>("PAYOS_CHECKSUM_KEY") ?? throw new Exception("PAYOS_CHECKSUM_KEY is missing")
);


builder.Services.AddSingleton(payOS);
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IAuthorsServices, AuthorsServices>();
builder.Services.AddScoped<ICategoriseServices, CategoriseServices>();
builder.Services.AddScoped<ISubjectServices, SubjectServices>();
builder.Services.AddScoped<IReviewsServices, ReviewsServices>();
builder.Services.AddScoped<IUploadServices, UploadServices>();
builder.Services.AddScoped<IDocumentServices, DocumentServices>();
builder.Services.AddScoped<INationServices, NationServices>();
builder.Services.AddScoped<IDocumentAuthorServices, DocumentAuthorServices>();
builder.Services.AddScoped<IDocumentCategoriesServices, DocumentCategoriesServices>();
builder.Services.AddScoped<IDocumentSubjectsServices, DocumentSubjectsServices>();
builder.Services.AddScoped<IRolesServices, RolesServices>();
builder.Services.AddScoped<IUserPermissionServices, UserPermissionServices>();
builder.Services.AddScoped<IStatisticsServices, StatisticServices>();
builder.Services.AddScoped<IUserSubscriptionServices, UserSubscriptionServices>();
builder.Services.AddScoped<IPaymentHistoryServices, PaymentHistoryServices>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();