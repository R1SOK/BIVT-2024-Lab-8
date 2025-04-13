using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    public class Blue_2 : Blue
    {
        private string _sequence;
        private string _output;
        public string Output => _output;

        public Blue_2(string input, string sequence) : base(input)
        {
            _sequence = sequence;
            _output = null;
        }

        private (int, int) FindWord(string str, int index)
        {
            if (String.IsNullOrEmpty(str) || index < 0 || index > str.Length - 1) return (-1, str.Length);

            int start = index;
            while (start < str.Length && !Char.IsLetter(str[start]))
            {  
                start++; 
            }
            if (start == str.Length) return (-1, str.Length);

            int end = start;
            while (end < str.Length && (Char.IsLetter(str[end]) || str[end] == '\'') || str[end] == '-')
            {
                end++;
            }
            end--;
            return (start, end);
        }
        private (int, int) FindWordWithSeq(string str, string sequense)
        {
            if (String.IsNullOrEmpty(str) || String.IsNullOrEmpty(sequense)) return (-1, 0);

            (int start, int end) = FindWord(str, 0);

            while (end < str.Length)  
            {
                // Вырезаем текущее "слово" (подстроку от start до end включительно)
                string word = str.Substring(start, end - start + 1);
                if (word.Contains(sequense)) return (start, end);
                (start, end) = FindWord(str, end + 1);
            }
            return (start, end);
        }
        private string DeleteWord(string str, int startI, int endI)
        {
            if (startI < 0 || endI > str.Length - 1) return str;
            return str.Remove(startI, endI - startI + 1);
        }
        public override void Review()
        {
            _output = Input;

            while (true)
            {
                (int start, int end) = FindWordWithSeq(_output, _sequence);
                if (start == -1) break;

                _output = DeleteWord(_output, start, end);
                if (start == 0)
                {
                    if (Char.IsWhiteSpace(_output[0]))
                    {
                        _output = _output.Remove(0, 1);
                    }
                }
                else
                {
                    if (Char.IsWhiteSpace(_output[start - 1]))
                    {
                        _output = _output.Remove(start - 1, 1);
                    }
                }
            }
        }
        public override string ToString()
        {
            if (String.IsNullOrEmpty(Output)) return null;
            return Output;
        }
    }
}