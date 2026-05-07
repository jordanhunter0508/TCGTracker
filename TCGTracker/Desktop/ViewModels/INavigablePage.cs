using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DataDomain;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Used to help Navigate pages and pass around the
    /// AccessToken of the logged in user
    /// </summary>
    public interface INavigablePage
    {
        event Action<Page> NavigateRequested;
        event Action<UserVM> UserLoggedIn;
    }
}
