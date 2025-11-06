using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace GesstiFFest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_acceder_Click(object sender, RoutedEventArgs e)
        {
            string correoElectronico = Txt_box_Correo.Text;
            string contraseña = txt_box_Contraseña.Password;
            string rutaXml = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuarios.xml");

            var (esValido, nombre, rutaFoto, ultimaConexion) = ValidarUsuarioDesdeXml(correoElectronico, contraseña, rutaXml);

            if (esValido)
            {
              
                Login_correcto login_Correcto = new Login_correcto();
                login_Correcto.ConfigurarUsuario(nombre, rutaFoto, ultimaConexion);
                login_Correcto.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrectos. Intente nuevamente.");
            }
        }


        private (bool, string, string, string) ValidarUsuarioDesdeXml(string correo, string contraseña, string rutaXml)
        {
            try
            {
                if (!File.Exists(rutaXml))
                {
                    MessageBox.Show("El archivo de usuarios no se encontró.");
                    return (false, null, null, null);
                }

                XDocument xmlDoc = XDocument.Load(rutaXml);
                var usuario = xmlDoc.Descendants("usuario")
                                    .FirstOrDefault(u => u.Element("correo")?.Value == correo &&
                                                         u.Element("contraseña")?.Value == contraseña);

                if (usuario != null)
                {
                    string nombre = usuario.Element("nombre")?.Value;
                    string rutaFoto = usuario.Element("UrlFoto")?.Value;
                    string ultimaConexion = usuario.Element("ultimaConexion")?.Value;

                    ultimaConexion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    usuario.Element("ultimaConexion").Value = ultimaConexion;
                    xmlDoc.Save(rutaXml);

                    return (true, nombre, rutaFoto, ultimaConexion);
                }

                return (false, null, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer o actualizar el archivo XML: " + ex.Message);
                return (false, null, null, null);
            }
        }

        private void btn_registrarse_Click(object sender, RoutedEventArgs e)
        {
         //   Ventana_registro ventana_Regi = new Ventana_registro();
           // ventana_Regi.Show();
        }

        private void Txt_box_Correo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txt_box_Contraseña.IsEnabled = true;
                txt_box_Contraseña.Focus();
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            string rutaXml = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuarios.xml");

            if (File.Exists(rutaXml))
            {
                try
                {
                    string correoIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                        "Ingrese su correo electrónico para recuperar la contraseña:",
                        "Recuperar Contraseña",
                        "");

                    if (string.IsNullOrEmpty(correoIngresado))
                    {
                        MessageBox.Show("Debe ingresar un correo electrónico.",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                        return;
                    }
                    XDocument xmlDoc = XDocument.Load(rutaXml);
                    var usuario = xmlDoc.Descendants("usuario")
                        .FirstOrDefault(u => u.Element("correo")?.Value == correoIngresado);

                    if (usuario != null)
                    {
                        string nombreIngresado = Microsoft.VisualBasic.Interaction.InputBox(
                            "Ingrese su nombre para confirmar su identidad:",
                            "Verificación de Identidad",
                            "");

                        string nombreRegistrado = usuario.Element("nombre")?.Value;

                        if (nombreIngresado == nombreRegistrado)
                        {
                            string contraseña = usuario.Element("contraseña")?.Value;
                            MessageBox.Show($"Tu contraseña es: {contraseña}",
                                            "Recuperación Exitosa",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("El nombre ingresado no coincide con el registrado.",
                                            "Error de Verificación",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El correo ingresado no está registrado.",
                                        "Error: Usuario no encontrado",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar la solicitud: " + ex.Message,
                                    "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("El archivo de credenciales no existe.",
                                "Error: Archivo no encontrado",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }



    }
}
