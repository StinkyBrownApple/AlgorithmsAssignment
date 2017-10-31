using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
	static class InsertionSort<T> where T : IComparable<T>
	{
		public static T[] Sort(T[] array, out int steps, bool descending = false)
		{
            steps = 0;
            int checkNo;
            if (descending == true)
                checkNo = 1;
            else
                checkNo = -1;


			for (int i = 1; i < array.Length; i++)
			{
				int j = i;
				while(j > 0 && array[j].CompareTo(array[j-1]) == checkNo)
				{
					T temp = array[j - 1];
					array[j - 1] = array[j];
					array[j] = temp;
					j--;
                    steps++;
				}
			}
			return array;
		}

        public static T[] SortMultiple(T[] array, string[][] otherData, out string[][] otherSortedData, out int steps, bool descending = false)
        {
            steps = 0;
            int checkNo;
            if (descending == true)
                checkNo = 1;
            else
                checkNo = -1;


            for (int i = 1; i < array.Length; i++)
            {
                int j = i;
                while (j > 0 && array[j].CompareTo(array[j - 1]) == checkNo)
                {
                    T temp = array[j - 1];
                    array[j - 1] = array[j];
                    array[j] = temp;
                    for (int x = 0; x < otherData.Length; x++)
                    {
                        string otherTemp = otherData[x][j - 1];
                        otherData[x][j - 1] = otherData[x][j];
                        otherData[x][j] = otherTemp;
                    }
                    j--;
                    steps++;
                }
            }

            otherSortedData = otherData;
            return array;
        }
    }
}
