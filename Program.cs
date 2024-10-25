using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schronisko.Views.Tekstowy;
using Schronisko.Controllers;
using Spectre.Console;
using Schronisko.Models;


var builder = WebApplication.CreateBuilder(args);

// Dodanie us³ug MVC (controllers with views)
builder.Services.AddControllersWithViews();

// Rejestracja us³ugi nazwa, aby mog³a byæ wstrzykniêta do kontrolera
builder.Services.AddSingleton<WidokTekstowy>();
builder.Services.AddSingleton<Menu>();
builder.Services.AddSingleton<MenuGlowne>();
builder.Services.AddSingleton<MenuAdmin>();
builder.Services.AddSingleton<MenuWolontariusz>();
builder.Services.AddSingleton<Logowanie>();
builder.Services.AddSingleton<MenuKalendarz>();


var app = builder.Build();

// Konfiguracja œrodowiska aplikacji
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Definiowanie domyœlnej trasy dla kontrolerów
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Tworzenie instancji kontrolera i wywo³anie metody tworzenia oraz wyœwietlania wolontariusza w konsoli
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var opcje = new List<string>
    {
        //weryfikacja
        "Goœæ",
        "Wolontariusz",
        "Administrator",
        "Strona internetowa",
        "Zakoñcz przegl¹danie"
    };
   // var menu = services.GetRequiredService<Menu>();
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

       
        //AnsiConsole.Markup("[bold][on Pink1][Deeppink4]FUTRZANA FERAJNA[/][/][/]\n\n");

        var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz opcjê:")
                    .AddChoices(opcje)
                    .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1)));
        logowanie.Login(choice,widokTekstowy,app);
    }
    
    //var menuLogowania = services.GetRequiredService<Logowanie>(); // U¿ycie nowej klasy
    //menuLogowania.MenuLogowania(app);

}
// Uruchomienie aplikacji
//app.Run();
