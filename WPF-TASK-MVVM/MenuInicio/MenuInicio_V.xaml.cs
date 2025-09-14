using System.Windows.Controls;


namespace Wpf_Task_Mvvm.MenuInicio
{
    /// <summary>
    /// Lógica de interacción para MenuInicio_V.xaml
    /// </summary>
    public partial class MenuInicio_V : UserControl
    {
        public MenuInicio_V(MenuInicio_VM menuInicio_VM) // CONFIGURACION DEL MENUINICIO-VM , DATACONTEXT Y EVENTO DE CARGA
        {
            InitializeComponent();
            this.DataContext = menuInicio_VM;
            this.Loaded += async (s, e) => await menuInicio_VM.CargarDatos();
        }
    }
}
