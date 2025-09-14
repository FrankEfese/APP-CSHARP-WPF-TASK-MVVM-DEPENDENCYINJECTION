using System.Windows;


namespace Wpf_Task_Mvvm.MenuEquipo.OpcionesEquipo.VerEquipo
{
    /// <summary>
    /// Lógica de interacción para VerEquipo_V.xaml
    /// </summary>
    public partial class VerEquipo_V : Window
    {
        public VerEquipo_V(VerEquipo_VM verEquipo_VM) // CONFIGURACION DEL VEREQUIPO-VM , DATACONTEXT Y EVENTO DE CARGA
        {
            InitializeComponent();
            this.DataContext = verEquipo_VM;
            this.Loaded += async (s, e) => await verEquipo_VM.CargarDatos();
        }
    }
}
