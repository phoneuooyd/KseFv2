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

            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var field = value.GetType().GetField(value.ToString());
                var displayNameAttribute = (EnumDisplayNameAttribute?)Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute));
                var displayName = displayNameAttribute != null ? displayNameAttribute.DisplayName : value.ToString();

                items.Add(displayName);
            }

            picker.ItemsSource = items;
            if (defaultSelection != null)
            {
                picker.SelectedIndex = items.IndexOf(defaultSelection);
            }
        }

        /*
        public static string? SelectEnumIndex(string? selectedVatRate)
        {
            switch (selectedVatRate)
            {
                case "Zwol. z opod.":
                    return "zw";
                case "Odwrotne obciąż.":
                    return "oo";
                case "Niepodlegające opdot.":
                    return "np";
                default:
                    return selectedVatRate = selectedVatRate.Replace("%", "");
            }
        }
        */
    }
}
