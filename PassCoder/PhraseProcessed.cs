using System;
using System.Text;

namespace PassCoder
{
    public class PhraseProcessed
    {
        public readonly string _name;
        public readonly uint _lenght;
        public readonly bool _even;

        public readonly uint _weightOriginal;
        public readonly uint _weightBlended;

        public readonly uint _offset;

        public readonly ValuedSymboleList _blendedSymboleList = new ValuedSymboleList(Constante.AllSymbole);

        public StringBuilder _base = new StringBuilder();

        public UniqueSymbolePool _poolLetter = new UniqueSymbolePool(Constante.LetterSymbole);
        public UniqueSymbolePool _poolFigure = new UniqueSymbolePool(Constante.FigureSymbole);

        public PhraseProcessed(string name)
        {
            Console.WriteLine();
            Console.WriteLine("Information :");
            Console.WriteLine("----------------------------------");
            // Set Value
            _name = name;
            _lenght = (uint)name.Length;
            Console.WriteLine("Length : " + _lenght);
            _even = _lenght % 2 == 0;
            Console.WriteLine("Even : " + (_even ? "true" : "false"));

            // Generate SymboleBlend
            // Seed create
            var value = (uint)((_even ? 7 : 12) * _lenght);
            for (var i = 0; i < _lenght; i++)
            {
                value += Constante.AllSymbole.Symbole2Value(_name[i]);
            }
            value *= Constante.AllSymbole.Length();

            // Randomize create
            Chaos chaos = new Chaos(value);

            // Randomize
            _blendedSymboleList.Randomize(chaos);

            Console.WriteLine("Origi : " + Constante.AllSymbole.List);
            Console.WriteLine("Randomize : " + _blendedSymboleList.List);

            // Calculate WeightOriginal
            _weightOriginal = 0;
            for (int i = 0; i < _lenght; i++)
                _weightOriginal += Constante.AllSymbole.Symbole2Value(name[i]);
            _weightOriginal *= _lenght;
            Console.WriteLine("WeightOriginal : " + _weightOriginal);

            // Calculate WeightBlended
            _weightBlended = 0;
            for (int i = 0; i < _lenght; i++)
                _weightBlended += _blendedSymboleList.Symbole2Value(name[i]);
            _weightBlended *= _lenght;
            Console.WriteLine("WeightBlended : " + _weightBlended);

            // Calculate Offset
            _offset = (_weightOriginal * _weightBlended - _lenght - _weightBlended) % Constante.AllSymbole.Length();
            Console.WriteLine("Offset : " + _offset);

            // Base String Calculate
            
            ProcessBase();

            Console.WriteLine("Base : " + _base);

            //
            Console.WriteLine();
        }

        private void ProcessBase()
        {
            string[] words = SplitWords();

            for (int i = 0; i < words.Length; i++)
            {
                char to_add;
                StringBuilder tempo = new StringBuilder("");
                int size = words[i].Length;
                if (char.IsLetter(words[i][0]))
                {
                    uint valueOffseted = Constante.LetterSymbole.Symbole2Value(words[i][0]);
                    valueOffseted += _offset;
                    if (valueOffseted >= Constante.LetterSymbole.Length())
                        valueOffseted -= (uint) Constante.LetterSymbole.Length();
                    char charOffseted = Constante.LetterSymbole.Value2Symbole(valueOffseted);

                    to_add = _poolLetter.TryAdding(charOffseted);
                    if(to_add != '\0')
                        tempo.Append(to_add);
                }
                to_add = _poolFigure.TryAdding((words[i].Length%10).ToString()[0]);
                if(to_add != '\0')
                    tempo.Append(to_add);
                _base.Append(tempo);
            }
           
        }

        private string[] SplitWords()
        {
            string[] words = _name.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(i + " : " + words[i]);
            }

            return words;
        }
    }
}