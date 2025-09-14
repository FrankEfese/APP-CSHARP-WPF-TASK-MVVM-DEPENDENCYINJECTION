using Wpf_Task_Mvvm.MenuJugador.OpcionesJugador.ActualizarJugador;
using Wpf_Task_Mvvm.MenuJugador.OpcionesJugador.AgregarJugador;
using Wpf_Task_Mvvm.MenuJugador.OpcionesJugador.VerJugador;
using Wpf_Task_Mvvm.Models;
using Wpf_Task_Mvvm.Services;
using Wpf_Task_Mvvm.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Wpf_Task_Mvvm.MenuJugador
{
    public class MenuJugador_VM : INotifyPropertyChanged
    {

        // VARIABLES VENTANAS-OPCIONES

        public VerJugador_V verJugador = null;
        public AgregarJugador_V agregarJugador = null;
        public ActualizarJugador_V actualizarJugador = null;


        // VARIABLES VIEW-MODEL

        public VerJugador_VM verJugador_VM = null;
        public AgregarJugador_VM agregarJugador_VM = null;
        public ActualizarJugador_VM actualizarJugador_VM = null;


        // VARIABLES COMMAND
        public ICommand RecargarDatosCommand { get; private set; }
        public ICommand VerJugadorCommand { get; private set; }
        public ICommand AgregarJugadorCommand { get; private set; }
        public ICommand ActualizarJugadorCommand { get; private set; }
        public ICommand EliminarJugadorCommand { get; private set; }


        // INYECCION DE DEPENDENCIAS

        private readonly JugadorService _serviceJugador;


        public MenuJugador_VM(JugadorService serviceJugador) // CONSTRUCTOR CON LAS DEPENDENCIAS
        {
            this._serviceJugador = serviceJugador;

            this.RecargarDatosCommand = new AsyncRelayComand(RecargarDatos);
            this.VerJugadorCommand = new AsyncRelayComand(VerJugador);
            this.AgregarJugadorCommand = new RelayComand(AgregarJugador);
            this.ActualizarJugadorCommand = new AsyncRelayComand(ActualizarJugador);
            this.EliminarJugadorCommand = new AsyncRelayComand(EliminarJugador);
        }


        // IMPLEMENTACION DE INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        // VARIABLES DATABINDING

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
                    FiltrarJugadores();
                }
            }
        }


        private ICollectionView _datosGridView;
        public ICollectionView DatosGridView
        {
            get => _datosGridView;
            private set
            {
                _datosGridView = value;
                OnPropertyChanged(nameof(DatosGridView));
            }
        }


        private string _filtroJugador;
        public string FiltroJugador
        {
            get => _filtroJugador;
            set
            {
                if (_filtroJugador != value)
                {
                    _filtroJugador = value;
                    OnPropertyChanged(nameof(FiltroJugador));
                    FiltrarJugadores();
                }
            }
        }


        private Jugador _jugadorSeleccionado;
        public Jugador JugadorSeleccionado
        {
            get => _jugadorSeleccionado;
            set
            {
                if (_jugadorSeleccionado != value)
                {
                    _jugadorSeleccionado = value;
                    OnPropertyChanged(nameof(JugadorSeleccionado));
                }
            }
        }



        // METODOS


        public void CerrarVentanas() // METODO PARA CERRAR TODAS LAS VENTANAS DE OPCIONES
        {

            if (this.verJugador != null) this.verJugador.Close();

            if (this.agregarJugador != null) this.agregarJugador.Close();

            if (this.actualizarJugador != null) this.actualizarJugador.Close();

        }


        public async Task CargarDatos() // METODO PARA CARGAR LOS DATOS DE LOS JUGADORES
        {

            try
            {

                var jugadoresObtenidos = await this._serviceJugador.ObtenerJugadores_M();

                this.Jugadores = new ObservableCollection<Jugador>(jugadoresObtenidos);

                this.DatosGridView = CollectionViewSource.GetDefaultView(this.Jugadores);

                this.TotalJugadores = this.Jugadores.Count();

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL CARGAR LOS DATOS DE LOS JUGADORES EN LA VENTANA", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public void FiltrarJugadores() // METODO PARA FILTRAR POR NOMBRE DEL JUGADOR
        {

            try
            {

                if (this.DatosGridView == null) return;

                this.DatosGridView.Filter = item =>
                {
                    if (item is Jugador jugador)
                    {
                        return string.IsNullOrEmpty(this.FiltroJugador) ||
                               jugador.Nombre.Contains(this.FiltroJugador, StringComparison.OrdinalIgnoreCase);
                    }
                    return false;
                };

                this.DatosGridView.Refresh();

                this.TotalJugadores = this.DatosGridView.Cast<object>().Count();

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL FILTRAR LOS DATOS", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }


        public async Task RecargarDatos() // METODO PARA RECARGAR LOS DATOS
        {
            this.FiltroJugador = "";

            await CargarDatos();
        }


        public async Task VerJugador() // METODO PARA ABRIR LA VENTANA VER LOS DATOS DE UN JUGADOR
        {

            try
            {

                if (this.JugadorSeleccionado != null)
                {

                    if(this.verJugador == null || !this.verJugador.IsLoaded)
                    {

                        var serviceJugador = App.ServiceProvider.GetRequiredService<JugadorService>();
                        this.verJugador_VM = new VerJugador_VM(serviceJugador);
                        this.verJugador = new VerJugador_V(this.verJugador_VM);
                        this.verJugador_VM.idJugador = this.JugadorSeleccionado.Id;
                        this.verJugador.Show();

                    }else
                    {

                        this.verJugador_VM.idJugador = this.JugadorSeleccionado.Id;
                        this.verJugador.Show();
                        this.verJugador.Focus();
                        await this.verJugador_VM.CargarDatos();

                    }

                }
                else MessageBox.Show("DEBES SELECCIONAR UNA FILA", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL MOSTRAR LA VENTANA DE VER JUGADOR", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public void AgregarJugador() // METODO PARA ABRIR LA VENTANA AGREGAR JUGADOR
        {

            try
            {

                if (this.agregarJugador == null || !this.agregarJugador.IsLoaded)
                {

                    var serviceJugador = App.ServiceProvider.GetRequiredService<JugadorService>();
                    this.agregarJugador_VM = new AgregarJugador_VM(this,serviceJugador);
                    this.agregarJugador = new AgregarJugador_V(this.agregarJugador_VM);
                    this.agregarJugador.Show();

                }
                else
                {

                    this.agregarJugador.Show();
                    this.agregarJugador.Focus();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL MOSTRAR LA VENTANA DE AGREGAR JUGADOR", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task ActualizarJugador() // METODO PARA ABRIR LA VENTANA DE ACTUALIZAR UN JUGADOR
        {

            try
            {

                if (this.JugadorSeleccionado != null)
                {

                    if (this.actualizarJugador == null || !this.actualizarJugador.IsLoaded)
                    {

                        var serviceJugador = App.ServiceProvider.GetRequiredService<JugadorService>();
                        this.actualizarJugador_VM = new ActualizarJugador_VM(this, serviceJugador);
                        this.actualizarJugador = new ActualizarJugador_V(this.actualizarJugador_VM);
                        this.actualizarJugador_VM.idJugador = this.JugadorSeleccionado.Id;
                        this.actualizarJugador.Show();

                    }
                    else
                    {

                        this.actualizarJugador_VM.idJugador = this.JugadorSeleccionado.Id;
                        this.actualizarJugador.Show();
                        this.actualizarJugador.Focus();
                        await this.actualizarJugador_VM.CargarDatos();

                    }

                }
                else MessageBox.Show("DEBES SELECCIONAR UNA FILA", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL MOSTRAR LA VENTANA DE ACTUALIZAR DE JUGADOR", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        public async Task EliminarJugador() // METODO PARA ELIMINAR UN JUGADOR
        {

            try
            {

                if (this.JugadorSeleccionado != null)
                {

                    MessageBoxResult alerta = MessageBox.Show("¿Estás seguro de que quieres eliminar al Jugador?", "ELIMINAR JUGADOR", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (alerta == MessageBoxResult.OK)
                    {

                        int idJugador = this.JugadorSeleccionado.Id;

                        var eliminado = await this._serviceJugador.EliminarJugador_M(this.JugadorSeleccionado.Id);

                        if (eliminado)
                        {

                            this.Jugadores.Remove(this.JugadorSeleccionado);

                            this.TotalJugadores = this.Jugadores.Count();

                            if (this.verJugador != null && this.verJugador_VM.idJugador == idJugador) this.verJugador.Close();

                            if (this.actualizarJugador != null && this.actualizarJugador_VM.idJugador == idJugador) this.actualizarJugador.Close();

                        }

                    }

                }
                else MessageBox.Show("DEBES SELECCIONAR UNA FILA", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ELIMINAR AL JUGADOR", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task ActualizarDatosVentanas(Jugador jugadorActualizado) // METODO PARA ACTUALIZAR LOS DATOS DE LAS DISTINTAS VENTANAS DESPUES DE UNA ACTUALIZACION O INSERCCION DE UN JUGADOR
        {

            try
            {

                var jugador = this.Jugadores.FirstOrDefault(j => j.Id == jugadorActualizado.Id);

                if (jugador != null)
                {
                    jugador.Nombre = jugadorActualizado.Nombre;
                    jugador.Edad = jugadorActualizado.Edad;
                    jugador.Nacionalidad = jugadorActualizado.Nacionalidad;
                    jugador.Altura = jugadorActualizado.Altura;
                    jugador.Peso = jugadorActualizado.Peso;

                    if (this.verJugador != null && this.verJugador_VM.idJugador == jugadorActualizado.Id) await this.verJugador_VM.CargarDatos();
                }
                else if (jugadorActualizado.Id != -1)
                {
                    this.Jugadores.Add(jugadorActualizado);
                    this.TotalJugadores = this.Jugadores.Count();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ACTUALIZAR LOS DATOS DE LAS VENTANAS", "MENU JUGADOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
