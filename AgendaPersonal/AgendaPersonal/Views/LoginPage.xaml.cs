using AgendaPersonal.Datos;
using AgendaPersonal.Modelos;
using System.IO;
namespace AgendaPersonal.Views;

public partial class LoginPage : ContentPage
{
    private UsuarioDataBase db;
	public LoginPage()
	{
		InitializeComponent();
        string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "usuario.db3");
        db = new UsuarioDataBase(dbPath);
	}
    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }

    private async void TapGestureRecognizerPwd_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//RecuperarPage");
    }
    private async void TapGestureRecognizerReg_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RegistroPage());
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        if(string.IsNullOrWhiteSpace(Username.Text) || string.IsNullOrWhiteSpace(Password.Text))
        {
            await DisplayAlert("Error","Por favor, completar todos los campos.", "OK");
            return;
        }
        //Depuración: Ver usuarios guardados en la base de datos
        //var todosUsuarios = await db.ObtenerTodosLosUsuariosAsync(); 
        //string listaCorreos = string.Join("\n", todosUsuarios.Select(u => u.Correo));
        //await DisplayAlert("Usuarios en DB", listaCorreos, "OK");
        //temporal
        if (await IsCredentialCorrectAsync(Username.Text, Password.Text))
        {
            Preferences.Set("UsuarioActual", Username.Text.Trim());
            await SecureStorage.SetAsync("hasAuth", "true");
            await Shell.Current.GoToAsync("///home");
        }
        else
        {
            Preferences.Remove("UsuarioActual");
            await DisplayAlert("Error de inicio de sesión "," Nombre de usuario o contraseña no es válida "," Intente de nuevo");
        }
    }
    private async Task<bool> IsCredentialCorrectAsync(string nombreUsuario, string contraseña)
    {
        
        var usuario = await db.ObtenerUsuarioPorNombreAsync(nombreUsuario.Trim().ToLower());       
        //await DisplayAlert("Debug", $"Encontrado:\nCorreo:{usuario.Correo}\nContraseña: {usuario.Contraseña}", "OK");
        return usuario.Contraseña.Trim() == contraseña.Trim();

    }
}