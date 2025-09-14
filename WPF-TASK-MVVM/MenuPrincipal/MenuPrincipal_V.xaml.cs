using System.Windows;


namespace Wpf_Task_Mvvm.MenuPrincipal
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal_V.xaml
    /// </summary>
    public partial class MenuPrincipal_V : Window
    {
        public MenuPrincipal_V() // CONFIGURACION DEL MENUPRINCIPAL-VM , DATACONTEXT Y EVENTO DE CARGA
        {
            InitializeComponent();

            MenuPrincipal_VM menuPrincipal_VM = new MenuPrincipal_VM(this);

            this.DataContext = menuPrincipal_VM;
            this.Loaded += (s, e) => menuPrincipal_VM.CargarMenuInicio();
        }
    }
}
