using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility.Utility
{
    public static partial class Helper
    {
        #region Yield

        public static IEnumerable<T> YieldEmpty<T>()
        {
            yield break;
        }

        public static IEnumerable<T> YieldSingle<T>(T t)
        {
            yield return t;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2)
        {
            yield return t1;
            yield return t2;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3)
        {
            yield return t1;
            yield return t2;
            yield return t3;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3, T t4)
        {
            yield return t1;
            yield return t2;
            yield return t3;
            yield return t4;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3, T t4, T t5)
        {
            yield return t1;
            yield return t2;
            yield return t3;
            yield return t4;
            yield return t5;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3, T t4, T t5, T t6)
        {
            yield return t1;
            yield return t2;
            yield return t3;
            yield return t4;
            yield return t5;
            yield return t6;
        }


        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3, T t4, T t5, T t6, T t7)
        {
            yield return t1;
            yield return t2;
            yield return t3;
            yield return t4;
            yield return t5;
            yield return t6;
            yield return t7;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3, T t4, T t5, T t6, T t7, T t8)
        {
            yield return t1;
            yield return t2;
            yield return t3;
            yield return t4;
            yield return t5;
            yield return t6;
            yield return t7;
            yield return t8;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3, T t4, T t5, T t6, T t7, T t8, T t9)
        {
            yield return t1;
            yield return t2;
            yield return t3;
            yield return t4;
            yield return t5;
            yield return t6;
            yield return t7;
            yield return t8;
            yield return t9;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3, T t4, T t5, T t6, T t7, T t8, T t9, T t10)
        {
            yield return t1;
            yield return t2;
            yield return t3;
            yield return t4;
            yield return t5;
            yield return t6;
            yield return t7;
            yield return t8;
            yield return t9;
            yield return t10;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3, T t4, T t5, T t6, T t7, T t8, T t9, T t10, T t11)
        {
            yield return t1;
            yield return t2;
            yield return t3;
            yield return t4;
            yield return t5;
            yield return t6;
            yield return t7;
            yield return t8;
            yield return t9;
            yield return t10;
            yield return t11;
        }

        public static IEnumerable<T> YieldList<T>(T t1, T t2, T t3, T t4, T t5, T t6, T t7, T t8, T t9, T t10, T t11, T t12)
        {
            yield return t1;
            yield return t2;
            yield return t3;
            yield return t4;
            yield return t5;
            yield return t6;
            yield return t7;
            yield return t8;
            yield return t9;
            yield return t10;
            yield return t11;
            yield return t12;
        }

        #endregion

        public static bool IsEquals<T>(T[] arr1, T[] arr2)
        {
            for (int i = 0; i < arr1.Length; i++)
                if (!arr1[i].Equals(arr2[i]))
                    return false;
            return true;
        }
    }
}
