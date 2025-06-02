using System;
using Microsoft.Maui.Controls;
using AgendaPersonal.Datos;
using AgendaPersonal.Modelos;
using System.IO;
namespace AgendaPersonal.Views;

public partial class RegistroPage : ContentPage
{
	private UsuarioDataBase db;
	public RegistroPage()
	{
		InitializeComponent();
		string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "usuario.db3");
		db = new UsuarioDataBase(dbPath);
	}
	private async void OnRegistrarseClicked(object sender, EventArgs e)
	{
		string nombreUsuario = nombreUsuarioEntry.Text?.Trim();
		string correo = emailEntry.Text?.Trim().ToLower();
		string contraseña = passwordEntry.Text;
		string confirmarContraseña = confirmPasswordEntry.Text;
		string pregunta = preguntaPicker.SelectedItem?.ToString();
		string respuesta = respuestaSeguridadEntry.Text?.Trim();
		if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña) || string.IsNullOrEmpty(confirmarContraseña) ||
			string.IsNullOrEmpty(pregunta) || string.IsNullOrEmpty(respuesta))
		{
			await DisplayAlert("Error", "Tododos los campos son obligatorios.", "OK");
			return;
		}
		if(contraseña != confirmarContraseña)
		{
			await DisplayAlert("Error", "Las contraseñas no coinciden.", "Ok");
			return;
		}
		var usuarioExistente = await db.ObtenerUsuarioPorNombreAsync(correo);
		if(usuarioExistente != null)
		{
			await DisplayAlert("Error", "Ya existe un usuario con ese correo.", "OK");
			return;
		}
		var nuevoUsuario = new Usuario
		{
			NombreUsuario = nombreUsuario,
			Correo = correo,
			Contraseña = contraseña,
			PreguntaSeguridad = pregunta,
			RespuestaSeguridad = respuesta
		};
		await db.GuardarUsuarioAsync(nuevoUsuario);
		await DisplayAlert("Exito", "Registro completo", "OK");
		await Navigation.PopAsync();
	}
}