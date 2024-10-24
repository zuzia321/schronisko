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
        // Dynamiczne pobieranie i walidacja danych z terminala
        public static string PobierzPoprawneDane(string komunikat, object model, string nazwaWlasciwosci)
        {
            Console.WriteLine(komunikat);
            string daneUzytkownika = Console.ReadLine();

            // Dynamicznie ustawiamy wartość właściwości obiektu za pomocą refleksji
            UstawWlasciwosc(model, nazwaWlasciwosci, daneUzytkownika);

            // Walidujemy tylko wybraną właściwość, a nie cały model
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

            // Jeśli walidacja się nie powiodła, wyświetlamy błędy i ponownie prosimy o dane
            if (!czyDaneSaPoprawne)
            {
                foreach (var wynikWalidacji in wynikiWalidacji)
                {
                    Console.WriteLine(wynikWalidacji.ErrorMessage);
                }
                // Rekursja, aby ponownie pobrać dane
                return PobierzPoprawneDane(komunikat, model, nazwaWlasciwosci);
            }

            return daneUzytkownika;
        }

        // Metoda ustawiająca wartość właściwości za pomocą refleksji
        public static void UstawWlasciwosc(object model, string nazwaWlasciwosci, string wartosc)
        {
            try
            {
                PropertyInfo wlasciwoscInfo = model.GetType().GetProperty(nazwaWlasciwosci);

                if (wlasciwoscInfo != null)
                {
                    /*if (wlasciwoscInfo.PropertyType == typeof(int))
                    {
                        if (int.TryParse(wartosc, out int wartoscInt))
                            wlasciwoscInfo.SetValue(model, wartoscInt);
                        else
                            Console.WriteLine("Wartość musi być liczbą całkowitą.");
                    }
                    else if (wlasciwoscInfo.PropertyType == typeof(decimal))
                    {
                        if (decimal.TryParse(wartosc, out decimal wartoscDecimal))
                            wlasciwoscInfo.SetValue(model, wartoscDecimal);
                        else
                            Console.WriteLine("Wartość musi być liczbą dziesiętną.");
                    }
                    else if (wlasciwoscInfo.PropertyType == typeof(DateTime))
                    {
                        if (DateTime.TryParse(wartosc, out DateTime wartoscData))
                        {
                            wlasciwoscInfo.SetValue(model, wartoscData);
                        }
                        else
                        {
                            Console.WriteLine("Wartość musi być prawidłową datą. Użyj formatu yyyy-MM-dd lub dd-MM-yyyy.");
                        }
                    }
                    else if (wlasciwoscInfo.PropertyType == typeof(bool))
                    {
                        if (bool.TryParse(wartosc, out bool boolValue))
                            wlasciwoscInfo.SetValue(model, boolValue);
                        else
                            Console.WriteLine("Wartość musi być prawdą lub fałszem.");
                    }
                    else*/
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
