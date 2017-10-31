using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    static class BubbleSort<T> where T : IComparable<T>
    {
        static int steps = 0;
        static int compNo = -1;

        public static T[] Sort(T[] data, out int _steps, bool descending = false) //Sorting just one set of data
        {
            steps = 0; //Counter for steps
            if (descending == false)    //Check whether we're sorting ascendnig or descending
                compNo = -1;
            else
                compNo = 1;

            for (int i = data.Length - 1; i > 0; i--) //Loop through the data
            {
                for (int j = 0; j <= i - 1; j++) // each time we need to make comparisons one 1 less number as the last number will be sorted after each loop
                {
                    steps++;
                    if (data[j + 1].CompareTo(data[j]) == compNo) //compare
                    {
                        T temp = data[j];
                        data[j] = data[j + 1];  //swap
                        data[j + 1] = temp;
                        steps++;
                    }
                }
            }

            _steps = steps; //set steps to the steps we counted
            return data; //return data
        }

        public static T[] SortMultiple(T[] data, string[][] otherData, out string[][] sortedOtherData, out int _steps, bool descending = false) //same as single data, except swaps values in other array in relation to array we're sorting
        {
            steps = 0;
            if (descending == false)
                compNo = -1;
            else
                compNo = 1;

            for (int i = data.Length - 1; i > 0; i--)
            {
                for (int j = 0; j <= i - 1; j++)
                {
                    steps++;
                    if (data[j + 1].CompareTo(data[j]) == compNo)
                    {
                        T temp = data[j];
                        data[j] = data[j + 1];      //When we make a swap in the data
                        data[j + 1] = temp;
                        for (int x = 0; x < otherData.Length; x++)
                        {
                            string newTemp = otherData[x][j];
                            otherData[x][j] = otherData[x][j + 1];  //swap the data in other arrays at same position
                            otherData[x][j + 1] = newTemp;
                        }
                        steps++;
                    }
                }
            }

            _steps = steps;
            sortedOtherData = otherData;    //set data to return
            return data; //return data
        }

    }
}
