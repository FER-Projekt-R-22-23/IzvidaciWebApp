using System.Net;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.Providers.Http.Options;

var builder = WebApplication.CreateBuilder(args);


// take the appsettings file depending on the environment
IConfiguration configuration;
if (builder.Environment.IsDevelopment())
{
    configuration = builder.Configuration.AddJsonFile("appsettings.Development.json").Build();
}
else
{
    configuration = builder.Configuration.AddJsonFile("appsettings.json").Build();
}

// load the options from the appsettings file for AkcijaProvider
var akcijaProviderOptions =
    configuration.GetSection("AkcijaProviderOptions")
    .Get<AkcijaProviderOptions>();

var rangZaslugaOptions =
    configuration.GetSection("RangZaslugaOptions")
        .Get<RangZaslugaProviderOptions>();
// Add services to the container.
builder.Services.AddControllersWithViews();

// register the required options
builder.Services.AddTransient<AkcijaProviderOptions>(services => akcijaProviderOptions);
builder.Services.AddTransient<RangZaslugaProviderOptions>(services => rangZaslugaOptions);

// register the required providers
builder.Services.AddTransient<IAkcijaProvider, AkcijaProvider>();
builder.Services.AddTransient<IRangZaslugaProvider, RangZaslugaProvider>();
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
builder.Services.AddHttpClient("RangZaslugaOptions", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("Clanstvo").GetValue<String>("BaseUrl"));
}).ConfigurePrimaryHttpMessageHandler(x => clientHandler);
System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
