using Wpf_Task_Mvvm.Services;
using Wpf_Task_Mvvm.Tools;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using Wpf_Task_Mvvm.Models;

namespace Wpf_Task_Mvvm.MenuEquipo.OpcionesEquipo.ActualizarEquipo
{
    public class ActualizarEquipo_VM : INotifyPropertyChanged
    {

        // VARIABLE ID-EQUIPO

        public int idEquipo = -1;


        // VENTANA-ACTUALIZAREQUIPO

        public Window _actualizarEquipoWindow { get; set; }


        // VIEWMODEL-MENUEQUIPO

        private MenuEquipo_VM _menuEquipo_VM;


        // VARIABLES COMMAND
        public ICommand ActualizarEquipoCommand { get; private set; }


        // INYECCION DE DEPENDENCIA

        private readonly EquipoService _serviceEquipo;


        public ActualizarEquipo_VM(MenuEquipo_VM menuEquipo_VM, EquipoService serviceEquipo) // CONSTRUCTOR CON LA DEPENDENCIA
        {
            this._menuEquipo_VM = menuEquipo_VM;
            this._serviceEquipo = serviceEquipo;

            this.ActualizarEquipoCommand = new AsyncRelayComand(ActualizarEquipo);
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



        // METODOS

        public async Task CargarDatos() // METODO PARA CARGAR LOS DATOS DEL EQUIPO
        {

            try
            {

                var equipo = await this._serviceEquipo.ObtenerUnEquipo_M(this.idEquipo);

                if(equipo != null)
                {
                    this.NombreEquipo = equipo.Nombre;
                    this.LigaEquipo = equipo.Liga;
                }               

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL CARGAR LOS DATOS DEL EQUIPO", "ACTUALIZAR EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        public async Task ActualizarEquipo() // METODO PARA ACTUALIZAR AL EQUIPO
        {

            try
            {

                if (Validaciones.ValidarEquipo(this.NombreEquipo, this.LigaEquipo))
                {

                    Equipo equipoActualizado = new Equipo();
                    equipoActualizado.Id = this.idEquipo;
                    equipoActualizado.Nombre = this.NombreEquipo;
                    equipoActualizado.Liga = this.LigaEquipo;

                    var actualizado = await this._serviceEquipo.ActualizarEquipo_M(equipoActualizado);

                    if (actualizado) await this._menuEquipo_VM.ActualizarDatosVentanas(equipoActualizado);

                    this._actualizarEquipoWindow.Close();

                }
                else MessageBox.Show("DATOS INTRODUCIDOS DE FORMA INCORRECTA", "ACTUALIZAR EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL ACTUALIZAR LOS DATOS DEL EQUIPO", "ACTUALIZAR EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
