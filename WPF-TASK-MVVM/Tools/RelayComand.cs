using System.Windows.Input;

namespace Wpf_Task_Mvvm.Tools
{
    public class RelayComand : ICommand
    {

        // EVENTO Y ACCION

        public event EventHandler? CanExecuteChanged;

        private Action _execute;


        public RelayComand(Action execute) // CONSTRUCTOR CON LA ACCION
        {
            this._execute = execute;
        }


        // METODOS

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (this._execute != null)
            {
                this._execute();
            }
        }

    }
}
