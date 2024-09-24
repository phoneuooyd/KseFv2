using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.EnumLibrary;

namespace KseF.Services
{
    public class DataLoadingService
    {
        public static void LoadEnumValues<T>(Picker picker, string defaultSelection = null) where T : Enum
        {
            var items = new List<string>();
            var enumDisplayNameMap = new Dictionary<string, T>(); // Lokalny słownik mapujący nazwy wyświetlane na Enum

            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var field = value.GetType().GetField(value.ToString());
                var displayNameAttribute = (EnumDisplayNameAttribute?)Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute));
                var displayName = displayNameAttribute != null ? displayNameAttribute.DisplayName : value.ToString();

                items.Add(displayName);
                enumDisplayNameMap[displayName] = (T)value; // Mapowanie nazwy wyświetlanej na wartość Enum
            }

            picker.ItemsSource = items;
            if (defaultSelection != null)
            {
                picker.SelectedIndex = items.IndexOf(defaultSelection);
            }

            // Zapamiętaj mapowanie nazw dla przyszłych operacji
            picker.BindingContext = enumDisplayNameMap;
        }

        // Metoda do zamiany wybranej nazwy VAT z picker na wartość Enum
        public static T? GetEnumValueFromPicker<T>(Picker picker) where T : struct, Enum
        {
            if (picker.SelectedItem != null && picker.BindingContext is Dictionary<string, T> enumDisplayNameMap)
            {
                var selectedDisplayName = picker.SelectedItem.ToString();
                if (enumDisplayNameMap.TryGetValue(selectedDisplayName, out T enumValue))
                {
                    return enumValue;
                }
            }
            return null;
        }

        public static string GetEnumDisplayName<T>(T enumValue) where T : Enum
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var displayNameAttribute = (EnumDisplayNameAttribute?)Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute));
            return displayNameAttribute != null ? displayNameAttribute.DisplayName : enumValue.ToString();
        }

        public static decimal VatRateToDecimal(EnumLibrary.StawkiPodatkuPL? selectedVatRate)
        {
            switch (selectedVatRate)
            {
                case StawkiPodatkuPL.zw:
                    return 0.0m;
                case StawkiPodatkuPL.oo:
                    return 0.0m;
                case StawkiPodatkuPL.np:
                    return 0.0m;
                case StawkiPodatkuPL.Item0:
                    return 0.0m;
                case StawkiPodatkuPL.Item3:
                    return 0.03m;
                case StawkiPodatkuPL.Item4:
                    return 0.04m;
                case StawkiPodatkuPL.Item5:
                    return 0.05m;
                case StawkiPodatkuPL.Item7:
                    return 0.07m;
                case StawkiPodatkuPL.Item8:
                    return 0.08m;
                case StawkiPodatkuPL.Item22:
                    return 0.22m;
                case StawkiPodatkuPL.Item23:
                    return 0.23m;
                default:
                    return 0.00m;
            }
        }
        
    }
}
