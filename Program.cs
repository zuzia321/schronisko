using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schronisko.Views.Tekstowy;
using Schronisko.Controllers;
using Spectre.Console;


var builder = WebApplication.CreateBuilder(args);

// Dodanie us³ug MVC (controllers with views)
builder.Services.AddControllersWithViews();

// Rejestracja us³ugi WidokTekstowy, aby mog³a byæ wstrzykniêta do kontrolera
builder.Services.AddSingleton<WidokTekstowy>();
builder.Services.AddSingleton<Menu>();

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


    var options = new List<string>
        {
            "Formularz Wolontariusza",
            "Zwierzêta do adopcji",
            "Informacje o schronisku",
            "Darowizna",
            "Strona internetowa",
            "Zakoñcz przegl¹danie"
        };
    var opcje = new List<string>
    {
        //weryfikacja
        "Goœæ",
        "Wolontariusz",
        "Administrator"
    };
    var menu = services.GetRequiredService<Menu>();
    var widokTekstowy = services.GetRequiredService<WidokTekstowy>();
    while (true)
    {
        AnsiConsole.Clear();
        AnsiConsole.Markup("[bold][on Pink1][Deeppink4]FUTRZANA FERAJNA[/][/][/]\n\n");

        var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz opcjê:")
                    .AddChoices(opcje)
                    .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1)));
        menu.Logowanie(choice);
    }

    while (true)
    {
        AnsiConsole.Clear();
        AnsiConsole.Markup("[bold][on Pink1][Deeppink4]FUTRZANA FERAJNA[/][/][/]\n\n");

        var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz opcjê:")
                    .AddChoices(options)
                    .HighlightStyle(new Style(foreground: Color.DeepPink3_1, background: Color.Pink1)));

        menu.MenuGlowne(choice, widokTekstowy, app);
    }
}
// Uruchomienie aplikacji
//app.Run();
