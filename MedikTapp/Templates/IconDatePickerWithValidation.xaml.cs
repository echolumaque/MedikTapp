using System;
using Xamarin.Forms;

namespace MedikTapp.Templates
{
    public partial class IconDatePickerWithValidation : ContentView
    {
        public IconDatePickerWithValidation()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
            nameof(IsValid),
            typeof(bool),
            typeof(IconDatePickerWithValidation),
            default,
            BindingMode.OneWayToSource);
        public static readonly BindableProperty IsValidationMessageVisibleProperty = BindableProperty.Create(
            nameof(IsValidationMessageVisible),
            typeof(bool),
            typeof(IconDatePickerWithValidation),
            default);
        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
            nameof(SelectedDate),
            typeof(DateTime),
            typeof(IconDatePickerWithValidation),
            DateTime.Today,
            BindingMode.TwoWay,
            propertyChanged: SelectedDatePropertyChanged);

        public static readonly BindableProperty ValidationMessageProperty = BindableProperty.Create(
            nameof(ValidationMessage),
            typeof(string),
            typeof(IconDatePickerWithValidation),
            default);

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

        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        private static void SelectedDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not IconDatePickerWithValidation iconDatePickerWithValidation)
                return;

            var currentDate = DateTime.Now.Date;
            var suppliedDate = ((DateTime)newValue).Date;
            var difference = (currentDate - suppliedDate).Days;
            var legalAgeDays = 6570;

            iconDatePickerWithValidation.IsValidationMessageVisible = difference < legalAgeDays;
            iconDatePickerWithValidation.IsValid = difference > legalAgeDays;
        }

        public string ValidationMessage
        {
            get => (string)GetValue(ValidationMessageProperty);
            set => SetValue(ValidationMessageProperty, value);
        }
    }
}