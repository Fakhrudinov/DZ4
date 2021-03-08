using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Algo_DZ4_HashSetSearch
{
    public class BechmarkClass
    {
        private string[] arrayStrings;
        private HashSet<MyStringHashOverraide> hashSet;

        string first = "First String";
        string middle = "Middle String";
        string end = "Last String";

        public BechmarkClass()
        {
            arrayStrings = new string[10_000];
            hashSet = new HashSet<MyStringHashOverraide>();

            //заполнение заготовленными значениями
            SetToHash(first);
            arrayStrings[0] = first;

            //заполнение случайными значениями
            for (int i = 1; i < arrayStrings.Length - 1; i++)
            {
                string randomString = CreateSomeString();

                if (i == 5000)
                    randomString = middle; //заполнение заготовленными значениями    

                SetToHash(randomString);
                arrayStrings[i] = randomString;
            }

            //заполнение заготовленными значениями
            SetToHash(end);
            arrayStrings[9_999] = end;
        }

        private void SetToHash(string str)
        {
            var strHash = new MyStringHashOverraide() { SingleString = str };
            hashSet.Add(strHash);
        }

        private string CreateSomeString()
        {
            string result = "";

            Random random = new Random();
            string charsCollection = "ABCDEFGHIJKL MNOPQRSTUVWXYZ abcdefghijklm nopqrstuvwxyz 0123456789";

            byte newStrLenght = (byte)random.Next(10, 50);

            for (int i = 0; i < newStrLenght; i++)
            {
                result = result + charsCollection[random.Next(charsCollection.Length)];
            }

            return result;
        }

        private bool GetInfoFromHashSet(MyStringHashOverraide newString)
        {
            bool isPresent = hashSet.Contains(newString);
            return isPresent;
        }

        private bool GetInfoFromArray(string searchString)
        {
            bool isPresent = false;

            foreach (string str in arrayStrings)
            {
                if (str.Contains(searchString))
                {
                    isPresent = true;
                    break;
                }
            }

            return isPresent;
        }

        [Params("First String", "Middle String", "Last String", "String not exist")]
        public string str { get; set; }

        [Benchmark]
        public void SearchInHashSet()
        {
            var searchString = new MyStringHashOverraide() { SingleString = str };

            GetInfoFromHashSet(searchString);
        }

        [Benchmark]
        public void SearchInArray()
        {
            string searchString = str;

            GetInfoFromArray(searchString);
        }
    }
}
