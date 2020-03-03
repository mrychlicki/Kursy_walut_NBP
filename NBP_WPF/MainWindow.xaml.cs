using NBP_console;
using System.Windows;

namespace NBP_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int country_number;
        string data_Od = "";
        string data_Do = "";


        Download df = new Download();
        ReadXml readXml = new ReadXml();

        public MainWindow()
        {

            InitializeComponent();


        }

        private void fn1()
        {
            Waluta_nazwa.Visibility = Visibility;
            max_kup.Visibility = Visibility;
            min_kup.Visibility = Visibility;
            srednia.Visibility = Visibility;
            max_buy_price.Visibility = Visibility;
            min_buy_price.Visibility = Visibility;
            avg_buy.Visibility = Visibility;
            waluta.Visibility = Visibility;
            Max_sell_l.Visibility = Visibility;
            max_sell.Visibility = Visibility;
            Min_sell_l.Visibility = Visibility;
            min_sell.Visibility = Visibility;
            avg_sell.Visibility = Visibility;
            avg_sell_l.Visibility = Visibility;
            dev_sell.Visibility = Visibility;
            dev_sell_label.Visibility = Visibility;
            dev_buy_label.Visibility = Visibility;
            dev_buy.Visibility = Visibility;
            delete.Visibility = Visibility;

            readXml.set_country(country_number);
            readXml.read();
            avg_buy.Content = readXml.avg_buy_price();
            max_buy_price.Content = readXml.max_buy_price();
            min_buy_price.Content = readXml.min_buy_price();
            min_sell.Content = readXml.min_sell_price();
            max_sell.Content = readXml.max_sell_price();
            avg_sell.Content = readXml.avg_sell_price();
            dev_buy.Content = readXml.dev_buy();
            dev_sell.Content = readXml.dev_sell();

            if (country_number == 1)
            {
                waluta.Content = "USD";
            }
            if (country_number == 2)
            {
                waluta.Content = "EUR";
            }
            if (country_number == 3)
            {
                waluta.Content = "GBP";
            }
            if (country_number == 4)
            {
                waluta.Content = "CHF";
            }
        }



        private void usd_click(object sender, RoutedEventArgs e)
        {
            country_number = 1;
            ReadXml.buy_price_list.Clear();
            ReadXml.sell_price_list.Clear();
            fn1();
        }

        private void euro_click(object sender, RoutedEventArgs e)
        {
            country_number = 2;
            ReadXml.buy_price_list.Clear();
            ReadXml.sell_price_list.Clear();
            fn1();
        }

        private void chf_click(object sender, RoutedEventArgs e)
        {
            ReadXml.currency_country = 5;
            ReadXml.buy_price_list.Clear();
            ReadXml.sell_price_list.Clear();
            fn1();
        }

        private void gbp_click(object sender, RoutedEventArgs e)
        {
            country_number = 3;
            ReadXml.buy_price_list.Clear();
            ReadXml.sell_price_list.Clear();
            fn1();
        }

        private void chf(object sender, RoutedEventArgs e)
        {
            country_number = 4;
            ReadXml.buy_price_list.Clear();
            ReadXml.sell_price_list.Clear();
            fn1();
        }

        private void send(object sender, RoutedEventArgs e)
        {

            data_Od = dataOd.Text;
            data_Do = dataDo.Text;

            df.data(data_Od + " " + data_Do);
            MessageBox.Show("Pobieranie danych z NBP. Przy dużym okresie czasu może to chwilę potrwać");

            df.Generate_files();
            usd.Visibility = Visibility;
            euro.Visibility = Visibility;
            gbp.Visibility = Visibility;
            chf_name.Visibility = Visibility;
        }

        private void delete_files(object sender, RoutedEventArgs e)
        {
            df.delete_files(1);
            this.Close();
        }
    }
}
