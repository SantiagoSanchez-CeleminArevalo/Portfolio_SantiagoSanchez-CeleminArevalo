using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static GesstiFFest.Gestion_Festivales;


namespace GesstiFFest
{
    public partial class Gestion_Festivales : Window
    {
        private List<Festival> festivales = new List<Festival>();
        private Festival festivalSeleccionado = null;
        private Escenario escenarioSeleccionado = null;
        private string nombre;
        private string rutaFoto;
        private string ultimaConexion;
        private List<CalendarioEscenario> calendario = new List<CalendarioEscenario>();


        public Gestion_Festivales()
        {
            InitializeComponent();
            CargarFestivales();
        }
        public void ConfigurarFestival(string nombre, string rutaFoto, string ultimaConexion)
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

        private void CargarFestivales()
        {
            try
            {
                string rutaArchivoXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosFestivales.xml");
                XDocument doc = XDocument.Load(rutaArchivoXML);

                festivales.Clear();

                var festivalesXML = doc.Descendants("festival");

                foreach (var festivalXML in festivalesXML)
                {
                    Festival festival = new Festival
                    {
                        Nombre = festivalXML.Element("nombre").Value,
                        Fecha = festivalXML.Element("fecha").Value,
                        PrecioGeneral = festivalXML.Element("minimo").Value,
                        PrecioVIP = festivalXML.Element("maximo").Value,
                        Estado = festivalXML.Element("estado").Value,
                        NormasGenerales = festivalXML.Element("restriccion").Value,
                        UrlFoto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FotosFestivales", festivalXML.Element("UrlFotoFest").Value.Trim()),
                        UrlInsta = festivalXML.Element("instagram").Value,
                        UrlFace = festivalXML.Element("facebook").Value,
                        UrlTw = festivalXML.Element("twitter").Value
                    };

                    var escenariosXML = festivalXML.Element("escenarios")?.Elements("escenario");
                    if (escenariosXML != null)
                    {
                        foreach (var escenarioXML in escenariosXML)
                        {
                            festival.Escenarios.Add(new Escenario
                            {
                                Nombre = escenarioXML.Element("nombre").Value,
                                Aforo = escenarioXML.Element("aforo").Value,
                                UrlFoto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FotosEscenarios", escenarioXML.Element("UrlFotoEscen").Value.Trim())
                            });
                        }
                    }

                    festivales.Add(festival);
                }

                var festivalesSinDuplicados = festivales
                    .GroupBy(f => f.Nombre)
                    .Select(g => g.First())
                    .ToList();

                Festivales.ItemsSource = festivalesSinDuplicados;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los festivales: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void festivales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Festivales.SelectedItem is Festival festival)
            {
                festivalSeleccionado = festival;
                txtFestivalNombre.Text = festival.Nombre;
                txtFestivalFecha.Text = festival.Fecha;
                txtPrecioGeneral.Text = festival.PrecioGeneral;
                txtPrecioVIP.Text = festival.PrecioVIP;
                cmbEstado.Text = festival.Estado;
                txtNormasGenerales.Text = festival.NormasGenerales;
                CargarEdiciones(festival);
                MostrarEscenarios(festival);
            }
        }

        private void CargarEdiciones(Festival festival)
        {
            try
            {
                cmbEdicion.Items.Clear(); 
                var ediciones = festivales.Where(f => f.Nombre == festival.Nombre).ToList();

                foreach (var edicion in ediciones)
                {
                    if (!cmbEdicion.Items.Contains(edicion.Fecha))
                    {
                        cmbEdicion.Items.Add(edicion.Fecha);
                    }
                }
                if (cmbEdicion.Items.Count > 0)
                {
                    cmbEdicion.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las ediciones: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void cmbEdicion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (festivalSeleccionado != null && cmbEdicion.SelectedItem != null)
            {
                string edicionSeleccionada = cmbEdicion.SelectedItem.ToString();
                var edicion = festivales.FirstOrDefault(f => f.Nombre == festivalSeleccionado.Nombre && f.Fecha == edicionSeleccionada);

                if (edicion != null)
                {

                    txtFestivalNombre.Text = edicion.Nombre;
                    txtFestivalFecha.Text = edicion.Fecha;
                    txtPrecioGeneral.Text = edicion.PrecioGeneral.ToString();
                    txtPrecioVIP.Text = edicion.PrecioVIP.ToString();
                    cmbEstado.Text = edicion.Estado;
                    txtNormasGenerales.Text = edicion.NormasGenerales;

                    string rutaImagen = edicion.UrlFoto;
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        BitmapImage imagen = new BitmapImage();
                        imagen.BeginInit();
                        imagen.UriSource = new Uri(rutaImagen);
                        imagen.CacheOption = BitmapCacheOption.OnLoad;
                        imagen.EndInit();
                        Foto_festi.Source = imagen;
                    }
                    else
                    {
                        Foto_festi.Source = null;
                        MessageBox.Show("No se encontró la imagen en la carpeta 'FotosFestivales'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    MostrarEscenarios(edicion);
                }
            }
        }
   


        public class Festival
        {
            public string Nombre { get; set; }
            public string Fecha { get; set; }
            public string PrecioGeneral { get; set; }
            public string PrecioVIP { get; set; }
            public string Estado { get; set; }
            public string NormasGenerales { get; set; }
            public string UrlFoto { get; set; }
            public string UrlInsta { get; set; }
            public string UrlFace { get; set; }
            public string UrlTw { get; set; }
            public List<Escenario> Escenarios { get; set; } = new List<Escenario>();
            public List<string> Fechas { get; internal set; }
        }

        public class Escenario
        {
            public string Nombre { get; set; }
            public string Aforo { get; set; }
            public string UrlFoto { get; set; }
        }


        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            if (festivalSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un festival para modificar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var resultado = MessageBox.Show("¿Está seguro de que desea modificar este festival?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFestivalNombre.Text) ||
                string.IsNullOrWhiteSpace(txtFestivalFecha.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioGeneral.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioVIP.Text) ||
                string.IsNullOrWhiteSpace(cmbEstado.Text) ||
                string.IsNullOrWhiteSpace(txtNormasGenerales.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios y no pueden contener espacios en blanco.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

         
            if (!decimal.TryParse(txtPrecioGeneral.Text, out decimal precioGeneral) || precioGeneral < 0)
            {
                MessageBox.Show("El precio general debe ser un número válido y mayor o igual a 0.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecioVIP.Text, out decimal precioVIP) || precioVIP < 0)
            {
                MessageBox.Show("El precio VIP debe ser un número válido y mayor o igual a 0.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (precioVIP <= precioGeneral)
            {
                MessageBox.Show("El precio VIP debe ser mayor que el precio general.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (!DateTime.TryParseExact(txtFestivalFecha.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha))
            {
                MessageBox.Show("La fecha debe estar en el formato 'día/mes/año'. Ejemplo: 31/12/2024", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosFestivales.xml");
                var doc = XDocument.Load(rutaArchivo);

                
                var festival = doc.Descendants("festival")
                                  .FirstOrDefault(f => f.Element("nombre")?.Value == festivalSeleccionado.Nombre
                                                    && f.Element("fecha")?.Value == festivalSeleccionado.Fecha);

                if (festival == null)
                {
                    MessageBox.Show("No se encontró el festival en el archivo XML.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

               
                festival.Element("nombre")?.SetValue(txtFestivalNombre.Text);
                festival.Element("fecha")?.SetValue(txtFestivalFecha.Text);
                festival.Element("minimo")?.SetValue(txtPrecioGeneral.Text);
                festival.Element("maximo")?.SetValue(txtPrecioVIP.Text);
                festival.Element("estado")?.SetValue(cmbEstado.Text);
                festival.Element("restriccion")?.SetValue(txtNormasGenerales.Text);

                doc.Save(rutaArchivo);

                festivalSeleccionado.Nombre = txtFestivalNombre.Text;
                festivalSeleccionado.Fecha = txtFestivalFecha.Text;
                festivalSeleccionado.PrecioGeneral = txtPrecioGeneral.Text;
                festivalSeleccionado.PrecioVIP = txtPrecioVIP.Text;
                festivalSeleccionado.Estado = cmbEstado.Text;
                festivalSeleccionado.NormasGenerales = txtNormasGenerales.Text;

                Festivales.ItemsSource = null;
                Festivales.ItemsSource = festivales
                    .GroupBy(f => f.Nombre)
                    .Select(g => g.First())
                    .ToList();

                MessageBox.Show("Festival modificado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el festival: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DarBaja_Click(object sender, RoutedEventArgs e)
        {
            if (festivalSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un festival para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este festival?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
            {
                return;
            }

            try
            {

                string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosFestivales.XML");

                XDocument documento = XDocument.Load(rutaArchivo);

                XElement nodoFestival = documento.Root.Elements("festival")
                    .FirstOrDefault(f =>
                        (string)f.Element("nombre") == festivalSeleccionado.Nombre &&
                        (string)f.Element("fecha") == festivalSeleccionado.Fecha);

                if (nodoFestival != null)
                {
                    nodoFestival.Remove(); 
                    documento.Save(rutaArchivo);

                    festivales.Remove(festivalSeleccionado);

                    Festivales.ItemsSource = null;
                    Festivales.ItemsSource = festivales
                        .GroupBy(f => f.Nombre) 
                        .Select(g => g.First()) 
                        .ToList();

                    MessageBox.Show("Festival eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("El festival no se encontró en el archivo XML.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el festival: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DarAlta_Click(object sender, RoutedEventArgs e)
        {

            AddFestival añadirFestival = new AddFestival(festivales);
            añadirFestival.ShowDialog();
            CargarFestivales();
            //this.Close();
        }
        private void Instagram_Click(object sender, RoutedEventArgs e)
        {
            if (festivalSeleccionado == null || string.IsNullOrEmpty(festivalSeleccionado.UrlInsta))
            {
                MessageBox.Show("No se ha seleccionado un festival o el enlace de Instagram no está disponible.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AbrirEnlace(festivalSeleccionado.UrlInsta);
        }

        private void Facebook_Click(object sender, RoutedEventArgs e)
        {
            if (festivalSeleccionado == null || string.IsNullOrEmpty(festivalSeleccionado.UrlFace))
            {
                MessageBox.Show("No se ha seleccionado un festival o el enlace de Facebook no está disponible.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AbrirEnlace(festivalSeleccionado.UrlFace);
        }

        private void Twitter_Click(object sender, RoutedEventArgs e)
        {
            if (festivalSeleccionado == null || string.IsNullOrEmpty(festivalSeleccionado.UrlTw))
            {
                MessageBox.Show("No se ha seleccionado un festival o el enlace de Twitter no está disponible.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AbrirEnlace(festivalSeleccionado.UrlTw);
        }

        private void AbrirEnlace(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true 
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo abrir el enlace: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnGestionEscenarios_a_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Modificar(object sender, RoutedEventArgs e)
        {

        }
        //public class CalendarioEscenario
        //{
        //  public string hora { get; set; }
        //public string Artista_fest { get; set; }
        //public string Estado { get; set; }

        //        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void bton_gestion_artistas_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_calendario_Click(object sender, RoutedEventArgs e)
        {
        }

        private void VerArtistas_Click(object sender, RoutedEventArgs e)
        {
        }

        private void txtUltimaConexion_TextChanged(object sender, TextChangedEventArgs e) { }
        public class CalendarioEscenario : INotifyPropertyChanged
        {
            private string _hora;
            private string _artistaFest;
            private string _estado;

            public string hora
            {
                get => _hora;
                set
                {
                    if (_hora != value)
                    {
                        _hora = value;
                        OnPropertyChanged(nameof(hora));
                    }
                }
            }

            public string Artista_fest
            {
                get => _artistaFest;
                set
                {
                    if (_artistaFest != value)
                    {
                        _artistaFest = value;
                        OnPropertyChanged(nameof(Artista_fest));
                    }
                }
            }

            public string Estado
            {
                get => _estado;
                set
                {
                    if (_estado != value)
                    {
                        _estado = value;
                        OnPropertyChanged(nameof(Estado));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void dataGridArtistas_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                
                ActualizarXMLCalendario(festivalSeleccionado.Nombre, festivalSeleccionado.Fecha, escenarioSeleccionado.Nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ActualizarXMLCalendario(string nombreFestival, string fechaFestival, string escenario)
        {
            try
            {
                string rutaArchivoXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosArtistas.XML");
                if (!File.Exists(rutaArchivoXML))
                {
                    MessageBox.Show("El archivo XML no existe.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                XDocument doc = XDocument.Load(rutaArchivoXML);

            
                var artistasXML = doc.Descendants("Artista")
                    .Where(a => a.Element("Nombre").Value == nombreFestival &&
                                a.Element("Fecha").Value == fechaFestival &&
                                a.Element("Escenario").Value == escenario)
                    .ToList();

         
                for (int i = 0; i < calendario.Count; i++)
                {
                    var artistaCalendario = calendario[i];

                    if (i < artistasXML.Count) 
                    {
                        var artistaXML = artistasXML[i];
                        artistaXML.Element("hora").Value = artistaCalendario.hora;
                        artistaXML.Element("Artista_fest").Value = artistaCalendario.Artista_fest;
                        artistaXML.Element("Estado").Value = artistaCalendario.Estado;
                    }
                }

              
                doc.Save(rutaArchivoXML);

                MessageBox.Show("Los cambios se han guardado correctamente en el archivo XML.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el archivo XML: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void CargarCalendario_Click(object sender, RoutedEventArgs e)
        {
            if (escenarioSeleccionado == null || string.IsNullOrEmpty(escenarioSeleccionado.Nombre))
            {
                MessageBox.Show("No se ha seleccionado escenario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            CargarCalendario(festivalSeleccionado.Nombre, festivalSeleccionado.Fecha, escenarioSeleccionado.Nombre);
        }

        private void CargarCalendario(string nombreFestival, string fechaFestival, string escenario)
        {
            
            if (escenario == null)
            {
                MessageBox.Show("Selecciona escenario para cargar el calendario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                
                string rutaArchivoXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosArtistas.XML");
                if (!File.Exists(rutaArchivoXML))
                {
                    MessageBox.Show("El archivo XML no existe.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                XDocument doc = XDocument.Load(rutaArchivoXML);

                calendario.Clear(); 
                var artistasXML = doc.Descendants("Artista")
                    .Where(a => a.Element("Nombre").Value == nombreFestival &&
                                a.Element("Fecha").Value == fechaFestival &&
                                a.Element("Escenario").Value == escenario);

                foreach (var artistaXML in artistasXML)
                {
                    CalendarioEscenario art = new CalendarioEscenario
                    {
                        hora = artistaXML.Element("hora").Value,
                        Artista_fest = artistaXML.Element("Artista_fest").Value,
                        Estado = artistaXML.Element("Estado").Value,
                    };

                    calendario.Add(art);
                }
                dataGridArtistas.ItemsSource = null;
                dataGridArtistas.ItemsSource = calendario;

                if (!calendario.Any())
                {
                    MessageBox.Show("No se encontró calendario para los criterios seleccionados.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el calendario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void escenarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGridArtistas.ItemsSource = null;
            if (EscenariosGrid.SelectedItem is Escenario escenario)
            {

                
                escenarioSeleccionado = escenario;
          
                txtEscenarioNombre.Text = escenario.Nombre;
                txtEscenarioAforo1.Text = escenario.Aforo.ToString();
                string rutaImagen = escenario.UrlFoto;
                CargarCalendario(festivalSeleccionado.Nombre, festivalSeleccionado.Fecha, escenarioSeleccionado.Nombre);

                if (System.IO.File.Exists(rutaImagen))
                {
                 
                    BitmapImage imagen = new BitmapImage();
                    imagen.BeginInit();
                    imagen.UriSource = new Uri(rutaImagen);
                    imagen.CacheOption = BitmapCacheOption.OnLoad;
                    imagen.EndInit();

                  
                    FotoEscenario.Source = imagen;
                }
                else
                {
                   
                    Foto_festi.Source = null;
                    MessageBox.Show("No se encontró la imagen en la carpeta 'FotosEscenarios'.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                txtEscenarioNombre.Text = string.Empty;
                txtEscenarioAforo1.Text = string.Empty;
                FotoEscenario.Source = null;
            }
        }


        private void MostrarEscenarios(Festival festival)
        {
            try
            {
                if (festival.Escenarios.Count > 0)
                {
                    EscenariosGrid.ItemsSource = null;
                    EscenariosGrid.ItemsSource = festival.Escenarios;
                }
                else
                {
                    EscenariosGrid.ItemsSource = null;
                    MessageBox.Show("Este festival no tiene escenarios asociados.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar los escenarios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void txtEscenarioAforo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtEscenarioNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void bt_Inicio_Click_1(object sender, RoutedEventArgs e)
        {
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Login_correcto login_Correcto = new Login_correcto();
            login_Correcto.ConfigurarUsuario(nombre, rutaFoto, ultimaConexion);
            login_Correcto.Show();
            this.Close();

        }

        private void bton_gestion_artistas_Click_1(object sender, RoutedEventArgs e)
        {
            if (Festivales.SelectedItem == null & EscenariosGrid.SelectedItem == null)
            {
                MostrarMensajeCargando("Cargando ...", "En un momento", 250);
                Gestion_Artistas art = new Gestion_Artistas();
                art.ConfigurarUsuario(nombre, rutaFoto, ultimaConexion);
                art.Show();
                this.Close();
            }
            else if (EscenariosGrid.SelectedItem == null)
            {
                MostrarMensajeCargando("Cargando ...", "En un momento", 250);
                Gestion_Artistas art = new Gestion_Artistas(festivalSeleccionado.Nombre, festivalSeleccionado.Fecha);
                art.ConfigurarUsuario(nombre, rutaFoto, ultimaConexion);
                art.Show();
                this.Close();
            }
            else
            {
                MostrarMensajeCargando("Cargando ...", "En un momento", 250);
                Gestion_Artistas artistasG = new Gestion_Artistas(festivalSeleccionado.Nombre, festivalSeleccionado.Fecha, escenarioSeleccionado
                    .Nombre);
                artistasG.ConfigurarUsuario(nombre, rutaFoto, ultimaConexion);
                artistasG.Show();
                this.Close();

            }
        }
        private void DarAltaEscenario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModificarEscenario(object sender, RoutedEventArgs e)
        {
            if (escenarioSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un escenario para modificar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var resultado = MessageBox.Show("¿Está seguro de que desea modificar este escenario?", "Confirmar Modificación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEscenarioNombre.Text) ||
                string.IsNullOrWhiteSpace(txtEscenarioAforo1.Text)
                )
            {
                MessageBox.Show("Todos los campos son obligatorios y no pueden contener espacios en blanco.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtEscenarioAforo1.Text, out int aforo) || aforo < 0)
            {
                MessageBox.Show("Todos los campos son obligatorios.\n" +
                                "- El aforo debe ser un número entero mayor o igual a 0.",
                                "Advertencia",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }
            try
            {
                string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosFestivales.xml");
                var doc = XDocument.Load(rutaArchivo);

                var festival = doc.Descendants("festival")
                                  .FirstOrDefault(f => f.Element("nombre").Value == festivalSeleccionado.Nombre
                                                    && f.Element("fecha").Value == festivalSeleccionado.Fecha);

                if (festival == null)
                {
                    MessageBox.Show("No se encontró el festival en el archivo XML.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var escenario = doc.Descendants("escenario")
                                  .FirstOrDefault(f => f.Element("nombre").Value == escenarioSeleccionado.Nombre);

                escenario.Element("nombre")?.SetValue(txtEscenarioNombre.Text);
                escenario.Element("aforo")?.SetValue(txtEscenarioAforo1.Text);

  
                doc.Save(rutaArchivo);

                escenarioSeleccionado.Nombre = txtEscenarioNombre.Text;
                escenarioSeleccionado.Aforo = txtEscenarioAforo1.Text;

                MostrarEscenarios(festivalSeleccionado);

                MessageBox.Show("Festival modificado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el festival: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBorrarEscenario(object sender, RoutedEventArgs e)
        {
            if (escenarioSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un escenario para borrar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este escenario?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                
                string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosFestivales.xml");
                var doc = XDocument.Load(rutaArchivo);

                var festival = doc.Descendants("festival")
                                   .FirstOrDefault(f => f.Element("nombre").Value == festivalSeleccionado.Nombre
                                                     && f.Element("fecha").Value == festivalSeleccionado.Fecha);

                if (festival == null)
                {
                    MessageBox.Show("No se encontró el festival en el archivo XML.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var escenario = festival.Element("escenarios")?.Descendants("escenario")
                    .FirstOrDefault(f => f.Element("nombre").Value == escenarioSeleccionado.Nombre);

                if (escenario == null)
                {
                    MessageBox.Show("El escenario no se encuentra en el festival.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                escenario.Remove();

                doc.Save(rutaArchivo);

                festivalSeleccionado.Escenarios.Remove(escenarioSeleccionado);
                MostrarEscenarios(festivalSeleccionado);

                MessageBox.Show("Escenario eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el escenario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void AyudaClick(object sender, RoutedEventArgs e)
        {
            string mensaje =
                "En esta página se pueden realizar las siguientes acciones:\n\n" +
                "1. **Gestionar festivales**:\n" +
                "   - Ver los festivales.\n" +
                "   - Modificar festivales.\n" +
                "   - Añadir festivales.\n" +
                "   - Eliminar festivales.\n\n" +
                "2. **Explorar detalles de los festivales**:\n" +
                "   - Ver los escenarios de cada festival.\n" +
                "   - Cargar el calendario de cada escenario.\n\n" +
                "3. **Gestionar artistas**:\n" +
                "   - Ver todos los artistas.\n" +
                "   - Ver los artistas de un festival seleccionado.\n" +
                "   - Ver los artistas de un festival y escenario seleccionados.\n\n" +
                "Por último:\n" +
                "- Puede volver a la página inicial.\n" +
                "- Puede consultar el calendario.\n" +
                "- Se mostrará un video con la ayuda necesaria para la utilización de esta pestaña.";


            MessageBox.Show(
                mensaje,
                "Ayuda",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            string url = "https://youtu.be/L8C9zzf2DNA";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
            return;
        }

        private void btn_calendario_Click_1(object sender, RoutedEventArgs e)
        {
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Calendario cal = new Calendario();
            cal.ConfigurarCalendario(nombre, rutaFoto, ultimaConexion);
            cal.Show();
            this.Close();

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

