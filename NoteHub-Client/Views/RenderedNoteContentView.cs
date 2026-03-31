namespace NoteHub_Client.Views;

using Token = string;

public class RenderedNoteContentView : ContentView
{
    // 1. Définition de la BindableProperty pour le XAML
    public static readonly BindableProperty TokensProperty = BindableProperty.Create(
        nameof(Tokens),
        typeof(IList<Token>),
        typeof(RenderedNoteContentView),
        default(IList<Token>),
        propertyChanged: OnTokensChanged); // Appelé quand la liste change

    public IList<Token> Tokens
    {
        get => (IList<Token>)GetValue(TokensProperty);
        set => SetValue(TokensProperty, value);
    }

    private readonly VerticalStackLayout _mainLayout;

    public RenderedNoteContentView()
    {
        _mainLayout = new VerticalStackLayout { Spacing = 10 };
        Content = _mainLayout;

        // Message par défaut
        RenderPlaceholder();
    }

    private static void OnTokensChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (RenderedNoteContentView)bindable;
        view.RenderNoteContent();
    }

    private void RenderPlaceholder()
    {
        _mainLayout.Children.Clear();
        _mainLayout.Children.Add(new Label { Text = "Aucun contenu...", HorizontalOptions = LayoutOptions.Center });
    }

    private void RenderNoteContent()
    {
        _mainLayout.Children.Clear();

        if (Tokens == null || !Tokens.Any())
        {
            RenderPlaceholder();
            return;
        }

        var formattedString = new FormattedString();
        foreach (var token in Tokens)
        {
            formattedString.Spans.Add(new Span { Text = token + " ", FontSize = 16 });
        }

        _mainLayout.Children.Add(new Label { FormattedText = formattedString });
    }
}
