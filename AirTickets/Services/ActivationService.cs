using AirTickets.Activation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AirTickets.Services
{
    // For more information on understanding and extending activation flow see
    // https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/activation.md
    internal class ActivationService
    {
        private readonly App _app;
        private readonly Type _defaultNavItem;
        private Lazy<UIElement> _shell;

        private object _lastActivationArgs;

        public ActivationService(App app, Type defaultNavItem, Lazy<UIElement> shell = null)
        {
            _app = app;
            _shell = shell;
            _defaultNavItem = defaultNavItem;
        }

        /// <summary>
        /// Activate app function
        /// </summary>
        public async Task ActivateAsync(object activationArgs)
        {
            if (IsInteractive(activationArgs))
            {
                // Initialize services that you need before app activation
                // Take into accont that splash screen is shown while this code runs.
                await InitializeAsync();

                // Don't repeat app initialization when the window already has content
                // just ensure that the window is active          
                if (Window.Current.Content == null)
                {
                    // Create a Shell or Frame to act the navigation context
                    Window.Current.Content = _shell?.Value ?? new Frame();
                }
            }

            // DEpending on activationArgs one of ActivationHandlers of DefaultActivationHandler
            // will navigate to the first page
            await HandleActivationAsync(activationArgs);
        }

        private Task HandleActivationAsync(object activationArgs)
        {
            return Task.CompletedTask;
        }

        private IEnumerable<ActivationHandler> GetActivationHandlers()
        {
            yield break;
        }

        /// <summary>
        /// Activate all services async while application is starting
        /// </summary>
        /// <returns>Completed task</returns>
        private Task InitializeAsync()
        {
            // await Do some activation stuff
            return Task.CompletedTask;
        }

        /// <summary>
        /// Check, if object can be handeled as <see cref="IActivatedEventArgs"/> 
        /// </summary>
        /// <param name="args">object</param>
        /// <returns>True if obj can be converted to <see cref="IActivatedEventArgs"/></returns>
        private bool IsInteractive(object args) => args is IActivatedEventArgs;
    }
}