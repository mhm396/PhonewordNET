namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        //int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        string? numeroTraducido;

        private void OnTranslate(object sender, EventArgs e)
        {
            string numeroIntroducido = entryNumText.Text;
            numeroTraducido = PhonewordTranslator.ToNumber(numeroIntroducido);

            if (!string.IsNullOrEmpty(numeroTraducido))
            {
                Console.WriteLine("Efectivamente no esta vacío");
                CallButon.IsEnabled = true;
                CallButon.Text = "LLamar al " + numeroTraducido;
                
            }
            else
            {   
                CallButon.IsEnabled = false;
                CallButon.Text = "Llamar";
                Console.WriteLine("Esta vacío");
            }

        }

        async void OnCall(object sender, System.EventArgs e)
        {
            //Alerta por si el usuario realmente quiere llamar al usuario
            if (await this.DisplayAlert( //Ventana emergente, si tiene dos valores devuelve un booleano
                "Aviso",
                "¿Quieres llamar a " + numeroTraducido + "?",
                "Si",
                "No"))
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported)
                        PhoneDialer.Default.Open(numeroTraducido);
                    else
                    {
                        await DisplayAlert("Error al llamar", "El dispositivo no admite llamadas", "Ok");
                    }
                    
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Error al llamar", "Número de teléfono no válido", "Ok");
                }
                catch (Exception)
                {
                    await DisplayAlert("Error al llamar", "El proceso de llamada ha fallado", "Ok");
                }

            }
        }
    }

}
