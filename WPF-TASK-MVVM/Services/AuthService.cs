using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Windows;
using Wpf_Task_Mvvm.Models;
using Wpf_Task_Mvvm.Config;

namespace Wpf_Task_Mvvm.Services
{
    public class AuthService
    {

        static readonly HttpClient client = new HttpClient(); // VARIABLE ESTATICA PARA REALIZAR PETICIONES A LA API


        // METODOS

        public static async Task<bool> AutenticarYObtenerToken(UserCredentials usuario) // METODO PARA OBTENER EL JWT
        {

            try
            {

                var json = JsonConvert.SerializeObject(usuario);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:7232/api/Auth/authenticate", data);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<dynamic>(result);
                    AppConfig.jwtToken = tokenResponse.token;
                    return true;
                }

                return false;

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }


        public static async Task ActualizarToken() // METODO PARA ACTUALIZAR EL JWT
        {

            try
            {

                var json = JsonConvert.SerializeObject(AppConfig.user);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:7232/api/Auth/authenticate", data);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<dynamic>(result);
                    AppConfig.jwtToken = tokenResponse.token;
                }
                else MessageBox.Show("ERROR AL ACTUALIZAR EL TOKEN", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
