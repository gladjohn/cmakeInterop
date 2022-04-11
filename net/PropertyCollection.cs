using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Client.NativeInterop
{
    public class PropertyCollection
    {
        private Dictionary<string, string> _properties = new Dictionary<string, string>();

        Action<string, string> _setter = null;

        internal PropertyCollection(Action<string, string> setter)
        {
            _setter = setter;
        }

        public string this[string key]
        {
            set
            {
                _setter(key, value);
                _properties[key] = value;
            }

            get
            {
                string value;
                if (_properties.TryGetValue(key, out value))
                {
                    return value;
                }

                return value;
            }
        }
    }

}
