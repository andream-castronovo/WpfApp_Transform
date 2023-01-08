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
        
        int x = 0;
        int y = 0;
        bool destra = false;

        double gradi = 0.0;
        
        double scalaX = 0.3;
        double scalaY = 0.3;

        const int deltaX = 2;
        const int deltaY = 2;

        const double deltaScaleX = 0.01;
        const double deltaScaleY = 0.01;

        /// <summary>
        /// Aggiungi gli oggetti allo schermo
        /// </summary>
        private void AggiungiOggetti()
        {
            Uri source; // Uri è la classe che ci consente di gestire una risorsa e il suo relativo indirizzo
            source = new Uri(@"/Immagini/fox.png", UriKind.RelativeOrAbsolute); // UriKind ci permette di definire come deve essere il percorso
            
            // La classe BitmapImage prende l'URI della risorsa
            BitmapImage bitmap = new BitmapImage(source);

            // La classe Image ha bisogno di una BitmapImage, per questo l'abbiamo creata prima
            image = new Image();
            image.Source = bitmap; // Assegnamo alla risorsa dell'Image l'immagine bitmap che abbiamo creato
    
            ScaleTransform st = new ScaleTransform(scalaX, scalaY);
            image.RenderTransform = st;

            image.Width = 1000;
            image.Height = 500;


            cnvForesta.Children.Add(image); // Aggiungiamo l'immagine creata come figlia della canvas

        }

        /// <summary>
        /// Aggiornamento dello schermo
        /// </summary>
        /// <param name="toRender">Immagine da aggiornare</param>
        void Aggiorna(Image toRender)
        {
            TransformGroup transformGroup = new TransformGroup();

            transformGroup.Children.Add(
                new ScaleTransform(
                    destra ? -scalaX : scalaX, 
                    scalaY,
                    x,
                    y
                    )
                );
            transformGroup.Children.Add(
                new TranslateTransform(
                    destra ? x + image.Width * scalaX : x, 
                    y
                    )
                );
            transformGroup.Children.Add(
                new RotateTransform(
                    gradi,
                    x + (image.Width * scalaX) / 2,
                    y + (image.Height * scalaY) / 2
                    )
                );


            //if (destra)
            //    transformGroup.Children.Add(
            //        new ScaleTransform(-scalaX, scalaY)
            //        );
            //else
            //    transformGroup.Children.Add(
            //        new ScaleTransform(scalaX, scalaY)
            //        );

            Console.WriteLine($"Scala: {scalaX}, {scalaY} Coordinate: {x}, {y} Rotazione: {gradi}° intorno a {x}, {y}");


            toRender.RenderTransform = transformGroup;

        }


        #region Metodi bottoni

        /// <summary>
        /// Bottoni che controllano lo spostamento dello sprite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Spostamento(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Tag)
            {
                case "Up":
                    y -= deltaY;
                    break;
                case "Down":
                    y += deltaY;
                    break;
                case "Left":
                    x -= deltaX;
                    destra = false;
                    break;
                case "Right":
                    x += deltaX;
                    destra = true;
                    break;
            }

            Aggiorna(image);
        }

        /// <summary>
        /// Bottoni che controllano la grandezza dello sprite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Scale(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Tag)
            {
                case "Up":
                    scalaY += deltaScaleY;
                    scalaX += deltaScaleX;
                    break;
                case "Down":
                    scalaY -= deltaScaleY;
                    scalaX -= deltaScaleX;
                    break;
            }

            Aggiorna(image);
        }

        /// <summary>
        /// Bottoni che controllano la rotazione dello sprite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Rotate(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;


            switch (btn.Tag)
            {
                case "Left":
                    gradi -= 1;
                    break;
                case "Right":
                    gradi += 1;
                    break;
            }
            Aggiorna(image);
        }
        
        #endregion

    }
}
