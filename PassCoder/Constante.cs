using System.Linq;

namespace PassCoder
{
    public class Constante
    {
        public int[] MajVariation = { 1, 3, 2, 1, 1, 4, 3, 1, 1, 4, 1, 2, 4, 1, 2, 4, 1, 2, 1, 1, 2, 1, 2, 3, 1, 2, 1, 1, 2, 3, 2 };
        public int[] SymboleVariation = { 1, 4, 3, 2, 1, 2, 1, 2, 1, 1, 2, 1, 3, 2, 1, 1, 2, 1, 4, 1, 2, 1, 4, 1, 1, 2, 3, 1, 2, 1, 2, 1, 1, 2, 4, 1 };

        public static ValuedSymboleList AllSymbole = new ValuedSymboleList("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
        public static ValuedSymboleList LetterSymbole = new ValuedSymboleList("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
        public static ValuedSymboleList SpecialSymbole = new ValuedSymboleList(" !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~");
        public static ValuedSymboleList FigureSymbole = new ValuedSymboleList("0123456789");
    }
}