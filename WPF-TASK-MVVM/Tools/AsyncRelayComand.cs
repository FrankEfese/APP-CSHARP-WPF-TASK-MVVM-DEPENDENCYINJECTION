using System.Windows.Input;

namespace Wpf_Task_Mvvm.Tools
{
    public class AsyncRelayComand : ICommand
    {

        // EVENTO Y FUNCION ASINCRONA

        public event EventHandler? CanExecuteChanged;

        private readonly Func<Task> _executeAsync;


        public AsyncRelayComand(Func<Task> executeAsync) // CONSTRUCTOR CON LA FUNCION ASCINCRONA
        {
            this._executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
        }

       
        // METODOS

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if (this._executeAsync != null)
            {
                await this._executeAsync();  
            }
        }

    }
}
