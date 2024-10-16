using Schronisko.Models;

namespace Schronisko.Views.Tekstowy
{
    public class wTekstowyWolontariusz
    {
        public Wolontariusz stworzWolontariusza()
        {
            Wolontariusz wolontariusz=new Wolontariusz();

            Console.WriteLine("Podaj imie");
            wolontariusz.Imie = Console.ReadLine();

            return wolontariusz;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
