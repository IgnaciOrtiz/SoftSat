using System.Net.Http.Headers;
using static SoftSat.Pages.VehiculosPage;
namespace SoftSat.Pages;

public partial class DetalleVehiculos : ContentPage
{
    private readonly Vehiculo vehiculo;
    private const string ApiUrl = "https://apisat.hsoft.es/api/InsertAsignacionVehiculo";

    public DetalleVehiculos(Vehiculo vehiculo)
    {
        InitializeComponent();
        this.vehiculo = vehiculo;

        // Configurar el BindingContext de la página
        BindingContext = vehiculo;
    }

    private async void OnLiberarClicked(object sender, EventArgs e)
    {

        string parameter = "?idvehiculo=1&idusuarios=2&tarjeta=0&estado=0";

        var accessToken = await SecureStorage.GetAsync("AccessToken");

        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

        var content = new FormUrlEncodedContent(new[]
        {
        new KeyValuePair<string, string>("", ""), // No need to include any parameters here
        });

        var response = await httpClient.PostAsync(ApiUrl + parameter, content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Actualización exitosa", "Vehiculo liberado correctamente.", "Aceptar");
        }
        else
        {
            await DisplayAlert("Error", $"La llamada a la API falló con el código de estado {response.StatusCode}.", "Aceptar");
        }

        await Navigation.PopAsync();
    }

    private async void OnOcuparClicked(object sender, EventArgs e)
    {
        string parameter = "?idvehiculo=1&idusuarios=2&tarjeta=1&estado=1";

        var accessToken = await SecureStorage.GetAsync("AccessToken");

        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

        var content = new FormUrlEncodedContent(new[]
        {
        new KeyValuePair<string, string>("", ""), // No need to include any parameters here
        });

        var response = await httpClient.PostAsync(ApiUrl + parameter, content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Actualización exitosa", "Vehiculo Ocupado correctamente.", "Aceptar");
        }
        else
        {
            await DisplayAlert("Error", $"La llamada a la API falló con el código de estado {response.StatusCode}.", "Aceptar");
        }

        await Navigation.PopAsync();
    }
}