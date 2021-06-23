using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // Given 2 Strings, find common characters between them and print in alphabatical order.
            // Find first duplicate character in a given string.
            Console.WriteLine("Find common characters between two strings and print in alphabatical order.");
            string str = "Weather";
            str.CommonCharacters("Temperature");
            Console.WriteLine("Find 1st duplicate character in a given string.");
            string firstDuplicateStr = "Praveen";
            firstDuplicateStr.PrintFirstDuplicate();
            Console.ReadLine();
        }

        public static void PrintFirstDuplicate(this string str)
        {
            char[] charArr = str.ToCharArray();
            bool isFound = false;

            for(int i=0; i<charArr.Length; i++)
            {
                char c1 = charArr[i];
                for(int j=i+1; j<charArr.Length;j++)
                {
                    if(charArr[j] == c1)
                    {
                        isFound = true;
                        Console.WriteLine(c1);
                        break;
                    }
                }

                if (isFound) break;
            }
        }

        public static void CommonCharacters(this string str1, string str2)
        {
            List<char> output = new List<char>();

            char[] arr1 = str1.ToCharArray();
            List<char> lstchar2 = new List<char>(str2.ToCharArray());

            for (int i = 0; i < arr1.Length; i++)
            {
                if (lstchar2.Contains(arr1[i]))
                {
                    output.Add(arr1[i]);
                    lstchar2.Remove(arr1[i]);
                }
            }

            output.Sort();
            foreach (char outchar in output)
            {
                Console.WriteLine(outchar);
            }
        }
    }
}
