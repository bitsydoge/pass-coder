using System;
using System.Text;

namespace PassCoder
{
	public class PhraseProcessed
	{
		public string _name;
		public uint _lenght;
		public bool _even;
		public uint _weightOriginal;
		public uint _weightBlended;
		public uint _offset;
		public bool _debug;
		public ValuedSymboleList _blendedSymboleList = new ValuedSymboleList(Constant.AllSymbole);
		public StringBuilder _base = new StringBuilder();
		public UniqueSymbolePool _poolLetter = new UniqueSymbolePool(Constant.LetterSymbole);
		public UniqueSymbolePool _poolFigure = new UniqueSymbolePool(Constant.FigureSymbole);

		public PhraseProcessed(string name, bool debug)
		{
			// Set Value
			_name = name;
			_lenght = (uint)name.Length;
			_even = _lenght % 2 == 0;
			_debug = debug;

			// Generate SymboleBlend
			// Seed create
			var value = (uint)((_even ? 7 : 12) * _lenght);
			for (var i = 0; i < _lenght; i++)
			{
				value += Constant.AllSymbole.Symbole2Value(_name[i]);
			}
			value *= Constant.AllSymbole.Length();

			// Randomize create
			RandGen chaos = new RandGen(value);

			// Randomize
			_blendedSymboleList.Randomize(chaos);


			// Calculate WeightOriginal
			_weightOriginal = 0;
			for (int i = 0; i < _lenght; i++)
				_weightOriginal += Constant.AllSymbole.Symbole2Value(name[i]);
			_weightOriginal *= _lenght;


			// Calculate WeightBlended
			_weightBlended = 0;
			for (int i = 0; i < _lenght; i++)
				_weightBlended += _blendedSymboleList.Symbole2Value(name[i]);
			_weightBlended *= _lenght;


			// Calculate Offset
			_offset = (_weightOriginal * _weightBlended - _lenght - _weightBlended) % Constant.AllSymbole.Length();

			string[] words = _name.Split(' ');

			// Base String Calculate
			ProcessBase(words);

			if (_debug)
			{
                Console.WriteLine("----------------------------");
                Console.WriteLine("|          Debug           |");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Length  : " + _lenght);
				Console.WriteLine("Even    : " + (_even ? "true" : "false"));
				Console.WriteLine("Origi   : " + Constant.AllSymbole.List);
				Console.WriteLine("Rand    : " + _blendedSymboleList.List);
				Console.WriteLine("WeightO : " + _weightOriginal);
				Console.WriteLine("WeightB : " + _weightBlended);
				Console.WriteLine("Offset  : " + _offset);
				Console.WriteLine("Base    : " + _base);
                Console.WriteLine("Words   : " + _base);
                for (int i = 0; i < words.Length; i++)
				{
					Console.Write("[" + i + "] " + words[i]);
					Console.WriteLine();
				}
            }
		}

		private void ProcessBase(string[] words)
		{
			for (int i = 0; i < words.Length; i++)
			{
				char to_add;
				StringBuilder tempo = new StringBuilder("");
				int size = words[i].Length;
				if (char.IsLetter(words[i][0]))
				{
					uint valueOffseted = Constant.LetterSymbole.Symbole2Value(words[i][0]);
					valueOffseted += _offset;
					if (valueOffseted >= Constant.LetterSymbole.Length())
						valueOffseted -= (uint)Constant.LetterSymbole.Length();
					char charOffseted = Constant.LetterSymbole.Value2Symbole(valueOffseted);

					to_add = _poolLetter.TryAdding(charOffseted);
					if (to_add != '\0')
						tempo.Append(to_add);
				}
				to_add = _poolFigure.TryAdding((words[i].Length % 10).ToString()[0]);
				if (to_add != '\0')
					tempo.Append(to_add);
				_base.Append(tempo);
			}
		}
	}
}

