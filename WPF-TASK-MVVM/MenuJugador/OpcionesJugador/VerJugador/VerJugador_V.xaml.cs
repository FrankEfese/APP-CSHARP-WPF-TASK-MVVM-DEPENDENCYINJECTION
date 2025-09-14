using System.Windows;


namespace Wpf_Task_Mvvm.MenuJugador.OpcionesJugador.VerJugador
{
    /// <summary>
    /// Lógica de interacción para VerJugador_V.xaml
    /// </summary>
    public partial class VerJugador_V : Window
    {
        public VerJugador_V(VerJugador_VM verJugador_VM) // CONFIGURACION DEL VERJUGADOR-VM , DATACONTEXT Y EVENTO DE CARGA
        {
            InitializeComponent();
            this.DataContext = verJugador_VM;
            this.Loaded += async (s, e) => await verJugador_VM.CargarDatos();
        }
    }
}
