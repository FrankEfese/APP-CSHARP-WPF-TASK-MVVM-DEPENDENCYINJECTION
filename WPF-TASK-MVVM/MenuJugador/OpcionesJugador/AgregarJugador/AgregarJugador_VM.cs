using Wpf_Task_Mvvm.Models;
using Wpf_Task_Mvvm.Services;
using Wpf_Task_Mvvm.Tools;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Wpf_Task_Mvvm.MenuJugador.OpcionesJugador.AgregarJugador
{
    public class AgregarJugador_VM : INotifyPropertyChanged
    {

        // VENTANA-AGREGARJUGADOR

        public Window _agregarJugadorWindow {  get; set; }


        // VIEWMODEL-MENUJUGADOR

        private MenuJugador_VM _menuJugador_VM;


        // VARIABLES COMMAND
        public ICommand AgregarJugadorCommand { get; private set; }


        // INYECCION DE DEPENDENCIA

        private readonly JugadorService _serviceJugador;


        public AgregarJugador_VM(MenuJugador_VM menuJugador_VM, JugadorService serviceJugador) // CONSTRUCTOR CON LA DEPENDENCIA
        {
            this._menuJugador_VM = menuJugador_VM;
            this._serviceJugador = serviceJugador;

            this.AgregarJugadorCommand = new AsyncRelayComand(AgregarJugador);
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

        public async Task AgregarJugador() // METODO PARA AGREGAR AL JUGADOR
        {

            try
            {

                if (Validaciones.ValidarJugador(this.NombreJugador, this.EdadJugador, this.NacionalidadJugador, this.AlturaJugador, this.PesoJugador))
                {

                    Jugador jugadorAgregado = new Jugador();
                    jugadorAgregado.Nombre = this.NombreJugador;
                    jugadorAgregado.Edad = this.EdadJugador;
                    jugadorAgregado.Nacionalidad = this.NacionalidadJugador;
                    jugadorAgregado.Altura = this.AlturaJugador;
                    jugadorAgregado.Peso = this.PesoJugador;

                    int idJugador = await this._serviceJugador.AgregarJugador_M(jugadorAgregado);

                    jugadorAgregado.Id = idJugador;

                    await this._menuJugador_VM.ActualizarDatosVentanas(jugadorAgregado);

                    this._agregarJugadorWindow.Close();

                }
                else MessageBox.Show("DATOS INTRODUCIDOS DE FORMA INCORRECTA", "AGREGAR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL AGREGAR AL JUGADOR", "AGREGAR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
