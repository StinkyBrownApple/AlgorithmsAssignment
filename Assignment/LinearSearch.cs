using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    static class LinearSearch<T> where T : IComparable<T>
    {
        static int[] positions;
        static int steps;

        public static int[] Find(T[] data, string find, bool findMultiple, out int _steps)
        {
            positions = new int[0];
            steps = 0;
            for (int i = 0; i < data.Length; i++)
            {
                steps++;
                if(data[i].ToString().CompareTo(find) == 0)
                {
                    AddToPositions(i);
                    if (!findMultiple)
                        break;
                }
            }

            _steps = steps;
            return positions;
        }

        private static void AddToPositions(int position)
        {
            Array.Resize(ref positions, positions.Length + 1);
            positions[positions.Length - 1] = position;
        }
    }
}
