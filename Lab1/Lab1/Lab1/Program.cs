﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Lab1
{
    class TextGenerator
    {
        private List<char> RusAlphabet = new List<char>();
        private List<char> EngAlphabet = new List<char>();
        private List<char> UkrAlphabet = new List<char>();

        private void FillRusList()
        {
            for (char c = 'А'; c <= 'Я'; c++)
                RusAlphabet.Add(c);
            for (char c = 'а'; c <= 'я'; c++)
                RusAlphabet.Add(c);
            RusAlphabet.Add('Ё');
            RusAlphabet.Add('ё');
        }

        private void FillEngList()
        {
            for (char c = 'A'; c <= 'Z'; c++)
                EngAlphabet.Add(c);
            for (char c = 'a'; c <= 'z'; c++)
                EngAlphabet.Add(c);
        }

        private void FillUkrList()
        {
            for (char c = 'А'; c <= 'Я'; c++)
                UkrAlphabet.Add(c);
            for (char c = 'а'; c <= 'я'; c++)
                UkrAlphabet.Add(c);
            UkrAlphabet.Remove('Э');
            UkrAlphabet.Remove('э');
            UkrAlphabet.Remove('Ы');
            UkrAlphabet.Remove('ы');
            UkrAlphabet.Remove('Ъ');
            UkrAlphabet.Remove('ъ');
            UkrAlphabet.AddRange(new char[4]{ 'ї', 'ґ','є','і'});
            UkrAlphabet.AddRange(new char[4] { 'Ї', 'Ґ', 'Є', 'І' });
        }
        public void Initialize()
        {
            FillEngList();
            FillUkrList();
        }

        public string GenerateRusText(int size)
        {
            FillRusList();
            Random r = new Random();
            string res = "";
            for (int i = 0; i < size; i++)
                res += RusAlphabet[r.Next(0, 65)];
            return res;
        }
    }
    class TextAnalyzer
    {

    }
    class Program
    {
        public void LaunchPythonScript(string url, string source_language)
        {
            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            //python interpreter location
            start.FileName = @"/Users/qwysam/.pyenv/shims/python3";
            //argument with file name and input parameters
            start.Arguments = string.Format("{0} {1} {2}", @"/Users/qwysam/repos/Information_Theory/Information-Theory/Lab1/Lab1.py", url, source_language);
            start.UseShellExecute = false;// Do not use OS shell
            start.CreateNoWindow = true; // We don't need new window
            start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    Console.WriteLine("From System Diagnostics");
                    Console.WriteLine("Python: {0}", result);
                }
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            //p.LaunchPythonScript("https://masterimargo.ru/book-1.html", "ru");
            TextGenerator gen = new TextGenerator();
            gen.Initialize();
            Console.WriteLine(gen.GenerateRusText(100));
        }
    }
}