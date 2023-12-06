using konyvtar_ui;
using konyvtar_ui.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5076") });
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReaderService, ReaderService>();

await builder.Build().RunAsync();
