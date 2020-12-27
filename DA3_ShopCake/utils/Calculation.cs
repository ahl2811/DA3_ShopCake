using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA3_ShopCake.utils
{
    class Calculation
    {
        public static int sumOfSerialNumbers(List<KeyValuePair<int, int>> serialNums)
        {
            int result = 0;

            foreach(KeyValuePair<int, int> element in serialNums)
            {
                result += element.Key * element.Value;
            }

            return result;
        }

        public static double divide(double a, double b)
        {
            return a / b;
        }
    }
}
