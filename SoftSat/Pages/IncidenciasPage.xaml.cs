using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SoftSat.Pages;

public partial class IncidenciasPage : ContentPage
{
    private const string ApiUrl = "https://apisat.hsoft.es/api/IncidenciasAsignadas?id=2";

    public IncidenciasPage()
    {
        OnAppearing();
        InitializeComponent();
        ListIncidencias.ItemTapped += OnItemTapped;
        LoadIncidentsAsync();
    }

    private void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        // Obtener el objeto de incidencia seleccionado
        var incident = e.Item as Incident;

        // Crear una nueva página para mostrar los detalles de la incidencia
        var DetallesIncidencia = new DetallesIncidencia(incident);

        // Navegar a la nueva página
        Navigation.PushAsync(DetallesIncidencia);
    }

    private async Task LoadIncidentsAsync()
    {
        // Obtener el token de autenticación del almacenamiento seguro local
        var accessToken = await SecureStorage.GetAsync("AccessToken");

        // Crear un objeto HttpClient para llamar a la API
        using var httpClient = new HttpClient();

        // Agregar el token de autenticación a la cabecera de autorización
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

        // Llamar a la API para obtener las incidencias
        var response = await httpClient.GetAsync(ApiUrl);
        var result = await response.Content.ReadFromJsonAsync<List<Incident>>();

        // Si se obtienen las incidencias, actualizar la fuente de datos de la lista
        if (result != null)
        {
            Incidents = result;
            ListIncidencias.ItemsSource = Incidents;
        }
    }
    protected override async void OnAppearing()
    {
        //Recarga página al volver
        base.OnAppearing();
        // Aquí puedes agregar código para actualizar la lista de tareas.
        await LoadIncidentsAsync();
    }

    //Lista Incidencias
    public List<Incident> Incidents { get; set; } = new List<Incident>();

    public class Incident
    {
        public int id { get; set; }
        public string descripcion { get; set; }
    }
}