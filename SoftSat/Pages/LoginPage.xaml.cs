using System.Net.Http.Json;

namespace SoftSat.Pages;

public partial class LoginPage : ContentPage
{
    private const string ApiUrl = "https://apisat.hsoft.es/token";
    public LoginPage()
    {
        InitializeComponent();
    }

    public async void Button_ClickedAsync(object sender, EventArgs e)
    {
        // Obtener el nombre de usuario y la contraseña ingresados por el usuario
        var username = txtUsuario.Text;
        var password = txtPassword.Text;

        // Crear un objeto HttpClient para llamar a la API
        using var httpClient = new HttpClient();

        // Crear un objeto FormUrlEncodedContent con las credenciales del usuario
        var content = new FormUrlEncodedContent(new[]
        {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

        // Llamar a la API para obtener el token de autenticacion
        var response = await httpClient.PostAsync(ApiUrl, content);
        var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();

        // Si la autenticacion es exitosa, guardar el token en el almacenamiento local
        if (result != null && !string.IsNullOrEmpty(result.access_token))
        {
            await SecureStorage.SetAsync("AccessToken", result.access_token);
        }

        // Navegar a la pagina de inicio de la aplicacion
        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }

    public class AuthenticationResult
    {
        public string access_token { get; set; }
    }
}