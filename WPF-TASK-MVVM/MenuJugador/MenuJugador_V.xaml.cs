using System.Windows.Controls;


namespace Wpf_Task_Mvvm.MenuJugador
{
    /// <summary>
    /// Lógica de interacción para MenuJugador_V.xaml
    /// </summary>
    public partial class MenuJugador_V : UserControl
    {
        public MenuJugador_V(MenuJugador_VM menuJugador_VM) // CONFIGURACION DEL MENUJUGADOR-VM , DATACONTEXT , EVENTO DE CARGA Y EVENTO DE CIERRE
        {
            InitializeComponent();
            this.DataContext = menuJugador_VM;
            this.Loaded += async (s, e) => await menuJugador_VM.CargarDatos();
            this.Unloaded += (s, args) => menuJugador_VM.CerrarVentanas();
        }
    }
}
