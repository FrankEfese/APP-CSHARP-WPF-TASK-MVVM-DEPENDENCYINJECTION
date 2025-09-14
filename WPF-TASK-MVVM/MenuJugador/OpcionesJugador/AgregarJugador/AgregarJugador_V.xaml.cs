using System.Windows;

namespace Wpf_Task_Mvvm.MenuJugador.OpcionesJugador.AgregarJugador
{
    /// <summary>
    /// Lógica de interacción para AgregarJugador_V.xaml
    /// </summary>
    public partial class AgregarJugador_V : Window
    {
        public AgregarJugador_V(AgregarJugador_VM agregarJugador_VM) // CONFIGURACION DEL AGREGARJUGADOR-VM Y DATACONTEXT
        {
            InitializeComponent();
            agregarJugador_VM._agregarJugadorWindow = this;
            this.DataContext = agregarJugador_VM;
        }
    }
}
