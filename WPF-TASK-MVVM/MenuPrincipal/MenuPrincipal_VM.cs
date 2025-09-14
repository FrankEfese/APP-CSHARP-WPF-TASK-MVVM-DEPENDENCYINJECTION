using Wpf_Task_Mvvm.MenuEquipo;
using Wpf_Task_Mvvm.MenuInicio;
using Wpf_Task_Mvvm.MenuJugador;
using Wpf_Task_Mvvm.Services;
using Wpf_Task_Mvvm.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Wpf_Task_Mvvm.MenuPrincipal
{
    public class MenuPrincipal_VM : INotifyPropertyChanged
    {

        // VENTANA-MENUPRINCIPAL

        private Window _menuPrincipalWindow;


        // VARIABLES MENUS

        public MenuEquipo_V menuEquipo = null;

        public MenuJugador_V menuJugador = null;


        // VARIABLES COMMAND
        public ICommand MenuInicioCommand { get; private set; }
        public ICommand MenuEquipoCommand { get; private set; }
        public ICommand MenuJugadorCommand { get; private set; }
        public ICommand SalirCommand { get; private set; }


        public MenuPrincipal_VM(Window menuPrincipal)
        {

            this._menuPrincipalWindow = menuPrincipal;

            this.MenuInicioCommand = new RelayComand(CargarMenuInicio);
            this.MenuEquipoCommand = new RelayComand(CargarMenuEquipo);
            this.MenuJugadorCommand = new RelayComand(CargarMenuJugador);
            this.SalirCommand = new RelayComand(Salir);

        }


        // IMPLEMENTACION DE INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        // VARIABLE DATABINDING

        private object _content;
        public object Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }


        // METODOS

        public void CargarMenuInicio() // METODO PARA CARGAR EN EL CONTENEDOR EL MENU INICIO
        {

            try
            {

                this.Content = null;

                var serviceEquipo = App.ServiceProvider.GetRequiredService<EquipoService>();
                var serviceJugador = App.ServiceProvider.GetRequiredService<JugadorService>();

                var menuInicio_VM = new MenuInicio_VM(serviceEquipo, serviceJugador);

                MenuInicio_V menuInicio = new MenuInicio_V(menuInicio_VM);

                this.Content = menuInicio;

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ABRIR EL MENU INICIO", "MENU PRINCIPAL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }


        public void CargarMenuEquipo() // METODO PARA CARGAR EN EL CONTENEDOR EL MENU EQUIPO
        {

            try
            {

                this.Content = null;

                var serviceEquipo = App.ServiceProvider.GetRequiredService<EquipoService>();

                var menuEquipo_VM = new MenuEquipo_VM(serviceEquipo);

                this.menuEquipo = new MenuEquipo_V(menuEquipo_VM);

                this.Content = this.menuEquipo;

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ABRIR EL MENU EQUIPO", "MENU PRINCIPAL", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public void CargarMenuJugador() // METODO PARA CARGAR EN EL CONTENEDOR EL MENU JUGADOR
        {

            try
            {

                this.Content = null;

                var serviceJugador = App.ServiceProvider.GetRequiredService<JugadorService>();

                var menuJugador_VM = new MenuJugador_VM(serviceJugador);

                this.menuJugador = new MenuJugador_V(menuJugador_VM);
                
                this.Content = this.menuJugador;

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ABRIR EL MENU JUGADOR", "MENU PRINCIPAL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }


        public void Salir() // METODO PARA SALIR DEL MENU PRINCIPAL
        {
            this._menuPrincipalWindow.Close();
        }


    }
}
