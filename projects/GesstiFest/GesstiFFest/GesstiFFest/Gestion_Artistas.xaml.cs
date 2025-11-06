using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static GesstiFFest.Gestion_Festivales;
using static GesstiFFest.Login_correcto;

namespace GesstiFFest
{
    /// <summary>
    /// Lógica de interacción para Gestion_Artistas.xaml
    /// </summary>
    public partial class Gestion_Artistas : Window
    {
        private List<Artista> artistas = new List<Artista>();
        private Artista artistaSeleccionado = null;

        public string NombreFestival { get; set; }
        public string FechaFestival { get; set; } 
        public string Escenario { get; set; }

        private string nombre;
        private string fecha;
        private string rutaFoto;
        private string ultimaConexion;

       
        public Gestion_Artistas()
        {
            InitializeComponent();

            CargarArtistas();
        }

       
        public Gestion_Artistas(string festival, string fechafestival)
        {
            InitializeComponent();
            NombreFestival = festival;
            FechaFestival = fechafestival;

            BoxFest.Text = NombreFestival;
            TextEdic.Text = FechaFestival;

            CargarArtistas(NombreFestival,FechaFestival);

        }

      
        public Gestion_Artistas(string festival, string fechafestival, string escenario)
        {
            InitializeComponent();
            NombreFestival = festival;
            FechaFestival = fechafestival;
            Escenario = escenario;

            BoxFest.Text = NombreFestival;
            TextEdic.Text = FechaFestival;
            txtEscenario.Text = Escenario;
            CargarArtistas(NombreFestival,FechaFestival,Escenario);

        }

        public void ConfigurarUsuario(string nombre, string rutaFoto, string ultimaConexion)
        {
            NombreFestival = nombre;
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

       
        private void CargarArtistas()
        {
            try
            {
                string rutaArchivoXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosArtistas.xml");
                XDocument doc = XDocument.Load(rutaArchivoXML);

                artistas.Clear();

                var artistasXML = doc.Descendants("Artista");


                foreach (var artistaXML in artistasXML)
                {
                    Artista art = new Artista
                    {
                        Nombre = artistaXML.Element("Nombre").Value,
                        Fecha = artistaXML.Element("Fecha").Value,
                        Artista_fest = artistaXML.Element("Artista_fest").Value,
                        Genero = artistaXML.Element("Genero").Value,
                        Biografia = artistaXML.Element("Biografia").Value,
                        telfManager = artistaXML.Element("telfManager").Value,
                        correoManager = artistaXML.Element("correoManager").Value,
                        Estado = artistaXML.Element("Estado").Value,
                        Insta=artistaXML.Element("Insta").Value,
                        Face = artistaXML.Element("Face").Value,
                        Twt = artistaXML.Element("Twt").Value,
                        Direccion = artistaXML.Element("Direccion").Value,
                        telArtista = artistaXML.Element("telArtista").Value,
                        correoArtista=artistaXML.Element("correoArtista").Value,
                        Precio = artistaXML.Element("Precio").Value,
                        LugarAlojamiento=artistaXML.Element("LugarAlojamiento").Value,
                        PeticionesEspeciales = artistaXML.Element("PeticionesEspeciales").Value,
                        Escenario=artistaXML.Element("Escenario").Value,
                        hora=artistaXML.Element("hora").Value,
                        ImagenUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fotos Artistas", artistaXML.Element("ImagenUrl").Value.Trim()),
                        
                    };

                    artistas.Add(art);
                }

                listViewArtistas.ItemsSource = artistas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los artistas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

   
        private void CargarArtistas(string festival, string fecha)
        {
            try
            {
                string rutaArchivoXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosArtistas.xml");
                XDocument doc = XDocument.Load(rutaArchivoXML);

                artistas.Clear();

                var artistasXML = doc.Descendants("Festival")
                                     .Where(f => string.Equals(f.Attribute("nombre")?.Value.Trim(), festival.Trim(), StringComparison.OrdinalIgnoreCase))
                                     .Descendants("Artista");

                foreach (var artistaXML in artistasXML)
                {
                    Artista art = new Artista
                    {
                        Nombre = artistaXML.Element("Nombre")?.Value.Trim(),
                        Fecha = artistaXML.Element("Fecha")?.Value.Trim(),
                        Artista_fest = artistaXML.Element("Artista_fest")?.Value.Trim(),
                        Genero = artistaXML.Element("Genero")?.Value.Trim(),
                        Biografia = artistaXML.Element("Biografia")?.Value.Trim(),
                        telfManager = artistaXML.Element("telfManager")?.Value.Trim(),
                        correoManager = artistaXML.Element("correoManager")?.Value.Trim(),
                        Estado = artistaXML.Element("Estado")?.Value.Trim(),
                        Insta = artistaXML.Element("Insta")?.Value.Trim(),
                        Face = artistaXML.Element("Face")?.Value.Trim(),
                        Twt = artistaXML.Element("Twt")?.Value.Trim(),
                        Direccion = artistaXML.Element("Direccion")?.Value.Trim(),
                        telArtista = artistaXML.Element("telArtista")?.Value.Trim(),
                        correoArtista = artistaXML.Element("correoArtista")?.Value.Trim(),
                        Precio = artistaXML.Element("Precio")?.Value.Trim(),
                        LugarAlojamiento = artistaXML.Element("LugarAlojamiento")?.Value.Trim(),
                        PeticionesEspeciales = artistaXML.Element("PeticionesEspeciales")?.Value.Trim(),
                        Escenario = artistaXML.Element("Escenario")?.Value.Trim(),
                        hora = artistaXML.Element("hora")?.Value.Trim(),
                        ImagenUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fotos Artistas", artistaXML.Element("ImagenUrl")?.Value.Trim())
                    };

                    artistas.Add(art);
                }

                var artistasFiltrados = artistas.Where(art =>
                {
                    if (DateTime.TryParseExact(art.Fecha.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaArtista) &&
                        DateTime.TryParseExact(fecha.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaFiltro))
                    {
                        return fechaArtista == fechaFiltro;
                    }
                    return false;
                }).ToList();

                listViewArtistas.ItemsSource = artistasFiltrados;
                if (artistasFiltrados == null || artistasFiltrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron artistas para el festival seleccionado en la fecha especificada.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los artistas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

      
        private void CargarArtistas(string festival, string fecha, string escenario)
        {
            try
            {
                string rutaArchivoXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosArtistas.xml");
                XDocument doc = XDocument.Load(rutaArchivoXML);

                artistas.Clear();

                var artistasXML = doc.Descendants("Festival")
                                     .Where(f => string.Equals(f.Attribute("nombre")?.Value.Trim(), festival.Trim(), StringComparison.OrdinalIgnoreCase))
                                     .Descendants("Artista");

                foreach (var artistaXML in artistasXML)
                {
                    Artista art = new Artista
                    {
                        Nombre = artistaXML.Element("Nombre")?.Value.Trim(),
                        Fecha = artistaXML.Element("Fecha")?.Value.Trim(),
                        Artista_fest = artistaXML.Element("Artista_fest")?.Value.Trim(),
                        Genero = artistaXML.Element("Genero")?.Value.Trim(),
                        Biografia = artistaXML.Element("Biografia")?.Value.Trim(),
                        telfManager = artistaXML.Element("telfManager")?.Value.Trim(),
                        correoManager = artistaXML.Element("correoManager")?.Value.Trim(),
                        Estado = artistaXML.Element("Estado")?.Value.Trim(),
                        Insta = artistaXML.Element("Insta")?.Value.Trim(),
                        Face = artistaXML.Element("Face")?.Value.Trim(),
                        Twt = artistaXML.Element("Twt")?.Value.Trim(),
                        Direccion = artistaXML.Element("Direccion")?.Value.Trim(),
                        telArtista = artistaXML.Element("telArtista")?.Value.Trim(),
                        correoArtista = artistaXML.Element("correoArtista")?.Value.Trim(),
                        Precio = artistaXML.Element("Precio")?.Value.Trim(),
                        LugarAlojamiento = artistaXML.Element("LugarAlojamiento")?.Value.Trim(),
                        PeticionesEspeciales = artistaXML.Element("PeticionesEspeciales")?.Value.Trim(),
                        Escenario = artistaXML.Element("Escenario")?.Value.Trim(),
                        hora = artistaXML.Element("hora")?.Value.Trim(),
                        ImagenUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fotos Artistas", artistaXML.Element("ImagenUrl")?.Value.Trim())
                    };

                    artistas.Add(art);
                }

                var artistasFiltrados = artistas.Where(art =>
                {
                    bool fechaValida = DateTime.TryParseExact(art.Fecha.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaArtista) &&
                                       DateTime.TryParseExact(fecha.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fechaFiltro) &&
                                       fechaArtista == fechaFiltro;

                    bool escenarioValido = string.Equals(art.Escenario?.Trim(), escenario.Trim(), StringComparison.OrdinalIgnoreCase);

                    return fechaValida && escenarioValido;
                }).ToList();

                listViewArtistas.ItemsSource = artistasFiltrados;
                if (artistasFiltrados == null || artistasFiltrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron artistas para el festival seleccionado en la fecha especificada y en el escenario especificado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los artistas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void ListViewArtistas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewArtistas.SelectedItem is Artista artista)
            {

                artistaSeleccionado = artista;
                BoxFest.Text = artista.Nombre;
                TextEdic.Text = artista.Fecha;
                txtArtistaNombre.Text = artista.Artista_fest;
                txtArtistaGenero.Text = artista.Genero;
                txtArtistaBiografia.Text = artista.Biografia;
                txtManagerContacto.Text = artista.telfManager;
                txtManagerCorreo.Text = artista.correoManager;
                cmbEstadoArtista.Text = artista.Estado;
                txtDireccion.Text=artista.Direccion;
                txtTelefono.Text = artista.telArtista;
                txtCorreo.Text = artista.correoArtista;
                txtPrecio.Text = artista.Precio;
                txtAlojamiento.Text = artista.LugarAlojamiento;
                txtPeticiones.Text = artista.PeticionesEspeciales;
                txtEscenario.Text = artista.Escenario;
                txtHora.Text = artista.hora;
            }
            
        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            if (artistaSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un artista para modificar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var resultado = MessageBox.Show("¿Está seguro de que desea modificar este artista?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtArtistaNombre.Text) ||
                string.IsNullOrWhiteSpace(txtArtistaGenero.Text) ||
                string.IsNullOrWhiteSpace(txtArtistaBiografia.Text) ||
                string.IsNullOrWhiteSpace(txtManagerContacto.Text) ||
                string.IsNullOrWhiteSpace(txtManagerCorreo.Text) ||
                string.IsNullOrWhiteSpace(cmbEstadoArtista.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtAlojamiento.Text) ||
                string.IsNullOrWhiteSpace(txtPeticiones.Text) ||
                string.IsNullOrWhiteSpace(txtEscenario.Text) ||
                string.IsNullOrWhiteSpace(txtHora.Text)
                )
            {
                MessageBox.Show("Todos los campos son obligatorios y no pueden contener espacios en blanco.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txtManagerContacto.Text.Length != 9 || !txtManagerContacto.Text.All(char.IsDigit))
            {
                MessageBox.Show("El número de teléfono del manager no puede tener carácteres y debe contenter 9 dígitos.");
                return;
            }

            if (txtTelefono.Text.Length != 9 || !txtTelefono.Text.All(char.IsDigit))
            {
                MessageBox.Show("El número de teléfono del artista no puede tener carácteres y debe contenter 9 dígitos.");
                return;
            }

            if (!int.TryParse(txtPrecio.Text, out int precio) || precio < 0)
            {
                MessageBox.Show("El precio del artista debe ser un número y mayor de 0.");
                return;
            }

            if (!TimeSpan.TryParseExact(txtHora.Text, @"hh\:mm", null, out _))
            {
                MessageBox.Show("La hora debe tener el formato hh:mm (por ejemplo, 14:30).",
                                "Advertencia",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }


            try
            {
    
                string rutaArchivoXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosArtistas.XML");

                if (!File.Exists(rutaArchivoXML))
                {
                    MessageBox.Show("El archivo XML no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                XDocument doc = XDocument.Load(rutaArchivoXML);

                var artistaXML = doc.Descendants("Artista")
                    .FirstOrDefault(a => a.Element("Nombre")?.Value == artistaSeleccionado.Nombre &&
                                         a.Element("Fecha")?.Value == artistaSeleccionado.Fecha &&
                                         a.Element("Artista_fest")?.Value == artistaSeleccionado.Artista_fest);

                if (artistaXML != null)
                {
                  
                    artistaXML.Element("Genero").SetValue(txtArtistaGenero.Text);
                    artistaXML.Element("Biografia").SetValue(txtArtistaBiografia.Text);
                    artistaXML.Element("telfManager").SetValue(txtManagerContacto.Text);
                    artistaXML.Element("correoManager").SetValue(txtManagerCorreo.Text);
                    artistaXML.Element("Estado").SetValue(cmbEstadoArtista.Text);
                    artistaXML.Element("Direccion").SetValue(txtDireccion.Text);
                    artistaXML.Element("telArtista").SetValue(txtTelefono.Text);
                    artistaXML.Element("correoArtista").SetValue(txtCorreo.Text);
                    artistaXML.Element("Precio").SetValue(txtPrecio.Text);
                    artistaXML.Element("LugarAlojamiento").SetValue(txtAlojamiento.Text);
                    artistaXML.Element("PeticionesEspeciales").SetValue(txtPeticiones.Text);
                    artistaXML.Element("Escenario").SetValue(txtEscenario.Text);
                    artistaXML.Element("hora").SetValue(txtHora.Text);
                }

                doc.Save(rutaArchivoXML);

                artistaSeleccionado.Genero = txtArtistaGenero.Text;
                artistaSeleccionado.Biografia = txtArtistaBiografia.Text;
                artistaSeleccionado.telfManager = txtManagerContacto.Text;
                artistaSeleccionado.correoManager = txtManagerCorreo.Text;
                artistaSeleccionado.Estado = cmbEstadoArtista.Text;
                artistaSeleccionado.Direccion = txtDireccion.Text;
                artistaSeleccionado.telArtista = txtTelefono.Text;
                artistaSeleccionado.correoArtista = txtCorreo.Text;
                artistaSeleccionado.Precio = txtPrecio.Text;
                artistaSeleccionado.LugarAlojamiento = txtAlojamiento.Text;
                artistaSeleccionado.PeticionesEspeciales = txtPeticiones.Text;
                artistaSeleccionado.Escenario = txtEscenario.Text;
                artistaSeleccionado.hora = txtHora.Text;

           

                MessageBox.Show("Artista modificado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el artista: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DarBaja_Click(object sender, RoutedEventArgs e)
        {
            if (artistaSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un artista para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var resultado = MessageBox.Show("¿Está seguro de que desea eliminar este artista?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosArtistas.XML");

                XDocument doc = XDocument.Load(rutaArchivo);

                XElement festival = doc.Descendants("Festival")
                    .FirstOrDefault(f => f.Attribute("nombre").Value == artistaSeleccionado.Nombre &&
                                         f.Elements("Artista")
                                          .Any(a => a.Element("Fecha").Value == artistaSeleccionado.Fecha &&
                                                    a.Element("Artista_fest").Value == artistaSeleccionado.Artista_fest));

                if (festival != null)
                {

                    XElement artista = festival.Elements("Artista")
                        .FirstOrDefault(a => a.Element("Fecha").Value == artistaSeleccionado.Fecha &&
                                             a.Element("Artista_fest").Value == artistaSeleccionado.Artista_fest);

                    if (artista != null)
                    {
                        artista.Remove(); 
                        doc.Save(rutaArchivo);

                        artistas.Remove(artistaSeleccionado);

                        listViewArtistas.ItemsSource = null;
                        listViewArtistas.ItemsSource = artistas;

                        MessageBox.Show("Artista eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

                MessageBox.Show("No se encontró el artista en el archivo XML.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el artista: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    private void Facebook_Click(object sender, RoutedEventArgs e)
        {
            if (artistaSeleccionado == null || string.IsNullOrEmpty(artistaSeleccionado.Face))
            {
                MessageBox.Show("No se ha seleccionado un artista o el enlace de Facebook no está disponible.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AbrirEnlace(artistaSeleccionado.Face);

        }

        private void Instagram_Click(object sender, RoutedEventArgs e)
        {
            if (artistaSeleccionado == null || string.IsNullOrEmpty(artistaSeleccionado.Insta))
            {
                MessageBox.Show("No se ha seleccionado un artista o el enlace de Instagram no está disponible.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AbrirEnlace(artistaSeleccionado.Insta);

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
        private void Twitter_Click(object sender, RoutedEventArgs e)
        {
            if (artistaSeleccionado == null || string.IsNullOrEmpty(artistaSeleccionado.Twt))
            {
                MessageBox.Show("No se ha seleccionado un artista o el enlace de Twitter no está disponible.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AbrirEnlace(artistaSeleccionado.Twt);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            AddArtista nuevoArtista = new AddArtista(artistas);
            nuevoArtista.ShowDialog();
            CargarArtistas();
            listViewArtistas.ItemsSource = null;
            listViewArtistas.ItemsSource = artistas;
        }

        private void txtArtistaNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtArtistaGenero_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtArtistaBiografia_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtManagerContacto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtManagerCorreo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbEstadoArtista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtHora_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtEscenario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPeticiones_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtAlojamiento_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCorreo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtDireccion_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextEdic_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BoxFest_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_calendario_Click(object sender, RoutedEventArgs e)
        {
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Calendario cal = new Calendario();
            cal.ConfigurarCalendario(nombre, rutaFoto, ultimaConexion);
            cal.Show();
            this.Close();
        }

        private void Gestion_fest_Click(object sender, RoutedEventArgs e)
        {
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Gestion_Festivales festi = new Gestion_Festivales();
            festi.ConfigurarFestival(nombre, rutaFoto, ultimaConexion);
            festi.Show();
            this.Close();
        }

        private void bt_Inicio_Click_1(object sender, RoutedEventArgs e)
        {
            MostrarMensajeCargando("Cargando ...", "En un momento", 250);
            Login_correcto login = new Login_correcto();
            login.ConfigurarUsuario(nombre, rutaFoto, ultimaConexion);
            login.Show();
            this.Close();
        }
        
        private void bton_gestion_artistas_Clic(object sender, RoutedEventArgs e)
        {

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
            "1. Ver artistas**.\n" +
            "2. Modificar artistas.\n" +
            "3. Añadir artistas.\n" +
            "4. Eliminar artistas.\n\n" +
            "Además, se puede:\n" +
            "- Volver a la página inicial.\n" +
            "- Gestionar festivales.\n" +
            "- Ver el calendario.\n\n" +
            "Por último, se mostrará un video con la ayuda necesaria para la utilización de esta pestaña.";

            MessageBox.Show(
                mensaje,
                "Ayuda",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            string url = "https://youtu.be/8vu-MchYfeA";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

            return;
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
    public class Artista
    {
        public string Nombre { get; set; }
        public string Fecha { get; set; }
        public string Artista_fest { get; set; }
        public string Genero { get; set; }
        public string Biografia { get; set; }
        public string telfManager { get; set; }
        public string correoManager { get; set; }
        public string Estado { get; set; }
        public string Insta { get; set; }
        public string Face { get; set; }
        public string Twt { get; set; }
        public string Direccion { get; set; }
        public string telArtista { get; set; }
        public string correoArtista { get; set; }
        public string Precio { get; set; }
        public string LugarAlojamiento { get; set; }
        public string PeticionesEspeciales { get; set; }
        public string Escenario { get; set; }
        public string hora { get; set; }
        public string ImagenUrl { get; set; }

    }

  

}
