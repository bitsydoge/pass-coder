namespace PassCoder;

public class Constant
{
	public static ValuedSymbolList AllSymbol = new("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
	public static ValuedSymbolList LetterSymbol = new("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
	public static ValuedSymbolList SpecialSymbol = new(" !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
	public static ValuedSymbolList FigureSymbol = new("0123456789");
}