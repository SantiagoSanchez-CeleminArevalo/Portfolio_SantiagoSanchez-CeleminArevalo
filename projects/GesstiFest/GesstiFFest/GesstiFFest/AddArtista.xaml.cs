using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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
using System.Windows.Shapes;
using System.Xml.Linq;
using static GesstiFFest.Gestion_Festivales;

namespace GesstiFFest
{
    /// <summary>
    /// Lógica de interacción para AddArtista.xaml
    /// </summary>
    public partial class AddArtista : Window
    {
        private List<Artista> artistas;
        private List<Festival> festivalesList;
        public string NombreFestival { get; set; }
        public string Fechas { get; set; }
        public string Escenario { get; set; }   

        public AddArtista( List <Artista> artistas)
        {
            InitializeComponent();
            this.artistas = artistas;
        
            LlenarComboBoxFestivales();
            LlenarComboBoxEscenarios();
        }

        private void LlenarComboBoxFestivales()
        {
            try
            {
             
                string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosFestivales.xml");
                var doc = XDocument.Load(rutaArchivo);

             
                festivalesList = doc.Descendants("festival")
                                    .GroupBy(f => f.Element("nombre")?.Value) 
                                    .Select(g => new Festival
                                    {
                                        Nombre = g.Key,
                                        Fechas = g.Elements("fecha").Select(f => f.Value).Distinct().ToList() 
                                    })
                                   .ToList();

                cmbFestivalesNombre.Items.Clear();
                cmbFestivalesFecha.Items.Clear();

                foreach (var festival in festivalesList)
                {
                    cmbFestivalesNombre.Items.Add(festival);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los festivales: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbFestivalesNombre_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbFestivalesNombre.SelectedItem is null)
            {

                return;
            }

   
            cmbFestivalesFecha.Items.Clear();

            var festivalSeleccionado = (Festival)cmbFestivalesNombre.SelectedItem;

   
            if (festivalSeleccionado?.Fechas != null)
            {
                foreach (var fecha in festivalSeleccionado.Fechas)
                {
                    cmbFestivalesFecha.Items.Add(fecha);
                }
            }
            NombreFestival = festivalSeleccionado.Nombre;


            if (cmbFestivalesFecha.Items.Count > 0)
            {
                cmbFestivalesFecha.SelectedIndex = 0; 
            }
        }






        private void LlenarComboBoxEscenarios()
        {
            try
            {
           
                string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosFestivales.xml");
                var doc = XDocument.Load(rutaArchivo);

                var escenariosList = doc.Descendants("festival")
                                         .Descendants("escenario")
                                         .Select(e => new Escenario
                                         {
                                             Nombre = e.Element("nombre")?.Value,
                                             Aforo = e.Element("aforo")?.Value, 
                                             UrlFoto = e.Element("UrlFotoEscen")?.Value
                                         })
                                         .GroupBy(esc => esc.Nombre)
                                         .Select(g => g.First())
                                         .ToList();

                cmbEscenarios.Items.Clear();

                foreach (var escenario in escenariosList)
                {
                    cmbEscenarios.Items.Add(escenario);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los escenarios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private void cmbEscenarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      
            if (cmbEscenarios.SelectedItem is Escenario escenarioSeleccionado)
            {
           
                Escenario = escenarioSeleccionado.Nombre;
            }
            else
            {
              
                Escenario = null;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro de que deseas añadir este artista?", "Confirmar Nuevo Artista", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(cmbFestivalesNombre.Text) ||
                    string.IsNullOrWhiteSpace(cmbFestivalesFecha.Text) ||
                    string.IsNullOrWhiteSpace(txtNombreArtista.Text) ||
                    string.IsNullOrWhiteSpace(txtGeneroArtista.Text) ||
                    string.IsNullOrWhiteSpace(txtBiografia.Text) ||
                    string.IsNullOrWhiteSpace(txttelfManager.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreoManager.Text) ||
                    string.IsNullOrWhiteSpace(cmbEstadoArtista.Text) ||
                    string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                    string.IsNullOrWhiteSpace(txtTelf.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtAlojamiento.Text) ||
                    string.IsNullOrWhiteSpace(txtPeticiones.Text) ||
                    string.IsNullOrWhiteSpace(cmbEscenarios.Text) ||
                    string.IsNullOrWhiteSpace(txtHora.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios y no pueden contener espacios en blanco.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txttelfManager.Text.Length != 9 || !txttelfManager.Text.All(char.IsDigit))
                {
                    MessageBox.Show("El número de teléfono del manager no puede tener carácteres y debe contenter 9 dígitos.");
                    return;
                }

                 if(txtTelf.Text.Length != 9 || !txtTelf.Text.All(char.IsDigit))
                 {
                    MessageBox.Show("El número de teléfono del artista no puede tener carácteres y debe contenter 9 dígitos.");
                    return;
                 }

                 if(!int.TryParse(txtPrecio.Text, out int precio) || precio < 0)
                 {
                    MessageBox.Show("El precio del artista debe ser un número y mayor de 0.");
                    return;
                 }

                 if( !TimeSpan.TryParseExact(txtHora.Text, @"hh\:mm", null, out _))
                 {
                    MessageBox.Show("La hora debe tener el formato hh:mm (por ejemplo, 14:30).",
                                    "Advertencia",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }

                string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosArtistas.XML");


   
                XDocument doc;
                if (File.Exists(rutaArchivo))
                {
                    doc = XDocument.Load(rutaArchivo);
                }
                else
                {
                    doc = new XDocument(new XElement("Festivales"));
                }

                XElement festival = doc.Root.Elements("Festival")
                    .FirstOrDefault(f => f.Attribute("nombre")?.Value == NombreFestival);

                if (festival == null)
                {
                    festival = new XElement("Festival", new XAttribute("nombre", NombreFestival));
                    doc.Root.Add(festival);
                }

      
                XElement nuevoArtista = new XElement("Artista",
                    new XElement("Nombre", NombreFestival),
                    new XElement("Fecha", cmbFestivalesFecha.Text),
                    new XElement("Artista_fest", txtNombreArtista.Text),
                    new XElement("Genero", txtGeneroArtista.Text),
                    new XElement("Biografia", txtBiografia.Text),
                    new XElement("telfManager", txttelfManager.Text),
                    new XElement("correoManager", txtCorreoManager.Text),
                    new XElement("Estado", cmbEstadoArtista.Text),
                    new XElement("Insta", txtInsta.Text),
                    new XElement("Face", txtFace.Text),
                    new XElement("Twt", txtTwt.Text),
                    new XElement("Direccion", txtDireccion.Text),
                    new XElement("telArtista", txtTelf.Text),
                    new XElement("correoArtista", txtCorreo.Text),
                    new XElement("Precio", txtPrecio.Text),
                    new XElement("LugarAlojamiento", txtAlojamiento.Text),
                    new XElement("PeticionesEspeciales", txtPeticiones.Text),
                    new XElement("Escenario", Escenario),
                    new XElement("hora", txtHora.Text),
                    new XElement("ImagenUrl", txtImagen.Text)
                 );

       
                festival.Add(nuevoArtista);


                doc.Save(rutaArchivo);

                MessageBox.Show("Artista agregado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);


                Gestion_Artistas gestion_Artistas = new Gestion_Artistas();
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el artista: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CargarFoto_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
    
                string selectedFile = openFileDialog.FileName;

                string destinationFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FotosArtistas");
                if (!System.IO.Directory.Exists(destinationFolder))
                {
                    System.IO.Directory.CreateDirectory(destinationFolder); 
                }


                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(selectedFile);
                string fileName = fileNameWithoutExtension + ".jpg";

                string destinationPath = System.IO.Path.Combine(destinationFolder, fileName);


                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(selectedFile));

                    using (FileStream stream = new FileStream(destinationPath, FileMode.Create))
                    {
                       
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));

                    
                        encoder.Save(stream);
                    }

                    
                    txtImagen.Text = fileName;  
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al convertir la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtNombreArtista_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtGeneroArtista_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBiografia_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txttelfManager_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCorreoManager_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbEstadoArtista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtInsta_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtFace_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtTwt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtDireccion_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtTelf_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCorreo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtImagen_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPrecio_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtAlojamiento_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPeticiones_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtEscenario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtHora_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void cmbFestivales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void cmbFestivalesFecha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
