using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para AddFestival.xaml
    /// </summary>
    public partial class AddFestival : Window
    {
        private List<Festival> festivales;
        private List<Escenario> escenarios;
   
        public AddFestival(List<Festival> festivales)
        {
            InitializeComponent();

            this.festivales = festivales;
            LlenarListBoxEscenarios();

        }
        private void LlenarListBoxEscenarios()
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

                lstEscenarios.Items.Clear();

              
                foreach (var escenario in escenariosList)
                {
                    lstEscenarios.Items.Add(escenario);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los escenarios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Está seguro de que deseas añadir este Festival?", "Confirmar Nuevo Festival", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.No)
            {
                return;
            }
            try
            {
          
                if (string.IsNullOrWhiteSpace(txtFestivalNombre.Text) ||
                    string.IsNullOrWhiteSpace(FechaSelect.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecioGeneral.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecioVIP.Text) ||
                    cmbEstado.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtFacebook.Text) ||
                    string.IsNullOrWhiteSpace(txtInstagram.Text) ||
                    string.IsNullOrWhiteSpace(txtTwitter.Text) ||
                    string.IsNullOrWhiteSpace(txtReglas.Text) ||
                    string.IsNullOrWhiteSpace(txtFoto.Text))
                {
                    MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (lstEscenarios.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Por favor, selecciona al menos un escenario.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; 
                }

              
                if (!decimal.TryParse(txtPrecioGeneral.Text, out decimal precioGeneral) || precioGeneral < 0)
                {
                    MessageBox.Show("El precio general debe ser un número válido y mayor o igual a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrecioVIP.Text, out decimal precioVIP) || precioVIP < 0)
                {
                    MessageBox.Show("El precio VIP debe ser un número válido y mayor o igual a 0.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (precioVIP <= precioGeneral)
                {
                    MessageBox.Show("El precio VIP debe ser mayor que el precio general.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


               
                var nuevoFestival = new Festival
                {
                    Nombre = txtFestivalNombre.Text,
                    Fecha = FechaSelect.Text,
                    PrecioGeneral = txtPrecioGeneral.Text,
                    PrecioVIP = txtPrecioVIP.Text,
                    Estado = cmbEstado.Text,
                    NormasGenerales = txtReglas.Text,
                    UrlFoto = txtFoto.Text,
                    UrlInsta = txtInstagram.Text,
                    UrlFace = txtFacebook.Text,
                    UrlTw = txtTwitter.Text
                };

   
                festivales.Add(nuevoFestival);

                string rutaArchivoXml = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosFestivales.xml");

                XDocument xmlDoc;
                if (File.Exists(rutaArchivoXml))
                {
                    xmlDoc = XDocument.Load(rutaArchivoXml);
                }
                else
                {

                    xmlDoc = new XDocument(new XElement("festivales"));
                }


                XElement nuevoFestivalElemento = new XElement("festival",
                    new XElement("nombre", nuevoFestival.Nombre),
                    new XElement("fecha", nuevoFestival.Fecha),
                    new XElement("minimo", 0), 
                    new XElement("maximo", 0), 
                    new XElement("estado", nuevoFestival.Estado),
                    new XElement("restriccion", nuevoFestival.NormasGenerales),
                    new XElement("UrlFotoFest", nuevoFestival.UrlFoto),
                    new XElement("instagram", nuevoFestival.UrlInsta),
                    new XElement("facebook", nuevoFestival.UrlFace),
                    new XElement("twitter", nuevoFestival.UrlTw),
                    new XElement("escenarios")
                );

                foreach (var escenario in lstEscenarios.SelectedItems)
                {
       
                    var escenarioSeleccionado = (Escenario)escenario;

                    var escenarioItem = new XElement("escenario",
                        new XElement("nombre", escenarioSeleccionado.Nombre),
                        new XElement("aforo", escenarioSeleccionado.Aforo),
                        new XElement("UrlFotoEscen", escenarioSeleccionado.UrlFoto)
                    );

                    nuevoFestivalElemento.Element("escenarios").Add(escenarioItem);

                    }


                xmlDoc.Root.Add(nuevoFestivalElemento);

                xmlDoc.Save(rutaArchivoXml);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el festival: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private void txtFestivalNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtFecha_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPrecioGeneral_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPrecioVIP_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtFacebook_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtInstagram_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtTwitter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtReglas_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtFoto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtReglas_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void txtFestivalNombre_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void txtReglas_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }
        private void CargarFoto_Click(object sender, RoutedEventArgs e)
        {
         
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
      
                string selectedFile = openFileDialog.FileName;

          
                string destinationFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FotosFestivales");
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

                    txtFoto.Text = fileName; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al convertir la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void lstEscenarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

