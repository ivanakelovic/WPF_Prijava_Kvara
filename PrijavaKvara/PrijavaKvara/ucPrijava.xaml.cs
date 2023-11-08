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

using MySql.Data.MySqlClient;

namespace PrijavaKvara
{
    public partial class ucPrijava : UserControl
    {
        public ucPrijava()
        {
            InitializeComponent();
        }
        //Izmjenje konstruktor i omogućeno je da prihvata neke argumenta
        public ucPrijava(string sifra, string vrsta, string opis, string lokacija, string odgovorna, string odgovorio, DateTime izmjena, DateTime kreirana, int status_id, string status)
        {
            InitializeComponent();
            //Nakon inicijalizacije komponenti postavlja se odgovarajuči naslov
            lblNaslov.Content = "Prijava broj: " + sifra + ", podnesena: " + kreirana.ToString("dd.MM.yyyy hh:mm") + ", status: " + status;
            
            //Na ostalim kontrolama prikazuje se podaci dobijeni kroz konstruktor
            //Prilikom prikaza podataka ako ne postoji podatak prikazuje se "crtica"
            lblVrsta.Content = vrsta != "" ? vrsta : "-";
            lblOpis.Content = opis!="" ? opis : "-";
            lblLokacija.Content = lokacija!= "" ? lokacija : "-";
            lblOdgovorna.Content = odgovorna!=""?odgovorna : "-";
            lblOdgovorio.Content = odgovorio!=""?odgovorio : "-";
            lblZadnjaIzmjena.Content =  izmjena.ToString("dd.MM.yyyy hh:mm");

            //Ispituje se status prijave i u zavisnosti od statusa postavlja se odgovarajuća slika i boja pozadine
            switch (status_id)
            {
                case 1:
                    imgSlika.Source = new ImageSourceConverter().ConvertFromString(@"Resources\podnesena.png") as ImageSource;
                    lblNaslov.Background = Brushes.LightYellow;
                    break;
                case 2:
                    imgSlika.Source = new ImageSourceConverter().ConvertFromString(@"Resources\obrada.png") as ImageSource;
                    lblNaslov.Background = Brushes.LightSkyBlue;
                    break;
                case 3:
                    imgSlika.Source = new ImageSourceConverter().ConvertFromString(@"Resources\rijesena.png") as ImageSource;
                    lblNaslov.Background = Brushes.LightGreen;
                    break;
                case 4:
                    imgSlika.Source = new ImageSourceConverter().ConvertFromString(@"Resources\odbacena.png") as ImageSource;
                    lblNaslov.Background = Brushes.LightCoral;
                    break;
            }
        }
    }
}
