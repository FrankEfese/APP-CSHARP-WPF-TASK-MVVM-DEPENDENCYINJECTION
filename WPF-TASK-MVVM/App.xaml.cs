using Wpf_Task_Mvvm.Login;
using Wpf_Task_Mvvm.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Wpf_Task_Mvvm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {

        public static IServiceProvider ServiceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // CONFIGURACION DEL CONTENEDOR DE DEPENDENCIAS

            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();


            // INICIO DE APLICACION CON LA VENTANA LOGIN

            Login_V login = new Login_V();
            login.Show();

        }

        private void ConfigureServices(IServiceCollection services) // METODO PARA INYECTAR LAS DEPENDENCIAS
        {
            services.AddSingleton<EquipoService>();
            services.AddSingleton<JugadorService>();
        }

    }

}
