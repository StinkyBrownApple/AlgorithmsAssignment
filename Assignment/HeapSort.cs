using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    static class HeapSort<T> where T : IComparable<T>
    {
        static int compNo = -1;
        static int steps = 0;

        public static T[] Sort(T[] data, out int _steps, bool descending = false)
        {
            steps = 0;
            if (descending == false)
                compNo = 1;
            else
                compNo = -1;

            int heapSize = data.Length;
            int i;
            for (i = (heapSize - 1) / 2; i >= 0; i--)
            {
                MaxHeapify(data, heapSize, i);
            }
            for (i = data.Length - 1; i > 0; i--)
            {
                T temp = data[i];
                data[i] = data[0];
                data[0] = temp;
                heapSize--;
                steps++;
                MaxHeapify(data, heapSize, 0);
            }

            _steps = steps;
            return data;
        }

        private static void MaxHeapify(T[] Heap, int HeapSize, int Index)
        {
            int Left = (Index + 1) * 2 - 1;
            int Right = (Index + 1) * 2;
            int largest = 0;
            if (Left < HeapSize && Heap[Left].CompareTo(Heap[Index]) == compNo)
            {
                largest = Left;
                steps++;
            }
            else
            {
                largest = Index;
                steps++;
            }
            if (Right < HeapSize && Heap[Right].CompareTo(Heap[largest]) == compNo)
            {
                largest = Right;
                steps++;
            }
            if (largest != Index)
            {
                T temp = Heap[Index];
                Heap[Index] = Heap[largest];
                Heap[largest] = temp;
                steps++;
                MaxHeapify(Heap, HeapSize, largest);
            }
        }

        public static T[] SortMultiple(T[] data, string[][] otherData, out string[][] otherSortedData, out int _steps, bool descending = false)
        {
            steps = 0;
            if (descending == false)
                compNo = 1;
            else
                compNo = -1;

            int heapSize = data.Length;
            int i;
            for (i = (heapSize - 1) / 2; i >= 0; i--)
            {
                MaxHeapifyMultiple(data, otherData, heapSize, i);
            }
            for (i = data.Length - 1; i > 0; i--)
            {
                T temp = data[i];
                data[i] = data[0];
                data[0] = temp;
                heapSize--;
                for (int x = 0; x < otherData.Length; x++)
                {
                    string otherTemp = otherData[x][i];
                    otherData[x][i] = otherData[x][0];
                    otherData[x][0] = otherTemp;
                }
                steps++;
                MaxHeapifyMultiple(data, otherData, heapSize, 0);
            }

            _steps = steps;
            otherSortedData = otherData;
            return data;
        }

        private static void MaxHeapifyMultiple(T[] Heap, string[][] otherData, int HeapSize, int Index)
        {
            int Left = (Index + 1) * 2 - 1;
            int Right = (Index + 1) * 2;
            int largest = 0;
            if (Left < HeapSize && Heap[Left].CompareTo(Heap[Index]) == compNo)
            {
                largest = Left;
                steps++;
            }
            else
            {
                largest = Index;
                steps++;
            }
            if (Right < HeapSize && Heap[Right].CompareTo(Heap[largest]) == compNo)
            {
                largest = Right;
                steps++;
            }
            if (largest != Index)
            {
                T temp = Heap[Index];
                Heap[Index] = Heap[largest];
                Heap[largest] = temp;
                for(int x = 0; x < otherData.Length; x++)
                {
                    string otherTemp = otherData[x][Index];
                    otherData[x][Index] = otherData[x][largest];
                    otherData[x][largest] = otherTemp;
                }
                steps++;
                MaxHeapifyMultiple(Heap, otherData, HeapSize, largest);
            }
        }

    }
}
