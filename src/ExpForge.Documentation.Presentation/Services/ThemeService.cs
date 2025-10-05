using MudBlazor;
using Microsoft.AspNetCore.Components;

namespace ExpForge.Documentation.Presentation.Services
{
    public class ThemeService
    {
        private readonly NavigationManager _navigation;

        public ThemeService(NavigationManager navigation)
        {
            _navigation = navigation;
        }

        public MudTheme GetTheme()
        {
            if (IsHome())
                return HomeTheme();
            else
                return DefaultTheme();
        }

        private bool IsHome()
        {
            var uri = _navigation.Uri;
            return uri.EndsWith("/") || uri.EndsWith("/index");
        }

        private MudTheme HomeTheme()
        {
            return new MudTheme()
            {
                PaletteDark = new PaletteDark()
                {
                    Primary = Colors.Shades.White,
                    Secondary = "#45A29E",
                    Background = "#0B0C10",
                    Surface = "#1F2833",
                    TextPrimary = "#E4E4E4",
                    TextSecondary = "#C5C6C7",
                    DrawerBackground = "#1F2833",
                    AppbarBackground = "#1F2833",
                    AppbarText = "#66FCF1"
                },
                PaletteLight = new PaletteLight()
                {
                    Primary = Colors.Shades.White,
                    Secondary = "#45A29E"
                },
                Typography = new Typography()
                {
                    Default = new DefaultTypography() { FontFamily = new[] { "Cinzel", "serif" } },
                    H6 = new H6Typography() { FontSize = "1.4rem", FontWeight = "600" }
                }
            };
        }

        private MudTheme DefaultTheme()
        {
            return new MudTheme()
            {
                PaletteDark = new PaletteDark()
                {
                    Primary = Colors.Shades.Black,
                    Secondary = "#FF5722",
                    Background = "#121212",
                    Surface = "#1E1E1E",
                    TextPrimary = Colors.Shades.Black,
                    TextSecondary = Colors.Gray.Default,
                    DrawerBackground = "#1E1E1E",
                    AppbarBackground = "#1E1E1E",
                    AppbarText = "#FFC107"
                },
                PaletteLight = new PaletteLight()
                {
                    Primary = Colors.Shades.Black,
                    Secondary = "#FF5722"
                },
                Typography = new Typography()
                {
                    Default = new DefaultTypography() { FontFamily = new[] { "Arial", "sans-serif" } },
                    H6 = new H6Typography() { FontSize = "1.2rem", FontWeight = "500" }
                }
            };
        }
    }
}
