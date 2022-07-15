using System;
using System.Collections.Generic;
using System.Text;

namespace IOTests.Utils
{
    public class HelperFunctions
    {
        public static bool IsObjectPropertyValueMissing(object obj)
        {
            bool retval = false;
            var objProperties = obj.GetType().GetProperties();
            foreach (var property in objProperties)
            {
                retval &= string.IsNullOrEmpty(property.GetValue(obj).ToString());
            }

            return retval;
        }
    }
}
