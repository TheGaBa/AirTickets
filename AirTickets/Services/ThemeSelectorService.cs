using AirTickets.Helpers;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace AirTickets.Services
{
    /// <summary>
    /// Class for managing app themes
    /// </summary>
    public static class ThemeSelectorService
    {
        /// <summary>
        /// Theme key in <see cref="ApplicationDataContainer"/>
        /// </summary>
        private const string SettingsKey = "AppBackgroundRequestedTheme";

        /// <summary>
        /// Current theme
        /// </summary>
        public static ElementTheme Theme { get; set; } = ElementTheme.Default;

        /// <summary>
        /// Initialize current app theme
        /// </summary>
        /// <returns>Completed task</returns>
        public static async Task InitializeAsync()
        {
            Theme = await LoadThemeFromSettingsAsync();
        }

        /// <summary>
        /// Gets theme from app <see cref="ApplicationDataContainer"/> by key <see cref="SettingsKey"/>
        /// </summary>
        /// <returns>Current theme</returns>
        private static async Task<ElementTheme> LoadThemeFromSettingsAsync()
        {
            ElementTheme cacheTheme = ElementTheme.Default;
            string themeName = await ApplicationData.Current.LocalSettings.ReadAsync<string>(SettingsKey);

            if (!string.IsNullOrEmpty(themeName))
            {
                Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }

        /// <summary>
        /// Set selected theme for all the application
        /// </summary>
        /// <param name="theme">Selected theme</param>
        /// <returns>Completed task</returns>
        public static async Task SetThemeAsync(ElementTheme theme)
        {
            Theme = theme;

            await SetRequestedThemeAsync();
            await SaveThemeInSettingsAsync(theme);
        }

        /// <summary>
        /// Save theme to <see cref="ApplicationDataContainer"/> with key: <see cref="SettingsKey"/>
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        private static async Task SaveThemeInSettingsAsync(ElementTheme theme) => await ApplicationData.Current.LocalSettings.SaveAsync(SettingsKey, theme.ToString());

        /// <summary>
        /// Set default theme for all application 
        /// </summary>
        /// <returns>Completed task</returns>
        public static async Task SetRequestedThemeAsync()
        {
            foreach (var view in CoreApplication.Views)
            {
                await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (Window.Current.Content is FrameworkElement frameworkElement)
                    {
                        frameworkElement.RequestedTheme = Theme;
                    }
                });
            }
        }
    }
}