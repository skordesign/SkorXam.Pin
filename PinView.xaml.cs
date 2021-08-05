using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkorXam.Pin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinView : ContentView
    {
        public PinView()
        {
            InitializeComponent();
        }
        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }
        public static readonly BindableProperty SpacingProperty =
            BindableProperty.Create(nameof(Spacing), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 16d);
        public double DotSpacing
        {
            get => (double)GetValue(DotSpacingProperty);
            set => SetValue(DotSpacingProperty, value);
        }
        public static readonly BindableProperty DotSpacingProperty =
            BindableProperty.Create(nameof(DotSpacing), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 4d);
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
                defaultValue: Color.Red, propertyChanged: OnPinAppearanceChanged);
        public double DotSize
        {
            get => (double)GetValue(DotSizeProperty);
            set => SetValue(DotSizeProperty, value);
        }
        public static readonly BindableProperty DotSizeProperty =
            BindableProperty.Create(nameof(DotSize), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 24d);
        public Color DotEmptyColor
        {
            get => (Color)GetValue(DotEmptyColorProperty);
            set => SetValue(DotEmptyColorProperty, value);
        }
        public static readonly BindableProperty DotEmptyColorProperty =
            BindableProperty.Create(nameof(DotEmptyColor), typeof(Color), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: Color.White, propertyChanged: OnPinAppearanceChanged);
        public Color DotBorderColor
        {
            get => (Color)GetValue(DotBorderColorProperty);
            set => SetValue(DotBorderColorProperty, value);
        }
        public static readonly BindableProperty DotBorderColorProperty =
            BindableProperty.Create(nameof(DotBorderColor), typeof(Color), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: Color.Red, propertyChanged: OnPinAppearanceChanged);
        public ICommand PinSubmitCommand
        {
            get => (ICommand)GetValue(PinSubmitCommandProperty);
            set => SetValue(PinSubmitCommandProperty, value);
        }
        public static readonly BindableProperty PinSubmitCommandProperty =
            BindableProperty.Create(nameof(PinSubmitCommand), typeof(ICommand), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: null);

        public event EventHandler<PinChangedEventArg> PinChanged;
        public event EventHandler<PinSubmitEventArg> PinSubmit;
        public string Pin
        {
            get => (string)GetValue(PinProperty);
            set => SetValue(PinProperty, value);
        }
        public static readonly BindableProperty PinProperty =
            BindableProperty.Create(nameof(Pin), typeof(string), typeof(PinView), defaultBindingMode: BindingMode.TwoWay,
                defaultValue: string.Empty, propertyChanged: OnPinChanged);
        public double ButtonSize
        {
            get => (double)GetValue(ButtonSizeProperty);
            set => SetValue(ButtonSizeProperty, value);
        }
        public static readonly BindableProperty ButtonSizeProperty =
            BindableProperty.Create(nameof(ButtonSize), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 64d);
        public double ButtonVerticalSpacing
        {
            get => (double)GetValue(ButtonVerticalSpacingProperty);
            set => SetValue(ButtonVerticalSpacingProperty, value);
        }
        public static readonly BindableProperty ButtonVerticalSpacingProperty =
            BindableProperty.Create(nameof(ButtonVerticalSpacing), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 6d);
        public double ButtonHorizontalSpacing
        {
            get => (double)GetValue(ButtonHorizontalSpacingProperty);
            set => SetValue(ButtonHorizontalSpacingProperty, value);
        }
        public static readonly BindableProperty ButtonHorizontalSpacingProperty =
            BindableProperty.Create(nameof(ButtonHorizontalSpacing), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 6d);
        public double ButtonFontsize
        {
            get => (double)GetValue(ButtonFontsizeProperty);
            set => SetValue(ButtonFontsizeProperty, value);
        }
        public static readonly BindableProperty ButtonFontsizeProperty =
            BindableProperty.Create(nameof(ButtonFontsize), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 32d);
        public int ButtonCornerRadius
        {
            get => (int)GetValue(ButtonCornerRadiusProperty);
            set => SetValue(ButtonCornerRadiusProperty, value);
        }
        public static readonly BindableProperty ButtonCornerRadiusProperty =
            BindableProperty.Create(nameof(ButtonCornerRadius), typeof(int), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 32);
        public Color ButtonColor
        {
            get => (Color)GetValue(ButtonColorProperty);
            set => SetValue(ButtonColorProperty, value);
        }
        public static readonly BindableProperty ButtonColorProperty =
            BindableProperty.Create(nameof(ButtonColor), typeof(Color), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: Color.Red);
        public Color ButtonTextColor
        {
            get => (Color)GetValue(ButtonTextColorProperty);
            set => SetValue(ButtonTextColorProperty, value);
        }
        public static readonly BindableProperty ButtonTextColorProperty =
            BindableProperty.Create(nameof(ButtonTextColor), typeof(Color), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: Color.White);
        public ImageSource ClearButtonImageSource
        {
            get => (ImageSource)GetValue(ClearButtonImageSourceProperty);
            set => SetValue(ClearButtonImageSourceProperty, value);
        }
        public static readonly BindableProperty ClearButtonImageSourceProperty =
            BindableProperty.Create(nameof(ClearButtonImageSource), typeof(ImageSource), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: ImageSource.FromResource("SkorXam.Pin.reset.png"));
        public ImageSource DeleteButtonImageSource
        {
            get => (ImageSource)GetValue(DeleteButtonImageSourceProperty);
            set => SetValue(DeleteButtonImageSourceProperty, value);
        }
        public static readonly BindableProperty DeleteButtonImageSourceProperty =
            BindableProperty.Create(nameof(DeleteButtonImageSource), typeof(ImageSource), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: ImageSource.FromResource("SkorXam.Pin.backspace.png"));
        public bool DotHasShadow
        {
            get => (bool)GetValue(DotHasShadowProperty);
            set => SetValue(DotHasShadowProperty, value);
        }
        public static readonly BindableProperty DotHasShadowProperty =
            BindableProperty.Create(nameof(DotHasShadow), typeof(bool), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: false);
        public double DotCornerRadius
        {
            get => (double)GetValue(DotCornerRadiusProperty);
            set => SetValue(DotCornerRadiusProperty, value);
        }
        public static readonly BindableProperty DotCornerRadiusProperty =
            BindableProperty.Create(nameof(DotCornerRadius), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 12d);
        public double ButtonOpacity
        {
            get => (double)GetValue(ButtonOpacityProperty);
            set => SetValue(ButtonOpacityProperty, value);
        }
        public static readonly BindableProperty ButtonOpacityProperty =
            BindableProperty.Create(nameof(ButtonOpacity), typeof(double), typeof(PinView), defaultBindingMode: BindingMode.OneWay,
                defaultValue: 1d);
        #region Methods
        private static void OnPinChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var pinView = (bindable as PinView);
            pinView.RenderPin(newValue?.ToString() ?? string.Empty);
            pinView?.PinChanged?.Invoke(pinView, new PinChangedEventArg(pinView, (string)newValue));
            pinView?.TrySubmit();
        }
        private static void OnPinAppearanceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue) return;
            var pinView = (bindable as PinView);
            pinView.RenderPin(pinView.Pin);
        }
        public void TrySubmit()
        {
            if(Pin.Length == PinLength)
            {
                if(this.PinSubmitCommand?.CanExecute(null) ?? false)
                {
                    this.PinSubmitCommand?.Execute(Pin);
                }
                this.PinSubmit?.Invoke(this, new PinSubmitEventArg(this, Pin));
            }
        }
        private void RenderPin(string newValue)
        {
            if(stackLayout == null)
                return;
            int newValueLength = newValue?.Length ?? 0;
            for(int i = 0; i < PinLength; i++)
                Fill(((stackLayout.Children[i]) as Frame), i < newValueLength);
        }

        private void Fill(Frame frame, bool isFill)
        {
            if(isFill)
                frame.BackgroundColor = DotColor;
            else
                frame.BackgroundColor = DotEmptyColor;
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "1";
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "2";
        }

        private void Button3_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "3";
        }

        private void Button4_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "4";
        }

        private void Button5_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "5";
        }

        private void Button6_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "6";
        }

        private void Button7_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "7";
        }

        private void Button8_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "8";
        }

        private void Button9_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "9";
        }

        private void Button0_Clicked(object sender, EventArgs e)
        {
            if(Pin.Length < PinLength)
                Pin += "0";
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Pin))
                return;
            if(Pin.Length > 0)
            {
                Pin = Pin.Substring(0, Pin.Length - 1);
            }
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            Pin = string.Empty;
        }
        #endregion

    }
    public class PinChangedEventArg
    {
        public PinChangedEventArg(object sender, string pin)
        {
            Source = sender;
            Pin = pin;
        }
        public object Source { get; private set; }
        public string Pin { get; private set; }
    }

    public class PinSubmitEventArg
    {
        public PinSubmitEventArg(object sender, string pin)
        {
            Source = sender;
            Pin = pin;
        }
        public object Source { get; private set; }
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
