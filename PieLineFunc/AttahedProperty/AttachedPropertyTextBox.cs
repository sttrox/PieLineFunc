using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PieLineFunc.AttahedProperty
{
    public class AttachedPropertyTextBox
    {
        public static readonly DependencyProperty SteepWheelProperty = DependencyProperty.RegisterAttached(
            "SteepWheel", typeof(double), typeof(AttachedPropertyTextBox),
            new PropertyMetadata(default(double), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.PreviewMouseWheel += TextBoxOnPreviewMouseWheel;
            }

            void TextBoxOnPreviewMouseWheel(object sender, MouseWheelEventArgs args)
            {
                string text = textBox.Text;
                if (double.TryParse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                    new CultureInfo("en-US"), out double value))
                {
                    value += GetSteepWheel(textBox) * (args.Delta > 0 ? 1 : -1);
                    textBox.Text = value.ToString(new CultureInfo("en-US"));
                }
            }
        }


        public static void SetSteepWheel(DependencyObject element, double value)
        {
            element.SetValue(SteepWheelProperty, value);
        }

        public static double GetSteepWheel(DependencyObject element)
        {
            return (double) element.GetValue(SteepWheelProperty);
        }
    }
}