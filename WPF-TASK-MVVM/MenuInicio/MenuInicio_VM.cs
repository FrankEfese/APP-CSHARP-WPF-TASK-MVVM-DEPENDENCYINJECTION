using Wpf_Task_Mvvm.Services;
using System.ComponentModel;
using System.Windows;

namespace Wpf_Task_Mvvm.MenuInicio
{
    public class MenuInicio_VM : INotifyPropertyChanged
    {

        // INYECCION DE DEPENDENCIAS

        private readonly EquipoService _serviceEquipo;
        private readonly JugadorService _serviceJugador;


        public MenuInicio_VM(EquipoService serviceEquipo, JugadorService serviceJugador) // CONSTRUCTOR CON LAS DEPENDENCIAS
        {
            this._serviceEquipo = serviceEquipo;
            this._serviceJugador = serviceJugador;
        }


        // IMPLEMENTACION DE INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        // VARIABLES DATABINDING

        private int _totalEquipos;
        public int TotalEquipos
        {
            get => _totalEquipos;
            set
            {
                if (_totalEquipos != value)
                {
                    _totalEquipos = value;
                    OnPropertyChanged(nameof(TotalEquipos));
                }
            }
        }


        private int _totalJugadores;
        public int TotalJugadores
        {
            get => _totalJugadores;
            set
            {
                if (_totalJugadores != value)
                {
                    _totalJugadores = value;
                    OnPropertyChanged(nameof(TotalJugadores));
                }
            }
        }


        private int _totalJugadoresParados;
        public int TotalJugadoresParados
        {
            get => _totalJugadoresParados;
            set
            {
                if (_totalJugadoresParados != value)
                {
                    _totalJugadoresParados = value;
                    OnPropertyChanged(nameof(TotalJugadoresParados));
                }
            }
        }



        // METODOS

        public async Task CargarDatos() // METODO PARA CARGAR LOS TODOS LOS DATOS
        {

            try
            {
                var totalEquipos = await this._serviceEquipo.ObtenerTotal_M();
                var totalJugadores = await this._serviceJugador.ObtenerTotal_M();
                var totalJugadoresParados = await this._serviceJugador.ObtenerTotalParados_M();

                this.TotalEquipos = totalEquipos;
                this.TotalJugadores = totalJugadores;
                this.TotalJugadoresParados = totalJugadoresParados;
            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL CARGAR LOS DATOS EN LA VENTANA", "MENU INICIO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
