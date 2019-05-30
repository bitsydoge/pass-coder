using System;
using System.Text;

namespace PassCoder
{
    public class PhraseProcessed
    {
        private readonly string _name;
        private readonly int _lenght;
        private readonly bool _even;

        private readonly int _weightOriginal;
        private readonly int _weightBlended;

        private readonly uint _offset;

        private readonly StringBuilder _blendedSymboleList = new StringBuilder(Constante.AllSymboleList);

        private StringBuilder _base = new StringBuilder();
        private StringBuilder _baseOffseted = new StringBuilder();

        private Chaos _chaos;

        private char[] usedLetter = new char[26+26];
        private char[] usedSymbole = new char[Constante.SpecialSymbole.Length];
        private char[] usedFigure = new char[9];
        public PhraseProcessed(string name)
        {
            Console.WriteLine();
            Console.WriteLine("Information :");
            Console.WriteLine("----------------------------------");
            // Set Value
            _name = name;
            _lenght = name.Length;
            Console.WriteLine("Lenght : " + _lenght);
            _even = _lenght % 2 == 0;
            Console.WriteLine("Even : " + (_even ? "true" : "false"));

            // Generate SymboleBlend
            BlendAllSymbole();
            Console.WriteLine("Origi : " + Constante.AllSymboleList);
            Console.WriteLine("Blend : " + _blendedSymboleList);

            // Calculate WeightOriginal
            _weightOriginal = 0;
            for (int i = 0; i < _lenght; i++)
                _weightOriginal += Constante.SymboleValue(_name[i]);
            _weightOriginal *= _lenght;
            Console.WriteLine("WeightOriginal : " + _weightOriginal);

            // Calculate WeightBlended
            _weightBlended = 0;
            for (int i = 0; i < _lenght; i++)
                _weightBlended += SymboleValue(_name[i]);
            _weightBlended *= _lenght;
            Console.WriteLine("WeightBlended : " + _weightBlended);

            // Calculate Offset
            _offset = (uint)(_weightOriginal * _weightBlended - _lenght - _weightBlended) % (uint)Constante.AllSymboleList.Length;
            Console.WriteLine("Offset : " + _offset);

            // Base String Calculate
            BaseStringCreate();

            //
            Console.WriteLine();
        }

        private void BlendAllSymbole()
        {
            // Seed create
            var value = (uint)((_even ? 7 : 12) * _lenght);
            for (var i = 0; i < _lenght; i++)
            {
                value += (uint)Constante.SymboleValue(_name[i]);
            }
            value *= (uint)Constante.AllSymboleList.Length;

            // Randomize create
            _chaos = new Chaos(value);

            // "Random" swap
            for (int i = 0; i < Constante.AllSymboleList.Length; i++)
            {
                var rand = (int)(_chaos.Rand() % Constante.AllSymboleList.Length);
                var temp = _blendedSymboleList[i];
                _blendedSymboleList[i] = _blendedSymboleList[rand];
                _blendedSymboleList[rand] = temp;
            }
        }

        private int SymboleValue(char character)
        {
            if (character <= 0) throw new ArgumentOutOfRangeException(nameof(character));
            for (int i = 0; i < _blendedSymboleList.Length; i++)
            {
                if (character == _blendedSymboleList[i])
                {
                    return i;
                }
            }
            return _blendedSymboleList.Length;
        }

        private void BaseStringCreate()
        {
            string[] words = _name.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(i + " : " + words[i]);
            }
        }

        private char GetAvaibleLetterBase(char character)
        {
            char returned = character;
            for(int i = 0; i < usedLetter.Length; i++)
            {
                if (usedLetter[i] == character)
                {
                    
                }
            }
            return returned;
        }
    }
}