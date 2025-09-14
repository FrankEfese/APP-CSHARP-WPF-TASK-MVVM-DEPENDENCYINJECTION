using Wpf_Task_Mvvm.Services;
using System.ComponentModel;
using System.Windows;

namespace Wpf_Task_Mvvm.MenuJugador.OpcionesJugador.VerJugador
{
    public class VerJugador_VM : INotifyPropertyChanged
    {

        // VARIABLE ID-JUGADOR

        public int idJugador = -1;


        // INYECCION DE DEPENDENCIA

        private readonly JugadorService _serviceJugador;


        public VerJugador_VM(JugadorService serviceJugador) // CONSTRUCTOR CON LA DEPENDENCIA
        {
            this._serviceJugador = serviceJugador;
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


        private string _edadJugador;
        public string EdadJugador
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


        private string _alturaJugador;
        public string AlturaJugador
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


        private string _pesoJugador;
        public string PesoJugador
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


        private string _equipoJugador;
        public string EquipoJugador
        {
            get => _equipoJugador;
            set
            {
                if (_equipoJugador != value)
                {
                    _equipoJugador = value;
                    OnPropertyChanged(nameof(EquipoJugador));

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
                    this.EdadJugador = jugador.Edad.ToString();
                    this.NacionalidadJugador = jugador.Nacionalidad;
                    this.AlturaJugador = jugador.Altura.ToString();
                    this.PesoJugador = jugador.Peso.ToString();
                    this.EquipoJugador = jugador.NombreEquipo;

                }

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL CARGAR LOS DATOS DEL JUGADOR", "VER JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
