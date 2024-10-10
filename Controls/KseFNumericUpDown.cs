using Microsoft.Maui.Controls;

namespace KseF.Controls
{
    public class KseFNumericUpDown : ContentView
    {
        private readonly Entry _entry;
        private readonly Frame _increaseButton;
        private readonly Frame _decreaseButton;

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            nameof(Value), typeof(double), typeof(KseFNumericUpDown), 0.0,
            propertyChanged: OnValueChanged);

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double Increment { get; set; } = 1.0;

        public static readonly BindableProperty EntryWidthProperty = BindableProperty.Create(
            nameof(EntryWidth), typeof(double), typeof(KseFNumericUpDown), 80.0);

        public double EntryWidth
        {
            get => (double)GetValue(EntryWidthProperty);
            set => SetValue(EntryWidthProperty, value);
        }

        public static readonly BindableProperty ButtonWidthProperty = BindableProperty.Create(
            nameof(ButtonWidth), typeof(double), typeof(KseFNumericUpDown), 30.0);

        public double ButtonWidth
        {
            get => (double)GetValue(ButtonWidthProperty);
            set => SetValue(ButtonWidthProperty, value);
        }

        public static readonly BindableProperty ButtonHeightProperty = BindableProperty.Create(
            nameof(ButtonHeight), typeof(double), typeof(KseFNumericUpDown), 30.0);

        public double ButtonHeight
        {
            get => (double)GetValue(ButtonHeightProperty);
            set => SetValue(ButtonHeightProperty, value);
        }

        public KseFNumericUpDown()
        {
            // Grid jako nadrzędny kontener dla całej kontrolki
            var layoutGrid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Star }, // 50% dla Entry
                    new RowDefinition { Height = GridLength.Star }  // 50% dla przycisków
                },
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(EntryWidth * 0.7, GridUnitType.Star) }, // Entry width
                    new ColumnDefinition { Width = new GridLength(ButtonWidth * 0.3, GridUnitType.Star) }  // Button width
                }
            };

            // Entry (pole tekstowe)
            _entry = new Entry
            {
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = EntryWidth * 0.55
            };
            _entry.SetBinding(Entry.TextProperty, new Binding(nameof(Value), source: this, stringFormat: "{0:F2}"));

            // Stos dla przycisków + i -
            var buttonStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                Padding = -7,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
            };

            // Frame dla przycisku "+"
            _increaseButton = new Frame
            {
                Content = new Label 
                { 
                    Text = "+", 
                    TextColor = Color.FromHex("#FFFFFF"), 
                    FontAttributes = FontAttributes.Bold, 
                    HorizontalOptions = LayoutOptions.Center, 
                    VerticalOptions = LayoutOptions.Center 
                },
                CornerRadius = 5, // Zaokrąglenie rogów
                BackgroundColor = Color.FromHex("#512BD4"), // Kolor tła
                WidthRequest = ButtonWidth / 2,
                HeightRequest = ButtonHeight / 2,
                Padding = -7, // Bez wypełnienia wewnętrznego
                HasShadow = false
            };
            var tapIncrease = new TapGestureRecognizer();
            tapIncrease.Tapped += (s, e) => IncreaseButton_Clicked();
            _increaseButton.GestureRecognizers.Add(tapIncrease);

            // Frame dla przycisku "-"
            _decreaseButton = new Frame
            {
                Content = new Label 
                { Text = "-", 
                    TextColor=Color.FromHex("#FFFFFF"), 
                    FontAttributes = FontAttributes.Bold, 
                    FontSize = 15,HorizontalOptions = LayoutOptions.Center, 
                    VerticalOptions = LayoutOptions.Center 
                },
                CornerRadius = 5, // Zaokrąglenie rogów
                BackgroundColor = Color.FromHex("#512BD4"), // Kolor tła
                WidthRequest = ButtonWidth / 2,
                HeightRequest = ButtonHeight / 2,
                Padding = -8, // Bez wypełnienia wewnętrznego
                HasShadow = false
            };
            var tapDecrease = new TapGestureRecognizer();
            tapDecrease.Tapped += (s, e) => DecreaseButton_Clicked();
            _decreaseButton.GestureRecognizers.Add(tapDecrease);

            // Dodanie przycisków do stosu
            buttonStack.Children.Add(_increaseButton);
            buttonStack.Children.Add(_decreaseButton);

            // Dodanie elementów do layoutGrid

            // Dodajemy Entry do Grid, z kolumną 0 i obejmującym 2 wiersze.
            layoutGrid.Children.Add(_entry);
            Grid.SetColumn(_entry, 0);
            Grid.SetRowSpan(_entry, 2);

            // Dodajemy buttonStack do Grid, z kolumną 1 i obejmującym 2 wiersze.
            layoutGrid.Children.Add(buttonStack);
            Grid.SetColumn(buttonStack, 1);
            Grid.SetRowSpan(buttonStack, 2);

            layoutGrid.HorizontalOptions = LayoutOptions.Start;
            // Ustawienie nadrzędnego widoku w ContentView
            Content = layoutGrid;
        }

        private void IncreaseButton_Clicked()
        {
            Value += Increment;
            UpdateEntry();
        }

        private void DecreaseButton_Clicked()
        {
            // Sprawdź, czy nowa wartość nie będzie mniejsza niż 1
            if (Value - Increment >= 1)
            {
                Value -= Increment;
                UpdateEntry();
            }
            // Jeśli wartość jest poniżej 1, nie wykonuj żadnej operacji
        }      

        private void UpdateEntry()
        {
            _entry.Text = Value.ToString("F2");
        }

        private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (KseFNumericUpDown)bindable;
            control.UpdateEntry();
        }
    }
}
