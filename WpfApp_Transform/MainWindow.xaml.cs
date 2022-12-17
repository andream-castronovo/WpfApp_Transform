using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;




namespace WpfApp_Transform
{
    // Programmato da Andrea Maria Castronovo - 4°I - Data inizio: 17/12/2022

    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AggiungiOggetti();
        }

        Image image;
        private void AggiungiOggetti()
        {
            Uri source; // Uri è la classe che ci consente di gestire una risorsa e il suo relativo indirizzo
            source = new Uri(@"/Immagini/fox.png", UriKind.RelativeOrAbsolute); // UriKind ci permette di definire come deve essere il percorso
            
            // La classe BitmapImage prende l'URI della risorsa
            BitmapImage bitmap = new BitmapImage(source);

            // La classe Image ha bisogno di una BitmapImage, per questo l'abbiamo creata prima
            image = new Image();
            image.Source = bitmap; // Assegnamo alla risorsa dell'Image l'immagine bitmap che abbiamo creato
            image.Margin = new Thickness(50, 50, 0, 0); // Impostiamo la sua posizione definendo gli spazi dai margini
                                                        // Thickness(Spazio da sinistra, spazio da sopra, spazio da destra, spazio da sotto)
            
            cnvForesta.Children.Add(image); // Aggiungiamo l'immagine creata come figlia della canvas

        }

        int x = 0;
        int y = 0;
        double scalaX = 1.3;
        double scalaY = 1.3;
        int gradi = 0;

        private void btnTrasla_Click(object sender, RoutedEventArgs e)
        {
            TranslateTransform translateTransform;
            translateTransform = new TranslateTransform(++x, ++y);
            image.RenderTransform = translateTransform;
        }
    }
}
