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
using System.Windows.Threading;



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
            SetupTimer();
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
        
        DispatcherTimer _dt;

        void SetupTimer()
        {
            _dt = new DispatcherTimer();
            _dt.Interval = TimeSpan.FromMilliseconds(50);
            _dt.Tick += new EventHandler(DispatcherTimer_Tick);
            _dt.Start();
        }

        void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            
            // Translate
            if (btnTranslateUp.IsPressed)
                y -= deltaY;
            
            else if (btnTranslateDown.IsPressed)
                y += deltaY;
            
            if (btnTranslateLeft.IsPressed)
            {
                x -= deltaX;
                destra = false;
            }
            else if (btnTranslateRight.IsPressed)
            {
                x += deltaX;
                destra = true;
            }

            // Scale
            if (btnScaleUp.IsPressed)
            {
                scalaY += deltaScaleY;
                scalaX += deltaScaleX;

            }
            else if (btnScaleDown.IsPressed)
            {
                scalaY -= deltaScaleY;
                scalaX -= deltaScaleX;
            }

            // Rotate

            if (btnRotateLeft.IsPressed)
                gradi -= 1;
            else if (btnRotateRight.IsPressed)
                gradi += 1;



            Aggiorna(image);
        }

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

            //Console.WriteLine($"Scala: {scalaX}, {scalaY} Coordinate: {x}, {y} Rotazione: {gradi}° intorno a {x}, {y}");


            toRender.RenderTransform = transformGroup;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            x = 0; 
            y = 0; 
            gradi = 0; 
            scalaX = 0.3; 
            scalaY = 0.3;
            destra= false;

            Aggiorna(image);
        }

    }
}
