namespace PassCoder;

public class UniqueSymbolPool
{
	private readonly ValuedSymbolList _originalSymbolList;
	private readonly ValuedSymbolList _poolSymbolList;

	public UniqueSymbolPool(ValuedSymbolList originalSymbolList)
	{
		_originalSymbolList = new ValuedSymbolList(originalSymbolList);
		_poolSymbolList = new ValuedSymbolList(_originalSymbolList);
	}

	public char TryAdding(char symbol)
	{
		if (_poolSymbolList.Length() <= 0)
			return '\0';

		for (var i = 0; i < _poolSymbolList.Length(); i++)
		{
			if (_poolSymbolList.Value2Symbol((uint)i) != symbol) continue;
			_poolSymbolList.List.Remove(i, 1);
			return symbol;
		}

		for (var i = 0; i < _originalSymbolList.Length(); i++)
		{
			if (_originalSymbolList.Value2Symbol((uint)i) == symbol)
			{
				return TryAdding(_originalSymbolList.Value2Symbol((uint)((i + 1) % _originalSymbolList.Length())));
			}
		}

		return '\0';
	}
}