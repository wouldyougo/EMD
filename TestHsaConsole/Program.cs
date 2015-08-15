using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using EmdClassLibrary.Transform;
//using Accord.Math;

namespace TestConsoleApplication
{

    class Program
    {
        /// <summary>
        /// тестируем передачу параметров
        /// </summary>
        static void Main()
        {
            int[] arr = { 1, 4, 5 };
            System.Console.WriteLine("Main, {0}, {1}", arr[0], arr[1]);

            Change(arr);
            System.Console.WriteLine("Change1, {0}, {1}", arr[0], arr[1]);

            arr = new int[] { 1, 4, 5 };
            Change2(arr);
            System.Console.WriteLine("Change2, {0}, {1}", arr[0], arr[1]);
            System.Console.ReadKey();

            arr = new int[]{ 1, 4, 5 };
            Change3(arr);
            System.Console.WriteLine("Change3, {0}, {1}", arr[0], arr[1]);
            System.Console.ReadKey();

            List<double> list1 = new List<double> { 3, 2, 1 };

            double value = 78;
            for (int i = 0; i < list1.Count; i++ )
            {
                list1[i] = list1[i] + value;
            }
            System.Console.WriteLine("list, {0}, {1}, {2}", list1[0], list1[1], list1[2]);
            System.Console.ReadKey();

            System.Console.WriteLine("double.MinValue, {0}", double.MinValue);
            System.Console.ReadKey();
            /*
            Main, 1, 4
            Inside the method, the first element is: -3, -1
            Change1, 888, 4
             * 
            Inside the method, the first element is: 2, 4
            Inside the method, the first element is: 2, 4
            Change2, 2, 4
             * 
            Inside the method, the first element is: 2, 4
            Inside the method, the first element is: 1, 4
            Change3, 1, 4
             * 
             list, 81, 80, 79
             double.MinValue, -1,79769313486232E+308
                        */
        }

        static void Change(int[] pArray)
        {
            pArray[0] = 888;  // This change affects the original element.
            pArray = new int[5] { -3, -1, -2, -3, -4 };   // This change is local.
            System.Console.WriteLine("Inside the method, the first element is: {0}, {1}", pArray[0], pArray[1]);
        }

        static void Change2(int[] pArray)
        {
            int[] p2 = pArray;

            p2[0] = 2;
            p2[1] = 4;
            System.Console.WriteLine("Inside the method, the first element is: {0}, {1}", p2[0], p2[1]);
            System.Console.WriteLine("Inside the method, the first element is: {0}, {1}", pArray[0], pArray[1]);
        }

        static void Change3(int[] pArray)
        {
            int[] p2;

            p2 = (int[])pArray.Clone();
            p2[0] = 2;
            p2[1] = 4;
            System.Console.WriteLine("Inside the method, the first element is: {0}, {1}", p2[0], p2[1]);
            System.Console.WriteLine("Inside the method, the first element is: {0}, {1}", pArray[0], pArray[1]);
        }
    }
}
