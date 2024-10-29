using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Schronisko.Models
{
    public class Validation
    {
        public static string PobierzPoprawneDane(string komunikat, object model, string nazwaWlasciwosci)
        {
            Console.WriteLine(komunikat);
            string daneUzytkownika = Console.ReadLine();
            UstawWlasciwosc(model, nazwaWlasciwosci, daneUzytkownika);
            var kontekstWalidacji = new ValidationContext(model)
            {
                MemberName = nazwaWlasciwosci
            };
            var wynikiWalidacji = new List<ValidationResult>();
            PropertyInfo wlasciwoscInfo = model.GetType().GetProperty(nazwaWlasciwosci);

            bool czyDaneSaPoprawne = Validator.TryValidateProperty(
                wlasciwoscInfo.GetValue(model),
                kontekstWalidacji,
                wynikiWalidacji
            );
            if (!czyDaneSaPoprawne)
            {
                foreach (var wynikWalidacji in wynikiWalidacji)
                {
                    Console.WriteLine(wynikWalidacji.ErrorMessage);
                }
                return PobierzPoprawneDane(komunikat, model, nazwaWlasciwosci);
            }

            return daneUzytkownika;
        }
        public static void UstawWlasciwosc(object model, string nazwaWlasciwosci, string wartosc)
        {
            try
            {
                PropertyInfo wlasciwoscInfo = model.GetType().GetProperty(nazwaWlasciwosci);

                if (wlasciwoscInfo != null)
                {
                    wlasciwoscInfo.SetValue(model, wartosc);
                }
                else
                    Console.WriteLine($"Właściwość '{nazwaWlasciwosci}' nie została znaleziona w modelu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas ustawiania właściwości: {ex.Message}");
            }
        }
    }
}
