using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8
{
    public class Blue_1 : Blue
    {
        private string[] _output;
        public string[] Output => _output;
        public Blue_1(string input) : base(input)
        {
            _output = null;
        }

        private void AddToOutput(string str)
        {
            if (_output == null) _output = new string[1];
            else Array.Resize(ref _output, _output.Length + 1);

            _output[_output.Length - 1] = str;
        }

        private string SplitOne(string str)
        {
            if (String.IsNullOrEmpty(str)) return null;

            if (str.Length < 50)
            {
                AddToOutput(str);
                return null;
            }
            int counter = 50;

            while (!Char.IsWhiteSpace(str[counter])) // Идем назад от 50-го символа, пока не найдем пробельный символ
            {
                counter--;
            }

            char[] resultArray = new char[counter];
            char[] copy = str.ToCharArray();
            Array.Copy(copy, resultArray, counter);
            string res = new string(resultArray);

            AddToOutput(res);

            str = str.Remove(0, counter + 1);

            return str;
        }

        // Метод для обработки входной строки (разбивка на части)
        public override void Review()
        {
            string temp = Input;

            while (!String.IsNullOrEmpty(temp))
            {
                temp = SplitOne(temp);
            }
        }

        public override string ToString()
        {
            if (_output == null) return null;

            string log = "";

            for (int i = 0; i < _output.Length; i++)
            {
                log += $"{_output[i]}\n";
            }
            log = log.Remove(log.Length - 1, 1);

            return log;
        }
    }
}