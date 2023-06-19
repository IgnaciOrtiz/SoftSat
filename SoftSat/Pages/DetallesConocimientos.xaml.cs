using static SoftSat.Pages.ConocimientosPage;


namespace SoftSat.Pages;



public partial class DetallesConocimientos : ContentPage
{
    private readonly Conocimiento conocimiento;


    public DetallesConocimientos(Conocimiento conocimiento)
    {
        InitializeComponent();
        // Mostrar el ID y el detalle de la incidencia en las etiquetas Label
        IdLabel.Text = conocimiento.id.ToString();
        TituloLabel.Text = conocimiento.Titulo;
        DescripcionEditor.Text = conocimiento.Descripcion;
    }

    private async void Button_Clicked_CerrarConocimiento(object sender, EventArgs e)
    {
        //Vuelve a home
        await Navigation.PopAsync();
    }
}