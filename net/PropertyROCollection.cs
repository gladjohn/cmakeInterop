using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Client.NativeInterop
{
    public class PropertyROCollection
    {
        private Dictionary<string, string> _properties = new Dictionary<string, string>();

        Func<string, string> _reader = null;

        internal PropertyROCollection(Func<string, string> reader)
        {
            _reader = reader;
        }

        public string this[string key]
        {
            get
            {
                string value;
                if (_properties.TryGetValue(key, out value))
                {
                    return value;
                }
                else
                {
                    value = _reader(key);
                    _properties[key] = value;
                    return value;
                }
            }
        }
    }

}
