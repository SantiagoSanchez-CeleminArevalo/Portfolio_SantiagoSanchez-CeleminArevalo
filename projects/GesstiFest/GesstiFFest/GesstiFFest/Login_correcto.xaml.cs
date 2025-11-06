using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace GesstiFFest
{
    public partial class Login_correcto : Window
    {
        private string nombre;
        private string rutaFoto;
        private string ultimaConexion;

        public Login_correcto()
        {
            InitializeComponent();
        }

        public void ConfigurarUsuario(string nombre, string rutaFoto, string ultimaConexion)
        {
            this.nombre = nombre;
            this.rutaFoto = rutaFoto;
            this.ultimaConexion = ultimaConexion;

            txtNombreUsuario.Text = nombre;
            txtUltimaConexion.Text = "Última conexión: " + ultimaConexion;

            CargarFotoUsuario();
        }

        private void CargarFotoUsuario()
        {
 
            if (!string.IsNullOrEmpty(rutaFoto))
            {
                try
                {
                   
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string fullPath = Path.Combine(basePath, rutaFoto); 

                    if (File.Exists(fullPath))
                    {
                      
                        BitmapImage imagen = new BitmapImage();
                        imagen.BeginInit();
                        imagen.UriSource = new Uri(fullPath, UriKind.Absolute); 
                        imagen.CacheOption = BitmapCacheOption.OnLoad;
                        imagen.EndInit();
                        Foto_persona.Source = imagen;
                    }
                    else
                    {
                        MessageBox.Show($"La foto del usuario no se encontró en: {fullPath}", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                
                    MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
               
                MessageBox.Show($"La ruta de la foto está vacía", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void bt_Inicio_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_calendario_Click_1(object sender, RoutedEventArgs e)
        {
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Calendario cal = new Calendario();
            cal.ConfigurarCalendario(nombre, rutaFoto, ultimaConexion);
            cal.Show();
            this.Close();
        }

      
        private void bton_gestion_artistas_Click_1(object sender, RoutedEventArgs e)
        {
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Gestion_Artistas artistas = new Gestion_Artistas();
            artistas.ConfigurarUsuario(nombre, rutaFoto, ultimaConexion);
            artistas.Show();
            this.Close();
        }

      
        private void Gestion_fest_Click_2(object sender, RoutedEventArgs e)
        {

            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
          
            Gestion_Festivales gest_fest = new Gestion_Festivales();
            gest_fest.ConfigurarFestival(nombre, rutaFoto, ultimaConexion);
            gest_fest.Show();

            this.Close();
        }

        private void AyudaClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se encuentra en la página inicial, para continuar haga lo siguiente: \n Si desea Gestionar los festivales pulse sobre Gestión de Festivales. \n Si desea gestionar los artistas pulse sobre el botón Gestión Artistas. \n Si desea ver el Calendario pulse sobre el botón Calendario. \n Si por lo contrario desea cerrar su sesión pulse en el botón cercano a su usario.", "Ayuda", MessageBoxButton.OK, MessageBoxImage.Question);
            return;
        }

        private void CerrarSesionClick(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro de que desea cerrar sesión?", "Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
            {
                return;
            }
            MainWindow inicio = new MainWindow();
            inicio.Show();
            this.Close();
        }
        private void MostrarMensajeCargando(string mensaje, string titulo, int milisegundos)
        {
           
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((_) =>
            {
           
                IntPtr hWnd = FindWindow(null, titulo);
                if (hWnd != IntPtr.Zero)
                {
                    SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }

        
                timer.Dispose();
            }, null, milisegundos, Timeout.Infinite);

    
            MessageBox.Show(mensaje, titulo);
        }

       
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_CLOSE = 0x0010;

    }
}
