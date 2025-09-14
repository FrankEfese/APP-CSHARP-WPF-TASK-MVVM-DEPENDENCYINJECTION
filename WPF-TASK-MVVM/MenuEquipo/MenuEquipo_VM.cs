using Wpf_Task_Mvvm.Models;
using Wpf_Task_Mvvm.Services;
using Wpf_Task_Mvvm.Tools;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;
using System.Windows.Input;
using Wpf_Task_Mvvm.MenuEquipo.OpcionesEquipo.AgregarEquipo;
using Wpf_Task_Mvvm.MenuEquipo.OpcionesEquipo.VerEquipo;
using Wpf_Task_Mvvm.MenuEquipo.OpcionesEquipo.ActualizarEquipo;
using Microsoft.Extensions.DependencyInjection;


namespace Wpf_Task_Mvvm.MenuEquipo
{
    public class MenuEquipo_VM : INotifyPropertyChanged
    {

        // VARIABLES VENTANAS-OPCIONES

        public VerEquipo_V verEquipo = null;
        public AgregarEquipo_V agregarEquipo = null;
        public ActualizarEquipo_V actualizarEquipo = null;


        // VARIABLES VIEW-MODEL

        public VerEquipo_VM verEquipo_VM = null;
        public AgregarEquipo_VM agregarEquipo_VM = null;
        public ActualizarEquipo_VM actualizarEquipo_VM = null;


        // VARIABLES COMMAND
        public ICommand RecargarDatosCommand { get; private set; }
        public ICommand VerEquipoCommand { get; private set; }
        public ICommand AgregarEquipoCommand { get; private set; }
        public ICommand ActualizarEquipoCommand { get; private set; }
        public ICommand EliminarEquipoCommand { get; private set; }


        // INYECCION DE DEPENDENCIAS

        private readonly EquipoService _serviceEquipo;


        public MenuEquipo_VM(EquipoService serviceEquipo) // CONSTRUCTOR CON LAS DEPENDENCIAS
        {
            this._serviceEquipo = serviceEquipo;

            this.RecargarDatosCommand = new AsyncRelayComand(RecargarDatos);
            this.VerEquipoCommand = new AsyncRelayComand(VerEquipo);
            this.AgregarEquipoCommand = new RelayComand(AgregarEquipo);
            this.ActualizarEquipoCommand = new AsyncRelayComand(ActualizarEquipo);
            this.EliminarEquipoCommand = new AsyncRelayComand(EliminarEquipo);
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


        private ObservableCollection<Equipo> _equipos = new ObservableCollection<Equipo>();
        public ObservableCollection<Equipo> Equipos
        {
            get => _equipos;
            set
            {
                if (_equipos != value)
                {
                    _equipos = value;
                    OnPropertyChanged(nameof(Equipos));
                    FiltrarEquipos();
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


        private string _filtroEquipo;
        public string FiltroEquipo
        {
            get => _filtroEquipo;
            set
            {
                if (_filtroEquipo != value)
                {
                    _filtroEquipo = value;
                    OnPropertyChanged(nameof(FiltroEquipo));
                    FiltrarEquipos();
                }
            }
        }


        private Equipo _equipoSeleccionado;
        public Equipo EquipoSeleccionado
        {
            get => _equipoSeleccionado;
            set
            {
                if (_equipoSeleccionado != value)
                {
                    _equipoSeleccionado = value;
                    OnPropertyChanged(nameof(EquipoSeleccionado));
                }
            }
        }



        // METODOS


        public void CerrarVentanas() // METODO PARA CERRAR TODAS LAS VENTANAS DE OPCIONES
        {

            if (this.verEquipo != null) this.verEquipo.Close();

            if (this.agregarEquipo != null) this.agregarEquipo.Close();

            if (this.actualizarEquipo != null) this.actualizarEquipo.Close();

        }


        public async Task CargarDatos() // METODO PARA CARGAR LOS DATOS DE LOS EQUIPOS
        {

            try
            {

                var equiposObtenidos = await this._serviceEquipo.ObtenerEquipos_M();

                this.Equipos = new ObservableCollection<Equipo>(equiposObtenidos);

                this.DatosGridView = CollectionViewSource.GetDefaultView(this.Equipos);

                this.TotalEquipos = this.Equipos.Count();

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL CARGAR LOS DATOS DE LOS EQUIPOS EN LA VENTANA", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public void FiltrarEquipos() // METODO PARA FILTRAR POR NOMBRE DEL EQUIPO
        {

            try
            {

                if (this.DatosGridView == null) return;

                this.DatosGridView.Filter = item =>
                {
                    if (item is Equipo equipo)
                    {
                        return string.IsNullOrEmpty(this.FiltroEquipo) ||
                               equipo.Nombre.Contains(this.FiltroEquipo, StringComparison.OrdinalIgnoreCase);
                    }
                    return false;
                };

                this.DatosGridView.Refresh();

                this.TotalEquipos = this.DatosGridView.Cast<object>().Count();

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL FILTRAR LOS DATOS", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task RecargarDatos() // METODO PARA RECARGAR LOS DATOS
        {
            this.FiltroEquipo = "";

            await CargarDatos();
        }


        public async Task VerEquipo() // METODO PARA ABRIR LA VENTANA VER LOS DATOS DE UN EQUIPO
        {

            try
            {

                if (this.EquipoSeleccionado != null)
                {

                    if (this.verEquipo == null || !this.verEquipo.IsLoaded)
                    {

                        var serviceEquipo = App.ServiceProvider.GetRequiredService<EquipoService>();
                        var serviceJugador = App.ServiceProvider.GetRequiredService<JugadorService>();
                        this.verEquipo_VM = new VerEquipo_VM(this,serviceEquipo, serviceJugador);
                        this.verEquipo = new VerEquipo_V(this.verEquipo_VM);
                        this.verEquipo_VM.idEquipo = this.EquipoSeleccionado.Id;
                        this.verEquipo.Show();

                    }
                    else
                    {

                        this.verEquipo_VM.idEquipo = this.EquipoSeleccionado.Id;
                        this.verEquipo.Show();
                        this.verEquipo.Focus();
                        await this.verEquipo_VM.CargarDatos();

                    }

                }
                else MessageBox.Show("DEBES SELECCIONAR UNA FILA", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL MOSTRAR LA VENTANA DE VER EQUIPO", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public void AgregarEquipo() // METODO PARA ABRIR LA VENTANA AGREGAR EQUIPO
        {

            try
            {

                if (this.agregarEquipo == null || !this.agregarEquipo.IsLoaded)
                {

                    var serviceEquipo = App.ServiceProvider.GetRequiredService<EquipoService>();
                    this.agregarEquipo_VM = new AgregarEquipo_VM(this, serviceEquipo);
                    this.agregarEquipo = new AgregarEquipo_V(this.agregarEquipo_VM);
                    this.agregarEquipo.Show();

                }
                else
                {

                    this.agregarEquipo.Show();
                    this.agregarEquipo.Focus();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL MOSTRAR LA VENTANA DE AGREGAR EQUIPO", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task ActualizarEquipo() // METODO PARA ABRIR LA VENTANA DE ACTUALIZAR UN EQUIPO
        {

            try
            {

                if (this.EquipoSeleccionado != null)
                {

                    if (this.actualizarEquipo == null || !this.actualizarEquipo.IsLoaded)
                    {

                        var serviceEquipo = App.ServiceProvider.GetRequiredService<EquipoService>();
                        this.actualizarEquipo_VM = new ActualizarEquipo_VM(this, serviceEquipo);
                        this.actualizarEquipo = new ActualizarEquipo_V(this.actualizarEquipo_VM);
                        this.actualizarEquipo_VM.idEquipo = this.EquipoSeleccionado.Id;
                        this.actualizarEquipo.Show();

                    }
                    else
                    {

                        this.actualizarEquipo_VM.idEquipo = this.EquipoSeleccionado.Id;
                        this.actualizarEquipo.Show();
                        this.actualizarEquipo.Focus();
                        await this.actualizarEquipo_VM.CargarDatos();

                    }

                }
                else MessageBox.Show("DEBES SELECCIONAR UNA FILA", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL MOSTRAR LA VENTANA DE ACTUALIZAR DE EQUIPO", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task EliminarEquipo() // METODO PARA ELIMINAR UN EQUIPO
        {

            try
            {

                if (this.EquipoSeleccionado != null)
                {

                    MessageBoxResult alerta = MessageBox.Show("¿Estás seguro de que quieres eliminar al Equipo?", "ELIMINAR EQUIPO", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                    if (alerta == MessageBoxResult.OK)
                    {

                        int idEquipo = this.EquipoSeleccionado.Id;

                        var eliminado = await this._serviceEquipo.EliminarEquipo_M(this.EquipoSeleccionado.Id);

                        if (eliminado)
                        {

                            this.Equipos.Remove(this.EquipoSeleccionado);

                            this.TotalEquipos = this.Equipos.Count();

                            if (this.verEquipo != null && this.verEquipo_VM.idEquipo == idEquipo) this.verEquipo.Close();

                            else if (this.verEquipo != null) await this.verEquipo_VM.CargarDatos();

                            if (this.actualizarEquipo != null && this.actualizarEquipo_VM.idEquipo == idEquipo) this.actualizarEquipo.Close();

                        }                       

                    }

                }
                else MessageBox.Show("DEBES SELECCIONAR UNA FILA", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ELIMINAR AL EQUIPO", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task ActualizarDatosVentanas(Equipo equipoActualizado) // METODO PARA ACTUALIZAR LOS DATOS DE LAS DISTINTAS VENTANAS DESPUES DE UNA ACTUALIZACION O INSERCCION DE UN EQUIPO
        {

            try
            {

                var equipo = this.Equipos.FirstOrDefault(j => j.Id == equipoActualizado.Id);

                if (equipo != null)
                {
                    equipo.Nombre = equipoActualizado.Nombre;
                    equipo.Liga = equipoActualizado.Liga;


                    if (equipoActualizado.Jugadores != null) equipo.Jugadores = equipoActualizado.Jugadores;

                    if (this.verEquipo != null && this.verEquipo_VM.idEquipo == equipoActualizado.Id) await this.verEquipo_VM.CargarDatos();

                }
                else if (equipoActualizado.Id != -1)
                {
                    this.Equipos.Add(equipoActualizado);
                    this.TotalEquipos = this.Equipos.Count();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ACTUALIZAR LOS DATOS DE LAS VENTANAS", "MENU EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
