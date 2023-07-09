using MudBlazor;

namespace Client.Infrastructure.Settings
{
    public class CustomMudTheme
    {

        private static string[] fontFamily = new[] { "Roboto", "Helvetica", "sans-serif" };

        private static Typography typography = new Typography()
        {
            Default = new() { FontFamily = fontFamily },
        };

        public static MudTheme DefaultTheme = new MudTheme()
        {
            Palette = new Palette
            {
                AppbarBackground = Colors.BlueGrey.Darken1
            },
            Typography = typography
        };

        public static MudTheme EmptyLayoutTheme = new MudTheme()
        {
            Palette = new Palette
            {
                Background = Colors.Grey.Lighten2,
            },
            Typography = typography
        };

    }
}
