using Microsoft.Maui.Controls;

namespace KseF.Controls
{
    public class KseFSmallButton : Frame
    {
        // Właściwości do ustawienia wymiarów
        public static readonly BindableProperty ButtonWidthProperty = BindableProperty.Create(
            nameof(ButtonWidth), typeof(double), typeof(KseFSmallButton));

        public double ButtonWidth
        {
            get => (double)GetValue(ButtonWidthProperty);
            set => SetValue(ButtonWidthProperty, value);
        }

        public static readonly BindableProperty ButtonHeightProperty = BindableProperty.Create(
            nameof(ButtonHeight), typeof(double), typeof(KseFSmallButton));

        public double ButtonHeight
        {
            get => (double)GetValue(ButtonHeightProperty);
            set => SetValue(ButtonHeightProperty, value);
        }

        // Dodanie zdarzenia Clicked
        public event EventHandler Clicked;


        public KseFSmallButton(string text)
        {
            Content = new Label
            {
                Text = text,
                TextColor = Color.FromHex("#FFFFFF"),
                FontAttributes = FontAttributes.Bold,
                FontSize = (ButtonHeight+ButtonWidth)/2,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
                
            };


            // Ustawienia Frame
            CornerRadius = 5; // Zaokrąglenie rogów
            BackgroundColor = Color.FromHex("#512BD4"); // Kolor tła
            Padding = 0; // Bez wypełnienia wewnętrznego
            HasShadow = false;

            // Ustaw rozmiar przycisku
            WidthRequest = ButtonWidth/4;
            HeightRequest = ButtonHeight/4;
            

            // Dodaj gest tapnięcia
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => OnTapped();
            GestureRecognizers.Add(tapGesture);
        }

        public void OnTapped()
        {
            // Logika po tapnięciu (możesz dostosować tę metodę)
            // Przykład: wysłać zdarzenie lub zmienić kolor
            BackgroundColor = BackgroundColor == Color.FromHex("#512BD4") ? Color.FromHex("#9C6BA0") : Color.FromHex("#A084CA");

            // Wywołaj zdarzenie Clicked
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
