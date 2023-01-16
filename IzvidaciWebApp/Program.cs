using System.Net;
using IzvidaciWebApp.Providers;
using IzvidaciWebApp.Providers.Http;
using IzvidaciWebApp.Providers.Http.Options;
using IzvidaciWebApp.ViewModels;

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
    configuration.GetSection("AkcijaOptions")
    .Get<AkcijaProviderOptions>();

var mjestoProviderOptions =
    configuration.GetSection("MjestoOptions")
    .Get<MjestoProviderOptions>();

var materijalnePotrebeProviderOptions =
    configuration.GetSection("MaterijalnaPotrebaOptions")
    .Get<MaterijalnaPotrebaProviderOptions>();

var rangZaslugaOptions =
    configuration.GetSection("RangZaslugaOptions")
        .Get<RangZaslugaProviderOptions>();
var rangStarostOptions =
    configuration.GetSection("RangStarostOptions")
        .Get<RangStarostProviderOptions>();

var udrugeProviderOptions =
    configuration.GetSection("UdrugeOptions")
    .Get<UdrugaProviderOptions>();
// Add services to the container.
builder.Services.AddControllersWithViews();

// register the required options
builder.Services.AddTransient<AkcijaProviderOptions>(services => akcijaProviderOptions);
builder.Services.AddTransient<RangZaslugaProviderOptions>(services => rangZaslugaOptions);
builder.Services.AddTransient<RangStarostProviderOptions>(services => rangStarostOptions);
builder.Services.AddTransient<MjestoProviderOptions>(services => mjestoProviderOptions);
builder.Services.AddTransient<MaterijalnaPotrebaProviderOptions>(services => materijalnePotrebeProviderOptions);
builder.Services.AddTransient<UdrugaProviderOptions>(services => udrugeProviderOptions);

// register the required providers
builder.Services.AddTransient<IAkcijaProvider, AkcijaProvider>();
builder.Services.AddTransient<IRangZaslugaProvider, RangZaslugaProvider>();
builder.Services.AddTransient<IRangStarostProvider, RangStarostProvider>();
builder.Services.AddTransient<IAktivnostProvider, AktivnostiProvider>();
builder.Services.AddTransient<IMjestoProvider, MjestoProvider>();
builder.Services.AddTransient<IMaterijalnaPotrebaProvider, MaterijalnaPotrebaProvider>();
builder.Services.AddTransient<ISkolaProvider, SkolaProvider>();
builder.Services.AddTransient<IUdrugeProvider, UdrugeProvider>();
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
builder.Services.AddHttpClient("RangZaslugaOptions", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("Clanstvo").GetValue<String>("BaseUrl"));
}).ConfigurePrimaryHttpMessageHandler(x => clientHandler);

builder.Services.AddHttpClient("RangStarostOptions", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("Clanstvo").GetValue<String>("BaseUrl"));
}).ConfigurePrimaryHttpMessageHandler(x => clientHandler);
builder.Services.AddHttpClient("AkcijeOptions", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("Akcije").GetValue<String>("BaseUrl"));
}).ConfigurePrimaryHttpMessageHandler(x => clientHandler);
builder.Services.AddHttpClient("MjestoOptions", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("Mjesta").GetValue<String>("BaseUrl"));
}).ConfigurePrimaryHttpMessageHandler(x => clientHandler);
builder.Services.AddHttpClient("MaterijalnaPotrebaOptions", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("MaterijalnePotrebe").GetValue<String>("BaseUrl"));
}).ConfigurePrimaryHttpMessageHandler(x => clientHandler);

builder.Services.AddHttpClient("UdrugeOptions", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("Udruge").GetValue<String>("BaseUrl"));
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
