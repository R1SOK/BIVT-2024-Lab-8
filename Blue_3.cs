using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    public class Blue_3 : Blue
    {
        private (char, double)[] _output;
        private (char, int)[] _countLetter;
        private int _num;

        public (char, double)[] Output
        {
            get
            {
                if (_output == null) return null;

                (char, double)[] copy = new (char, double)[_output.Length];
                Array.Copy(_output, copy, _output.Length);
                return copy;
            }
        }

        public Blue_3(string input) : base(input)
        {
            _output = null;  
            _countLetter = new (char, int)[0];
            _num = 0;
        }

        private void AddCount(char symb)
        {
            if (_countLetter == null) return;
            _num++;
            for (int i = 0; i < _countLetter.Length; i++)
            {
                (char letter, int count) = _countLetter[i];
                if (symb == letter)
                {
                    count++;
                    _countLetter[i] = (letter, count);
                    return;
                }
            }
            Array.Resize(ref _countLetter, _countLetter.Length + 1);
            _countLetter[_countLetter.Length - 1] = (symb, 1);
        }
        private int NextWord(string str, int indexofNull)
        {
            if (String.IsNullOrEmpty(str) || indexofNull < 0) return -1;

            int index = indexofNull + 1;
            while (index < str.Length)
            {
                if ((Char.IsLetter(str[index]) || str[index] == '\'') || str[index] == '-') 
                    index++;
                else break;
            }
            while (index < str.Length)
            {
                if (!Char.IsLetter(str[index])) index++;
                else break;
            }
            if (index == str.Length) return -1; 
            if (Char.IsDigit(str[index - 1]))
                index = NextWord(str, index);
            return index;
        }

        private void CountAllLetters()
        {
            if (String.IsNullOrEmpty(Input)) return;

            string inputToLower = Input.ToLower();
            int index = 0;
            if (!Char.IsLetter(inputToLower[index]))
                index = NextWord(inputToLower, index);
            if (index == -1) return;

            while (true)
            {
                char letter = inputToLower[index];
                AddCount(letter);

                index = NextWord(inputToLower, index);
                if (index == -1) return;
            }
        }
        private double LetterFrequency(int counter)
        {
            double rate = counter / (double)_num;
            return 100 * rate;
        }
        private void FindAllFrequency()
        {
            if (_countLetter == null) return;
            _output = new (char, double)[_countLetter.Length];

            int counter = 0;
            foreach ((char letter, int letterNum) in _countLetter)
            {
                double rate = LetterFrequency(letterNum);
                _output[counter] = (letter, rate);
                counter++;
            }
        }
        private void CountLetterSort()
        {
            if (_countLetter == null) return;

            int length = _countLetter.Length;
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {
                    (char beg, int begNum) = _countLetter[j];
                    (char next, int nextNum) = _countLetter[j + 1];
                    if (nextNum > begNum || (begNum == nextNum && next < beg))
                    {
                        _countLetter[j] = (next, nextNum);
                        _countLetter[j + 1] = (beg, begNum);
                    }
                }
            }
        }
        public override void Review()
        {
            CountAllLetters();
            CountLetterSort();
            FindAllFrequency();
        }
        private string FreqFormat(double input)
        {
            string answer = "";
            answer += $"{Math.Round(input, 4)}";
            if (input == 100)
            {
                answer += ",0000";
            }
            else if (input < 10)
            {
                if (answer.Length == 1) answer += ",";
                while (answer.Length < 6)
                {
                    answer += "0";
                }
            }
            else
            {
                if (answer.Length == 2) answer += ",";
                while (answer.Length < 7)
                {
                    answer += "0";
                }
            }
            return answer;
        }
        public override string ToString()
        {
            if (_output == null || _output.Length == 0) return null;
            string answer = "";
            foreach ((char letter, double sentense) in _output)
            {
                answer += $"{letter} - {FreqFormat(sentense)}\n";
            }
            return answer.Remove(answer.Length - 1, 1);
        }
    }
}