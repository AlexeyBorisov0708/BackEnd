using Microsoft.EntityFrameworkCore;
using TestVersta;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder();
string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/", async (ApplicationContext db) => await db.Orders.ToListAsync());

app.MapGet("/api/{id:int}", async (int id, ApplicationContext db) =>
{
    Order? order = await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
    if (order == null) return Results.NotFound(new { message = "«аказ не найден" });
    return Results.Json(order);
});

app.MapDelete("/api/{id:int}", async (int id, ApplicationContext db) =>
{
    // получаем пользовател€ по id
    Order? order = await db.Orders.FirstOrDefaultAsync(o => o.Id == id);

    // если не найден, отправл€ем статусный код и сообщение об ошибке
    if (order == null) return Results.NotFound(new { message = "«аказ не найден" });

    // если пользователь найден, удал€ем его
    db.Orders.Remove(order);
    await db.SaveChangesAsync();
    return Results.Json(order);
});

app.MapPost("/api/", async (Order order, ApplicationContext db) =>
{
    await db.Orders.AddAsync(order);
    await db.SaveChangesAsync();
    return order;
});

app.MapPut("/api/", async (Order orderData, ApplicationContext db) =>
{
    var order = await db.Orders.FirstOrDefaultAsync(u => u.Id == orderData.Id);

    if (order == null) return Results.NotFound(new { message = "ѕользователь не найден" });

    order.SenderCity = orderData.SenderCity;
    order.SenderAdress = orderData.SenderAdress;
    order.RecipientCity = orderData.RecipientCity;
    order.AddressRecipient = orderData.AddressRecipient;
    order.ParcelWeight = orderData.ParcelWeight;
    order.Date = orderData.Date;

    await db.SaveChangesAsync();
    return Results.Json(order);
});

app.UseCors(MyAllowSpecificOrigins);

app.Run();