namespace PassCoder;

using System;
using System.Collections.Generic;
using System.Text;

public class PhraseProcessed
{
	public string Name;
	public uint Length;
	public bool Even;
	public uint WeightOriginal;
	public uint WeightBlended;
	public uint Offset;
	public bool Debug;
	public ValuedSymbolList BlendedSymbolList = new(Constant.AllSymbol);
	public StringBuilder Base = new();
	public UniqueSymbolPool PoolLetter = new(Constant.LetterSymbol);
	public UniqueSymbolPool PoolFigure = new(Constant.FigureSymbol);

	public PhraseProcessed(string name, bool debug)
	{
		// Set Value
		Name = name;
		Length = (uint)name.Length;
		Even = Length % 2 == 0;
		Debug = debug;

		// Generate SymbolBlend
		// Seed create
		var value = (uint)((Even ? 7 : 12) * Length);
		for (var i = 0; i < Length; i++)
		{
			value += Constant.AllSymbol.Symbol2Value(Name[i]);
		}
		value *= Constant.AllSymbol.Length();

		// Randomize create
		var chaos = new RandGen(value);

		// Randomize
		BlendedSymbolList.Randomize(chaos);


		// Calculate WeightOriginal
		WeightOriginal = 0;
		for (var i = 0; i < Length; i++)
			WeightOriginal += Constant.AllSymbol.Symbol2Value(name[i]);
		WeightOriginal *= Length;


		// Calculate WeightBlended
		WeightBlended = 0;
		for (var i = 0; i < Length; i++)
			WeightBlended += BlendedSymbolList.Symbol2Value(name[i]);
		WeightBlended *= Length;


		// Calculate Offset
		Offset = (WeightOriginal * WeightBlended - Length - WeightBlended) % Constant.AllSymbol.Length();

		var words = Name.Split(' ');

		// Base String Calculate
		ProcessBase(words);

		if (!Debug) return;

		Console.WriteLine("----------------------------");
		Console.WriteLine("|          Debug           |");
		Console.WriteLine("----------------------------");
		Console.WriteLine("Length  : " + Length);
		Console.WriteLine("Even    : " + (Even ? "true" : "false"));
		Console.WriteLine("Original: " + Constant.AllSymbol.List);
		Console.WriteLine("Rand    : " + BlendedSymbolList.List);
		Console.WriteLine("WeightO : " + WeightOriginal);
		Console.WriteLine("WeightB : " + WeightBlended);
		Console.WriteLine("Offset  : " + Offset);
		Console.WriteLine("Base    : " + Base);
		Console.WriteLine("Words   : " + Base);
		for (var i = 0; i < words.Length; i++)
		{
			Console.Write("[" + i + "] " + words[i]);
			Console.WriteLine();
		}
	}

	private void ProcessBase(IEnumerable<string> wordList)
	{
		foreach (var word in wordList)
		{
			char toAdd;
			var tempo = new StringBuilder("");
			if (char.IsLetter(word[0]))
			{
				var valueWithOffset = Constant.LetterSymbol.Symbol2Value(word[0]);
				valueWithOffset += Offset;
				if (valueWithOffset >= Constant.LetterSymbol.Length())
					valueWithOffset -= Constant.LetterSymbol.Length();
				var charWithOffset = Constant.LetterSymbol.Value2Symbol(valueWithOffset);

				toAdd = PoolLetter.TryAdding(charWithOffset);
				if (toAdd != '\0')
					tempo.Append(toAdd);
			}
			toAdd = PoolFigure.TryAdding((word.Length % 10).ToString()[0]);
			if (toAdd != '\0')
				tempo.Append(toAdd);
			Base.Append(tempo);
		}
	}
}