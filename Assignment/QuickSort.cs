using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    static class QuickSort<T> where T : IComparable<T>
    {
        static int compNo = -1;
        static int steps = 0;

        public static T[] Sort(T[] data, out int _steps, bool descending = false)
        {
            // pre: 0 <= n <= data.length
            // post: values in data[0 … n-1] are in ascending order

            steps = 0;
            if (descending == false)
                compNo = -1;
            else
                compNo = 1;

            Quick_Sort(data, 0, data.Length - 1);
            _steps = steps;
            return data;
        }

        private static void Quick_Sort(T[] data, int left, int right)
        {
            int i, j;
            T pivot, temp;
            i = left;
            j = right;
            pivot = data[(left + right) / 2];
            do
            {
                while ((data[i].CompareTo(pivot) == compNo) && (i < right)) { i++; steps++; }
                while ((pivot.CompareTo(data[j]) == compNo) && (j > left)) { j--; steps++; }
                if (i <= j)
                {
                    temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    i++;
                    j--;
                    steps++;
                }
            } while (i <= j);
            if (left < j) Quick_Sort(data, left, j);
            if (i < right) Quick_Sort(data, i, right);
        }

        public static T[] SortMultiple(T[] data, string[][] otherData, out string[][] otherSortedData, out int _steps, bool descending = false)
        {
            // pre: 0 <= n <= data.length
            // post: values in data[0 … n-1] are in ascending order

            steps = 0;
            if (descending == false)
                compNo = -1;
            else
                compNo = 1;

            QuickSortMultiple(data, otherData, 0, data.Length - 1);
            _steps = steps;
            otherSortedData = otherData;
            return data;
        }

        private static void QuickSortMultiple(T[] data, string[][] otherData, int left, int right)
        {
            int i, j;
            T pivot, temp;
            i = left;
            j = right;
            pivot = data[(left + right) / 2];
            do
            {
                while ((data[i].CompareTo(pivot) == compNo) && (i < right)) { i++; steps++; }
                while ((pivot.CompareTo(data[j]) == compNo) && (j > left)) { j--; steps++; }
                if (i <= j)
                {
                    temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    for (int x = 0; x < otherData.Length; x++)
                    {
                        string otherTemp = otherData[x][i];
                        otherData[x][i] = otherData[x][j];
                        otherData[x][j] = otherTemp;
                    }
                    i++;
                    j--;
                    steps++;
                }
            } while (i <= j);
            if (left < j) QuickSortMultiple(data, otherData, left, j);
            if (i < right) QuickSortMultiple(data, otherData, i, right);
        }
    }
}
