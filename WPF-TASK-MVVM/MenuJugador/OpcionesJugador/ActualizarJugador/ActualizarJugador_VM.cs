using Wpf_Task_Mvvm.Services;
using Wpf_Task_Mvvm.Tools;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using Wpf_Task_Mvvm.Models;

namespace Wpf_Task_Mvvm.MenuJugador.OpcionesJugador.ActualizarJugador
{
    public class ActualizarJugador_VM : INotifyPropertyChanged
    {

        // VARIABLE ID-JUGADOR

        public int idJugador = -1;


        // VENTANA-ACTUALIZARJUGADOR

        public Window _actualizarJugadorWindow { get; set; }


        // VIEWMODEL-MENUJUGADOR

        private MenuJugador_VM _menuJugador_VM;


        // VARIABLES COMMAND
        public ICommand ActualizarJugadorCommand { get; private set; }


        // INYECCION DE DEPENDENCIA

        private readonly JugadorService _serviceJugador;


        public ActualizarJugador_VM(MenuJugador_VM menuJugador_VM, JugadorService serviceJugador) // CONSTRUCTOR CON LA DEPENDENCIA
        {
            this._menuJugador_VM = menuJugador_VM;
            this._serviceJugador = serviceJugador;

            this.ActualizarJugadorCommand = new AsyncRelayComand(ActualizarJugador);
        }


        // IMPLEMENTACION DE INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        // VARIABLES DATABINDING

        private string _nombreJugador;
        public string NombreJugador
        {
            get => _nombreJugador;
            set
            {
                if (_nombreJugador != value)
                {
                    _nombreJugador = value;
                    OnPropertyChanged(nameof(NombreJugador));

                }
            }
        }


        private int _edadJugador = 17;
        public int EdadJugador
        {
            get => _edadJugador;
            set
            {
                if (_edadJugador != value)
                {
                    _edadJugador = value;
                    OnPropertyChanged(nameof(EdadJugador));

                }
            }
        }


        private string _nacionalidadJugador;
        public string NacionalidadJugador
        {
            get => _nacionalidadJugador;
            set
            {
                if (_nacionalidadJugador != value)
                {
                    _nacionalidadJugador = value;
                    OnPropertyChanged(nameof(NacionalidadJugador));

                }
            }
        }


        private int _alturaJugador = 101;
        public int AlturaJugador
        {
            get => _alturaJugador;
            set
            {
                if (_alturaJugador != value)
                {
                    _alturaJugador = value;
                    OnPropertyChanged(nameof(AlturaJugador));

                }
            }
        }


        private int _pesoJugador = 31;
        public int PesoJugador
        {
            get => _pesoJugador;
            set
            {
                if (_pesoJugador != value)
                {
                    _pesoJugador = value;
                    OnPropertyChanged(nameof(PesoJugador));

                }
            }
        }


        // METODOS

        public async Task CargarDatos() // METODO PARA CARGAR LOS DATOS DEL JUGADOR
        {

            try
            {

                var jugador = await this._serviceJugador.ObtenerUnJugador_M(this.idJugador);

                if(jugador != null)
                {

                    this.NombreJugador = jugador.Nombre;
                    this.EdadJugador = jugador.Edad;
                    this.NacionalidadJugador = jugador.Nacionalidad;
                    this.AlturaJugador = jugador.Altura;
                    this.PesoJugador = jugador.Peso;

                }

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL CARGAR LOS DATOS DEL JUGADOR", "ACTUALIZAR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task ActualizarJugador() // METODO PARA ACTUALIZAR AL JUGADOR
        {

            try
            {

                if (Validaciones.ValidarJugador(this.NombreJugador, this.EdadJugador, this.NacionalidadJugador, this.AlturaJugador, this.PesoJugador))
                {

                    Jugador jugadorActualizado = new Jugador();
                    jugadorActualizado.Id = this.idJugador;
                    jugadorActualizado.Nombre = this.NombreJugador;
                    jugadorActualizado.Edad = this.EdadJugador;
                    jugadorActualizado.Nacionalidad = this.NacionalidadJugador;
                    jugadorActualizado.Altura = this.AlturaJugador;
                    jugadorActualizado.Peso = this.PesoJugador;

                    var actualizado = await this._serviceJugador.ActualizarJugador_M(jugadorActualizado);

                    if(actualizado) await this._menuJugador_VM.ActualizarDatosVentanas(jugadorActualizado);

                    this._actualizarJugadorWindow.Close();

                }
                else MessageBox.Show("DATOS INTRODUCIDOS DE FORMA INCORRECTA", "ACTUALIZAR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ACTUALIZAR LOS DATOS DEL JUGADOR", "ACTUALIZAR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
