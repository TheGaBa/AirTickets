using AirTickets.Services;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace AirTickets.Activation
{
    /// <summary>
    /// Default activation class, which anyway will navigate you to some frame
    /// </summary>
    internal class DefaultActivationHandler : ActivationHandler<IActivatedEventArgs>
    {
        /// <summary>
        /// Navgation element
        /// </summary>
        private readonly Type _navElement;

        public DefaultActivationHandler(Type navElement)
        {
            _navElement = navElement;
        }

        /// <summary>
        /// When the navigation stacks isn't restored, navigate to the first page and configure 
        /// <para>the enw page by passing required information in the navigation parametr</para>
        /// </summary>
        protected override async Task HandleInternalAsync(IActivatedEventArgs args)
        {
            object arguments = null;
            if (args is LaunchActivatedEventArgs launchArgs)
            {
                arguments = launchArgs.Arguments;
            }
            NavigationService.Navigate(_navElement, arguments);
            await Task.CompletedTask;
        }

        /// <summary>
        /// If none of ActivationHandlers has handled tha app activation
        /// </summary>
        protected override bool CanHandleInternal(IActivatedEventArgs args) => NavigationService.Frame.Content == null && _navElement != null;
    }
}