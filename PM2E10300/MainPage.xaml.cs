
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2E10300
{
    public partial class MainPage : ContentPage
    {
        string fotoPath;
        bool hasImage = false;

        public MainPage()
        {
            InitializeComponent();
            ObtenerUbicacion();
        }

        private void ObtenerUbicacion()
        {
            var location = Geolocation.GetLastKnownLocationAsync().Result;

            if (location != null)
            {
                latitudLabel.Text = $"Latitud: {location.Latitude}";
                longitudLabel.Text = $"Longitud: {location.Longitude}";
            }
            else
            {
                DisplayAlert("Advertencia", "No se pudo obtener la ubicación. Verifica que el GPS esté activado.", "OK");
            }
        }

        private async void TomarFoto_Clicked(object sender, EventArgs e)
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            if (photo != null)
            {
                fotoPath = photo.FullPath;
                fotoImage.Source = ImageSource.FromFile(fotoPath);
                hasImage = true;
            }
            else
            {
                await DisplayAlert("Error", "No se pudo capturar la foto.", "OK");
            }
        }

        private async void GuardarSitio_Clicked(object sender, EventArgs e)
        {
            var descripcion = descripcionEntry.Text;

            if (string.IsNullOrWhiteSpace(descripcion) || !hasImage)
            {
                await DisplayAlert("Error", "La descripción y la imagen son obligatorias.", "OK");
                return;
            }

            var location = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(30)
            });

            if (location == null)
            {
                await DisplayAlert("Error", "No se pudo obtener la ubicación. Verifica que el GPS esté activado.", "OK");
                return;
            }

            var sitio = new Sitio
            {
                Descripcion = descripcion,
                Latitud = location.Latitude,
                Longitud = location.Longitude,
                ImagenPath = fotoPath
            };

            // Guarda el sitio en la base de datos SQLite (ver el código de la base de datos a continuación)
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sitios.db");
            var database = new SitioDatabase(dbPath);
            database.InsertSitio(sitio);

            await DisplayAlert("Éxito", "Sitio guardado en la base de datos.", "OK");
        }

        private async void ListaSitio_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ListaSitios()); 
        }

        private void Salir_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }


    }
}
