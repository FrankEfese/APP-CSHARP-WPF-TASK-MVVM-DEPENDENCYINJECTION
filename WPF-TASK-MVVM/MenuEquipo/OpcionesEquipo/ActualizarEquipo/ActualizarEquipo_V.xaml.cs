using System.Windows;


namespace Wpf_Task_Mvvm.MenuEquipo.OpcionesEquipo.ActualizarEquipo
{
    /// <summary>
    /// Lógica de interacción para ActualizarEquipo_V.xaml
    /// </summary>
    public partial class ActualizarEquipo_V : Window
    {
        public ActualizarEquipo_V(ActualizarEquipo_VM actualizarEquipo_VM) // CONFIGURACION DEL ACTUALIZAREQUIPO-VM , DATACONTEXT Y EVENTO DE CARGA
        {
            InitializeComponent();

            actualizarEquipo_VM._actualizarEquipoWindow = this;
            this.DataContext = actualizarEquipo_VM;
            this.Loaded += async (s, e) => await actualizarEquipo_VM.CargarDatos();
        }
    }
}
