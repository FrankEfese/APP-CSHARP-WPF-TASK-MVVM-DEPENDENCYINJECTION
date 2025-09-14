namespace Wpf_Task_Mvvm.Tools
{
    public class Validaciones
    {

        // METODOS

        public static bool ValidarEquipo(string nombreEquipo , string nombreLiga) // METODO PARA VALIDAR LOS DATOS DE UN EQUIPO
        {
            if(!String.IsNullOrEmpty(nombreEquipo) && !String.IsNullOrEmpty(nombreLiga)) return true;
            else return false;
        }


        public static bool ValidarJugador(string nombreJugador, int edadJugador, string nacionalidadJugador, int alturaJugador, int pesoJugador) // METODO PARA VALIDAR LOS DATOS DE UN JUGADOR
        {
            if (!String.IsNullOrEmpty(nombreJugador) && !String.IsNullOrEmpty(nacionalidadJugador) && (edadJugador > 16 && edadJugador < 45) &&
                (alturaJugador > 100 && alturaJugador < 250) && (pesoJugador > 30 && pesoJugador < 180)) return true;
            else return false;
        }

    }
}
