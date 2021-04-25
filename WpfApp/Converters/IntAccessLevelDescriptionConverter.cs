using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfApp.Models;

namespace WpfApp.Converters
{
    public class IntAccessLevelDescriptionConverter : IValueConverter
    {
        private string GetEnumDescription(Enum enumObj)
        {
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                DescriptionAttribute attrib = attribArray[0] as DescriptionAttribute;
                return attrib.Description;
            }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                AccessLevel myEnum = (AccessLevel)(int)value;
                string description = GetEnumDescription(myEnum);
                return description;
            }
            return "Передан параметр NULL";
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            foreach (var one in AccessLevel.GetValues(parameter as Type))
            {
                AccessLevel myEnum = (AccessLevel)one;
                if (value.ToString() == GetEnumDescription(myEnum))
                    return (int)one;
            }
            return null;
        }
    }
}
