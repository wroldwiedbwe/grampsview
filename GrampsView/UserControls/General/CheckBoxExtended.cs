﻿namespace GrampsView.UserControls
{
    using System;
    using System.Windows.Input;

    using Xamarin.Forms;

    public class CheckBoxExtended : StackLayout
    {
        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(Boolean?), typeof(CheckBoxExtended), null,
                BindingMode.TwoWay, propertyChanged: CheckedValueChanged);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(CheckBoxExtended));

        public static readonly BindableProperty TextProperty =
                BindableProperty.Create("Text", typeof(String), typeof(CheckBoxExtended), null, BindingMode.TwoWay, propertyChanged: TextValueChanged);

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

        public CheckBoxExtended()
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

        public String Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && (Boolean)newValue)
            {
                ((CheckBoxExtended)bindable)._image.Source = checkedTrue;
            }
            else
            {
                ((CheckBoxExtended)bindable)._image.Source = checkedFalse;
            }

            ((CheckBoxExtended)bindable).CheckedChanged?.Invoke(bindable, EventArgs.Empty);

            ((CheckBoxExtended)bindable).Command?.Execute(null);
        }

        private static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null) ((CheckBoxExtended)bindable)._label.Text = newValue.ToString();
        }

        private void Tg_Tapped(object sender, EventArgs e)
        {
            Checked = !Checked;
        }
    }
}