using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace PM2E10300
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Mapa : ContentPage
	{
        Sitio sitio;

        public Mapa(Sitio sitio)
        {
            InitializeComponent();
            this.sitio = sitio;

            // Centra el mapa en la ubicación del sitio
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(sitio.Latitud, sitio.Longitud), Distance.FromMiles(1)));
        }

        public Mapa()
        {
        }

        private async void CompartirImagen_Clicked(object sender, System.EventArgs e)
        {
            if (sitio != null)
            {
                
                var mediaFile = await MediaPicker.PickPhotoAsync(new MediaPickerOptions());
                await Share.RequestAsync(new ShareFileRequest
                {
                    File = new ShareFile(mediaFile.FullPath),
                   Title = "Compartir Imagen"
                });
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Verificar si el GPS está activo
            var status = Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>().Result;
            if (status != PermissionStatus.Granted)
            {
                // El GPS no está activo, muestra una alerta
                DisplayAlert("Alerta", "El GPS no está activo. Activa el GPS para obtener una ubicación precisa.", "OK");
            }
        }
    }
}
