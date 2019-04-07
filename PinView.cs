using Plugin.CrossPlatformTintedImage.Abstractions;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkorXam.Pin
{
    public class PinView : ContentView
    {
        private StackLayout stackLayout;
        public PinView()
        {
            DrawView();
        }
        public void DrawView()
        {
            this.Content = CreatePinContent();
        }
        private int _defaultPadding = 10;
        private View CreatePinContent()
        {
            var stackParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = ButtonSize / 2
            };
            stackParent.Children.Add(CreatePinDots());
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = ButtonSize+_defaultPadding  },
                    new RowDefinition { Height = ButtonSize+_defaultPadding  },
                    new RowDefinition { Height = ButtonSize+_defaultPadding  },
                    new RowDefinition { Height = ButtonSize+_defaultPadding  }
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = ButtonSize+_defaultPadding  },
                    new ColumnDefinition { Width = ButtonSize+_defaultPadding  },
                    new ColumnDefinition { Width = ButtonSize+_defaultPadding  },
                }
            };
            grid.Children.Add(CreateButton(PinButtonChar.One), 0, 0);
            grid.Children.Add(CreateButton(PinButtonChar.Two), 1, 0);
            grid.Children.Add(CreateButton(PinButtonChar.Three), 2, 0);
            grid.Children.Add(CreateButton(PinButtonChar.Four), 0, 1);
            grid.Children.Add(CreateButton(PinButtonChar.Five), 1, 1);
            grid.Children.Add(CreateButton(PinButtonChar.Six), 2, 1);
            grid.Children.Add(CreateButton(PinButtonChar.Seven), 0, 2);
            grid.Children.Add(CreateButton(PinButtonChar.Eight), 1, 2);
            grid.Children.Add(CreateButton(PinButtonChar.Nine), 2, 2);
            grid.Children.Add(CreateButton(PinButtonChar.Clear), 0, 3);
            grid.Children.Add(CreateButton(PinButtonChar.Zero), 1, 3);
            grid.Children.Add(CreateButton(PinButtonChar.Delete), 2, 3);

            stackParent.Children.Add(grid);
            return stackParent;

        }
        private View CreatePinDots()
        {
            stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = DotSize / 4,
            };
            for (int i = 0; i < PinLength; i++)
                stackLayout.Children.Add(new Frame
                {
                    WidthRequest = DotSize,
                    HeightRequest = DotSize,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    CornerRadius = (int)DotSize,
                    BorderColor = DotColor,
                    BackgroundColor = Color.White,
                    Margin = 0,
                    Padding = 0,
                });
            Grid.SetColumnSpan(stackLayout, 4);
            return stackLayout;
        }
        private View CreateButton(PinButtonChar pinButtonChar)
        {
            switch (pinButtonChar)
            {
                case PinButtonChar.One: return CreateButtonWithEvent("1");
                case PinButtonChar.Two: return CreateButtonWithEvent("2");
                case PinButtonChar.Three: return CreateButtonWithEvent("3");
                case PinButtonChar.Four: return CreateButtonWithEvent("4");
                case PinButtonChar.Five: return CreateButtonWithEvent("5");
                case PinButtonChar.Six: return CreateButtonWithEvent("6");
                case PinButtonChar.Seven: return CreateButtonWithEvent("7");
                case PinButtonChar.Eight: return CreateButtonWithEvent("8");
                case PinButtonChar.Nine: return CreateButtonWithEvent("9");
                case PinButtonChar.Clear: return CreateButtonWithEvent("*");
                case PinButtonChar.Zero: return CreateButtonWithEvent("0");
                case PinButtonChar.Delete: return CreateButtonWithEvent("<");
                default: throw new Exception("Not found character");
            }
        }
        private View CreateButtonWithEvent(string character)
        {
            View button = null;
            if (character != "*" && character != "<")
            {
                button = new Button
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    WidthRequest = ButtonSize,
                    HeightRequest = ButtonSize,
                    CornerRadius = (int)ButtonSize,
                    BackgroundColor = ButtonColor,
                    Text = character,
                    TextColor = ButtonTextColor,
                    FontSize = ButtonSize / 2,
                };
                (button as Button).Clicked += (s, e) =>
                {
                    if (Pin.Length < PinLength)
                        Pin += character;
                };
            }
            else
            {
                button = new Frame
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    WidthRequest = ButtonSize,
                    HeightRequest = ButtonSize,
                    CornerRadius = (int)ButtonSize,
                    BackgroundColor = ButtonColor,
                    Padding = 0,
                    HasShadow = false,
                    Content = new TintedImage
                    {
                        Aspect = Aspect.AspectFill,
                        Margin = new Thickness(ButtonSize / 4),
                        TintColor = ButtonTextColor,
                        Source = character == "*" ? ImageSource.FromResource("SkorXam.Pin.reset.png") : ImageSource.FromResource("SkorXam.Pin.backspace.png"),
                    }
                };
                (button as Frame).GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() =>
                    {
                        if (character == "*")
                            Pin = string.Empty;
                        else if (character == "<")
                            Pin = (Pin.Length > 1) ? Pin.Substring(0, Pin.Length - 1) : string.Empty;
                    })
                });
            }
            return button;
        }
        public string Pin
        {
            get => (string)GetValue(PinProperty);
            set => SetValue(PinProperty, value);
        }
        public static readonly BindableProperty PinProperty =
            BindableProperty.Create(nameof(Pin), typeof(string), typeof(PinView), defaultBindingMode: BindingMode.TwoWay,
                defaultValue: string.Empty, propertyChanged: OnPinChanged);

        private static void OnPinChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var pinView = (bindable as PinView);
            pinView?.RenderPin((string)newValue);
            pinView?.PinChanged?.Invoke(pinView, new PinChangedEventArg(pinView, (string)newValue));
            pinView?.Submit();
        }
        public void Submit()
        {
            if (Pin.Length == PinLength)
            {
                if (this.PinSubmitCommand?.CanExecute(null) ?? false)
                {
                    this.PinSubmitCommand?.Execute(Pin);
                }
                this.PinSubmit?.Invoke(this, new PinSubmitEventArg(this, Pin));
            }
        }
        private void RenderPin(string newValue)
        {
            if (stackLayout == null)
                return;
            int newValueLength = newValue?.Length ?? 0;
            for (int i = 0; i < PinLength; i++)
                Fill(((stackLayout.Children[i]) as Frame), i < newValueLength);
        }

        private void Fill(Frame frame, bool isFill)
        {
            if (isFill)
                frame.BackgroundColor = DotColor;
            else
                frame.BackgroundColor = Color.White;
        }

        public int PinLength
        {
            get => (int)GetValue(PinLengthProperty);
            set => SetValue(PinLengthProperty, value);
        }
        public static readonly BindableProperty PinLengthProperty =
            BindableProperty.Create(nameof(PinLength), typeof(int), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 4);
        public Color DotColor
        {
            get => (Color)GetValue(DotColorProperty);
            set => SetValue(DotColorProperty, value);
        }
        public static readonly BindableProperty DotColorProperty =
            BindableProperty.Create(nameof(DotColor), typeof(Color), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: Color.Red);
        public Color ButtonColor
        {
            get => (Color)GetValue(ButtonColorProperty);
            set => SetValue(ButtonColorProperty, value);
        }
        public static readonly BindableProperty ButtonColorProperty =
            BindableProperty.Create(nameof(ButtonColor), typeof(Color), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: Color.Red, propertyChanged: (b, o, n) => (b as PinView).DrawView());
        public Color ButtonTextColor
        {
            get => (Color)GetValue(ButtonTextColorProperty);
            set => SetValue(ButtonTextColorProperty, value);
        }
        public static readonly BindableProperty ButtonTextColorProperty =
            BindableProperty.Create(nameof(ButtonTextColor), typeof(Color), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: Color.White, propertyChanged: (b, o, n) => (b as PinView).DrawView());
        public double ButtonSize
        {
            get => (double)GetValue(ButtonSizeProperty);
            set => SetValue(ButtonSizeProperty, value);
        }
        public static readonly BindableProperty ButtonSizeProperty =
            BindableProperty.Create(nameof(ButtonSize), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 64d, propertyChanged: (b, o, n) => (b as PinView).DrawView());
        public double DotSize
        {
            get => (double)GetValue(DotSizeProperty);
            set => SetValue(DotSizeProperty, value);
        }
        public static readonly BindableProperty DotSizeProperty =
            BindableProperty.Create(nameof(DotSize), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 24d, propertyChanged: (b, o, n) => (b as PinView).DrawView());
        public event EventHandler<PinChangedEventArg> PinChanged;
        public event EventHandler<PinSubmitEventArg> PinSubmit;
        public ICommand PinSubmitCommand
        {
            get => (ICommand)GetValue(PinSubmitCommandProperty);
            set => SetValue(PinSubmitCommandProperty, value);
        }
        public static readonly BindableProperty PinSubmitCommandProperty =
            BindableProperty.Create(nameof(PinSubmitCommand), typeof(ICommand), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: null);

    }

    public class PinChangedEventArg
    {
        public PinChangedEventArg(PinView sender, string pin)
        {
            Source = sender;
            Pin = pin;
        }
        public PinView Source { get; private set; }
        public string Pin { get; private set; }
    }

    public class PinSubmitEventArg
    {
        public PinSubmitEventArg(PinView sender, string pin)
        {
            Source = sender;
            Pin = pin;
        }
        public PinView Source { get; private set; }
        public string Pin { get; private set; }
    }
    public enum PinButtonChar
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Zero,
        Delete,
        Clear
    }
}
