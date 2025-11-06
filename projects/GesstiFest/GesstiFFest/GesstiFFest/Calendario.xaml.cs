using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static GesstiFFest.Gestion_Festivales;

namespace GesstiFFest
{
    public partial class Calendario : Window
    {
        private string nombre;
        private string rutaFoto;
        private string ultimaConexion;
        private int CurrentYear = DateTime.Now.Year;
        private int CurrentMonth = DateTime.Now.Month;
        private List<Festival> festivales = new List<Festival>();

        public Calendario()
        {
            InitializeComponent();
            CargarFestivales();
            InitializeCalendar();
            
        }

        private void InitializeCalendar()
        {
            FillYearComboBox();  
            MonthComboBox.SelectedIndex = CurrentMonth - 1;  
            YearComboBox.SelectedItem = CurrentYear.ToString();  
            UpdateCalendar();  
        }

        private void FillYearComboBox()
        {
            try
            {
                YearComboBox.Items.Clear();
                for (int year = 2015; year <= 2028; year++)
                {
                    YearComboBox.Items.Add(year.ToString());
                }
                YearComboBox.SelectedItem = CurrentYear.ToString();
                if (YearComboBox.Items.Count == 0)
                {
                    MessageBox.Show("No se cargaron años en el ComboBox.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al llenar el ComboBox de años: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateCalendar()
        {
            DaysGrid.Children.Clear();
            FillDays(CurrentYear, CurrentMonth);
        }

        private void FillDays(int year, int month)
        {
            DaysGrid.Children.Clear();  

            int daysInMonth = DateTime.DaysInMonth(year, month);  
            DayOfWeek firstDayOfWeek = new DateTime(year, month, 1).DayOfWeek; 
            int emptyDays = (int)firstDayOfWeek;  

            for (int i = 0; i < emptyDays; i++)
            {
                DaysGrid.Children.Add(new TextBlock());
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                Button dayButton = new Button
                {
                    Tag = day,
                    Margin = new Thickness(2),
                    Height = 100, 
                    VerticalAlignment = VerticalAlignment.Top,
                    ClipToBounds = true
                };

                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                var festivalesDelDia = festivales.Where(f =>
                {
                    return DateTime.TryParse(f.Fecha, out DateTime fecha) && fecha.Date == new DateTime(year, month, day).Date;
                }).ToList();

                if (festivalesDelDia.Any())
                {

                    var festival = festivalesDelDia.First();

                    dayButton.Background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(festival.UrlFoto, UriKind.Absolute)),
                        Stretch = Stretch.Fill
                    };

                    TextBlock dayText = new TextBlock
                    {
                        Text = day.ToString(),
                        FontSize = 14,
                        FontWeight = FontWeights.Bold,
                        Foreground = Brushes.Black,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 5, 0, 0)
                    };
                    stackPanel.Children.Add(dayText);

                
                    TextBlock festivalName = new TextBlock
                    {
                        Text = festival.Nombre,
                        TextAlignment = TextAlignment.Center,
                        FontWeight = FontWeights.Bold,
                        FontSize = 10,
                        Foreground = Brushes.Blue,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    stackPanel.Children.Add(festivalName);
                }
                else
                {
               
                    TextBlock dayText = new TextBlock
                    {
                        Text = day.ToString(),
                        FontSize = 12,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Foreground = Brushes.Black
                    };
                    stackPanel.Children.Add(dayText);
                }

      
                dayButton.Content = stackPanel;

                DayOfWeek dayOfWeek = new DateTime(year, month, day).DayOfWeek;
                if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
                {
                    dayButton.Foreground = Brushes.Red;  
                }

       
                DaysGrid.Children.Add(dayButton);
            }
        }





        private void MonthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonthComboBox.SelectedItem != null)
            {
                CurrentMonth = MonthComboBox.SelectedIndex + 1;
                UpdateCalendar();
            }
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (YearComboBox.SelectedItem != null)
            {
                CurrentYear = int.Parse(YearComboBox.SelectedItem.ToString());
                UpdateCalendar();
            }
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            MessageBox.Show($"Día seleccionado: {button.Tag}/{CurrentMonth}/{CurrentYear}");
        }

        private void MostrarEventos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Función de mostrar eventos pendiente de implementar.");
        }

        public void ConfigurarCalendario(string nombre, string rutaFoto, string ultimaConexion)
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
                    string fullPath = System.IO.Path.Combine(basePath, rutaFoto);

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
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Login_correcto inicio = new Login_correcto();
            inicio.ConfigurarUsuario(nombre, rutaFoto, ultimaConexion);
            inicio.Show();
            this.Close();
        }

        private void Gestion_fest_Click_2(object sender, RoutedEventArgs e)
        {
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Gestion_Festivales fest = new Gestion_Festivales();
            fest.ConfigurarFestival(nombre, rutaFoto, ultimaConexion);
            fest.Show();
            this.Close();
        }

        private void bton_gestion_artistas_Click_1(object sender, RoutedEventArgs e)
        {
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Gestion_Artistas art = new Gestion_Artistas();
            art.ConfigurarUsuario(nombre, rutaFoto, ultimaConexion);
            art.Show();
            this.Close();
        }

        private void AyudaClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Se encuentra en el calendario, aqui podrá ver todos los festivales que programados independientemente del estado en el que se encuentren. ", "Ayuda", MessageBoxButton.OK, MessageBoxImage.Question);
                
            string url = "https://youtu.be/wUyKENQOHKE";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
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

        private void CargarFestivales()
        {
            try
            {
                string rutaArchivoXML = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosFestivales.xml");
                XDocument doc = XDocument.Load(rutaArchivoXML);

                var festivalesXML = doc.Descendants("festival");

                foreach (var festivalXML in festivalesXML)
                {
                    string fechaStr = festivalXML.Element("fecha")?.Value;

                    if (string.IsNullOrEmpty(fechaStr))
                    {
                        MessageBox.Show("¡Advertencia! Se encontró un festival con fecha vacía.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        continue; 
                    }
                    if (DateTime.TryParse(fechaStr, out DateTime fechaFestival))
                    {
               
                        Festival festival = new Festival
                        {
                            Nombre = festivalXML.Element("nombre").Value,
                            Fecha = fechaStr, 
                            PrecioGeneral = festivalXML.Element("minimo").Value,
                            PrecioVIP = festivalXML.Element("maximo").Value,
                            Estado = festivalXML.Element("estado").Value,
                            NormasGenerales = festivalXML.Element("restriccion").Value,
                            UrlFoto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FotosFestivales", festivalXML.Element("UrlFotoFest").Value.Trim()),
                            UrlInsta = festivalXML.Element("instagram").Value,
                            UrlFace = festivalXML.Element("facebook").Value,
                            UrlTw = festivalXML.Element("twitter").Value
                        };

                        festivales.Add(festival);
                    }
                    else
                    {
                        MessageBox.Show($"La fecha del festival '{festivalXML.Element("nombre").Value}' no es válida: {fechaStr}", "Error de formato de fecha", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los festivales: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void MostrarMensajeCargando(string mensaje, string titulo, int milisegundos)
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
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public const uint WM_CLOSE = 0x0010;
    }

}
