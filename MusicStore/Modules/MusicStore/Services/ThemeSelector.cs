using System.Web.Routing;
using Coevery.Themes;
using JetBrains.Annotations;

namespace MusicStore.Services {
    [UsedImplicitly]
    public class ThemeSelector : IThemeSelector {
        public ThemeSelectorResult GetTheme(RequestContext context) {
            return new ThemeSelectorResult { Priority = 1, ThemeName = "MusicStoreTheme" };
        }
    }
}