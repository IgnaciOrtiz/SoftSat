using System.Net.Http.Headers;
using static SoftSat.Pages.TareasPage;

namespace SoftSat.Pages;

public partial class DetallesTareas : ContentPage
{
    private readonly Tarea tarea;
    private const string ApiUrl = "https://apisat.hsoft.es/api/CierreTareas";
    private string Observaciones { get; set; }
    public DetallesTareas()
    {
        InitializeComponent();
    }
    public DetallesTareas(Tarea tarea)
    {
        InitializeComponent();
        // Mostrar el ID y el detalle de la tarea en las etiquetas Label 
        // Mostrar observaciones de la tarea en placeholder del editor
        IdLabel.Text = tarea.id.ToString();
        DescripcionLabel.Text = tarea.descripcion;
        ObservacionesEditor.Placeholder = tarea.Observaciones;
    }

    private async void Button_Clicked_GuardarTarea(object sender, EventArgs e)
    {
        Observaciones = ObservacionesEditor.Text;

        // Hacer algo con las observaciones, como guardarlas en una base de datos o enviarlas a un servidor
        await DisplayAlert("Observaciones guardadas", "Cerrando Tarea", "Aceptar");

        await Navigation.PopAsync();
    }

    private async void Button_Clicked_CerrarTarea(object sender, EventArgs e)
    {
        //Guarda observaciones y Cierra Tarea
        Observaciones = ObservacionesEditor.Text;
        string parameterId = IdLabel.Text;
        string parameter = "/?id=" + parameterId + "&Observaciones=" + Observaciones;
        var accessToken = await SecureStorage.GetAsync("AccessToken");

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

        var content = new FormUrlEncodedContent(new[]
        {
        new KeyValuePair<string, string>("", ""),
        });

        var response = await httpClient.PostAsync(ApiUrl + parameter, content);

        //Mensaje
        await DisplayAlert("Cerrar Tarea", "Cerrando Tarea", "Aceptar");

        //Vuelve a Tareas
        await Navigation.PopAsync();
    }
}