using System.ComponentModel;

namespace Wpf_Task_Mvvm.Models
{
    public class Jugador : INotifyPropertyChanged
    {

        // IMPLEMENTACIÓN DE INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // ATRIBUTOS

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _nombre;
        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        private int _edad;
        public int Edad
        {
            get => _edad;
            set
            {
                if (_edad != value)
                {
                    _edad = value;
                    OnPropertyChanged(nameof(Edad));
                }
            }
        }

        private string _nacionalidad;
        public string Nacionalidad
        {
            get => _nacionalidad;
            set
            {
                if (_nacionalidad != value)
                {
                    _nacionalidad = value;
                    OnPropertyChanged(nameof(Nacionalidad));
                }
            }
        }

        private int _altura;
        public int Altura
        {
            get => _altura;
            set
            {
                if (_altura != value)
                {
                    _altura = value;
                    OnPropertyChanged(nameof(Altura));
                }
            }
        }

        private int _peso;
        public int Peso
        {
            get => _peso;
            set
            {
                if (_peso != value)
                {
                    _peso = value;
                    OnPropertyChanged(nameof(Peso));
                }
            }
        }

        private int? _idEquipo;
        public int? IdEquipo
        {
            get => _idEquipo;
            set
            {
                if (_idEquipo != value)
                {
                    _idEquipo = value;
                    OnPropertyChanged(nameof(IdEquipo));
                }
            }
        }

        private Equipo? _equipo;
        public Equipo? Equipo
        {
            get => _equipo;
            set
            {
                if (_equipo != value)
                {
                    _equipo = value;
                    OnPropertyChanged(nameof(Equipo));
                    OnPropertyChanged(nameof(NombreEquipo));
                }
            }
        }


        // VARIABLE PARA OBTENER EL NOMBRE DEL EQUIPO
        public string NombreEquipo => Equipo?.Nombre ?? "*Sin Equipo*";

    }

}

