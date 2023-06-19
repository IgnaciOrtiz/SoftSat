using SoftSat.Pages;


namespace SoftSat;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(IncidenciasPage), typeof(IncidenciasPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(TareasPage), typeof(TareasPage));
        Routing.RegisterRoute(nameof(ConocimientosPage), typeof(ConocimientosPage));
        Routing.RegisterRoute(nameof(VehiculosPage), typeof(VehiculosPage));
        Routing.RegisterRoute(nameof(DetallesIncidencia), typeof(DetallesIncidencia));
        Routing.RegisterRoute(nameof(DetallesTareas), typeof(DetallesTareas));
        Routing.RegisterRoute(nameof(DetalleVehiculos), typeof(DetalleVehiculos));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

        ////Volver LoginPage
        Shell.Current.GoToAsync("LoginPage");

        //Cierra Aplicacion
        /*Application.Current.Quit();*/
    }
}
