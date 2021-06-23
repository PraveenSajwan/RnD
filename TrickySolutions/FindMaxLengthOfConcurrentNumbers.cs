using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

//Find the maximum Length Of Concurrent Numbers in an Array
class Solution
{
    public static void Main(string[] args)
    {
        int[] a = { 1, 3, 4, 5, 6, 20, 19, 18, 17, 16, 8, 10, 1, 2, 3, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1 };
        int maxConcurrentlength = 0;
        for (int i = 0; i < a.Length; i++)
        {
            int concurrentLength = 1;
            for (int j = i + 1; j < a.Length; j++)
            {
                if (a[j] == a[j - 1] + 1 || a[j] == a[j - 1] - 1)
                {
                    if (j > 1 && j< a.Length && a[j] == a[j-2])
                    {
                        concurrentLength = 1;
                    }
                    concurrentLength++;
                }
                else
                {
                    break;
                }
            }

            if (concurrentLength > maxConcurrentlength)
            {
                maxConcurrentlength = concurrentLength;
            }
        }

        Console.WriteLine($"Maximum Concurrent Length {maxConcurrentlength}");
        Console.Read();
    }
}
