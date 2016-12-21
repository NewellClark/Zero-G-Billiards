using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codonbyte
{
    /// <summary>
    /// Property's old value, property's new value, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PropertyChangedEventArgs<T> : EventArgs
    {
        public T OldValue { get; private set; }
        public T NewValue { get; private set; }
        public PropertyChangedEventArgs(T oldValue, T newValue) : base()
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
