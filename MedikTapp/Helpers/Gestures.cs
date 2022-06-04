using System.Windows.Input;
using Xamarin.Forms;

namespace MedikTapp.Helpers
{
    public class Gestures
    {
        public static readonly BindableProperty TappedProperty = BindableProperty.CreateAttached(
            "Tapped",
            typeof(ICommand),
            typeof(Gestures),
            default(ICommand),
            propertyChanged: OnTappedPropertyChanged);
        public static ICommand GetTapped(BindableObject view) => (ICommand)view.GetValue(TappedProperty);

        public static void SetTapped(BindableObject view, ICommand param) => view.SetValue(TappedProperty, param);

        private static void OnTappedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is View view && view != null)
            {
                view.GestureRecognizers.Clear();
                view.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Xamarin.Forms.Command(() =>
                    {
                        var command = GetTapped(view);
                        if (command != null && command.CanExecute(null))
                            command.Execute(GetTappedParameter(view));
                    })
                });
            }
        }

        public static readonly BindableProperty TappedParameterProperty = BindableProperty.CreateAttached(
            "TappedParameter",
            typeof(object),
            typeof(Gestures),
            default);
        public static object GetTappedParameter(BindableObject view) => view.GetValue(TappedParameterProperty);

        public static void SetTappedParameter(BindableObject view, object param) => view.SetValue(TappedParameterProperty, param);
    }
}