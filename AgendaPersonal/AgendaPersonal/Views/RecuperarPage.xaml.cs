using AgendaPersonal.Datos;
using AgendaPersonal.Modelos;
namespace AgendaPersonal.Views;

public partial class RecuperarPage : ContentPage
{
	private UsuarioDataBase db;
	private Usuario usuarioActual;
	public RecuperarPage()
	{
		InitializeComponent();
		string dbPath = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
			"ususario.db3");
		db = new UsuarioDataBase(dbPath);
	}
	private async void BuscarPreguntaClicked(object sender, EventArgs e)
	{
		string nombre = usuarioEntry.Text?.Trim();
		if(string.IsNullOrEmpty(nombre) )
		{
			await DisplayAlert("Error", "Ingresa un nombre de usuario", "OK");
			return;
		}
		usuarioActual = await db.ObtenerUsuarioPorNombreAsync(nombre);
		if(usuarioActual == null)
		{
			await DisplayAlert("Error", "Usuario no encontrado", "OK");
			return;
		}
		preguntaLabel.Text = usuarioActual.PreguntaSeguridad;
		respuestaEntry.IsVisible = true;
		respuestaEntry.IsVisible = true;
		respuestaEntry.Text = "";
		resultadoLabel.IsVisible = false;
		respuestaEntry.Focus();
	}
    private async void RecuperarClicked(object sender, EventArgs e)
    {
        string respuesta = respuestaEntry.Text?.Trim();
        if (respuesta?.ToLower() == usuarioActual.RespuestaSeguridad.ToLower())
        {
            resultadoLabel.Text = $"Tu contraseña es: {usuarioActual.Contraseña}";
            resultadoLabel.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Error", "Respuesta incorrecta", "OK");
			await Shell.Current.GoToAsync("//login");
        }
    }
}