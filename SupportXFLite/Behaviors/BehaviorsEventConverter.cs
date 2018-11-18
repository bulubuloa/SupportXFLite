using System;
using System.Globalization;
using Xamarin.Forms;

namespace SupportXFLite.Behaviors
{
    public class BehaviorsEventConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TextChangedEventArgs)
            {
                var textChanged = value as TextChangedEventArgs;
                return textChanged;
            }
            else if (value is ValueChangedEventArgs)
            {
                var valueChanged = value as ValueChangedEventArgs;
                return valueChanged;
            }
            else if (value is ItemVisibilityEventArgs)
            {
                var eventArgs = value as ItemVisibilityEventArgs;
                return eventArgs.Item;
            }
            else
            {
                return value;
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}