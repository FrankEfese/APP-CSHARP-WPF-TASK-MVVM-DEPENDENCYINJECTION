using Wpf_Task_Mvvm.Models;
using Wpf_Task_Mvvm.Services;

namespace Wpf_Task_Mvvm.Config
{
    public class AppConfig
    {

        // VARIABLES ESTATICAS 

        public static string jwtToken = "";

        public static UserCredentials user = new UserCredentials();


        // METODOS

        public static async Task<bool> InicioSesion(string correoUsuario , string passUsuario) // METODO PARA EL INICIO DE SESION DEL USUARIO
        {

            user.usuario = correoUsuario;
            user.password = passUsuario;

            if(await AuthService.AutenticarYObtenerToken(user)) return true;
            else return false;

        }

    }
}
