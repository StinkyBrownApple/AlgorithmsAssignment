using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    static class MergeSort<T> where T : IComparable<T>
    {
        static int compNo = -1;
        static int steps = 0;

        public static T[] Sort(T[] data, out int _steps, bool descending = false)
        {
            steps = 0;
            if (descending == false)
                compNo = -1;
            else
                compNo = 1;

            T[] temp = new T[data.Length];
            MergeSortRecursive(data, temp, 0, data.Length - 1);
            _steps = steps;
            return data;
        }

        private static void MergeSortRecursive(T[] data, T[] temp, int low, int high)
        {
            int n = high - low + 1;
            int middle = low + n / 2;
            int i;

            if (n < 2) return;  // move lower half of data into temporary storage
            for (i = low; i < middle; i++)
            {
                temp[i] = data[i];
                steps++;
            }
            // sort lower half array
            MergeSortRecursive(temp, data, low, middle - 1);
            // sort upper half array
            MergeSortRecursive(data, temp, middle, high);
            // merge halves together
            Merge(data, temp, low, middle, high);
        }

        private static void Merge(T[] data, T[] temp, int low, int middle, int high)
        {
            int ri = low; // result index
            int ti = low; // temp index
            int di = middle; // destination index
                             
            while (ti < middle && di <= high) // while two lists are not empty merge smaller //value
            {
                if (data[di].CompareTo(temp[ti]) == compNo)
                {
                    data[ri++] = data[di++];    // smaller is in high data
                    steps++;
                }
                else
                {
                    data[ri++] = temp[ti++];    // smaller is in temp
                }
            }
            
            while (ti < middle) //possibly some values left in temp array
            {
                data[ri++] = temp[ti++];
                steps++;
            }

            //… or some values left (in correct place) in data array
        }

        public static T[] SortMultiple(T[] data, string[][] otherData, out string[][] otherSortedData, out int _steps, bool descending = false)
        {
            steps = 0;
            if (descending == false)
                compNo = -1;
            else
                compNo = 1;

            T[] temp = new T[data.Length];
            string[][] otherDataTemp = new string[otherData.Length][];
            for (int x = 0; x < otherDataTemp.Length; x++)
            {
                otherDataTemp[x] = new string[otherData[x].Length];
            }
            MergeSortRecursiveMultiple(data, otherData, temp, otherDataTemp, 0, data.Length - 1);

            _steps = steps;
            otherSortedData = otherData;
            return data;
        }

        private static void MergeSortRecursiveMultiple(T[] data, string[][] otherData, T[] temp, string[][] otherDataTemp, int low, int high)
        {
            int n = high - low + 1;
            int middle = low + n / 2;
            int i;

            if (n < 2) return;  // move lower half of data into temporary storage
            for (i = low; i < middle; i++)
            {
                temp[i] = data[i];
                for (int x = 0; x < otherData.Length; x++)
                {
                    otherDataTemp[x][i] = otherData[x][i];
                }
                steps++;
            }
            // sort lower half array
            MergeSortRecursiveMultiple(temp, otherDataTemp, data, otherData, low, middle - 1);
            // sort upper half array
            MergeSortRecursiveMultiple(data, otherData, temp, otherDataTemp, middle, high);
            // merge halves together
            MergeMultiple(data, otherData, temp, otherDataTemp, low, middle, high);
        }

        private static void MergeMultiple(T[] data, string[][] otherData, T[] temp, string[][] otherDataTemp, int low, int middle, int high)
        {
            int ri = low; // result index
            int ti = low; // temp index
            int di = middle; // destination index

            while (ti < middle && di <= high) // while two lists are not empty merge smaller value
            {
                if (data[di].CompareTo(temp[ti]) == compNo)
                {
                    data[ri] = data[di];    // smaller is in high data
                    for (int x = 0; x < otherData.Length; x++)
                    {
                        otherData[x][ri] = otherData[x][di];
                    }
                    ri++;
                    di++;
                    steps++;
                }
                else
                {
                    data[ri] = temp[ti];    // smaller is in temp
                    for (int x = 0; x < otherData.Length; x++)
                    {
                        otherData[x][ri] = otherDataTemp[x][ti];
                    }
                    ri++;
                    ti++;
                }
            }

            while (ti < middle) //possibly some values left in temp array
            {
                data[ri] = temp[ti];
                for (int x = 0; x < otherData.Length; x++)
                {
                    otherData[x][ri] = otherDataTemp[x][ti];
                }
                ri++;
                ti++;
                steps++;
            }

            //… or some values left (in correct place) in data array
        }


    }
}
