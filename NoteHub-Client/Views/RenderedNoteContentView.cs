namespace NoteHub_Client.Views;

using System.Diagnostics;
using Token = string;

public class RenderedNoteContentView : Grid
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(RenderedNoteContentView),
        default(string),
        propertyChanged: OnTitleChanged); // Appelé quand le titre change

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    // 1. Définition de la BindableProperty pour le XAML
    public static BindableProperty TokensProperty = BindableProperty.Create(
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

    public static readonly BindableProperty ContentProperty = BindableProperty.Create(
        nameof(Content),
        typeof(string),
        typeof(RenderedNoteContentView),
        default(string),
        propertyChanged: OnContentChanged);

    public string Content
    {
        get => (string)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    private readonly Label titleLabel = new Label();
    private readonly Label noteContentLabel = new Label();


    public RenderedNoteContentView()
    {
        
        RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Title
        RowDefinitions.Add(new RowDefinition { Height = GridLength.Star }); // Content

        titleLabel.Text = "No Title";
        noteContentLabel.Text = "Chargement de la Note...";
        noteContentLabel.LineBreakMode = LineBreakMode.NoWrap;
        noteContentLabel.HorizontalOptions = LayoutOptions.Fill;
        noteContentLabel.VerticalOptions = LayoutOptions.Fill;

        RowSpacing = 15;
        this.Add(titleLabel, row: 0);
        this.Add(noteContentLabel, row: 1);
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (RenderedNoteContentView)bindable;
        Debug.WriteLine($"Title changed: {newValue}"); // Debug log
        view.Title = (string)newValue; // Update the property
        view.titleLabel.Text = view.Title; // Update the title label
    }

    private static void OnTokensChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (RenderedNoteContentView)bindable;
        Debug.WriteLine($"Tokens changed: {newValue}");
        view.Tokens = (IList<Token>)newValue;
        view.RenderNoteContent();
    }

    private static void OnContentChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (RenderedNoteContentView)bindable;
        Debug.WriteLine($"Content changed: {newValue}");
        view.Content = (string)newValue;
        view.RenderNoteContent();
    }

    private void RenderPlaceholder()
    {
        titleLabel.Text = "No Title";
        noteContentLabel.Text = "Aucun contenu...";
    }

    private void RenderNoteContent()
    {
        if (!string.IsNullOrEmpty(Content))
        {
            noteContentLabel.Text = Content;
            return;
        }

        if (Tokens == null || !Tokens.Any())
        {
            Debug.WriteLine("Tokens is null or empty");
            RenderPlaceholder();
            return;
        }

        Debug.WriteLine($"Rendering tokens: {string.Join(", ", Tokens)}");

        var formattedString = new FormattedString();
        foreach (var token in Tokens)
        {
            formattedString.Spans.Add(new Span { Text = token + " ", FontSize = 16 });
        }
        noteContentLabel.FormattedText = formattedString;
    }
}
