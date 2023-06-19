using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SoftSat.Pages;

public partial class ConocimientosPage : ContentPage
{
    private const string ApiUrl = "https://apisat.hsoft.es/api/BaseConocimientos";

    public ConocimientosPage()
    {
        InitializeComponent();
        LoadConocimientosAsync();
    }

    private async Task LoadConocimientosAsync()
    {
        // Obtener el token de autenticación del almacenamiento seguro local
        var accessToken = await SecureStorage.GetAsync("AccessToken");

        // Crear un objeto HttpClient para llamar a la API
        using var httpClient = new HttpClient();

        // Agregar el token de autenticación a la cabecera de autorización
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

        // Llamar a la API para obtener los conocimientos
        var response = await httpClient.GetAsync(ApiUrl);
        var result = await response.Content.ReadFromJsonAsync<List<Conocimiento>>();

        // Si se obtienen los conocimientos, actualizar la fuente de datos de la lista
        if (result != null)
        {
            Conocimientos = result;
            ListConocimientos.ItemsSource = Conocimientos;
        }
    }

    //Seleciona elementos de la lista
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
        {
            return;
        }

        // Obtener el objeto Conocimiento seleccionado
        var selectedItem = (Conocimiento)e.SelectedItem;

        // Deseleccionar el elemento para que no quede seleccionado
        ListConocimientos.SelectedItem = null;

        // Navegar a la página de detalles conocimiento y pasar el objeto conocimiento seleccionado como parámetro
        await Navigation.PushAsync(new DetallesConocimientos(selectedItem));
    }
    public List<Conocimiento> Conocimientos { get; set; } = new List<Conocimiento>();

    public class Conocimiento
    {
        public int id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

    }
}