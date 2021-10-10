using System;
using System.Linq;

public class InfoTheary_Lab2
{
    static double[] GenerateVector(int size)
    {
        double[] vector = new double[size];
        Random r = new Random();
            for(int i = 0;i<size;i++)
                vector[i] = Math.Round(r.NextDouble()/size,3);
        return vector;
    }
    //fills the matrix to it's expected sum of elements
    static void FillToSum(double[] arr,double expected_sum)
    {
        double actual_sum = arr.Sum();
        if (actual_sum < expected_sum)
        {
            Random r = new Random();
            arr[r.Next(0, arr.Length)] += expected_sum - actual_sum;
        }

    }
    static double[,] VectorToMatrix(double[]flat,int height, int width)
    {
        if (flat.Length != width * height)
        {
            throw new ArgumentException("Invalid length");
        }
        double[,] res = new double[width, height];
        // BlockCopy uses byte lengths: a double is 8 bytes
        Buffer.BlockCopy(flat, 0, res, 0, flat.Length * sizeof(double));
        return res;

    }
    static double Sum(double[,]matrix)
    {
        double sum = 0;
        foreach (double elem in matrix)
            sum += elem;
        return sum;
    }
    static double[] SumHorizontal(double[,] matrix, int width)
    {
        double[] Px = new double[width];
        for (int i = 0; i < width; i++)
        {
            double sum = 0;
            for (int j = 0; j < width; j++)
            {
                sum += matrix[i, j];
            }
            Px[i] = sum;
        }
        return Px;
    }
    static double[] SumVertical(double[,] matrix, int width)
    {
        double[] Py = new double[width];
        for (int i = 0; i < width; i++)
        {
            double sum = 0;
            for (int j = 0; j < width; j++)
            {
                sum += matrix[j, i];
            }
            Py[i] = sum;
        }
        return Py;
    }
    static double[,] GetPyx(double[,] matrix, double[] Px)
    {
        int length = Px.Length;
        double[,] Pyx = new double[length, length];
        for (int i = 0; i < length; i++)
            for (int j = 0; j < length; j++)
                Pyx[j, i] = matrix[i, j] / Px[i];
        return Pyx;
    }

    static double[,] GetPxy(double[,] matrix, double[] Py)
    {
        int length = Py.Length;
        double[,] Pxy = new double[length, length];
        for (int i = 0; i < length; i++)
            for (int j = 0; j < length; j++)
                Pxy[i, j] = matrix[i, j] / Py[j];
        return Pxy;
    }
    static string VectorToString<T>(T[] vector)
    {
        string res = "";
        foreach (T elem in vector)
            res += $"{elem}\t";
        return res.Trim();
    }

    static string SquareMatrixToString<T>(T[,] matrix,int length)
    {
        string res = "";
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
                res += $"{matrix[i,j]}\t";
            res += "\n";
        }
        return res.Trim();
    }
    static void RoundUp(double[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
            arr[i] = Math.Round(arr[i],3);
    }

    static void RoundUp(double[,] arr,int length)
    {
        for (int i = 0; i < length; i++)
            for(int j = 0;j<length;j++)
                arr[i,j] = Math.Round(arr[i,j], 3);
    }

    static double CountHx(double[] Px)
    {
        double res = 0;
        foreach (double elem in Px)
            res += elem * Math.Log2(elem);
        return res*-1;
    }

    static double CountHy(double[] Py)
    {
        double res = 0;
        foreach (double elem in Py)
            res += elem * Math.Log2(elem);
        return res*-1 ;
    }

    static double CountHxAndy(double[,] matrix)
    {
        double res = 0;
        foreach (double elem in matrix)
        {
            double tmp = elem * Math.Log2(elem);
            if(!double.IsNaN(tmp))
            res += tmp;
        }
        return res*-1;
    }

    static void Main(string[] args)
    {
        double[] test = GenerateVector(11*11);
        FillToSum(test, 1);
        double[,] matrix = VectorToMatrix(test,11,11);
        RoundUp(matrix, 11);
        Console.WriteLine("P(x,y):\n"+SquareMatrixToString(matrix,11)+"\n");
        double[] Px = SumHorizontal(matrix, 11);
        RoundUp(Px);
        Console.WriteLine("P(x):\n"+VectorToString(Px)+"\n");
        double[] Py = SumVertical(matrix, 11);
        RoundUp(Py);
        Console.WriteLine("P(y):\n"+VectorToString(Py)+"\n");
        double[,] Pyx = GetPyx(matrix, Px);
        RoundUp(Pyx,11);
        Console.WriteLine("P(y/x):\n"+SquareMatrixToString(Pyx, 11) + "\n");
        double[,] Pxy = GetPxy(matrix, Py);
        RoundUp(Pxy,11);
        Console.WriteLine("P(x/y):\n"+SquareMatrixToString(Pxy, 11));
        double Hx = CountHx(Px);
        Console.WriteLine("\nH(x) = " + Math.Round(Hx,3) + " bit\n");
        double Hy = CountHy(Py);
        Console.WriteLine("H(y) = " + Math.Round(Hy,3) + " bit\n");
        double Hx_y = CountHxAndy(matrix);
        Console.WriteLine("H(x,y) = " + Math.Round(Hx_y,3) + " bit\n");
        double Hyx = Hx_y - Hx;
        Console.WriteLine("H(y/x) = " + Math.Round(Hyx,3) + " bit\n");
        double Hxy = Hx_y - Hy;
        Console.WriteLine("H(x/y) = " + Math.Round(Hxy,3)+" bit");
    }
}
