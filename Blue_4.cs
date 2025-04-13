using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    public class Blue_4 : Blue
    {
        private int _output;
        public int Output => _output;
        public Blue_4(string input) : base(input) { _output = 0; }

        private string FindNextNumber(string str, ref int startIndex)
        {
            if (String.IsNullOrEmpty(str) || startIndex < 0 || startIndex > str.Length - 1) return null;

            string number = "";
            while (startIndex < str.Length && !Char.IsDigit(str[startIndex])) 
            {
                startIndex++;
            }
            if (startIndex == str.Length) return null; 

            if (startIndex != 0 && str[startIndex - 1] == '-') number += '-';
            else number += '+';

            while (startIndex < str.Length && Char.IsDigit(str[startIndex])) 
            {
                number += str[startIndex];
                startIndex++;
            }

            return number;
        }
        private int Signum(char signum)
        {
            switch (signum)
            {
                case '+': return 1;
                case '-': return -1;
                default: return 0;
            }
        }
        private double ToInt(string input)
        {
            if (String.IsNullOrEmpty(input)) return double.NaN;

            double output = 0;

            int digitIndex = input.Length - 1;
            for (int i = 1; i < input.Length; i++)
            {
                int number = (int)input[i] - (int)'0';
                output += number * Math.Pow(10, (digitIndex - i));
            }
            output *= Signum(input[0]);
            return output;
        }
        private void AddNumber(double number)
        {
            if (number == double.NaN) return;
            _output += (int)number;
        }
        public override void Review()
        {
            if (String.IsNullOrEmpty(Input)) return;

            int i = 0;
            while (i < Input.Length)
            {
                string strNumber = FindNextNumber(Input, ref i);
                if (strNumber == null) return;
                AddNumber(ToInt(strNumber));
            }
        }

        public override string ToString()
        {
            string output = $"{_output}";
            return output;
        }
    }
}