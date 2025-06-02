using AgendaPersonal.Utils;
using AgendaPersonal.Views;
using System.Globalization;
using AgendaPersonal.Datos;
using AgendaPersonal.Modelos;

namespace AgendaPersonal;

public partial class ConfiguracionPage : ContentPage
{
    private UsuarioDataBase db;
    public ConfiguracionPage()
	{
		InitializeComponent();
        temaSwitch.IsToggled = ConfiguracionApp.ObtenerTema();
        idiomaPicker.SelectedItem = ConfiguracionApp.ObtenerIdioma();
        string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "usuario.db3");
        db = new UsuarioDataBase(dbPath);
    }
    private void OnTemaToggled(object sender, ToggledEventArgs e)
    {
        ConfiguracionApp.GuardarTema(e.Value);

        Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;

        lblEstado.IsVisible = true;
    }
    private void OnIdiomaChanged(object sender, EventArgs e)
    {
        string? idiomaSeleccionado = idiomaPicker.SelectedItem.ToString();

        if (idiomaSeleccionado != null)
            ConfiguracionApp.GuardarIdioma(idiomaSeleccionado);

        // Aplicar cultura al hilo actual
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(idiomaSeleccionado);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(idiomaSeleccionado);

        // Actualizar UI con nuevos textos
        ActualizarTextos();
        lblEstado.IsVisible = true;
    }

    private void ActualizarTextos()
    {
        return;

        //this.Title = Traductor.Get("Configuracion");
        //lblEstado.Text = Traductor.Get("CamposRequeridos"); // ejemplo de recarga
        // Aquí podrías actualizar otros elementos visibles de la página si se desea
    }
    private async void IrCerrar(object sender, EventArgs e)
    {
        if (await DisplayAlert("¿Está seguro?", "Se cerrará su sesión.", "Sí", "No"))
        {
            Preferences.Remove("UsuarioActual");
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync("///login");
        }
    }
    private async void OnEliminarUsuarioClicked(object sender, EventArgs e)
    {
        string nombre = Preferences.Get("UsuarioActual", string.Empty);
        if (string.IsNullOrEmpty(nombre))
        {
            await DisplayAlert("Error", "No se encontró el usuario actual.", "OK");
            return;
        }

        var usuario = await db.ObtenerUsuarioPorNombreAsync(nombre);
        if (usuario == null)
        {
            await DisplayAlert("Error", "Usuario no encontrado.", "OK");
            return;
        }

        bool confirmar = await DisplayAlert("Confirmar eliminación",
            $"¿Estás seguro de que deseas eliminar tu cuenta ({usuario.NombreUsuario})?",
            "Sí", "No");

        if (confirmar)
        {
            await db.EliminarUsuarioAsync(usuario);
            Preferences.Remove("UsuarioActual");
            await DisplayAlert("Cuenta eliminada", "Tu cuenta ha sido eliminada correctamente.", "OK");
            
        }
        await Shell.Current.GoToAsync("//login");
    }
}