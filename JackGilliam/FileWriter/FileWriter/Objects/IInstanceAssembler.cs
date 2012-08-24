using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileWriter.Objects
{
    abstract class IInstanceAssembler
    {
        public static IInstanceAssembler GetInstance(params object[] values)
        {
            throw new NotImplementedException();
        }

        public static string GetObjectSeparator()
        {
            return ObjectSeparator;
        }

        private static string _valueSeparator;

        protected static string ValueSeparator
        {
            get
            {
                if (_valueSeparator == null)
                {
                    _valueSeparator = ",";
                }
                return _valueSeparator;
            }
            set
            {
                _valueSeparator = value;
            }
        }

        private static string _objectSeparator;

        protected static string ObjectSeparator
        {
            get
            {
                if (_objectSeparator == null)
                {
                    _objectSeparator = " ";
                }
                return _objectSeparator;
            }
            set
            {
                _objectSeparator = value;
            }
        }

        public abstract string PropertyAt(int index);

        public abstract List<string> GetPropertyNames();

        public abstract List<string> GetCurrentValues();
        public abstract void UpdateValue(int propertyChoice, string newValue);
    }
}
