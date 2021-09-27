using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab1
{
    class TextGenerator
    {
        private List<char> RusAlphabet = new List<char>();
        private List<char> EngAlphabet = new List<char>();
        private List<char> UkrAlphabet = new List<char>();
        private void AddPunctuation(List<char> list)
        {
            list.AddRange(new char[] { ',', '.', '!', '?', (char)34, (char)39, ':', ';', '-', '(', ')', '№' });
        }
        private void FillRusList()
        {
            for (char c = 'А'; c <= 'Я'; c++)
                RusAlphabet.Add(c);
            for (char c = 'а'; c <= 'я'; c++)
                RusAlphabet.Add(c);
            RusAlphabet.Add('Ё');
            RusAlphabet.Add('ё');
            AddPunctuation(RusAlphabet);
        }

        private void FillEngList()
        {
            for (char c = 'A'; c <= 'Z'; c++)
                EngAlphabet.Add(c);
            for (char c = 'a'; c <= 'z'; c++)
                EngAlphabet.Add(c);
            AddPunctuation(EngAlphabet);
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
            AddPunctuation(UkrAlphabet);
        }

        public string GenerateRusText(int size)
        {
            FillRusList();
            int alphabet_size = RusAlphabet.Count - 1;
            Random r = new Random();
            string res = "";
            for (int i = 0; i < size; i++)
                res += RusAlphabet[r.Next(0, alphabet_size-1)];
            return res;
        }

        public string GenerateEngText(int size)
        {
            FillEngList();
            int alphabet_size = EngAlphabet.Count - 1;
            Random r = new Random();
            string res = "";
            for (int i = 0; i < size; i++)
                res += EngAlphabet[r.Next(0, alphabet_size)];
            return res;
        }

        public string GenerateUkrText(int size)
        {
            FillUkrList();
            int alphabet_size = UkrAlphabet.Count - 1;
            Random r = new Random();
            string res = "";
            for (int i = 0; i < size; i++)
                res += UkrAlphabet[r.Next(0, alphabet_size)];
            return res;
        }
    }
    class TextAnalyzer
    {
        public Dictionary<char, int> char_count = new Dictionary<char, int>();
        public Dictionary<char, double> char_frequency = new Dictionary<char, double>();
        public void FillDictionary(string text)
        {
            foreach(char c in text)
            {
                if (c != '\n')
                {
                    if (char_count.ContainsKey(c))
                        char_count[c]++;
                    else
                        char_count.Add(c, 1);
                }
            }
        }
        
        public void ConvertToFrequency(Dictionary<char, int> d,int text_size)
        {
            foreach(KeyValuePair<char,int> k_v in d)
            {
                char_frequency.Add(k_v.Key, (double)k_v.Value / text_size);
            }
        }
        public void OutputSortedFrequency(Dictionary<char, double> d)
        {
            var sortedDict = from entry in d orderby entry.Value ascending select entry;
            Console.WriteLine("Frequency list:");
            foreach (KeyValuePair<char, double> k_v in sortedDict)
                Console.WriteLine($"{k_v.Key}: {k_v.Value}");
        }
        public int NumberOfPossibleMessages(int text_length,Dictionary<char,int> char_count)
        {
            return (int)Math.Pow(char_count.Count, text_length);
        }
        public double QuantityOfInformation(int text_length, Dictionary<char, double> char_frequency)
        {
            double res = -text_length;
            double tmp = 0;
            foreach(KeyValuePair<char,double> k_v in char_frequency)
            {
                tmp += k_v.Value * Math.Log2(k_v.Value);
            }
            return Math.Ceiling(res * tmp);
        }
    }
    class Program
    {
        public string ReadString(string path)
        {
            return File.ReadAllText(path);
        }
        public bool CheckStringLength(string text)
        {
            if (text.Length > 1600)
                return false;
            else return true;
        }
        public string ShortenString(string text, int size)
        {
            if (text.Length > size)
                return text.Substring(0, size);
            else
                return text;
        }
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
            Console.WriteLine(gen.GenerateRusText(100));
            //use substring to decrease char count in string
            string text = p.ReadString("/Users/qwysam/repos/Information_Theory/Information-Theory/Lab1/rus.txt");
            if (!p.CheckStringLength(text))
            {
                text = p.ShortenString(text, 1600);
            }
            Console.WriteLine(text);
            TextAnalyzer textAnalyzer = new TextAnalyzer();
            textAnalyzer.FillDictionary(text);
            textAnalyzer.ConvertToFrequency(textAnalyzer.char_count, text.Length);
            textAnalyzer.OutputSortedFrequency(textAnalyzer.char_frequency);
        }
    }
}