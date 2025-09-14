using System.Windows;
using Wpf_Task_Mvvm.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Wpf_Task_Mvvm.Config;


namespace Wpf_Task_Mvvm.Services
{
    public class JugadorService
    {

        static readonly HttpClient client = new HttpClient(); // VARIABLE ESTATICA PARA REALIZAR PETICIONES A LA API


        // METODOS


        public async Task<int> AgregarJugador_M(Jugador jugador) // METODO PARA AGREGAR UN JUGADOR
        {

            try
            {

                await AuthService.ActualizarToken();

                var json = JsonConvert.SerializeObject(jugador);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.PostAsync("https://localhost:7232/api/Jugador/post", data);

                if (response.IsSuccessStatusCode)
                {
                    int idJugador =  int.Parse(await response.Content.ReadAsStringAsync());
                    MessageBox.Show("JUGADOR AGREGADO DE FORMA EXITOSA", "AGREGAR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Information);
                    return idJugador;
                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - YA EXISTE UN JUGADOR CON EL NOMBRE INTRODUCIDO", 
                                    "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return -1;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }

        }



        public async Task<bool> ActualizarJugador_M(Jugador jugador) // METODO PARA ACTUALIZAR UN JUGADOR
        {

            try
            {

                await AuthService.ActualizarToken();

                var json = JsonConvert.SerializeObject(jugador);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.PutAsync($"https://localhost:7232/api/Jugador/put/{jugador.Id}", data);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("JUGADOR ACTUALIZADO DE FORMA EXITOSA", "ACTUALIZAR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - YA EXISTE UN JUGADOR CON EL NOMBRE INTRODUCIDO",
                                    "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - ID DE JUGADOR INCORRECTO",
                                    "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }


            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }



        public async Task<bool> InscribirJugador_M(int idJugador , int idEquipo) // METODO PARA INSCRIBIR UN JUGADOR PARADO A UN EQUIPO
        {

            try
            {

                await AuthService.ActualizarToken();

                var json = JsonConvert.SerializeObject(idEquipo);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.PutAsync($"https://localhost:7232/api/Jugador/put/signing/{idJugador}", data);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("JUGADOR INSCRITO DE FORMA EXITOSA", "INSCRIBIR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - ID DE JUGADOR INCORRECTO",
                                    "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }



        public async Task<bool> DespedirJugador_M(int idJugador , int idEquipo) // METODO PARA DESPEDIR UN JUGADOR DE UN EQUIPO
        {

            try
            {

                await AuthService.ActualizarToken();

                var json = JsonConvert.SerializeObject(idEquipo);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.PutAsync($"https://localhost:7232/api/Jugador/put/termination/{idJugador}", data);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("JUGADOR DESPEDIDO DE FORMA EXITOSA", "DESPEDIR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - ID DE JUGADOR INCORRECTO", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }



        public async Task<bool> EliminarJugador_M(int idJugador) // METODO PARA ELIMINAR UN JUGADOR
        {

            try
            {

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7232/api/Jugador/{idJugador}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("JUGADOR ELIMINADO DE FORMA EXITOSA", "ELIMINAR JUGADOR", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - ID DE JUGADOR INCORRECTO", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }



        public async Task<List<Jugador>> ObtenerJugadores_M() // METODO PARA OBTENER TODOS LOS JUGADORES
        {

            try
            {
                await AuthService.ActualizarToken();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.GetAsync("https://localhost:7232/api/Jugador");

                if (response.IsSuccessStatusCode)
                {

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var jugadores = JsonConvert.DeserializeObject<List<Jugador>>(responseBody);
                    return jugadores;

                }
                else
                {
                    return new List<Jugador>();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Jugador>();
            }

        }



        public async Task<List<Jugador>> ObtenerJugadoresParados_M() // METODO PARA OBTENER TODOS LOS JUGADORES PARADOS
        {

            try
            {
                await AuthService.ActualizarToken();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.GetAsync("https://localhost:7232/api/Jugador/terminates");

                if (response.IsSuccessStatusCode)
                {

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var jugadores = JsonConvert.DeserializeObject<List<Jugador>>(responseBody);
                    return jugadores;

                }
                else
                {
                    return new List<Jugador>();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Jugador>();
            }

        }



        public async Task<Jugador> ObtenerUnJugador_M(int idJugador) // METODO PARA OBTNER UN JUGADOR
        {

            try
            {

                await AuthService.ActualizarToken();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7232/api/Jugador/{idJugador}");

                if (response.IsSuccessStatusCode)
                {

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var jugador = JsonConvert.DeserializeObject<Jugador>(responseBody);
                    return jugador;

                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - ID DE JUGADOR INCORRECTO", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }



        public async Task<int> ObtenerTotal_M() // METODO PARA OBTENER EL TOTAL DE JUGADORES
        {

            try
            {
                await AuthService.ActualizarToken();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.GetAsync("https://localhost:7232/api/Jugador/count");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var total = JsonConvert.DeserializeObject<int>(responseBody);
                    return total;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

        }



        public async Task<int> ObtenerTotalParados_M() // METODO PARA OBTENER EL TOTAL DE JUGADORES PARADOS
        {

            try
            {
                await AuthService.ActualizarToken();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.GetAsync("https://localhost:7232/api/Jugador/countTerminates");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var total = JsonConvert.DeserializeObject<int>(responseBody);
                    return total;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

        }

    }
}
