using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx
{
    public static class AssertObject
    {
        public static void IsNotNull<T>(T objectToCheck) where T : class
        {
            if (objectToCheck == null)
            {
                throw new LynxObjectNotFoundException<T>();
            }
        }

        public static void IsNotNull<T>(T objectToCheck, string errorIfNull) where T : class
        {
            if (objectToCheck == null)
            {
                throw new LynxObjectNotFoundException<T>(errorIfNull);
            }
        }
    }
}
