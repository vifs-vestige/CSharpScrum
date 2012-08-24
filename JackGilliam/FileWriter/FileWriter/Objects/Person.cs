using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FileWriter.Objects
{
    sealed class Person : IInstanceAssembler
    {
        [Flags]
        enum Property
        {
            Firstname = 0x0,
            Lastname = 1 << 1,
            Email = 1 << 2,
        }

        private Dictionary<Property, string> _propertyDictionary;

        private List<string> Properties
        {
            get
            {
                return _propertyDictionary.Values.ToList();
            }
        }

        private Person()
        {
            _propertyDictionary = new Dictionary<Property, string>()
            {
                {Property.Firstname, ""},
                {Property.Lastname, ""},
                {Property.Email, ""},
            };
        }

        public override void UpdateValue(int propertyIndex, string newValue)
        {
            var values = GetPropertyValues<Property>(typeof(Property));
            if (propertyIndex <= values.Length && propertyIndex >= 0)
            {
                var propertyKey = values[propertyIndex];
                if (_propertyDictionary.ContainsKey(propertyKey))
                {
                    _propertyDictionary[propertyKey] = newValue;
                }
            }
        }

        public override string PropertyAt(int index)
        {
            string property = null;
            if (index >= 0 && index < _propertyDictionary.Count)
            {
                property = Properties[index];
            }
            return property;
        }

        public static IInstanceAssembler GetInstance(object vals)
        {
            IInstanceAssembler instance = null;
            if (vals != null)
            {
                var values = Regex.Split(vals.ToString(), ValueSeparator);
                var person = new Person();
                var properties = (Property[])Enum.GetValues(typeof(Property));
                for (int i = 0; i < values.Length; i += 1)
                {
                    person._propertyDictionary[properties[i]] = values[i].ToString();
                }
                instance = person;
            }
            return instance;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            var properties = Properties;
            int length = properties.Count - 1;
            for (int i = 0; i <= length; i += 1)
            {
                stringBuilder.Append(properties[i]);
                if (i < length)
                {
                    stringBuilder.Append(",");
                }
            }
            return stringBuilder.ToString();
        }

        public override List<string> GetPropertyNames()
        {
            return ((string[])Enum.GetNames(typeof(Property))).ToList();
        }

        public override List<string> GetCurrentValues()
        {
            List<string> currentValues = new List<string>();

            var propertyNames = ((string[])Enum.GetNames(typeof(Property))).ToList();
            string propertyFormat = "{0}: {1}";
            for (int i = 0; i < Properties.Count; i += 1)
            {
                currentValues.Add(String.Format(propertyFormat, propertyNames[i], Properties[i]));
            }
            return currentValues;
        }

        private T[] GetPropertyValues<T>(Type enumType)
        {
            T[] values = default(T[]);
            if (enumType.IsEnum)
            {
                values = (T[])Enum.GetValues(enumType);
            }
            return values;
        }
    }
}
