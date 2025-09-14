using Wpf_Task_Mvvm.Models;
using Wpf_Task_Mvvm.Services;
using Wpf_Task_Mvvm.Tools;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Wpf_Task_Mvvm.MenuEquipo.OpcionesEquipo.VerEquipo
{
    public class VerEquipo_VM : INotifyPropertyChanged
    {

        // VARIABLE ID-EQUIPO

        public int idEquipo = -1;


        // VIEWMODEL-MENUEQUIPO

        private MenuEquipo_VM _menuEquipo_VM;


        // VARIABLES COMMAND
        public ICommand DespedirJugadorCommand { get; private set; }
        public ICommand FicharJugadorCommand { get; private set; }


        // INYECCION DE DEPENDENCIAS

        private readonly EquipoService _serviceEquipo;
        private readonly JugadorService _serviceJugador;


        public VerEquipo_VM(MenuEquipo_VM menuEquipo_VM, EquipoService serviceEquipo, JugadorService serviceJugador) // CONSTRUCTOR CON LAS DEPENDENCIAS
        {
            this._menuEquipo_VM = menuEquipo_VM;
            this._serviceEquipo = serviceEquipo;
            this._serviceJugador = serviceJugador;

            this.DespedirJugadorCommand = new AsyncRelayComand(DespedirJugador);
            this.FicharJugadorCommand = new AsyncRelayComand(FicharJugador);
        }


        // IMPLEMENTACION DE INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        // VARIABLES DATABINDING

        private string _nombreEquipo;
        public string NombreEquipo
        {
            get => _nombreEquipo;
            set
            {
                if (_nombreEquipo != value)
                {
                    _nombreEquipo = value;
                    OnPropertyChanged(nameof(NombreEquipo));

                }
            }
        }


        private string _ligaEquipo;
        public string LigaEquipo
        {
            get => _ligaEquipo;
            set
            {
                if (_ligaEquipo != value)
                {
                    _ligaEquipo = value;
                    OnPropertyChanged(nameof(LigaEquipo));

                }
            }
        }


        private int _totalJugadoresPlantilla;
        public int TotalJugadoresPlantilla
        {
            get => _totalJugadoresPlantilla;
            set
            {
                if (_totalJugadoresPlantilla != value)
                {
                    _totalJugadoresPlantilla = value;
                    OnPropertyChanged(nameof(TotalJugadoresPlantilla));

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


        private ObservableCollection<Jugador> _jugadores = new ObservableCollection<Jugador>();
        public ObservableCollection<Jugador> Jugadores
        {
            get => _jugadores;
            set
            {
                if (_jugadores != value)
                {
                    _jugadores = value;
                    OnPropertyChanged(nameof(Jugadores));
                    ActualizarDatos();
                }
            }
        }


        private ICollectionView _dgwDatosPlantilla;
        public ICollectionView DGWDatosPlantilla
        {
            get => _dgwDatosPlantilla;
            private set
            {
                _dgwDatosPlantilla = value;
                OnPropertyChanged(nameof(DGWDatosPlantilla));
            }
        }


        private ObservableCollection<Jugador> _jugadoresParados = new ObservableCollection<Jugador>();
        public ObservableCollection<Jugador> JugadoresParados
        {
            get => _jugadoresParados;
            set
            {
                if (_jugadoresParados != value)
                {
                    _jugadoresParados = value;
                    OnPropertyChanged(nameof(JugadoresParados));
                    ActualizarDatos();
                }
            }
        }


        private ICollectionView _dgwDatosParados;
        public ICollectionView DGWDatosParados
        {
            get => _dgwDatosParados;
            private set
            {
                _dgwDatosParados = value;
                OnPropertyChanged(nameof(DGWDatosParados));
            }
        }


        private Jugador _jugadorSeleccionadoPlantilla;
        public Jugador JugadorSeleccionadoPlantilla
        {
            get => _jugadorSeleccionadoPlantilla;
            set
            {
                if (_jugadorSeleccionadoPlantilla != value)
                {
                    _jugadorSeleccionadoPlantilla = value;
                    OnPropertyChanged(nameof(JugadorSeleccionadoPlantilla));
                }
            }
        }


        private Jugador _jugadorSeleccionadoParado;
        public Jugador JugadorSeleccionadoParado
        {
            get => _jugadorSeleccionadoParado;
            set
            {
                if (_jugadorSeleccionadoParado != value)
                {
                    _jugadorSeleccionadoParado = value;
                    OnPropertyChanged(nameof(JugadorSeleccionadoParado));
                }
            }
        }



        // METODOS

        public async Task CargarDatos() // METODO PARA CARGAR LOS DATOS DEL EQUIPO Y LOS JUGADORES
        {

            try
            {

                var equipo = await this._serviceEquipo.ObtenerUnEquipo_M(this.idEquipo);

                if(equipo != null)
                {

                    this.NombreEquipo = equipo.Nombre;
                    this.LigaEquipo = equipo.Liga;

                    if (equipo.Jugadores != null)
                    {
                        this.Jugadores = new ObservableCollection<Jugador>(equipo.Jugadores);
                        this.DGWDatosPlantilla = CollectionViewSource.GetDefaultView(this.Jugadores);
                        this.TotalJugadoresPlantilla = this.Jugadores.Count();
                    }

                }                              

                var jugadoresParados = await this._serviceJugador.ObtenerJugadoresParados_M();

                this.JugadoresParados = new ObservableCollection<Jugador>(jugadoresParados);
                this.DGWDatosParados = CollectionViewSource.GetDefaultView(this.JugadoresParados);
                this.TotalJugadoresParados = this.JugadoresParados.Count();

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL CARGAR LOS DATOS DEL EQUIPO", "VER EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public void ActualizarDatos() // METODO PARA ACTUALIZAR LAS TABLAS DE LOS JUGADORES
        {

            try
            {

                if (this.DGWDatosPlantilla == null || this.DGWDatosParados == null) return;

                this.DGWDatosPlantilla.Refresh();

                this.DGWDatosParados.Refresh();

                this.TotalJugadoresPlantilla = this.DGWDatosPlantilla.Cast<object>().Count();

                this.TotalJugadoresParados = this.DGWDatosParados.Cast<object>().Count();

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ACTUALIZAR LOS DATOS DE LAS TABLAS", "VER EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task DespedirJugador() // METODO PARA DESPEDIR A UN JUGADOR DE LA PLANTILLA
        {

            try
            {

                if (this.JugadorSeleccionadoPlantilla != null)
                {

                    MessageBoxResult alerta = MessageBox.Show("¿Estás seguro de que quieres despedir a este Jugador?" , "DESPEDIR JUGADOR", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (alerta == MessageBoxResult.OK)
                    {

                        var despedido = await this._serviceJugador.DespedirJugador_M(this.JugadorSeleccionadoPlantilla.Id, this.idEquipo);

                        if (despedido)
                        {

                            var jugador = this.Jugadores.FirstOrDefault(j => j.Id == this.JugadorSeleccionadoPlantilla.Id);

                            if (jugador != null)
                            {
                                this.Jugadores.Remove(jugador);

                                this.JugadoresParados.Add(jugador);
                            }

                            this.TotalJugadoresPlantilla = this.Jugadores.Count();

                            this.TotalJugadoresParados = this.JugadoresParados.Count();

                            var equipo = await this._serviceEquipo.ObtenerUnEquipo_M(this.idEquipo);

                            if (equipo != null) await this._menuEquipo_VM.ActualizarDatosVentanas(equipo);

                        }                        

                    }

                }
                else MessageBox.Show("DEBES SELECCIONAR UNA FILA", "VER EQUIPO", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL DESPEDIR A UN JUGADOR DE LA PLANTILLA", "VER EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task FicharJugador() // METODO PARA FICHAR A UN JUGADOR DE LA PLANTILLA
        {

            try
            {

                if (this.JugadorSeleccionadoParado != null)
                {

                    MessageBoxResult alerta = MessageBox.Show("¿Estás seguro de que quieres inscribir a este Jugador?", "FICHAR JUGADOR", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (alerta == MessageBoxResult.OK)
                    {

                        var fichado = await this._serviceJugador.InscribirJugador_M(this.JugadorSeleccionadoParado.Id, this.idEquipo);

                        if (fichado)
                        {

                            var jugador = this.JugadoresParados.FirstOrDefault(j => j.Id == this.JugadorSeleccionadoParado.Id);

                            if (jugador != null)
                            {
                                this.Jugadores.Add(jugador);

                                this.JugadoresParados.Remove(jugador);
                            }

                            this.TotalJugadoresPlantilla = this.Jugadores.Count();

                            this.TotalJugadoresParados = this.JugadoresParados.Count();

                            var equipo = await this._serviceEquipo.ObtenerUnEquipo_M(this.idEquipo);

                            if (equipo != null) await this._menuEquipo_VM.ActualizarDatosVentanas(equipo);

                        }                       

                    }

                }
                else MessageBox.Show("DEBES SELECCIONAR UNA FILA", "VER EQUIPO", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL INSCRIBIR A UN JUGADOR A LA PLANTILLA", "VER EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
