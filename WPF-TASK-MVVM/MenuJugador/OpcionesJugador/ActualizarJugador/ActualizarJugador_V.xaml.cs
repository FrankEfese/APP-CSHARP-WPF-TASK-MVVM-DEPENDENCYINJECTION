using System.Windows;


namespace Wpf_Task_Mvvm.MenuJugador.OpcionesJugador.ActualizarJugador
{
    /// <summary>
    /// Lógica de interacción para ActualizarJugador_V.xaml
    /// </summary>
    public partial class ActualizarJugador_V : Window
    {
        public ActualizarJugador_V(ActualizarJugador_VM actualizarJugador_VM) // CONFIGURACION DEL ACTUALIZARJUGADOR-VM , DATACONTEXT Y EVENTO DE CARGA
        {
            InitializeComponent();

            actualizarJugador_VM._actualizarJugadorWindow = this;
            this.DataContext = actualizarJugador_VM;
            this.Loaded += async (s, e) => await actualizarJugador_VM.CargarDatos();
        }
    }
}
