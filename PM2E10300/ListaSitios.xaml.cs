using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E10300
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaSitios : ContentPage
    {
        ObservableCollection<Sitio> sitios;

        public ListaSitios()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarSitios();
        }

        private void CargarSitios()
        {
           
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sitios.db");
            var database = new SitioDatabase(dbPath);
            sitios = new ObservableCollection<Sitio>(database.GetSitios());
            sitiosListView.ItemsSource = sitios;
        }

        private void SitioSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Sitio sitio)
            {
            }
        }

        private void EliminarSitio_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int sitioID)
            {
               
                var sitio = sitios.FirstOrDefault(s => s.ID == sitioID);
                if (sitio != null)
                {
                    var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sitios.db");
                    var database = new SitioDatabase(dbPath);
                    database.DeleteSitio(sitio);
                    sitios.Remove(sitio);
                }
            }
        }

        private async void VerMapa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Mapa());
        }
    }
}