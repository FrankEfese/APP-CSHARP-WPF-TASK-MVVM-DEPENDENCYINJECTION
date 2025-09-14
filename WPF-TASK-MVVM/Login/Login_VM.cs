using Wpf_Task_Mvvm.Config;
using Wpf_Task_Mvvm.MenuPrincipal;
using Wpf_Task_Mvvm.Tools;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Wpf_Task_Mvvm.Login
{
    public class Login_VM : INotifyPropertyChanged
    {

        // VENTANA-LOGIN

        private Window _loginWindow;


        // VARIABLE COMMAND
        public ICommand IniciarSesionCommand { get; private set; }


        public Login_VM(Window login)
        {
            this._loginWindow = login;

            this.IniciarSesionCommand = new AsyncRelayComand(IniciarSesion);
        }


        // IMPLEMENTACION DE INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        // VARIABLES DATABINDING

        private string _correoUsuario;
        public string CorreoUsuario
        {
            get => _correoUsuario;
            set
            {
                if (_correoUsuario != value)
                {
                    _correoUsuario = value;
                    OnPropertyChanged(nameof(CorreoUsuario));
                }
            }
        }


        private string _contraseñaUsuario;
        public string ContraseñaUsuario
        {
            get => _contraseñaUsuario;
            set
            {
                if (_contraseñaUsuario != value)
                {
                    _contraseñaUsuario = value;
                    OnPropertyChanged(nameof(ContraseñaUsuario));
                }
            }
        }


        // METODOS

        public async Task IniciarSesion() // METODO PARA INICAR SESION
        {

            try
            {

                if (await AppConfig.InicioSesion(this.CorreoUsuario, this.ContraseñaUsuario))
                {

                    MessageBox.Show("CREDENCIALES CORRECTAS", "INICIO DE SESION CORRECTO", MessageBoxButton.OK, MessageBoxImage.Information);

                    CargarMenuPrincipal();

                }
                else MessageBox.Show("CREDENCIALES INCORRECTAS", "INICIO DE SESION INCORRECTO", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("HAS INTRODUCIDO UN DATO ERRONEO", "LOGIN", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void CargarMenuPrincipal() // METODO PARA ABRIR EL MENU PRINCIPAL
        {
            
            var menuPrincipal = new MenuPrincipal_V();
            menuPrincipal.Closed += (s, args) => this._loginWindow.Show();
            menuPrincipal.Show();

            this._loginWindow.Hide();
        }


    }
}
