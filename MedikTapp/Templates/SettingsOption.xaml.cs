using System.Windows.Input;
using Xamarin.Forms;

namespace MedikTapp.Templates
{
    public partial class SettingsOption : ContentView
    {
        public SettingsOption()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(SettingsOption),
            default);
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly BindableProperty MainTextProperty = BindableProperty.Create(
            nameof(MainText),
            typeof(string),
            typeof(SettingsOption),
            default);
        public string MainText
        {
            get => (string)GetValue(MainTextProperty);
            set => SetValue(MainTextProperty, value);
        }

        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
            nameof(TapCommand),
            typeof(ICommand),
            typeof(SettingsOption),
            default);
        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }


        public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(
            nameof(IsExpanded),
            typeof(bool),
            typeof(SettingsOption),
            default);
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public static readonly BindableProperty ContentTemplateProperty = BindableProperty.Create(
            nameof(ContentTemplate),
            typeof(DataTemplate),
            typeof(SettingsOption),
            default);
        public DataTemplate ContentTemplate
        {
            get => (DataTemplate)GetValue(ContentTemplateProperty);
            set => SetValue(ContentTemplateProperty, value);
        }
    }
}