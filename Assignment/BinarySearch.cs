using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    static class BinarySearch<T> where T : IComparable<T>
    {
        static int[] positions;
        static int steps;

        public static int[] Find(T[] data, string find, bool findMultiple, out int _steps)
        {
            //Instantiate variables we'll need
            steps = 0;
            positions = new int[0];
            int min = 0;
            int max = data.Length - 1;

            while(min <= max) //while we havent gone through all the data yet
            {
                int middle = (min + max) / 2; //get the middle of the min and max
                if (data[middle].ToString().CompareTo(find) == 0) //see if the value is at the middle
                {
                    if (findMultiple) //if we neet to find mutiple values
                    {
                        for (int i = middle; ((i > 0) && (data[i].ToString().CompareTo(find) == 0)); i--) //starting at the found value, go left until there's no more of that value
                        {
                            AddToPositions(i); //Add the positions to the array
                            steps++;
                        }

                        for (int i = middle + 1; (i < data.Length) && (data[i].ToString().CompareTo(find) == 0); i++) //starting at the found value + 1, go right until there's no more of that value
                        {
                            AddToPositions(i); //Add the positions to the array
                            steps++;
                        }
                    }

                    else
                    {
                        AddToPositions(middle); //if we only need to find one value, just return this one
                    }

                    break;

                }
                else if (data[middle].ToString().CompareTo(find) == -1) //if the data is greater than the value at the middle
                {
                    min = middle + 1; //set the min value to 1 above the middle
                    steps++;
                }
                else //if the data is less that the value at the middle
                {
                    max = middle - 1; //set the max to 1 below the middle
                    steps++;
                }
            }

            _steps = steps; //set the steps the algroithm took
            return positions; //return the positions
        }

        private static void AddToPositions(int position) //add a value to the positions array
        {
            Array.Resize(ref positions, positions.Length + 1); //make the positions array one bigger
            positions[positions.Length - 1] = position; //add it to the end of the array
        }


    }
}
