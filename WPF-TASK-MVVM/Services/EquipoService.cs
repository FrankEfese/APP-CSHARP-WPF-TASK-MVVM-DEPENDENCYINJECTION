using System.Windows;
using Wpf_Task_Mvvm.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Wpf_Task_Mvvm.Config;

namespace Wpf_Task_Mvvm.Services
{
    public class EquipoService
    {

        static readonly HttpClient client = new HttpClient(); // VARIABLE ESTATICA PARA REALIZAR PETICIONES A LA API


        // METODOS

        public async Task<int> AgregarEquipo_M(Equipo equipo) // METODO PARA AGREGAR UN EQUIPO
        {

            try
            {

                await AuthService.ActualizarToken();

                var json = JsonConvert.SerializeObject(equipo);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.PostAsync("https://localhost:7232/api/Equipo/post", data);

                if (response.IsSuccessStatusCode)
                {
                    int idEquipo = int.Parse(await response.Content.ReadAsStringAsync());
                    MessageBox.Show("EQUIPO AGREGADO DE FORMA EXITOSA", "AGREGAR EQUIPO", MessageBoxButton.OK, MessageBoxImage.Information);
                    return idEquipo;
                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - YA EXISTE UN EQUIPO CON EL NOMBRE INTRODUCIDO",
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



        public async Task<bool> ActualizarEquipo_M(Equipo equipo) // METODO PARA ACTUALIZAR UN EQUIPO
        {

            try
            {

                await AuthService.ActualizarToken();

                var json = JsonConvert.SerializeObject(equipo);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.PutAsync($"https://localhost:7232/api/Equipo/put/{equipo.Id}", data);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("EQUIPO ACTUALIZADO DE FORMA EXITOSA", "ACTUALIZAR EQUIPO", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else if(response.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - YA EXISTE UN EQUIPO CON EL NOMBRE INTRODUCIDO",
                                    "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - ID DE EQUIPO INCORRECTO",
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



        public async Task<bool> EliminarEquipo_M(int idEquipo) // METODO PARA ELIMINAR UN EQUIPO
        {

            try
            {

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7232/api/Equipo/{idEquipo}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("EQUIPO ELIMINADO DE FORMA EXITOSA", "ELIMINAR EQUIPO", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - ID DE EQUIPO INCORRECTO", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }



        public async Task<List<Equipo>> ObtenerEquipos_M() // METODO PARA OBTENER TODOS LOS EQUIPOS
        {

            try
            {
                await AuthService.ActualizarToken();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.GetAsync("https://localhost:7232/api/Equipo");

                if (response.IsSuccessStatusCode)
                {

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var equipos = JsonConvert.DeserializeObject<List<Equipo>>(responseBody);
                    return equipos;
                    
                }
                else
                {
                    return new List<Equipo>();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Equipo>();
            }

        }



        public async Task<Equipo> ObtenerUnEquipo_M(int idEquipo) // METODO PARA OBTNER UN EQUIPO
        {

            try
            {

                await AuthService.ActualizarToken();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7232/api/Equipo/{idEquipo}");

                if (response.IsSuccessStatusCode)
                {

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var equipo = JsonConvert.DeserializeObject<Equipo>(responseBody);
                    return equipo;

                }
                else
                {
                    MessageBox.Show($"({response.StatusCode.ToString().ToUpper()}) - ID DE EQUIPO INCORRECTO", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CONSULTAR CON EL SERVIDOR", "ERROR DE SERVIDOR", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }



        public async Task<int> ObtenerTotal_M() // METODO PARA OBTENER EL TOTAL DE EQUIPOS
        {

            try
            {
                await AuthService.ActualizarToken();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppConfig.jwtToken);
                HttpResponseMessage response = await client.GetAsync("https://localhost:7232/api/Equipo/count");

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
