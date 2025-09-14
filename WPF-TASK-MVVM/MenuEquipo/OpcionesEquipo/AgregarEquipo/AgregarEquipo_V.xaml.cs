using System.Windows;


namespace Wpf_Task_Mvvm.MenuEquipo.OpcionesEquipo.AgregarEquipo
{
    /// <summary>
    /// Lógica de interacción para AgregarEquipo_V.xaml
    /// </summary>
    public partial class AgregarEquipo_V : Window
    {
        public AgregarEquipo_V(AgregarEquipo_VM agregarEquipo_VM) // CONFIGURACION DEL AGREGAREQUIPO-VM Y DATACONTEXT
        {
            InitializeComponent();
            agregarEquipo_VM._agregarEquipoWindow = this;
            this.DataContext = agregarEquipo_VM;
        }
    }
}
