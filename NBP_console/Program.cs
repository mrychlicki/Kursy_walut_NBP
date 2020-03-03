using System;

namespace NBP_console
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj zakres dat odzdzielone spacją w formacie RRRR-MM-DD:");
            string readData = Console.ReadLine();
            Download df = new Download();
            df.data(readData);
            Console.WriteLine("Pobieram dane z NBP");
            df.Generate_files();

            ReadXml readXml = new ReadXml();
            Console.WriteLine("Wybierz walutę wybierając numer z klawiatury: ");
            Console.WriteLine("1. Dolar amerykański USD");
            Console.WriteLine("2. Euro EUR");
            Console.WriteLine("3. Funt GBP");
            Console.WriteLine("4. Frank CHF");
            readXml.set_country(int.Parse(Console.ReadLine()));
            Console.WriteLine();

            readXml.read();
            Console.WriteLine();
            Console.WriteLine($"Średnia cena zakupu to: {readXml.avg_buy_price()}");
            Console.WriteLine($"Maksymalna cena zakupu to: {readXml.max_buy_price()}");
            Console.WriteLine($"Minimalna cena zakupu to: {readXml.min_buy_price()}");
            Console.WriteLine($"Odchylenie standardowe ceny zakupu: {readXml.dev_buy()}");
            Console.WriteLine($"Średnia cena sprzedaży to: {readXml.avg_sell_price()}");
            Console.WriteLine($"Maksymalna cena sprzedaży to: {readXml.max_sell_price()}");
            Console.WriteLine($"Minimalna cena sprzedaży to: {readXml.min_sell_price()}");
            Console.WriteLine($"Odchylenie standardowe ceny zakupu: {readXml.dev_sell()}");

            Console.WriteLine();
            Console.WriteLine("Usunąć pobrane pliki?");
            Console.WriteLine("1. Tak");
            Console.WriteLine("2. Nie");
            df.delete_files(int.Parse(Console.ReadLine()));




        }


    }
}
