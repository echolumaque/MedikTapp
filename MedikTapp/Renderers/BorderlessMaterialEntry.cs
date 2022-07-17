using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MedikTapp.Renderers
{
    public class BorderlessMaterialEntry : Entry
    {
        public static readonly BindableProperty IsEmailProperty = BindableProperty.Create(
            nameof(IsEmail),
            typeof(bool),
            typeof(BorderlessMaterialEntry),
            defaultValue: default);
        public bool IsEmail
        {
            get => (bool)GetValue(IsEmailProperty);
            set => SetValue(IsEmailProperty, value);
        }
        public static readonly BindableProperty IsLettersOnlyProperty = BindableProperty.Create(
            nameof(IsLettersOnly),
            typeof(bool),
            typeof(BorderlessMaterialEntry),
            defaultValue: default);
        public bool IsLettersOnly
        {
            get => (bool)GetValue(IsLettersOnlyProperty);
            set => SetValue(IsLettersOnlyProperty, value);
        }
        protected override void OnTextChanged(string oldValue, string newValue)
        {
            base.OnTextChanged(oldValue, newValue);

            if (!IsEmail)
                return;
            else
            {
                var emailRegex = Regex.IsMatch(newValue,
                    @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                    RegexOptions.IgnoreCase);

                //TextColor = emailRegex ? Color.FromHex("#2F2F32") : Color.Red;
            }

            if (!IsLettersOnly)
                return;
            else
            {
                var isOnlyLetters = newValue.All(x => char.IsLetter(x));

                TextColor = isOnlyLetters ? Color.FromHex("#2F2F32") : Color.Red;
            }
        }
    }
}