﻿using AirTickets.Views;
using AnimatedVisuals;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AirTickets
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            CustomizeTitleBar();
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(ShellPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();

#if !DEBUG
                await RunAnimatedSplashScreenAsync();
#endif

            }

            //using (DatabaseContext context = new DatabaseContext())
            //{
            //    context.Database.Migrate();
            //}
        }

        /// <summary>
        /// Customize titlebar
        /// </summary>
        private void CustomizeTitleBar()
        {
            // Customize button colors
            var appView = ApplicationView.GetForCurrentView();
            appView.TitleBar.ButtonBackgroundColor = Color.FromArgb(100, 61, 65, 136);
            appView.TitleBar.ButtonForegroundColor = Color.FromArgb(255, 255, 255, 255);
            appView.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(100, 30, 31, 47);

            // Hide default title bar.
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        /// <summary>
        /// Runs the animated splash screen as content for the current window 
        /// </summary>
        /// <returns>Task completes when the animation finishes</returns>
        private async Task RunAnimatedSplashScreenAsync()
        {
            // Insert splashBorder above the current window content.
            var originalWindowContent = Window.Current.Content;

            var splashBorder = new Border();
            splashBorder.Background = (SolidColorBrush)Current.Resources["LottieBasicBrush"];

            // Use modified LottieLogo1 animation based on user's accent color.
            var lottieSource = new Airplane();

            // Instantiate Player with modified Source
            var player = new AnimatedVisualPlayer
            {
                Stretch = Stretch.Uniform,
                AutoPlay = false,
                Source = lottieSource,
            };

            splashBorder.Child = player;
            Window.Current.Content = splashBorder;

            // Start playing the splashscreen animation.
            await player.PlayAsync(fromProgress: 0, toProgress: 1, looped: false);

            // Reset window content after the splashscreen animation has completed.
            Window.Current.Content = originalWindowContent;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}