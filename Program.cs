using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schronisko.Views.Tekstowy;
using Schronisko.Controllers;
using Spectre.Console;
using Schronisko.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Rejestracja us�ugi nazwa, aby mog�a by� wstrzykni�ta do kontrolera
builder.Services.AddSingleton<WidokTekstowy>();
builder.Services.AddSingleton<Menu>();
builder.Services.AddSingleton<MenuGlowne>();
builder.Services.AddSingleton<MenuAdmin>();
builder.Services.AddSingleton<MenuWolontariusz>();
builder.Services.AddSingleton<Logowanie>();
builder.Services.AddSingleton<MenuKalendarz>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var opcje = new List<string>
    {
        "Go��",
        "Wolontariusz",
        "Administrator",
        "Strona internetowa",
        "Zako�cz przegl�danie"
    };
    var logowanie=services.GetRequiredService<Logowanie>();
    var widokTekstowy = services.GetRequiredService<WidokTekstowy>();
    while (true)
    {
        AnsiConsole.Clear();

        var font = FigletFont.Load("Small.flf");

        AnsiConsole.Write(
            new FigletText(font, "FUTRZANA FERAJNA\n\n")
                .Centered()
                .Color(Color.DeepPink3));

        var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz opcj�:")
                    .AddChoices(opcje)
                    .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1)));
        logowanie.Login(choice,widokTekstowy,app);
    }
}
// Uruchomienie aplikacji
//app.Run();
