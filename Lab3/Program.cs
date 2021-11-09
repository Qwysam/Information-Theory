using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab3
{
    class IndexedDoubleStorage
    {
        public int index;
        public double value;
        public IndexedDoubleStorage(int index, double value)
        {
            this.index = index;
            this.value = value;
        }
    }
    class IndexedStringStorage
    {
        public int index;
        public string value;
        public IndexedStringStorage(int index, string value)
        {
            this.index = index;
            this.value = value;
        }
    }
    class A
    {

        public IndexedDoubleStorage[] P1;

        public IndexedStringStorage[] Res = new IndexedStringStorage[13];

        double schet1 = 0;
        double schet2 = 0;

        public void Sort()
        {
            for (int i = 0; i < P1.Length; i++)
            {
                for (int j = 0; j < P1.Length - i - 1; j++)
                {
                    if (P1[j].value < P1[j + 1].value)
                    {
                        IndexedDoubleStorage temp1 = new IndexedDoubleStorage(P1[j].index,P1[j].value);
                        P1[j] = P1[j + 1];
                        P1[j + 1] = temp1;

                    }
                }
            }
            for (int i = 0; i < P1.Length; i++)
                Res[i] = new IndexedStringStorage(P1[i].index, "");

        }
        int m;

        public int Delenie_Posledovatelnosty(int L, int R)
        {

            schet1 = 0;
            for (int i = L; i <= R - 1; i++)
            {
                schet1 = schet1 + P1[i].value;
            }

            schet2 = P1[R].value;
            m = R;
            while (schet1 >= schet2)
            {
                m = m - 1;
                schet1 = schet1 - P1[m].value;
                schet2 = schet2 + P1[m].value;
            }
            return m;

        }

        public void Fano(int L, int R)
        {
            int n;

            if (L < R)
            {

                n = Delenie_Posledovatelnosty(L, R);
                for (int i = L; i <= R; i++)
                {
                    if (i <= n)
                    {
                        Res[i].value += Convert.ToByte(0);
                    }
                    else
                    {
                        Res[i].value += Convert.ToByte(1);
                    }
                }

                Fano1(L, n);

                Fano(n + 1, R);

            }

        }

        public void Fano1(int L, int R)
        {
            int n;

            if (L < R)
            {

                n = Delenie_Posledovatelnosty(L, R);
                for (int i = L; i <= R; i++)
                {
                    if (i <= n)
                    {
                        Res[i].value += Convert.ToByte(0);
                    }
                    else
                    {
                        Res[i].value += Convert.ToByte(1);
                    }
                }

                Fano(L, n);

                Fano1(n + 1, R);

            }

        }
    }
    class Program
    {
        static double[] GenerateVector(int size)
        {
            double[] vector = new double[size];
            Random r = new Random();
            for (int i = 0; i < size; i++)
                vector[i] = Math.Round(r.NextDouble() / size, 3);
            return vector;
        }
        public IndexedDoubleStorage[] FromDoubleArr(double[] arr)
        {
            IndexedDoubleStorage[] res = new IndexedDoubleStorage[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                res[i] = new IndexedDoubleStorage(i, arr[i]);
            return res;
        }
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
            int k = 3;
            Console.WriteLine($"K = {k}");
            double[] arr1 = p.GenerateEqualArray(20 + k);
            Console.WriteLine("First array: ");
            p.PrintArray(arr1);
            double Hx1 = p.HX(20 + k);
            Console.WriteLine($"H(max): {Hx1} bit/symbol");
            double[] arr2 = p.GenerateDescendingArray(20 + k);
            Console.WriteLine("Second array: ");
            p.PrintArray(arr2);
            double Hx2 = p.Hx2(arr2);
            Console.WriteLine($"H(x2): {Hx2} bit/symbol");
            double dD = Hx1 - Hx2;
            Console.WriteLine($"dD : {dD} bit/symbol");
            double[] arr3 = p.GenerateEqualArray(10 + k);
            Console.WriteLine("Third array: ");
            p.PrintArray(arr3);
            double Hx3 = p.HX(10 + k);
            Console.WriteLine($"H(max2): {Hx3} bit/symbol");
            double Transfer_Speed_arr3 = p.TransferSpeed(Hx3, arr3);
            Console.WriteLine($"Transfer Speed: {Transfer_Speed_arr3} bit/second");
            double[] arr4 = p.GenerateDescendingArray(10 + k);
            Console.WriteLine("Fourth array: ");
            p.PrintArray(arr4);
            double Hx4 = p.Hx2(arr4);
            Console.WriteLine($"H(x2): {Hx4} bit/symbol");
            double Transfer_Speed_arr4 = p.TransferSpeed(Hx4, arr4);
            Console.WriteLine($"Transfer Speed: {Transfer_Speed_arr4} bit/second");
            double[] arr5 = GenerateVector(13);
            Console.WriteLine("Fifth array:");
            p.PrintArray(arr5);
            IndexedDoubleStorage[] arr = p.FromDoubleArr(arr5);
            A tmp = new A();
            tmp.P1 = arr;
            tmp.Sort();
            tmp.Fano(0, 12);
            Console.WriteLine("Кодирование методом Шеннона-Фано:");
            foreach(IndexedStringStorage elem in tmp.Res)
            {
                Console.WriteLine($"{arr5[elem.index]} :\t{elem.value}");
            }
        }
    }
}
