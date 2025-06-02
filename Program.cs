using HackathonProject.Services; // Replace 'YourNamespace.Services' with the actual namespace

var builder = WebApplication.CreateBuilder(args);

// ✅ Register your custom settings
builder.Services.Configure<DatabricksSettings>(
    builder.Configuration.GetSection("Databricks"));

// ✅ Register MVC services
builder.Services.AddControllersWithViews();

// ✅ Register DatabricksService if not done yet
builder.Services.AddScoped<IDatabricksService, DatabricksService>();

var app = builder.Build();

// ✅ Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // required for CSS/JS

app.UseRouting();

app.UseAuthorization(); // now valid

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
