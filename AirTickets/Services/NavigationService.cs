using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace AirTickets.Services
{
    /// <summary>
    /// Class to manage root frame
    /// </summary>
    public static class NavigationService
    {
        /// <summary>
        /// Navigated event
        /// </summary>
        public static event NavigatedEventHandler Navigated;

        /// <summary>
        /// NavigationFailed event
        /// </summary>
        public static event NavigationFailedEventHandler NavigationFailed;

        /// <summary>
        /// Root frame
        /// </summary>
        private static Frame _frame;

        /// <summary>
        /// Saves last parameter for method Navigate
        /// </summary>
        private static object _lastParamUsed;

        /// <summary>
        /// Root frame property
        /// </summary>
        public static Frame Frame
        {
            get
            {
                if (_frame == null)
                {
                    _frame = Window.Current.Content as Frame;
                    RegisterFrameEvents();
                }

                return _frame;
            }
            set
            {
                UnregisterFrameEvents();
                _frame = value;
                RegisterFrameEvents();
            }
        }

        /// <summary>
        /// Returns true, if frame can navigate back
        /// </summary>
        public static bool CanGoBack => Frame.CanGoBack;

        /// <summary>
        /// Returns true, if frame can navigate forward
        /// </summary>
        public static bool CanGoForward => Frame.CanGoForward;

        /// <summary>
        /// Trying to navigate frame back
        /// </summary>
        /// <returns>True if successfully</returns>
        public static bool GoBack()
        {
            if (CanGoBack)
            {
                Frame.GoBack();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Trying to navigate frame forward
        /// </summary>
        public static void GoForward() => Frame.GoForward();

        /// <summary>
        /// Add events to the frame
        /// </summary>
        private static void RegisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated += Frame_Navigated;
                _frame.NavigationFailed += Frame_NavigationFailed;
            }
        }

        /// <summary>
        /// Remove events from the frame
        /// </summary>
        private static void UnregisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated -= Frame_Navigated;
                _frame.NavigationFailed -= Frame_NavigationFailed;
            }
        }

        /// <summary>
        /// Navigation failed event function
        /// </summary>
        private static void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e) => NavigationFailed?.Invoke(sender, e);

        /// <summary>
        /// Navigated event function
        /// </summary>
        private static void Frame_Navigated(object sender, NavigationEventArgs e) => Navigated?.Invoke(sender, e);

        /// <summary>
        /// Method, which will navigate frame to specific page
        /// </summary>
        /// <param name="pageType">Type of page, which you want to navigate to</param>
        /// <param name="parameter">Additional parament</param>
        /// <param name="infoOverride">Navigation transition effect</param>
        /// <returns></returns>
        public static bool Navigate(Type pageType, object parameter = null, NavigationTransitionInfo infoOverride = null)
        {
            if (pageType == null || !pageType.IsSubclassOf(typeof(Page)))
            {
                throw new ArgumentException($"invalid pageType '{pageType}', please provide a valid pagetype.", nameof(pageType));
            }

            // Don't open the same page multiply times
            if (Frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParamUsed)))
            {
                bool navigationResult = Frame.Navigate(pageType, parameter, infoOverride);
                if (navigationResult)
                {
                    _lastParamUsed = parameter;
                }

                return navigationResult;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method, which will navigate frame to specific page
        /// </summary>
        /// <typeparam name="T">Type of page, which you want to navigate to</typeparam>
        /// <param name="parameter">Additional parameter</param>
        /// <param name="infoOverride">Navigation transition effect</param>
        /// <returns></returns>
        public static bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null) where T : Page => Navigate(typeof(T), parameter, infoOverride);
    }
}