using EmployeeAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDb")));

var app = builder.Build();

app.MapGet("/", () => "Hello World");

/// <summary>
/// REST methods for Employees Table
/// </summary>
// Get all employees
app.MapGet("/api/employees", async (EmployeeContext db) => await db.Employees.Include(i => i.Tasks).ToListAsync());

// Get specific employee by id
app.MapGet("/api/employees/{id}", async (EmployeeContext db, int id) => await db.Employees.FindAsync(id));

// Get employees by first name
app.MapGet("/api/employees/firstname/{firstname}", async (EmployeeContext db, string firstname) => await db.Employees.Where(e => e.FirstName.Contains(firstname)).ToListAsync());

// Get employees by last name
app.MapGet("/api/employees/lastname/{lastname}", async (EmployeeContext db, string lastname) => await db.Employees.Where(e => e.LastName.Contains(lastname)).ToListAsync());

// Create new employee
app.MapPost("/api/employees", async (EmployeeContext db, Employee employee) =>
{
    await db.Employees.AddAsync(employee);
    await db.SaveChangesAsync();
    Results.Accepted();
});

// Update employee by id
app.MapPut("/api/employees/{id}", async (EmployeeContext db, int id, Employee employee) =>
{
    if(id != employee.Id) return Results.BadRequest();

    db.Update(employee);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Delete employee by id
app.MapDelete("/api/employees/{id}", async (EmployeeContext db, int id) =>
{
    var employee = await db.Employees.FindAsync(id);
    if(employee == null) return Results.NotFound();

    db.Employees.Remove(employee);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

/// <summary>
/// REST methods for EmployeeTasks Table
/// </summary>
// Get all tasks
app.MapGet("/api/employeetasks", async (EmployeeContext db) => await db.EmployeeTasks.ToListAsync());

// Get specific task by id
app.MapGet("/api/employeetasks/{id}", async (EmployeeContext db, int id) => await db.EmployeeTasks.FindAsync(id));

// Get tasks by task name
app.MapGet("/api/employeetasks/taskname/{taskname}", async (EmployeeContext db, string taskname) => await db.EmployeeTasks.Where(e => e.TaskName.Contains(taskname)).ToListAsync());

// Get tasks by start time
app.MapGet("/api/employeetasks/starttime/{starttime}", async (EmployeeContext db, DateTimeOffset starttime) => await db.EmployeeTasks.Where(e => e.StartTime.Date == starttime.Date).ToListAsync());

// Get tasks by deadline
app.MapGet("/api/employeetasks/deadline/{deadline}", async (EmployeeContext db, DateTimeOffset deadline) => await db.EmployeeTasks.Where(e => e.Deadline.Date == deadline.Date).ToListAsync());

// Create new task
app.MapPost("/api/employeetasks", async (EmployeeContext db, EmployeeTask employeeTask) =>
{
    await db.EmployeeTasks.AddAsync(employeeTask);
    await db.SaveChangesAsync();
    Results.Accepted();
});

// Update task by id
app.MapPut("/api/employeetasks/{id}", async (EmployeeContext db, int id, EmployeeTask employeeTask) =>
{
    if (id != employeeTask.Id) return Results.BadRequest();

    db.Update(employeeTask);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Delete task by id
app.MapDelete("/api/employeetasks/{id}", async (EmployeeContext db, int id) =>
{
    var employeeTask = await db.EmployeeTasks.FindAsync(id);
    if (employeeTask == null) return Results.NotFound();

    db.EmployeeTasks.Remove(employeeTask);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();