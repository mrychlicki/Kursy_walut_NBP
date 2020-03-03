using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace NBP_console
{

    public class Download
    {
        private int current_year = DateTime.Now.Year;
        public static List<string> dir = new List<string>();
        private string[] dirArr;
        List<string> lista_Dat = new List<string>();
        string dataOd = "";
        string dataDo = "";

        string dataOd_rok = "";
        string dataOd_miesiac = "";
        string dataOd_dzien = "";

        string dataDo_rok = "";
        string dataDo_miesiac = "";
        string dataDo_dzien = "";

        int data_rok = 0;
        int data_miesiac = 0;
        int data_dzien = 0;
        public Download()
        {
            Directory.CreateDirectory(@"C:\nbp_files");
        }
        private void Download_Files(string files)
        {

            WebClient nbp = new WebClient();
            try
            {
                nbp.DownloadFile(new Uri("http://www.nbp.pl/kursy/xml/" + files), @"C:\nbp_files\" + files);
            }
            catch (WebException e)
            {
                Console.WriteLine("Błąd pobierania");

            }
        }
        public void data(string data)
        {
            dataOd = data.Split(' ')[0];
            dataDo = data.Split(' ')[1];

            dataOd_rok = dataOd.Split('-')[0];
            dataOd_miesiac = dataOd.Split('-')[1];
            dataOd_dzien = dataOd.Split('-')[2];

            dataDo_rok = dataDo.Split('-')[0];
            dataDo_miesiac = dataDo.Split('-')[1];
            dataDo_dzien = dataDo.Split('-')[2];

            data_rok = int.Parse(dataOd_rok);
            data_miesiac = int.Parse(dataOd_miesiac);
            data_dzien = int.Parse(dataOd_dzien);

            for (int i = 0; i < i + 1; i++)
            {
                string data_rok_string = data_rok.ToString()[2] + "" + data_rok.ToString()[3];
                string data_miesiac_string = data_miesiac.ToString();
                string data_dzien_string = data_dzien.ToString();
                if (data_miesiac_string.Length < 2) { data_miesiac_string = $"0{data_miesiac_string}"; };
                if (data_dzien_string.Length < 2) { data_dzien_string = $"0{data_dzien_string}"; };


                lista_Dat.Add($"{data_rok_string}{data_miesiac_string}{data_dzien_string}");
                data_dzien += 1;

                if (data_miesiac < 8 && data_miesiac % 2 != 0 && data_dzien > 31)
                {
                    data_dzien = 1;
                    data_miesiac += 1;
                }

                if (data_miesiac == 2 && data_dzien > 28)
                {
                    data_dzien = 1;
                    data_miesiac += 1;
                }

                if (data_miesiac < 8 && data_miesiac % 2 == 0 && data_dzien > 30 && data_miesiac != 2)
                {
                    data_dzien = 1;
                    data_miesiac += 1;
                }

                if (data_miesiac >= 8 && data_miesiac % 2 != 0 && data_dzien > 30)
                {
                    data_dzien = 1;
                    data_miesiac += 1;
                }

                if (data_miesiac >= 8 && data_miesiac % 2 == 0 && data_dzien > 31)
                {
                    data_dzien = 1;
                    data_miesiac += 1;
                }

                if (data_miesiac > 12)
                {
                    data_miesiac = 1;
                    data_rok += 1;
                }

                if (data_rok == int.Parse(dataDo_rok) && data_miesiac == int.Parse(dataDo_miesiac) && data_dzien == int.Parse(dataDo_dzien))
                {
                    data_rok_string = dataDo_rok[2] + "" + dataDo_rok[3];
                    data_miesiac_string = dataDo_miesiac;
                    data_dzien_string = dataDo_dzien;

                    lista_Dat.Add($"{data_rok_string}{data_miesiac_string}{data_dzien_string}");


                    break;
                }
            }
        }

        public void Generate_files()
        {
            int data_od = int.Parse(dataOd_rok);
            int data_do = int.Parse(dataDo_rok);
            if (data_od == current_year && data_do == 0 || data_do > current_year || data_od > current_year)
            {
                Download_Files("dir.txt");
                dirArr = (System.IO.File.ReadAllLines(@"C:\nbp_files\dir.txt"));
                foreach (string lines in dirArr)
                {
                    foreach (string data in lista_Dat)
                    {
                        if (lines.Contains("c") && lines.Contains(data))
                        {
                            dir.Add(lines);

                        }
                    }

                }
            }
            else
            {
                for (int i = data_od; i <= data_do; i++)
                {
                    if (i != current_year)
                    {
                        Download_Files("dir" + i + ".txt");
                        dirArr = System.IO.File.ReadAllLines(@"C:\nbp_files\dir" + i + ".txt");
                        foreach (string lines in dirArr)
                        {
                            foreach (string data in lista_Dat)
                            {
                                if (lines.Contains("c") && lines.Contains(data))
                                {
                                    dir.Add(lines);

                                }
                            }

                        }
                    }
                    else
                    {
                        Download_Files("dir.txt");
                        dirArr = (System.IO.File.ReadAllLines(@"C:\nbp_files\dir.txt"));
                        foreach (string lines in dirArr)
                        {
                            foreach (string data in lista_Dat)
                            {
                                if (lines.Contains("c") && lines.Contains(data))
                                {
                                    dir.Add(lines);

                                }
                            }

                        }
                    }

                }

            }

            foreach (string item in dir)
            {

                Download_Files(item + ".xml");
            }

        }

        public void delete_files(int del)
        {
            if (del == 1)
            {
                foreach (string lines in dir)
                {
                    System.IO.File.Delete($@"C:\nbp_files\{lines}.xml");
                }


            }
        }
    }

    public class ReadXml
    {
        public static int currency_country;
        private string read_buy_data = "";
        private string read_sell_data = "";
        public static List<double> buy_price_list = new List<double>();
        public static List<double> sell_price_list = new List<double>();
        private double avg_price = 0;
        private double max_price = 0;
        private double min_price = 0;
        private double dev_power = 0;
        public void set_country(int number_country)
        {
            if (number_country == 1)
            {
                currency_country = 0;
            }
            if (number_country == 2)
            {
                currency_country = 3;
            }
            if (number_country == 3)
            {
                currency_country = 6;
            }
            if (number_country == 4)
            {
                currency_country = 5;
            }
        }
        public void read()
        {
            for (int i = 0; i < Download.dir.Count; i++)
            {
                XmlDocument xmlRead = new XmlDocument();
                xmlRead.LoadXml(File.ReadAllText(@"C:\nbp_files\" + Download.dir[i] + ".xml", Encoding.GetEncoding("ISO-8859-1")));
                read_buy_data = xmlRead.GetElementsByTagName("pozycja").Item(currency_country).SelectNodes("kurs_kupna").Item(0).InnerText;
                buy_price_list.Add(Convert.ToDouble(read_buy_data));
                read_sell_data = xmlRead.GetElementsByTagName("pozycja").Item(currency_country).SelectNodes("kurs_sprzedazy").Item(0).InnerText;
                sell_price_list.Add(Convert.ToDouble(read_sell_data));
            }
            avg_buy_price();
            max_buy_price();
            min_buy_price();
            avg_sell_price();
            max_sell_price();
            min_sell_price();
            dev_buy();
            dev_sell();
        }
        public double avg_buy_price()
        {
            avg_price = 0;
            foreach (double price in buy_price_list)
            {
                avg_price += price;
            }
            avg_price = avg_price / buy_price_list.Count;
            return avg_price;
        }
        public double max_buy_price()
        {
            max_price = 0;
            foreach (double price in buy_price_list)
            {
                if (price > max_price)
                {
                    max_price = price;
                }
            }
            return max_price;
        }
        public double min_buy_price()
        {
            min_price = 0;
            min_price = buy_price_list[0];
            foreach (double price in buy_price_list)
            {
                if (price < min_price)
                {
                    min_price = price;
                }
            }
            return min_price;
        }
        public double avg_sell_price()
        {

            avg_price = 0;
            foreach (double price in sell_price_list)
            {
                avg_price += price;
            }
            avg_price = avg_price / sell_price_list.Count;
            return avg_price;
        }
        public double max_sell_price()
        {
            max_price = 0;
            foreach (double price in sell_price_list)
            {
                if (price > max_price)
                {
                    max_price = price;
                }
            }
            return max_price;
        }
        public double min_sell_price()
        {

            min_price = sell_price_list[0];
            foreach (double price in sell_price_list)
            {
                if (price < min_price)
                {
                    min_price = price;
                }
            }
            return min_price;
        }

        public double dev_buy()
        {
            avg_buy_price();

            foreach (double price in buy_price_list)
            {
                dev_power += Math.Pow(price - avg_price, 2);
            }
            return Math.Sqrt(dev_power / buy_price_list.Count);
        }

        public double dev_sell()
        {
            avg_sell_price();

            foreach (double price in sell_price_list)
            {
                dev_power += Math.Pow(price - avg_price, 2);
            }
            return Math.Sqrt(dev_power / sell_price_list.Count);
        }
    }
}
