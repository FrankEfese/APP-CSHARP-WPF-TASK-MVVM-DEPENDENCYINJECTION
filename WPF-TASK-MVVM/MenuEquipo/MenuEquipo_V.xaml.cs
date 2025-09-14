using System.Windows.Controls;


namespace Wpf_Task_Mvvm.MenuEquipo
{
    /// <summary>
    /// Lógica de interacción para MenuEquipo_V.xaml
    /// </summary>
    public partial class MenuEquipo_V : UserControl
    {
        public MenuEquipo_V(MenuEquipo_VM menuEquipo_VM) // CONFIGURACION DEL MENUEQUIPO-VM , DATACONTEXT , EVENTO DE CARGA Y EVENTO DE CIERRE
        {
            InitializeComponent();
            this.DataContext = menuEquipo_VM;
            this.Loaded += async (s, e) => await menuEquipo_VM.CargarDatos();
            this.Unloaded += (s, args) => menuEquipo_VM.CerrarVentanas();
        }
    }
}
