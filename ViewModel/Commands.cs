using System.Windows.Input;
using System;


namespace ViewModel
{
    public class Commands : ICommand
    {
        Action executable;
        Func<bool> canBeExecutable;

        public Commands(Action e, Func<bool> isExecutable)
        {
            this.executable = e;
            this.canBeExecutable = isExecutable;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            if (canBeExecutable == null)
            {
                return true;
            }
            if (parameter == null) { return canBeExecutable(); }
            return canBeExecutable();

        }

        public void Execute(object? parameter)
        {
            executable();
        }
    }
}
