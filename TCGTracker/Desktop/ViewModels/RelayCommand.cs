using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    /// <summary>
    /// This class is used to store a function
    /// so it can be attached to a property for binding.<br/>
    /// Can store a function to run to check if the command can run.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Constructor to pass in an Action and a Function that returns a bool.
        /// </summary>
        /// <param name="execute">function that is being stored</param>
        /// <param name="canExecute">Can be set to the command to run at a specific time</param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Verify the function can run
        /// </summary>
        /// <returns>Returns true by default, otherwise return the bool of the Function</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        /// Tries to run the method stored in _execute
        /// </summary>
        public void Execute(object parameter)
        {
            _execute();
        }

        /// <summary>
        /// Used to recheck if CanExecute has changed
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
