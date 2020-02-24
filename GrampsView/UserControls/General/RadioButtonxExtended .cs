namespace GrampsView.UserControls
{
    using System;
    using System.Windows.Input;

    using Xamarin.Forms;

    public class RadioButtonxExtended : StackLayout
    {
        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(Boolean?), typeof(RadioButtonxExtended), false,
                BindingMode.TwoWay, propertyChanged: CheckedValueChanged);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(RadioButtonxExtended));

        public static readonly BindableProperty RadioButtonCheckedProperty = BindableProperty.Create(" RadioButtonChecked", typeof(Boolean?), typeof(RadioButtonxExtended), null,
                  BindingMode.TwoWay, propertyChanged: RadioButtonCheckedChanged);

        public static readonly BindableProperty TextProperty =
                BindableProperty.Create("Text", typeof(String), typeof(RadioButtonxExtended), null, BindingMode.TwoWay, propertyChanged: TextValueChanged);

        private static FontImageSource checkedFalse
                = new FontImageSource
                {
                    Glyph = Common.IconFont.CheckBoxOutline,
                };

        private static FontImageSource checkedTrue
                = new FontImageSource
                {
                    Glyph = Common.IconFont.CheckboxMarked,
                };

        private readonly Image _image;

        private readonly Label _label;

        public RadioButtonxExtended()
        {
            Orientation = StackOrientation.Horizontal;
            BackgroundColor = Color.Transparent;

            // Setup fonts
            if (Device.RuntimePlatform == Device.iOS)
            {
                checkedFalse.FontFamily = "Material Design Icons";
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                checkedFalse.FontFamily = "materialdesignicons-webfont.ttf#Material Design Icons";
            }
            else
            {
                checkedFalse.FontFamily = "Assets/Fonts/materialdesignicons-webfont.ttf#Material Design Icons";
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                checkedTrue.FontFamily = "Material Design Icons";
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                checkedTrue.FontFamily = "materialdesignicons-webfont.ttf#Material Design Icons";
            }
            else
            {
                checkedTrue.FontFamily = "Assets/Fonts/materialdesignicons-webfont.ttf#Material Design Icons";
            }

            _image = new Image()
            {
                Source = checkedTrue
            };

            var tg = new TapGestureRecognizer();

            tg.Tapped += Tg_Tapped;

            _image.GestureRecognizers.Add(tg);

            Children.Add(_image);

            _label = new Label()
            {
                VerticalOptions = LayoutOptions.Center
            };

            _label.GestureRecognizers.Add(tg);

            Children.Add(_label);
        }

        public event EventHandler CheckedChanged;

        public Boolean? Checked
        {
            get => (bool?)GetValue(CheckedProperty);
            set => SetValue(CheckedProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public Boolean RadioButtonChecked
        {
            get => (bool)GetValue(RadioButtonCheckedProperty);
            set => SetValue(RadioButtonCheckedProperty, value);
        }

        public String Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            RadioButtonxExtended daRadioButton = bindable as RadioButtonxExtended;

            if (daRadioButton is null)
            {
                return;
            }

            // If it's meant to be checked then leave it
            if (daRadioButton.RadioButtonChecked)
            {
                daRadioButton._image.Source = checkedTrue;
                return;
            }

            // If it's meant to be unchecked then leave it
            if (!daRadioButton.RadioButtonChecked && !(Boolean)newValue)
            {
                daRadioButton._image.Source = checkedFalse;
                return;
            }

            if (!daRadioButton.RadioButtonChecked && (Boolean)newValue)
            {
                daRadioButton._image.Source = checkedTrue;
                return;
            }

            daRadioButton.CheckedChanged?.Invoke(bindable, EventArgs.Empty);

            //daRadioButton.Command?.Execute(null);
        }

        private static void RadioButtonCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        private static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null) ((RadioButtonxExtended)bindable)._label.Text = newValue.ToString();
        }

        private void Tg_Tapped(object sender, EventArgs e)
        {
            Checked = !Checked;
        }
    }
}