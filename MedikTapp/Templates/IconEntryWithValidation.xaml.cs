using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MedikTapp.Templates
{
    public partial class IconEntryWithValidation : ContentView
    {
        public IconEntryWithValidation()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public enum Validations
        {
            None,
            Email,
            Password,
            Name,
            ContactNumber
        }

        public static readonly BindableProperty EntryBackgroundColorProperty = BindableProperty.Create(
            nameof(EntryBackgroundColor),
            typeof(Color),
            typeof(IconEntryWithValidation),
            Color.FromHex("#F5F5F5"),
            BindingMode.TwoWay);

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(string),
            typeof(IconEntryWithValidation),
            default);

        public static readonly BindableProperty IsValidationMessageVisibleProperty = BindableProperty.Create(
            nameof(IsValidationMessageVisible),
            typeof(bool),
            typeof(IconEntryWithValidation),
            default);

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
            nameof(IsValid),
            typeof(bool),
            typeof(IconDatePickerWithValidation),
            default,
            BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(IconEntryWithValidation),
            default);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(IconEntryWithValidation),
            default,
            BindingMode.TwoWay,
            propertyChanged: TextPropertyChanged);

        public static readonly BindableProperty ValidationMessageProperty = BindableProperty.Create(
            nameof(ValidationMessage),
            typeof(string),
            typeof(IconEntryWithValidation),
            default);

        public static readonly BindableProperty ValidationProperty = BindableProperty.Create(
            nameof(Validation),
            typeof(Validations),
            typeof(IconEntryWithValidation),
            default);

        public static BindableProperty IsPasswordProperty => Entry.IsPasswordProperty;

        public static BindableProperty KeyboardProperty => InputView.KeyboardProperty;

        public static BindableProperty MaxLengthProperty => InputView.MaxLengthProperty;

        public Color EntryBackgroundColor
        {
            get => (Color)GetValue(EntryBackgroundColorProperty);
            set => SetValue(EntryBackgroundColorProperty, value);
        }
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        public bool IsValidationMessageVisible
        {
            get => (bool)GetValue(IsValidationMessageVisibleProperty);
            set => SetValue(IsValidationMessageVisibleProperty, value);
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not IconEntryWithValidation iconEntryWithValidation)
                return;

            var newText = newValue.ToString();
            if (string.IsNullOrWhiteSpace(newText))
            {
                iconEntryWithValidation.IsValid = false;
                return;
            }

            switch (iconEntryWithValidation.Validation)
            {
                case Validations.None:
                    iconEntryWithValidation.IsValid = true;
                    break;

                case Validations.Email:
                    var isEmail = Regex.IsMatch(newText, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                    iconEntryWithValidation.ValidationMessage = !isEmail
                        ? "Please enter a valid email address."
                        : "";
                    iconEntryWithValidation.IsValid = isEmail;
                    break;

                case Validations.Password:
                    iconEntryWithValidation.ValidationMessage = newText.Length <= 7
                        ? "Password should be more than 8 characters"
                        : "";
                    iconEntryWithValidation.IsValid = newText.Length >= 8;
                    break;

                case Validations.Name:
                    var isThereNumber = newText.Any(x => char.IsDigit(x));
                    iconEntryWithValidation.ValidationMessage = isThereNumber
                        ? "Please enter alphabetical letters only"
                        : "";
                    iconEntryWithValidation.IsValid = !isThereNumber;
                    break;

                case Validations.ContactNumber:
                    var isNumbersOnly = newText.All(x => char.IsDigit(x));
                    var contactNumberValidLength = newText.Length == 11;

                    iconEntryWithValidation.ValidationMessage = !isNumbersOnly
                        ? "Please enter digits only"
                        : !contactNumberValidLength
                        ? "Please enter a valid phone number"
                        : "";

                    iconEntryWithValidation.IsValid = isNumbersOnly && contactNumberValidLength;
                    break;

                default:
                    break;
            }

            iconEntryWithValidation.IsValidationMessageVisible = !string.IsNullOrWhiteSpace(iconEntryWithValidation.ValidationMessage);
        }

        public Validations Validation
        {
            get => (Validations)GetValue(ValidationProperty);
            set => SetValue(ValidationProperty, value);
        }

        public string ValidationMessage
        {
            get => (string)GetValue(ValidationMessageProperty);
            set => SetValue(ValidationMessageProperty, value);
        }
    }
}