using System.Windows.Ink;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Ink
{
    public partial class PenSettingsDialog : Window
    {
        public DrawingAttributes SelectedAttributes { get; private set; }
        private Color selectedColor;
        public PenSettingsDialog(DrawingAttributes currentAttributes)
        {
            Title = "Pen Settings";
            Width = 320;
            Height = 180;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            var stackPanel = new StackPanel { Margin = new Thickness(10) };
            stackPanel.Children.Add(new TextBlock { Text = "Pen Color:", Margin = new Thickness(0,0,0,5) });
            var button_Color = new Button { Content = "Choose Color", Margin = new Thickness(0,0,0,5), Height = 30 };
            button_Color.Click += Button_Color_Click;
            stackPanel.Children.Add(button_Color);
            stackPanel.Children.Add(new TextBlock { Text = "Pen Thickness:", Margin = new Thickness(0,10,0,5) });
            var slider_Thickness = new Slider { Minimum = 1, Maximum = 20, Value = currentAttributes.Width, Height = 30 };
            stackPanel.Children.Add(slider_Thickness);
            var panelButtons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0,10,0,0) };
            var buttonOK = new Button { Content = "OK", Width = 60, Margin = new Thickness(0,0,10,0) };
            var buttonCancel = new Button { Content = "Cancel", Width = 60 };
            buttonOK.Click += (s, e) => {
                SelectedAttributes = new DrawingAttributes
                {
                    Color = selectedColor == default ? currentAttributes.Color : selectedColor,
                    Width = slider_Thickness.Value,
                    Height = slider_Thickness.Value
                };
                DialogResult = true;
                Close();
            };
            buttonCancel.Click += (s, e) => { DialogResult = false; Close(); };
            panelButtons.Children.Add(buttonOK);
            panelButtons.Children.Add(buttonCancel);
            stackPanel.Children.Add(panelButtons);
            Content = stackPanel;
            selectedColor = currentAttributes.Color;
        }
        private void Button_Color_Click(object sender, RoutedEventArgs e)
        {
            var colorDialog = new ColorPickerDialog.ColorDialog(true);
            colorDialog.R = selectedColor.R;
            colorDialog.G = selectedColor.G;
            colorDialog.B = selectedColor.B;
            if (colorDialog.ShowDialog() == true)
            {
                selectedColor = Color.FromRgb((byte)colorDialog.R, (byte)colorDialog.G, (byte)colorDialog.B);
            }
        }
    }
}