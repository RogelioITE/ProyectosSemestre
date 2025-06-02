namespace AgendaPersonal
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();

        }

        private void IrListaContactos(object sender, EventArgs e)
        {
            Navigation.PushAsync( new ContactosPage(), true);
        }
        private void IrCrearContacto(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearContactoPage(), true);
        }
        private void IrConfiguracion(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConfiguracionPage(), true);
        }
    }

}
