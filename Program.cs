using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab3
{
    class Program
    {
        public void PrintArray(double[] arr)
        {
            foreach (double elem in arr)
                Console.Write(elem + " ");
            Console.WriteLine();
        }
        public void ArrayRound(double[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = Math.Round(arr[i], 7);
        }
        public double[] GenerateEqualArray(int size)
        {
            double[] arr = new double[size];
            for(int i =0;i<size;i++)
                arr[i] = (double)1 / size;
            ArrayRound(arr);
            return arr;
        }
        public double[] GenerateDescendingArray(int size)
        {
            double[] arr = new double[size];
            for (int i = 1; i < size; i++)
                arr[i-1] = Math.Pow(0.5,i);
            arr[size - 1] = arr[size - 2];
            ArrayRound(arr);
            return arr;
        }
        public double HX(int size)
        {
            double tmp = (double)1 / size;
            return Math.Round(Math.Log2(tmp)*-1,4);
        }
        public double Hx2(double[] second_arr)
        {
            double res = 0;
            foreach (double elem in second_arr)
                res += elem * Math.Log2(elem);
            return Math.Round(res*-1,4);
        }
        public double TransferSpeed(double Hx, double[] arr)
        {
            double s = 0;
            for (int i = 0; i < arr.Length; i++)
                s += (i + 1) * arr[i];
            return Hx / s;
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            double[] arr1 = p.GenerateEqualArray(23);
            Console.WriteLine("First array: ");
            p.PrintArray(arr1);
            double Hx1 = p.HX(23);
            Console.WriteLine($"H(max): {Hx1} bit/symbol");
            double[] arr2 = p.GenerateDescendingArray(23);
            Console.WriteLine("Second array: ");
            p.PrintArray(arr2);
            double Hx2 = p.Hx2(arr2);
            Console.WriteLine($"H(x2): {Hx2} bit/symbol");
            double dD = Hx1 - Hx2;
            Console.WriteLine($"dD : {dD} bit/symbol");
            double[] arr3 = p.GenerateEqualArray(13);
            Console.WriteLine("Third array: ");
            p.PrintArray(arr3);
            double Hx3 = p.HX(13);
            Console.WriteLine($"H(max2): {Hx3} bit/symbol");
            double Transfer_Speed_arr3 = p.TransferSpeed(Hx3, arr3);
            Console.WriteLine($"Transfer Speed: {Transfer_Speed_arr3} bit/second");
            double[] arr4 = p.GenerateDescendingArray(13);
            Console.WriteLine("Fourth array: ");
            p.PrintArray(arr4);
            double Hx4 = p.Hx2(arr4);
            Console.WriteLine($"H(x2): {Hx4} bit/symbol");
            double Transfer_Speed_arr4 = p.TransferSpeed(Hx4, arr4);
            Console.WriteLine($"Transfer Speed: {Transfer_Speed_arr4} bit/second");
        }
    }
}