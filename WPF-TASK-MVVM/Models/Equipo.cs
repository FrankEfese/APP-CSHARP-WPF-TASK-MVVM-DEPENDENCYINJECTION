using System.ComponentModel;

namespace Wpf_Task_Mvvm.Models
{
    public class Equipo : INotifyPropertyChanged
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

        private string _liga;
        public string Liga
        {
            get => _liga;
            set
            {
                if (_liga != value)
                {
                    _liga = value;
                    OnPropertyChanged(nameof(Liga));
                }
            }
        }

        private List<Jugador>? _jugadores;
        public List<Jugador>? Jugadores
        {
            get => _jugadores;
            set
            {
                if (_jugadores != value)
                {
                    _jugadores = value;
                    OnPropertyChanged(nameof(Jugadores));
                    OnPropertyChanged(nameof(TotalJugadores));
                }
            }
        }

        // VARIABLE PARA OBTENER EL TOTAL DE JUGADORES
        public int? TotalJugadores
        {
            get
            {
                return Jugadores?.Count ?? 0;
            }
        }
    }
}
