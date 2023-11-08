using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Učitava se namespace radi korištenja klasa za rad sa MySql bazom podataka
using MySql.Data.MySqlClient;

namespace PrijavaKvara
{
    public partial class MainWindow : Window
    {    
        int prikaz = 0;//Promjenljiva koja označava prikaz (0 - sve prijave, 1 - Podnesene, 2 - U obradi, 3 - Riješene i 4 - Odbačene )
        MySqlConnection konekcija = null;
        
        public MainWindow()
        {
            InitializeComponent();
            napraviKonekciju();
            ucitajPrijave(0);

        }
        //Funkcija za pravljenje i otvaranje konekcije
        public void napraviKonekciju()
        {
            konekcija = new MySqlConnection("host=localhost;database=tmp_ispit;uid=root;pwd=;");
            try
            {
                konekcija.Open();
            }
            catch
            {
                MessageBox.Show("Greška prilikom otvaranja konekcije", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                //U slučaju da se ne može otvoriti konekcija, aplikacija se zatvara jer nema smisla koristiti je ako ne postoji komunikacija sa bazom podataka
                Application.Current.Shutdown();
            }
        }
        //Funkcija pomoću koje se učitavaju prijave u StackPanel
        public void ucitajPrijave(int status_pretrage)
        {
            //Prije svakog učitavanja očisti se StackPanel (Dodato radi filtriranja)
            spPrijave.Children.Clear();
            //Upit pomoću koga se pribavljaju podaci o svim prijavama da bi se prikazale u aplikaciji
            string upit = @"SELECT prijava.id, sifra, opis_problema, lokacija, odgovor_sluzbe, odgovorio, prijava.modified, prijava.created, tip.naziv AS tip, status.naziv AS status, status_id 
                            FROM prijava LEFT JOIN tip ON prijava.tip_id = tip.id 
                            LEFT JOIN status ON prijava.status_id = status.id WHERE 1=1";

            //Status pretrage se proslijeđuje funkciji prilikom poziva
            //Status pretrage može imati vrijednosti 1 (Podnesena),2 (U obradi),3 (Riješena) i 4(Odbačena)
            //Radi filtriranja upitu se dodaje odgovarajući uslov
            if (status_pretrage == 1)
                upit += " AND status_id = 1";
            else if (status_pretrage == 2)
                upit += " AND status_id = 2";
            else if (status_pretrage == 3)
                upit += " AND status_id = 3";
            else if (status_pretrage == 4)
                upit += " AND status_id = 4";

            //Prijave se sortiraju po datumu kreiranja u opadajućem redosledu što se dodaje na kraj upita
            upit += " ORDER BY prijava.created DESC";
            
            //Kreira se komanda i izvrašava se
            MySqlCommand komanda = new MySqlCommand(upit, konekcija);
            MySqlDataReader reader = komanda.ExecuteReader();

            while (reader.Read())
            {
                //Svi dobijeni podaci se smještaju u lokalne promjenljive radi lakšeg korištenja
                int id = (int)reader["id"];
                string sifra = reader["sifra"].ToString();
                string vrsta = reader["tip"].ToString();
                string opis = reader["opis_problema"].ToString();
                string lokacija = reader["lokacija"].ToString();
                string odgovorna = reader["odgovor_sluzbe"].ToString();
                string odgovorio = reader["odgovorio"].ToString();
                DateTime izmjena = (DateTime)reader["modified"];
                DateTime kreirana = (DateTime)reader["created"];
                int status_id = (int)reader["status_id"];
                string status = reader["status"].ToString();

                //Kreira se nova kontrola kojoj se prosleđuju dobijeni podaci i dodaje se na StackPanel
                ucPrijava prijava = new ucPrijava(sifra, vrsta, opis,lokacija, odgovorna, odgovorio, izmjena, kreirana, status_id, status);
                spPrijave.Children.Add(prijava);
            }
            reader.Close();
        }

        //Filtriranje prijava koje imaju status "Podnesena"
        private void btnPodnesena_Click(object sender, RoutedEventArgs e)
        {
            //Ako je prikaz==1 prikazane su samo Podnesene prijave a prikaz će promjeniti vrijednost na 0 da se zna da su prikazane sve prijave
            if (prikaz == 1) 
            {
                prikaz = 0;
                //Boja okvira (Boder-a) postavlja se na zelenu boju kako bi se znalo da filter nije uključen
                brdPodnesena.BorderBrush = Brushes.Green;
            }
            else 
            {
                prikaz = 1; //Inače se vrijednost prikaz postavlja na 1 kako bi se imala informacija o tome da su prikazane samo podnesene prijave i da bi narednim klikom mogao da se isključi filter
                brdPodnesena.BorderBrush = Brushes.Red; //Boja linije okvira (border) se postavlja na crvenu boju kako bi se vizuelno vidjelo da je filter uključen
                brdObrada.BorderBrush = Brushes.Green;
                brdRijesena.BorderBrush = Brushes.Green;
                brdOdbacena.BorderBrush = Brushes.Green;
            }
            ucitajPrijave(prikaz); //Učitavaju se prijave i prosleđuje se vrijednost promjenljive prikaz (Učitavaju se sve - ako je 0 ili Podnesene ako je 1)
        }
        
        //Filtriranje prijava koje imaju status "U obradi"
        private void btnUObradi_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == 2) 
            {
                prikaz = 0; 
                brdObrada.BorderBrush = Brushes.Green;
            }
            else 
            {
                prikaz = 2;
                brdPodnesena.BorderBrush = Brushes.Green;
                brdObrada.BorderBrush = Brushes.Red;
                brdRijesena.BorderBrush = Brushes.Green;
                brdOdbacena.BorderBrush = Brushes.Green;
            }
            ucitajPrijave(prikaz); 
        }
        //Filtriranje prijava koje imaju status "Riješena"
        private void btnRijesena_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == 3) 
            {
                prikaz = 0; 
                brdRijesena.BorderBrush = Brushes.Green;
            }
            else 
            {
                prikaz = 3;
                brdPodnesena.BorderBrush = Brushes.Green;
                brdObrada.BorderBrush = Brushes.Green;
                brdRijesena.BorderBrush = Brushes.Red;
                brdOdbacena.BorderBrush = Brushes.Green;

            }
            ucitajPrijave(prikaz); 
        }
        //Filtriranje prijava koje imaju status "Odbačena"
        private void btnOdbacena_Click(object sender, RoutedEventArgs e)
        {
            if (prikaz == 4) 
            {
                prikaz = 0; 
                brdOdbacena.BorderBrush = Brushes.Green;
            }
            else 
            {
                prikaz = 4;
                brdPodnesena.BorderBrush = Brushes.Green;
                brdObrada.BorderBrush = Brushes.Green;
                brdRijesena.BorderBrush = Brushes.Green;
                brdOdbacena.BorderBrush = Brushes.Red;
            }
            ucitajPrijave(prikaz); 
        }
    }
}
