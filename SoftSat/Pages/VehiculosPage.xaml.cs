using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SoftSat.Pages;

public partial class VehiculosPage : ContentPage
{
    private const string ApiUrl = "https://apisat.hsoft.es/api/Vehiculos";
    public List<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    public class Vehiculo
    {

        public int id { get; set; }
        public string Matricula { get; set; }
    }
    public VehiculosPage()
    {
        //OnAppearing();
        InitializeComponent();
        LoadVehiculosAsync();
    }

    private async Task LoadVehiculosAsync()
    {
        // Obtener el token de autenticación del almacenamiento seguro local
        var accessToken = await SecureStorage.GetAsync("AccessToken");

        // Crear un objeto HttpClient para llamar a la API
        using var httpClient = new HttpClient();

        // Agregar el token de autenticación a la cabecera de autorización
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

        // Llamar a la API para obtener los vehiculos
        var response = await httpClient.GetAsync(ApiUrl);
        var result = await response.Content.ReadFromJsonAsync<List<Vehiculo>>();

        // Si se obtienen los vehiculos, actualizar la fuente de datos de la lista
        if (result != null)
        {
            Vehiculos = result;
            ListVehiculos.ItemsSource = Vehiculos;
        }
    }
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
        {
            return;
        }

        // Obtener el objeto Incident seleccionado
        var selectedItem = (Vehiculo)e.SelectedItem;

        // Deseleccionar el elemento para que no quede seleccionado
        ListVehiculos.SelectedItem = null;

        // Navegar a la página de actualización de incidencias y pasar el objeto Incident seleccionado como parámetro
        await Navigation.PushAsync(new DetalleVehiculos(selectedItem));
    }

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();

    //    // Aquí puedes agregar código para actualizar la lista de tareas.
    //    await LoadVehiculosAsync();
    //}
}