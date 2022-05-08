using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace MedikTapp.Templates
{
    public partial class ComboBox
    {
        public static readonly BindableProperty DropdownBoxBackgroundColorProperty = BindableProperty.Create(
            nameof(DropdownBoxBackgroundColor),
            typeof(Color),
            typeof(ComboBox),
            defaultValue: default);
        public Color DropdownBoxBackgroundColor
        {
            get => (Color)GetValue(DropdownBoxBackgroundColorProperty);
            set => SetValue(DropdownBoxBackgroundColorProperty, value);
        }
        public static readonly BindableProperty DropdownBoxCornerRadiusProperty = BindableProperty.Create(
           nameof(DropdownBoxCornerRadius),
           typeof(CornerRadius),
           typeof(ComboBox),
           defaultValue: default);
        public CornerRadius DropdownBoxCornerRadius
        {
            get => (CornerRadius)GetValue(DropdownBoxCornerRadiusProperty);
            set => SetValue(DropdownBoxCornerRadiusProperty, value);
        }
        public static readonly BindableProperty DropdownBoxHeightProperty = BindableProperty.Create(
            nameof(DropdownBoxHeight),
            typeof(double),
            typeof(ComboBox));
        public double DropdownBoxHeight
        {
            get => (double)GetValue(DropdownBoxHeightProperty);
            set => SetValue(DropdownBoxHeightProperty, value);
        }
        public static readonly BindableProperty DropdownBoxMarginProperty = BindableProperty.Create(
           nameof(DropdownBoxMargin),
           typeof(Thickness),
           typeof(ComboBox),
           defaultValue: default);
        public Thickness DropdownBoxMargin
        {
            get => (Thickness)GetValue(DropdownBoxMarginProperty);
            set => SetValue(DropdownBoxMarginProperty, value);
        }
        public static readonly BindableProperty DropdownBoxWidthRequestProperty = BindableProperty.Create(
               nameof(DropdownBoxWidthRequest),
               typeof(double),
               typeof(ComboBox));
        public double DropdownBoxWidthRequest
        {
            get => (double)GetValue(DropdownBoxWidthRequestProperty);
            set => SetValue(DropdownBoxWidthRequestProperty, value);
        }
        public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(
            nameof(IsExpanded),
            typeof(bool),
            typeof(ComboBox));
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(ComboBox));
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(ComboBox));
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
        public static readonly BindableProperty MainBoxBackgroundColorProperty = BindableProperty.Create(
            nameof(MainBoxBackgroundColor),
            typeof(Color),
            typeof(ComboBox));
        public Color MainBoxBackgroundColor
        {
            get => (Color)GetValue(MainBoxBackgroundColorProperty);
            set => SetValue(MainBoxBackgroundColorProperty, value);
        }
        public static readonly BindableProperty MainBoxCornerRadiusProperty = BindableProperty.Create(
           nameof(MainBoxCornerRadius),
           typeof(CornerRadius),
           typeof(ComboBox),
           defaultValue: default);
        public CornerRadius MainBoxCornerRadius
        {
            get => (CornerRadius)GetValue(MainBoxCornerRadiusProperty);
            set => SetValue(MainBoxCornerRadiusProperty, value);
        }
        public static readonly BindableProperty MainBoxTextProperty = BindableProperty.Create(
            nameof(MainBoxText),
            typeof(string),
            typeof(ComboBox));
        public string MainBoxText
        {
            get => (string)GetValue(MainBoxTextProperty);
            set => SetValue(MainBoxTextProperty, value);
        }
        public static readonly BindableProperty MainBoxTextColorProperty = BindableProperty.Create(
            nameof(MainBoxTextColor),
            typeof(Color),
            typeof(ComboBox),
            defaultValue: default);
        public Color MainBoxTextColor
        {
            get => (Color)GetValue(MainBoxTextColorProperty);
            set => SetValue(MainBoxTextColorProperty, value);
        }
        public static readonly BindableProperty MainBoxTextHorizontalOptionsProperty = BindableProperty.Create(
           nameof(MainBoxTextHorizontalOptions),
           typeof(LayoutOptions),
           typeof(ComboBox),
           defaultValue: default);
        public LayoutOptions MainBoxTextHorizontalOptions
        {
            get => (LayoutOptions)GetValue(MainBoxTextHorizontalOptionsProperty);
            set => SetValue(MainBoxTextHorizontalOptionsProperty, value);
        }
        public static readonly BindableProperty MainBoxWidthRequestProperty = BindableProperty.Create(
           nameof(MainBoxWidthRequest),
           typeof(double),
           typeof(ComboBox),
           defaultValue: default);
        public double MainBoxWidthRequest
        {
            get => (double)GetValue(MainBoxWidthRequestProperty);
            set => SetValue(MainBoxWidthRequestProperty, value);
        }
        public static readonly BindableProperty OpenComboBoxCommandProperty = BindableProperty.Create(
            nameof(OpenComboBoxCommand),
            typeof(ICommand),
            typeof(ComboBox));
        public ICommand OpenComboBoxCommand
        {
            get => (ICommand)GetValue(OpenComboBoxCommandProperty);
            set => SetValue(OpenComboBoxCommandProperty, value);
        }
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(ComboBox));
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        public static readonly BindableProperty SelectionChangedCommandProperty = BindableProperty.Create(
            nameof(SelectionChangedCommand),
            typeof(ICommand),
            typeof(ComboBox));
        public ICommand SelectionChangedCommand
        {
            get => (ICommand)GetValue(SelectionChangedCommandProperty);
            set => SetValue(SelectionChangedCommandProperty, value);
        }
        public static readonly BindableProperty SelectionChangedCommandParameterProperty = BindableProperty.Create(
            nameof(SelectionChangedCommandParameter),
            typeof(object),
            typeof(ComboBox));
        public object SelectionChangedCommandParameter
        {
            get => GetValue(SelectionChangedCommandParameterProperty);
            set => SetValue(SelectionChangedCommandParameterProperty, value);
        }
    }
}