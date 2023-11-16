using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SICS.Client;
using SICS.Client.Servicios.Contrato;
using SICS.Client.Servicios.Implementacion;
using SICS.Client.Utilidad;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddSingleton<MenuService>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<IConsumidorServicio, ConsumidorServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IItemServicio, ItemServicio>();
builder.Services.AddScoped<IPrestamoServicio, PrestamoServicio>();
builder.Services.AddScoped<IDashBoardServicio, DashBoardServicio>();
builder.Services.AddScoped<IPedidoServicio, PedidoServicio>();
builder.Services.AddScoped<IIdentificadoresServicio, IdentificadoresServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<IEntregaServicio, EntregaServicio>();


builder.Services.AddSweetAlert2();
await builder.Build().RunAsync();
