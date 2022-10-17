namespace PassCoder;

using System;
using System.Text;

public class ValuedSymbolList
{
	public StringBuilder List = new("");

	public ValuedSymbolList(string list)
	{
		List.Append(list);
	}

	public ValuedSymbolList(ValuedSymbolList valuedSymbolList)
	{
		List.Append(valuedSymbolList.List);
	}

	public uint Symbol2Value(char symbol)
	{
		if (symbol <= 0) throw new ArgumentOutOfRangeException(nameof(symbol));
		for (var i = 0; i < List.Length; i++)
		{
			if (symbol == List[i])
			{
				return (uint)i;
			}
		}
		return (uint)List.Length;
	}

	public char Value2Symbol(uint value)
	{
		return List[(int)(value % List.Length)];
	}

	public uint Length()
	{
		return (uint)List.Length;
	}

	public void Randomize(RandGen chaos)
	{
		for (var i = 0; i < Length(); i++)
		{
			var rand = (int)(chaos.Rand() % Length());
			(List[i], List[rand]) = (List[rand], List[i]); // Swap via deconstruction
		}
	}
}