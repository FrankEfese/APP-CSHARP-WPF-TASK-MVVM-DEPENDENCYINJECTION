using Wpf_Task_Mvvm.Services;
using Wpf_Task_Mvvm.Tools;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using Wpf_Task_Mvvm.Models;

namespace Wpf_Task_Mvvm.MenuEquipo.OpcionesEquipo.AgregarEquipo
{
    public class AgregarEquipo_VM : INotifyPropertyChanged
    {

        // VENTANA-AGREGAREQUIPO

        public Window _agregarEquipoWindow { get; set; }


        // VIEWMODEL-MENUEQUIPO

        private MenuEquipo_VM _menuEquipo_VM;


        // VARIABLES COMMAND
        public ICommand AgregarEquipoCommand { get; private set; }


        // INYECCION DE DEPENDENCIA

        private readonly EquipoService _serviceEquipo;


        public AgregarEquipo_VM(MenuEquipo_VM menuEquipo_VM, EquipoService serviceEquipo) // CONSTRUCTOR CON LA DEPENDENCIA
        {
            this._menuEquipo_VM = menuEquipo_VM;
            this._serviceEquipo = serviceEquipo;

            this.AgregarEquipoCommand = new AsyncRelayComand(AgregarEquipo);
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

        public async Task AgregarEquipo() // METODO PARA AGREGAR AL EQUIPO
        {

            try
            {

                if (Validaciones.ValidarEquipo(this.NombreEquipo, this.LigaEquipo))
                {

                    Equipo equipoAgregado = new Equipo();
                    equipoAgregado.Nombre = this.NombreEquipo;
                    equipoAgregado.Liga = this.LigaEquipo;

                    int idEquipo = await this._serviceEquipo.AgregarEquipo_M(equipoAgregado);

                    equipoAgregado.Id = idEquipo;

                    await this._menuEquipo_VM.ActualizarDatosVentanas(equipoAgregado);

                    this._agregarEquipoWindow.Close();

                }
                else MessageBox.Show("DATOS INTRODUCIDOS DE FORMA INCORRECTA", "AGREGAR EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("SE HA PRODUCIDO UN ERROR AL AGREGAR AL EQUIPO", "AGREGAR EQUIPO", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
