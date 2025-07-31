namespace Mobile.Controls;

public partial class CustomInput : ContentView
{
	public CustomInput()
	{
		InitializeComponent();
	}

    // Label
    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(CustomInput), string.Empty);
    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }

    // Icon (uses .png or FontImageSource)
    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(nameof(Icon), typeof(string), typeof(CustomInput), string.Empty);
    public string Icon { get => (string)GetValue(IconProperty); set => SetValue(IconProperty, value); }
    public bool HasIcon => !string.IsNullOrWhiteSpace(Icon);

    // Placeholder
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomInput), string.Empty);
    public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }

    // IsPassword
    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomInput), false);
    public bool IsPassword { get => (bool)GetValue(IsPasswordProperty); set => SetValue(IsPasswordProperty, value); }

    // Text
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomInput), string.Empty,
            BindingMode.TwoWay, propertyChanged: OnTextChanged);
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

    // Error
    public static readonly BindableProperty ErrorProperty =
        BindableProperty.Create(nameof(Error), typeof(string), typeof(CustomInput), string.Empty);
    public string Error { get => (string)GetValue(ErrorProperty); set => SetValue(ErrorProperty, value); }
    public bool HasError => !string.IsNullOrWhiteSpace(Error);

    // Colors
    public static readonly BindableProperty LabelColorProperty =
        BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(CustomInput), Colors.Gray);
    public Color LabelColor { get => (Color)GetValue(LabelColorProperty); set => SetValue(LabelColorProperty, value); }

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomInput), Colors.Black);
    public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }

    public static readonly BindableProperty PlaceholderColorProperty =
        BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(CustomInput), Colors.Gray);
    public Color PlaceholderColor { get => (Color)GetValue(PlaceholderColorProperty); set => SetValue(PlaceholderColorProperty, value); }

    public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomInput), Colors.Gray);
    public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }

    // EVENTS: TextChanged, Completed, Unfocused

    // Custom event for text changed
    public event EventHandler<TextChangedEventArgs> CustomTextChanged;
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        CustomTextChanged?.Invoke(this, e);
        // Update Text property, triggers propertyChanged
        Text = e.NewTextValue;
    }

    // Custom event for completed (enter pressed)
    public event EventHandler CustomCompleted;
    private void Entry_Completed(object sender, EventArgs e)
    {
        CustomCompleted?.Invoke(this, e);
    }

    // Custom event for onBlur/unfocus
    public event EventHandler CustomUnfocused;
    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        CustomUnfocused?.Invoke(this, e);
    }

    // Optional: track changes for binding
    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        // Custom logic if needed
    }

}