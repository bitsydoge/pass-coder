using System;

namespace PassCoder
{
    public class Constante
    {
        public int[] MajVariation = { 1, 3, 2, 1, 1, 4, 3, 1, 1, 4, 1, 2, 4, 1, 2, 4, 1, 2, 1, 1, 2, 1, 2, 3, 1, 2, 1, 1, 2, 3, 2 };
        public int[] SymboleVariation = { 1, 4, 3, 2, 1, 2, 1, 2, 1, 1, 2, 1, 3, 2, 1, 1, 2, 1, 4, 1, 2, 1, 4, 1, 1, 2, 3, 1, 2, 1, 2, 1, 1, 2, 4, 1 };
        public static string AllSymboleList = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
        public string SpecialSymbole = " !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";

        public static int SymboleValue(char character)
        {
            if (character <= 0) throw new ArgumentOutOfRangeException(nameof(character));
            for (int i = 0; i < AllSymboleList.Length; i++)
            {
                if (character == AllSymboleList[i])
                {
                    return i;
                }
            }
            return AllSymboleList.Length;
        }
    }
}