using AirTickets.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AirTickets.Services
{
    /// <summary>
    /// For more information on understanding and extending activation frow see:
    /// <a href="https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/activation.md" >this article</a>
    /// </summary>
    internal class ActivationService
    {
        /// <summary>
        /// Reference for current app class
        /// </summary>
        private readonly App _app;

        /// <summary>
        /// Default navigation item - NavigationView
        /// </summary>
        private readonly Type _defaultNavItem;

        /// <summary>
        /// Main page
        /// </summary>
        private Lazy<UIElement> _shell;

        private object _lastActivationArgs;

        public ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell)
        {
            _app = app;
            _defaultNavItem = defaultNavItem;
            _shell = shell;
        }

        /// <summary>
        /// Activate application async
        /// </summary>
        /// <param name="activationArgs"></param>
        /// <returns></returns>
        public async Task ActivateAsync(object activationArgs)
        {
            if (IsInteractive(activationArgs))
            {
                // Initialize services that you need before app activation
                // take into account thet the splash screen is shown while this code runs.
                await InitializeAsync();

                // Don't repeat app initialization when the window already has content
                // just ensure that the window is active
                if (Window.Current.Content == null)
                {
                    // Create a Shell or Frame to act as the navigation context
                    Window.Current.Content = _shell?.Value ?? new Frame();
                }

                // Depending on activationArgs one of the ActivationHandlers or DefaultActivationHandler
                // will navigate to the first page
                await HandleActivationAsync(activationArgs);
                _lastActivationArgs = activationArgs;

                if (IsInteractive(activationArgs))
                {
                    // Ensure that current window is active
                    Window.Current.Activate();

                    // Tasks after activation
                    await StartupAsync();
                }
            }
        }

        /// <summary>
        /// Async task
        /// </summary>
        /// <returns></returns>
        private async Task StartupAsync()
        {
            await ThemeSelectorService.SetRequestedThemeAsync();
        }

        /// <summary>
        /// Handle activation 
        /// </summary>
        /// <param name="activationArgs">Activation args</param>
        /// <returns>Completed task</returns>
        private async Task HandleActivationAsync(object activationArgs)
        {
            var activationHandler = GetActivationHandlers().FirstOrDefault(handler => handler.CanHandle(activationArgs));

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync(activationArgs);
            }

            if (IsInteractive(activationArgs))
            {
                DefaultActivationHandler defaultHandler = new DefaultActivationHandler(_defaultNavItem);
                if (defaultHandler.CanHandle(activationArgs))
                {
                    await defaultHandler.HandleAsync(activationArgs);
                }
            }

        }

        /// <summary>
        /// Return collection of activation services
        /// </summary>
        /// <returns>Collection fo handlers</returns>
        private IEnumerable<ActivationHandler> GetActivationHandlers()
        {
            yield break;
        }

        /// <summary>
        /// Initialize all the necessary services
        /// </summary>
        /// <returns>Completed task</returns>
        private async Task InitializeAsync()
        {
            await ThemeSelectorService.InitializeAsync();
        }

        /// <summary>
        /// Check if args can be handled 
        /// </summary>
        /// <param name="activationArgs">Arguments for handle current activation</param>
        /// <returns>True, if is interactive</returns>
        private bool IsInteractive(object activationArgs) => activationArgs is IActivatedEventArgs;
    }
}