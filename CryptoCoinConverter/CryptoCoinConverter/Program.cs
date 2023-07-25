using CryptoCoinConverter;
using CryptoCoinConverter.Service;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<AppSettings>(
          builder.Configuration.GetSection("AppConfiguration")
      );
builder.Services.AddHttpClient("CryptoClient", config =>
{
    config.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", builder.Configuration["AppConfiguration:APIKey"]);
    config.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
    config.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json; charset=utf-8");
});
builder.Services.AddTransient<ICryptoQuotationService, CryptoQuotationService>();
builder.Services.AddTransient<ICryptoDetailService, CryptoDetailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
