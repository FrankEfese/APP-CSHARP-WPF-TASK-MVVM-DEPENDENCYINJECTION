using System.Windows;


namespace Wpf_Task_Mvvm.Login
{
    /// <summary>
    /// Lógica de interacción para Login_V.xaml
    /// </summary>
    public partial class Login_V : Window
    {
        public Login_V() // CONFIGURACION DEL LOGIN-VM Y DATACONTEXT
        {
            InitializeComponent();

            Login_VM login_VM = new Login_VM(this);
            this.DataContext = login_VM;
        }


    }
}
