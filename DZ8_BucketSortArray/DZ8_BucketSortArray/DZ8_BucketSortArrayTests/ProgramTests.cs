using Microsoft.VisualStudio.TestTools.UnitTesting;
using DZ8_BucketSortArray;
using System;
using System.Collections.Generic;
using System.Text;

namespace DZ8_BucketSortArray.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void BucketSortArrayTest_123AllPositive()
        {
            int[] unsorted = new int[3] { 2, 1, 3 };
            int[] expected = new int[3] { 1, 2, 3 };
            int[] actual = Program.BucketSortArray(unsorted);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BucketSortArrayTest_123AllNegative()
        {
            int[] unsorted = new int[3] { -2, -1, -3 };
            int[] expected = new int[3] { -3, -2, -1 };
            int[] actual = Program.BucketSortArray(unsorted);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BucketSortArrayTest_111AllSame()
        {
            int[] unsorted = new int[3] { 1, 1, 1 };
            int[] expected = new int[3] { 1, 1, 1 };
            int[] actual = Program.BucketSortArray(unsorted);

            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void BucketSortArrayTest_21012()
        {
            int[] unsorted = new int[5] { 2, 0, 1, -2, -1 };
            int[] expected = new int[5] { -2, -1, 0, 1, 2 };
            int[] actual = Program.BucketSortArray(unsorted);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BucketSortArrayTest_BigNumbersAllNegative()
        {
            int[] unsorted = new int[5] { -428798477, -687682423, -895436127, -149586456, -301982753 };
            int[] expected = new int[5] { -895436127, -687682423, -428798477, -301982753, -149586456 };
            int[] actual = Program.BucketSortArray(unsorted);

            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void BucketSortArrayTest_BigNumbersAllPositive()
        {
            int[] unsorted = new int[5] { 428798477, 1587682423, 229543423, 1456234124, 434566889 };
            int[] expected = new int[5] { 229543423, 428798477, 434566889, 1456234124, 1587682423 };
            int[] actual = Program.BucketSortArray(unsorted);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}