using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Net.NetworkInformation;
using System.Text;
using API.BanSach.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    // Define a CORS policy named "AllowAll"
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });

    // Define a CORS policy named "AllowFrontend" specifically for http://127.0.0.1:8000 and http://localhost:3000
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:8000", "http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Add services to the container.
builder.Services.AddTransient<IDatabaseHelper, DatabaseHelper>();
builder.Services.AddTransient<ISachRepository, SachRepository>();
builder.Services.AddTransient<ISachBusiness, SachBusiness>();

builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<ICartBusiness, CartBusiness>();

builder.Services.AddTransient<INhaXuatBanRepository, NhaXuatBanRepository>();
builder.Services.AddTransient<INhaXuatBanBusiness, NhaXuatBanBusiness>();

builder.Services.AddTransient<ITacGiaRepository, TacGiaRepository>();
builder.Services.AddTransient<ITacGiaBusiness, TacGiaBusiness>();

builder.Services.AddTransient<ILoaiSachRepository,  LoaiSachRepository>();
builder.Services.AddTransient<ILoaiSachBusiness, LoaiSachBusiness>();
builder.Services.AddTransient<INhanVienRepository, NhanVienRepository>();
builder.Services.AddTransient<INhanVienBusiness, NhanVienBusiness>();
builder.Services.AddTransient<IKhachHangRepository, KhachHangRepository>();
builder.Services.AddTransient<IKhachHangBusiness, KhachHangBusiness>();
builder.Services.AddTransient<InccRepository, nccRepository>();
builder.Services.AddTransient<InccBusiness, nccBusiness>();
builder.Services.AddTransient<IHDBRepository, HDBRepository>();
builder.Services.AddTransient<IHDBBusiness, HDBBusiness>();
builder.Services.AddTransient<IHDNRepository, HDNRepository>();
builder.Services.AddTransient<IHDNBusiness, HDNBusiness>();
builder.Services.AddTransient<ICTHDNRepository, CT_HDNRepository>();
builder.Services.AddTransient<ICT_HDNBusiness, CT_HDNBusiness>();
builder.Services.AddTransient<ICTHDBRepository, CT_HDBRepository>();
builder.Services.AddTransient<ICT_HDBBusiness, CT_HDBBusiness>();


builder.Services.AddTransient<ITools, Tools>();



// Configure AppSettings from configuration
IConfiguration configuration = builder.Configuration;
builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

// Configure JWT authentication
var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Add controllers and configure Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS for "AllowFrontend" policy
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();