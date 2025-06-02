using AgendaPersonal.Modelos;
using AgendaPersonal.Datos;
using AgendaPersonal.Views;
namespace AgendaPersonal;

public partial class DetalleContactoPage : ContentPage
{
    private Contacto contacto;
    private ContactoDataBase db = new ContactoDataBase();
    public DetalleContactoPage()
	{
        InitializeComponent();

    }
    public DetalleContactoPage(Contacto c)
    {
        InitializeComponent();
        contacto = c;
        BindingContext = contacto;

    }
    private async void OnEditarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearContactoPage(contacto));
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar a {contacto.Nombre}?", "Sí", "No");
        if (confirm)
        {
            await db.EliminarContactoAsync(contacto);
            await DisplayAlert("Eliminado", "El contacto ha sido eliminado.", "OK");
            await Navigation.PopAsync(); // Regresar a la lista
        }
    }
}