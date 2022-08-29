using BackendApi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure CORS options
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Configure routing options
builder.Services.AddRouting(options =>
{
    options.LowercaseQueryStrings = true;
    options.LowercaseUrls = true;
});

// Configure controller options
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });


// Add http client service
builder.Services.AddHttpClient();

// Add authentication service
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var token = builder.Configuration.GetSection("JwtSettings").Get<JwtSetting>();

        options.Authority = token.Authority;
        options.Audience = "backend.api";
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

        // Save tokens into authentication session to enable automatic token management
        options.SaveToken = true;

        // Get claims without mappings
        options.MapInboundClaims = false;

        //options.RequireHttpsMetadata = false;
        //jwt.ClaimsIssuer = token.Issuer;
        //jwt.SaveToken = true;
        //jwt.TokenValidationParameters = new TokenValidationParameters
        //{
        //    ValidateIssuerSigningKey = true,
        //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
        //    ValidIssuer = token.Issuer,
        //    ValidateIssuer = true,
        //    ValidateAudience = false,
        //    ValidateLifetime = true,
        //    RequireExpirationTime = true,
        //    ClockSkew = TimeSpan.Zero
        //};
    });

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
