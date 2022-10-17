namespace PassCoder;

using System;
using System.Text;

public partial class Cli
{
	public class GeneratePassword
	{
		private readonly StringBuilder _final = new("");

		public GeneratePassword(PhraseProcessed phrase, PhraseProcessed site)
		{
			uint offsetNumber = 0;
			uint offsetLetter = 0;

			for (var i = 0; i < site.Base.Length; i++)
			{
				if (char.IsLetter(site.Base[i]))
				{
					offsetLetter += site.BlendedSymbolList.Symbol2Value(site.Base[i]) - phrase.BlendedSymbolList.Symbol2Value(site.Base[i]);
				}
				else if (char.IsNumber(site.Base[i]))
				{
					offsetNumber += site.BlendedSymbolList.Symbol2Value(site.Base[i]) - phrase.BlendedSymbolList.Symbol2Value(site.Base[i]);
				}
			}

			for (var i = 0; i < phrase.Base.Length; i++)
			{
				if (char.IsLetter(phrase.Base[i]))
				{
					var valueWithOffset = Constant.LetterSymbol.Symbol2Value(phrase.Base[i]);
					valueWithOffset += offsetLetter;
					if (valueWithOffset >= Constant.LetterSymbol.Length())
						valueWithOffset -= Constant.LetterSymbol.Length();
					var charWithOffset = Constant.LetterSymbol.Value2Symbol(valueWithOffset);
					_final.Append(charWithOffset);
				}
				else if (char.IsNumber(phrase.Base[i]))
				{
					var toInt = (uint)char.GetNumericValue(phrase.Base[i]);
					toInt += offsetNumber;
					if (toInt >= Constant.FigureSymbol.Length())
						toInt -= Constant.FigureSymbol.Length();
					var charWithOffset = Constant.FigureSymbol.Value2Symbol(toInt);
					_final.Append(charWithOffset);
				}
			}

			// 
			var newSeed = phrase.WeightBlended + phrase.WeightOriginal + site.WeightBlended + site.WeightOriginal;
			var newChaos = new RandGen(newSeed);

			var postLast = new ValuedSymbolList(_final.ToString());
			postLast.Randomize(newChaos);
			_final = new StringBuilder(postLast.List.ToString());

			// Check Size and compensate with adding symbol
			var numberSpecialToAdd = 4;
			if (_final.Length <= 14)
			{
				numberSpecialToAdd += 14 - _final.Length;
			}

			for (var i = 0; i < numberSpecialToAdd; i++)
			{
				_final.Append(Constant.SpecialSymbol.Value2Symbol(newChaos.Rand() % Constant.SpecialSymbol.Length()));
			}

			var last = new ValuedSymbolList(_final.ToString());
			last.Randomize(newChaos);
			_final = new StringBuilder(last.List.ToString());
		}

		public void WriteFinal()
		{
			Console.WriteLine("----------------------------");
			Console.WriteLine("|     Password Generated   |");
			Console.WriteLine("----------------------------");
			Console.WriteLine();
			Console.WriteLine(_final);
			Console.WriteLine();
			Console.WriteLine("----------------------------");
			Console.WriteLine("|  Press Any key to quit   |");
			Console.WriteLine("----------------------------");
			Console.ReadLine();
		}
	}
}