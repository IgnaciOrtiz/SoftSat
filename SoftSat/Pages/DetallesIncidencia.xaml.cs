using System.Net.Http.Headers;
using static SoftSat.Pages.IncidenciasPage;

namespace SoftSat.Pages;

public partial class DetallesIncidencia : ContentPage
{
    private const string ApiUrl = "https://apisat.hsoft.es/api/InsertActuaciones";
    private string Observaciones { get; set; }

    public DetallesIncidencia()
    {
        InitializeComponent();
    }

    public DetallesIncidencia(Incident incident)
    {
        InitializeComponent();
        // Mostrar el ID y el detalle de la incidencia en las etiquetas Label
        IdLabel.Text = incident.id.ToString();
        DescripcionLabel.Text = incident.descripcion;
    }

    private async void Button_Clicked_GuardarText(object sender, EventArgs e)
    {
        Observaciones = ObservacionesEditor.Text;

        // Hacer algo con las observaciones, como guardarlas en una base de datos o enviarlas a un servidor
        await DisplayAlert("Observaciones guardadas", Observaciones, "Aceptar");

        await Navigation.PopAsync();

    }

    private async void Button_Clicked_CerrarIncidencia(object sender, EventArgs e)
    {
        //Cerrar incidencia selecciona y añadir observaciones a la actuación.
        Observaciones = ObservacionesEditor.Text;
        string parameterId = IdLabel.Text;
        string parameter = "?idUsuarios=2&idIncidencia=" + parameterId + "&idTipoActuacion=1&descripcion=" + Observaciones;
        var accessToken = await SecureStorage.GetAsync("AccessToken");

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        var content = new FormUrlEncodedContent(new[]
        {
        new KeyValuePair<string, string>("", ""),
        });

        var response = await httpClient.PostAsync(ApiUrl + parameter, content);

        //Mensaje
        await DisplayAlert("Cerrar Incidencia", "Cerrando Incidencia", "Aceptar");

        //Vuelve a home
        OnAppearing();
        await Navigation.PopAsync();

    }
}