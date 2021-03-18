using System;
using System.Collections.Generic;

namespace DZ8_BucketSortArray
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
             * Alerxander Fakhrudinov = Александр Фахрудинов
             * asbuka@gmail.com
             * 
             * 1. Bucketsort
             * Реализовать Bucketsort, проверить корректность работы.
             */

            Console.WriteLine("Bucketsort");

            //генерация массива int 10000 элементов от MinValue до MaxValue
            int[] array = GenerateIntArray(10000);

            Console.WriteLine("\nUnsorted Array:");
            //ShowArray(array);

            bool IsSorted = CheckArraySorted(array);
            if (IsSorted)
                Console.WriteLine("Array sorted, OK");
            else
                Console.WriteLine("Array Unsorted.");

            //sort array
            array = BucketSortArray(array);

            Console.WriteLine("\nSorted Array:");
            //ShowArray(array);

            IsSorted = CheckArraySorted(array);
            if (IsSorted)
                Console.WriteLine("Array sorted, OK");
            else
                Console.WriteLine("Array Unsorted.");

            Console.Read();
        }

        private static bool CheckArraySorted(int[] array)
        {
            bool isSorted = true;

            // надо ли проверять?
            if (array.Length > 1)
            {
                // направление ?
                if (array[0] > array[array.Length - 1]) // если убывание элементов
                {
                    for (int i = 1; i < array.Length; i++)
                    {
                        if (array[i] > array[i - 1]) // следующий должен быть меньше или равен предыдущему
                            isSorted = false;
                    }
                }
                else // возрастание или равенство. равенство тоже надо проверить для случая массива из {1 2 1}
                {
                    for (int i = 1; i < array.Length; i++)
                    {
                        if (array[i] < array[i - 1]) // следующий должен быть больше или равен предыдущему
                            isSorted = false;
                    }
                }
            }
            else
                isSorted = true; // массив менее 2 элементов, сортировка не нужна 

            return isSorted;
        }

        public static int[] BucketSortArray(int[] array)
        {
            if (array.Length > 1)
            {
                int minimumInArray;
                int maximumInArray;
                int middleInArray;

                GetMinMaxFromArray(out minimumInArray, out maximumInArray, out middleInArray, array);

                Console.WriteLine($"\nmin={minimumInArray} max={maximumInArray} middle={middleInArray}");

                if (minimumInArray != maximumInArray)
                {
                    //our buckets
                    List<int>[] range = new List<int>[10];
                    range[0] = new List<int>();
                    range[1] = new List<int>();
                    range[2] = new List<int>();
                    range[3] = new List<int>();
                    range[4] = new List<int>();
                    range[5] = new List<int>();
                    range[6] = new List<int>();
                    range[7] = new List<int>();
                    range[8] = new List<int>();
                    range[9] = new List<int>();

                    int rangeSize = (middleInArray - minimumInArray) / 5;

                    if (rangeSize > 0)
                    {
                        Console.WriteLine("\nBucket range:");
                        Console.WriteLine($"rangeSize={rangeSize}.");
                        Console.Write($"{minimumInArray}/{minimumInArray + rangeSize} | ");
                        Console.Write($"{minimumInArray + rangeSize + 1}/{minimumInArray + (rangeSize * 2)} | ");
                        Console.Write($"{minimumInArray + (rangeSize * 2) + 1}/{minimumInArray + (rangeSize * 3)} | ");
                        Console.Write($"{minimumInArray + (rangeSize * 3) + 1}/{minimumInArray + (rangeSize * 4)} | ");
                        Console.Write($"{minimumInArray + (rangeSize * 4) + 1}/{middleInArray} | ");
                        Console.Write($"{middleInArray + 1}/{middleInArray + rangeSize} | ");
                        Console.Write($"{middleInArray + rangeSize + 1}/{middleInArray + (rangeSize * 2)} | ");
                        Console.Write($"{middleInArray + (rangeSize * 2) + 1}/{middleInArray + (rangeSize * 3)} | ");
                        Console.Write($"{middleInArray + (rangeSize * 3) + 1}/{middleInArray + (rangeSize * 4)} | ");
                        Console.Write($"{middleInArray + (rangeSize * 4) + 1}/{maximumInArray}");
                        Console.WriteLine();
                    }

                    foreach (int i in array)
                    {
                        if (rangeSize > 0)
                        {
                            if (i == minimumInArray || i <= minimumInArray + rangeSize)
                            {
                                range[0].Add(i);
                            }
                            else if (i == minimumInArray + rangeSize + 1 || i < minimumInArray + (rangeSize * 2))
                            {
                                range[1].Add(i);
                            }
                            else if (i == minimumInArray + (rangeSize * 2) + 1 || i < minimumInArray + (rangeSize * 3))
                            {
                                range[2].Add(i);
                            }
                            else if (i == minimumInArray + (rangeSize * 3) + 1 || i < minimumInArray + (rangeSize * 4))
                            {
                                range[3].Add(i);
                            }
                            else if (i == minimumInArray + (rangeSize * 4) + 1 || i < middleInArray)
                            {
                                range[4].Add(i);
                            }
                            else if (i == middleInArray + 1 || i < middleInArray + rangeSize)
                            {
                                range[5].Add(i);
                            }
                            else if (i == middleInArray + rangeSize + 1 || i < middleInArray + (rangeSize * 2))
                            {
                                range[6].Add(i);
                            }
                            else if (i == middleInArray + (rangeSize * 2) + 1 || i < middleInArray + (rangeSize * 3))
                            {
                                range[7].Add(i);
                            }
                            else if (i == middleInArray + (rangeSize * 3) + 1 || i < middleInArray + (rangeSize * 4))
                            {
                                range[8].Add(i);
                            }
                            else
                            {
                                range[9].Add(i);
                            }
                        }
                        else
                        {
                            if (i == minimumInArray)
                            {
                                range[0].Add(i);
                            }
                            else if (i == minimumInArray + 1)
                            {
                                range[1].Add(i);
                            }
                            else if (i == minimumInArray + 2)
                            {
                                range[2].Add(i);
                            }
                            else if (i == minimumInArray + 3)
                            {
                                range[3].Add(i);
                            }
                            else if (i == minimumInArray + 4)
                            {
                                range[4].Add(i);
                            }
                            else if (i == minimumInArray + 5)
                            {
                                range[5].Add(i);
                            }
                            else if (i == minimumInArray + 6)
                            {
                                range[6].Add(i);
                            }
                            else if (i == minimumInArray + 7)
                            {
                                range[7].Add(i);
                            }
                            else if (i == minimumInArray + 8)
                            {
                                range[8].Add(i);
                            }
                            else
                            {
                                range[9].Add(i);
                            }
                        }
                    }


                    Array.Clear(array, 0, array.Length);

                    int sortedCount = 0;//здесь счетчик уже записанных в финальный массив отсортированных данных

                    for (int i = 0; i < range.Length; i++)
                    {
                        Console.WriteLine($"Bucket {i} elements count = {range[i].Count}");
                        if (range[i].Count > 0)
                        {
                            FinalSortAndFillResultArray(array, range[i], sortedCount);
                            sortedCount = sortedCount + range[i].Count;
                        }
                    }
                }
                else // все числа одинаковы, нечего сортировать
                {
                    return array;
                }
            }
            else // размер меньше 2, нечего сортировать.
            {
                return array;
            }

            return array;
        }

        private static void FinalSortAndFillResultArray(int[] array, List<int> list, int sortedCount)
        {
            //list.Sort();

            int[] sorted = MergeSort(list.ToArray());//сортировка в отдельных корзинах

            for (int i = 0; i < sorted.Length; i++)
            {
                array[sortedCount + i] = sorted[i];
            }
        }

        static int[] MergeSort(int[] array)
        {
            return MergeSort(array, 0, array.Length - 1);
        }

        static int[] MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                if (highIndex - lowIndex == 1)
                {
                    if (array[highIndex] < array[lowIndex])
                    {
                        var t = array[lowIndex];
                        array[lowIndex] = array[highIndex];
                        array[highIndex] = t;
                    }
                }
                else
                {
                    var middleIndex = (lowIndex + highIndex) / 2;
                    MergeSort(array, lowIndex, middleIndex);
                    MergeSort(array, middleIndex + 1, highIndex);
                    Merge(array, lowIndex, middleIndex, highIndex);
                }
            }
            return array;
        }

        static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new int[highIndex - lowIndex + 1];
            var index = 0;
            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        private static void GetMinMaxFromArray(out int minimumInArray, out int maximumInArray, out int middleInArray, int[] array)
        {
            minimumInArray = array[0];
            maximumInArray = array[0];
            middleInArray = 0;

            foreach (int i in array)
            {
                if (i > maximumInArray)
                    maximumInArray = i;

                if (i < minimumInArray)
                    minimumInArray = i;
            }

            if (maximumInArray < 0) // все отрицательные
                middleInArray = ((minimumInArray + Math.Abs(maximumInArray)) / 2) + maximumInArray;
            else if (minimumInArray > 0) // все положительные
                middleInArray = ((maximumInArray - minimumInArray) / 2) + minimumInArray;
            else
                middleInArray = (minimumInArray + maximumInArray) / 2;
        }

        private static void ShowArray(int[] array)
        {
            foreach (int i in array)
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine();
        }

        private static int[] GenerateIntArray(int lenght)
        {
            int[] newArray = new int[lenght];
            Random rnd = new Random();

            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = rnd.Next(Int32.MinValue, Int32.MaxValue);
                //newArray[i] = rnd.Next(-100, -1);
            }

            return newArray;
        }
    }
}

