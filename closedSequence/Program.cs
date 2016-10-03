using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace closedSequence
{

    //https://codefights.com/challenge/Qjts7cukDvYpDW4Bc/main?utm_source=facebook&utm_medium=cpc&utm_campaign=Solve_A_Challenge_V6%28Europe%29
    class Program
    {
        static void Main(string[] args)
        {

            int[] a = { 1, 2, 6 };
            int[] b = { 0, 1, 3, 4, 5 };
     

            var validArrays = CreateValidArrays(a, b);
            var smallestDifference = CalculateSmallestDifference(a, validArrays);          

            Console.WriteLine(smallestDifference);
            Console.ReadKey();
        }

        static List<int[]> CreateValidArrays(int[] a, int[] b)
        {
            if (a.Length == b.Length) return new List<int[]> { b };

            int minValue = (int)Math.Pow(2, a.Length) - 1;
            int maxValue = (int)Math.Pow(2, b.Length);

            List<int[]> arr = new List<int[]>();
            {
                for (int i = minValue; i < maxValue; i++)
                {
                    if (CountBits(i) == a.Length)
                    {
                        arr.Add(CreateValidArray(b, i));
                    }
                }
            }
            return arr;
        }

        static int[] CreateValidArray(int[] b, int i)
        {
            List<int> result = new List<int>();
            var arr = ConvertToBooleanArray(i);

            while (arr.Count < b.Length)
            {
                arr.Insert(0, false);
            }
            for (int j = 0; j < arr.Count; j++)
            {
                if (arr[j] == true) result.Add(b[j]);
            }
            return result.ToArray();
        }

        static int CalculateSmallestDifference(int[] a, List<int[]> validArrays)
        {
            var result = int.MaxValue;
            foreach (var validArray in validArrays)
            {
                var currentDifference = CalculateTotalDifference(a, validArray, result);
                if (currentDifference < result) result = currentDifference;
                if (currentDifference == 0) return 0;
            }
            return result;
        }

        static int CalculateTotalDifference(int[] a, int[] validArray, int maxValue)
        {
            var sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += CalculateDifference(a[i], validArray[i]);
                if (sum > maxValue) return int.MaxValue;
            }
            return sum;
        }

        static int CalculateDifference(int a, int b)
        {
            var result = a - b;
            if (result < 0) result = result * -1;
            return result;
        }

        static int CountBits(int number)
        {
            int result = 0;
            while (number != 0)
            {
                result += number & 1;
                number = number >> 1;
            }
            return result;
        }

        static List<bool> ConvertToBooleanArray(int i)
        {
            return Convert.ToString(i, 2 /*for binary*/).Select(s => s.Equals('1')).ToList();
        }

    }


}
