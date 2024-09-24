using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace KseF.Controls
{
    public class KseFDatePicker : DatePicker
    {
        // Bindable property for exact width in device-independent units
        public static readonly BindableProperty PickerWidthProperty = BindableProperty.Create(
            nameof(PickerWidth),
            typeof(double),
            typeof(KseFDatePicker),
            -1.0,
            propertyChanged: OnPickerWidthChanged);

        // Bindable property for percentage-based width
        public static readonly BindableProperty PickerWidthPercentageProperty = BindableProperty.Create(
            nameof(PickerWidthPercentage),
            typeof(double),
            typeof(KseFDatePicker),
            -1.0, // Default value (-1 means no percentage set)
            propertyChanged: OnPickerWidthPercentageChanged);

        // Property for setting the width in exact units (e.g., pixels or device-independent units)
        public double PickerWidth
        {
            get => (double)GetValue(PickerWidthProperty);
            set => SetValue(PickerWidthProperty, value);
        }

        // Property for setting the width in percentage relative to the parent container
        public double PickerWidthPercentage
        {
            get => (double)GetValue(PickerWidthPercentageProperty);
            set => SetValue(PickerWidthPercentageProperty, value);
        }

        // Event triggered when PickerWidth changes
        private static void OnPickerWidthChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (KseFDatePicker)bindable;
            picker.UpdatePickerWidth();
        }

        // Event triggered when PickerWidthPercentage changes
        private static void OnPickerWidthPercentageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (KseFDatePicker)bindable;
            picker.UpdatePickerWidth();
        }

        // Method to update the width of the DatePicker control
        private void UpdatePickerWidth()
        {
            if (PickerWidth > 0)
            {
                WidthRequest = PickerWidth;
            }
            else if (PickerWidthPercentage > 0 && PickerWidthPercentage <= 100)
            {
                // If a percentage is set, calculate width based on the parent or screen size
                if (Parent is VisualElement parentElement && parentElement.Width > 0)
                {
                    WidthRequest = parentElement.Width * (PickerWidthPercentage / 100);
                }
            }
            else
            {
                // Reset to default width if neither property is valid
                ClearValue(WidthRequestProperty);
            }
        }

        // Constructor
        public KseFDatePicker()
        {
            // Optionally set default format, style, etc.
            Format = "dd/MM/yyyy";
            SizeChanged += OnSizeChanged;
        }

        // Event handler for when the size of the control or its parent changes
        private void OnSizeChanged(object sender, EventArgs e)
        {
            // Recalculate width when size changes, especially for percentage-based width
            if (PickerWidthPercentage > 0 && PickerWidthPercentage <= 100)
            {
                UpdatePickerWidth();
            }
        }
    }
}
