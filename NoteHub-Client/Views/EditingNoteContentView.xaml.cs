using NoteHub_Client.ViewModels;

namespace NoteHub_Client.Views;

public partial class EditingNoteContentView : ContentView
{
    public static readonly BindableProperty NoteTitleProperty =
            BindableProperty.Create(
                nameof(NoteTitle), 
                typeof(string), 
                typeof(EditingNoteContentView), 
                string.Empty, 
                BindingMode.TwoWay);
    public static readonly BindableProperty AnchorProperty =
    BindableProperty.Create(
        nameof(Anchor), 
        typeof(VisualElement), 
        typeof(EditingNoteContentView), 
        null);


    public static readonly BindableProperty NoteContentProperty =
        BindableProperty.Create(
            nameof(NoteContent), 
            typeof(string), 
            typeof(EditingNoteContentView), 
            string.Empty, 
            BindingMode.TwoWay);
    
    public static readonly BindableProperty IsUpdateModeProperty =
           BindableProperty.Create(
               nameof(IsUpdateMode),
               typeof(bool), 
               typeof(EditingNoteContentView),  
               false, 
               BindingMode.TwoWay);


    public static readonly BindableProperty SaveCommandProperty =
        BindableProperty.Create(
            nameof(SaveCommand), 
            typeof(System.Windows.Input.ICommand), 
            typeof(EditingNoteContentView), 
            null);
    
    public static readonly BindableProperty NewNoteCommandProperty =
       BindableProperty.Create(
           nameof(NewNoteCommand), 
           typeof(System.Windows.Input.ICommand), 
           typeof(EditingNoteContentView), 
           null);
    
    public string NoteTitle { 
        get => (string)GetValue(NoteTitleProperty); 
        set => SetValue(NoteTitleProperty, value); 
    }
    public string NoteContent { 
        get => (string)GetValue(NoteContentProperty); 
        set => SetValue(NoteContentProperty, value); 
    }
    public bool IsUpdateMode { 
        get => (bool)GetValue(IsUpdateModeProperty); 
        set => SetValue(IsUpdateModeProperty, value); 
    }
    public System.Windows.Input.ICommand SaveCommand { 
        get => (System.Windows.Input.ICommand)GetValue(SaveCommandProperty); 
        set => SetValue(SaveCommandProperty, value); 
    }
    public System.Windows.Input.ICommand NewNoteCommand { 
        get => (System.Windows.Input.ICommand)GetValue(NewNoteCommandProperty); 
        set => SetValue(NewNoteCommandProperty, value); 
    }
    public VisualElement Anchor
    {
        get => (VisualElement)GetValue(AnchorProperty);
        set => SetValue(AnchorProperty, value);
    }
    public EditingNoteContentView()
	{
		InitializeComponent();
	}
}