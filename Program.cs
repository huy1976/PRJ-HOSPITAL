using HealthyApp.Mapping;
using HealthyApp.Persistence;
using HealthyApp.Repositories.Implementations;
using HealthyApp.Repositories.IRepositories;
using HealthyApp.Repositories_of_Appointment.Implementations;
using HealthyApp.Repositories_of_Appointment.IRepositories;
using HealthyApp.Repositories1.Implementations;
using HealthyApp.Repositories1.IRepositories;
using HealthyApp.Services;
using HealthyApp.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .SetIsOriginAllowed(origin => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorServices, DoctorService>();

builder.Services.AddScoped<IUnitOfWork1, UnitOfWork1>();
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
builder.Services.AddScoped<IPatientServices, PatientServices>();

builder.Services.AddScoped<IAppointmentService,AppointmentServices>();
builder.Services.AddScoped<IUnitofWorkAppointment, UnitOfWorkAppointment>();
builder.Services.AddScoped<IAppointmentRepositories, AppointmentRepositories>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAutoMapper(typeof(ModelToViewModel).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// T?t c? dòng code trên ch? ch?y 1 l?n r có th? ch?y qua các dòng code trung gian ? d??i r m?i dô control
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // T? ??ng gán mác là Doctor

app.Run();
