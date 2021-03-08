using System;
using System.Collections.Generic;
using System.Text;

namespace Algo_DZ4_HashSetSearch
{
    public class MyStringHashOverraide
    {
        public string SingleString { get; set; }

        public override bool Equals(object obj)
        {
            var strToCompare = obj as MyStringHashOverraide;

            if (strToCompare == null)
                return false;

            return SingleString == strToCompare.SingleString;
        }

        public override int GetHashCode()
        {
            int strHashCode = SingleString?.GetHashCode() ?? 0;
            return strHashCode;
        }
    }
}
