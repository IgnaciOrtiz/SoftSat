using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SoftSat.Pages;

public partial class TareasPage : ContentPage
{

    private const string ApiUrl = "https://apisat.hsoft.es/api/Tareas?idUsuarios=2";

    public TareasPage()
    {
        OnAppearing();
        InitializeComponent();
        ListTareas.ItemTapped += OnItemTapped;
        LoadTareasAsync();
    }

    private void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        // Obtener el objeto de tarea seleccionado
        var tarea = e.Item as Tarea;

        // Crear una nueva página para mostrar los detalles de la tarea
        var DetallesTareas = new DetallesTareas(tarea);

        // Navegar a la nueva página
        Navigation.PushAsync(DetallesTareas);
    }

    private async Task LoadTareasAsync()
    {
        // Obtener el token de autenticación del almacenamiento seguro local
        var accessToken = await SecureStorage.GetAsync("AccessToken");

        // Crear un objeto HttpClient para llamar a la API
        using var httpClient = new HttpClient();

        // Agregar el token de autenticación a la cabecera de autorización
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

        // Llamar a la API para obtener las tareas
        var response = await httpClient.GetAsync(ApiUrl);
        var result = await response.Content.ReadFromJsonAsync<List<Tarea>>();

        // Si se obtienen las tareas, actualizar la fuente de datos de la lista
        if (result != null)
        {
            Tareas = result;
            ListTareas.ItemsSource = Tareas;
        }
    }

    // Actualiza la página para que al volver no aparezcan las tareas cerradas
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Aquí puedes agregar código para actualizar la lista de tareas.
        await LoadTareasAsync();
    }

    //Lista Tareas
    public List<Tarea> Tareas { get; set; } = new List<Tarea>();
    public class Tarea
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public string Observaciones { get; set; }
    }
}