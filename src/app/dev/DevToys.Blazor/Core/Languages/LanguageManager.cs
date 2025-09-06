using System.Globalization;

namespace DevToys.Blazor.Core.Languages;

public sealed class LanguageManager
{
    private static LanguageManager? languageManager;

    /// <summary>
    /// Gets an instance of <see cref="LanguageManager"/>.
    /// </summary>
    public static LanguageManager Instance => languageManager ??= new LanguageManager();

    /// <summary>
    /// Gets if the text must be written from left to right or from right to left.
    /// </summary>
    public FlowDirection FlowDirection { get; private set; }

    /// <summary>
    /// Gets the list of available languages in the app.
    /// </summary>
    public List<LanguageDefinition> AvailableLanguages { get; }

    public LanguageManager()
    {
        AvailableLanguages = new List<LanguageDefinition>
        {
            new LanguageDefinition() // default language
        };

        // TODO: Replace with actual supported cultures list. This is a minimal placeholder for clean build/fork.
        IReadOnlyList<string> supportedLanguageIdentifiers = new List<string> { "en-US" };
        foreach (var identifier in supportedLanguageIdentifiers)
        {
            AvailableLanguages.Add(new LanguageDefinition(identifier));
        }
    }

    /// <summary>
    /// Change the current culture of the application
    /// </summary>
    public void SetCurrentCulture(LanguageDefinition language)
    {
        CultureInfo.DefaultThreadCurrentCulture = language.Culture;
        CultureInfo.DefaultThreadCurrentUICulture = language.Culture;
        CultureInfo.CurrentCulture = language.Culture;
        CultureInfo.CurrentUICulture = language.Culture;

        if (language.Culture.TextInfo.IsRightToLeft)
        {
            FlowDirection = FlowDirection.RightToLeft;
        }
        else
        {
            FlowDirection = FlowDirection.LeftToRight;
        }
    }
}
