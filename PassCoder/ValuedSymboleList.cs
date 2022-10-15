using System;
using System.Text;

namespace PassCoder
{
    public class ValuedSymboleList
    {
        public StringBuilder List = new StringBuilder("");

        public ValuedSymboleList(String list)
        {
            List.Append(list);
        }

        public ValuedSymboleList(ValuedSymboleList valuedSymboleList)
        {
            List.Append(valuedSymboleList.List);
        }

        public uint Symbole2Value(char symbole)
        {
            if (symbole <= 0) throw new ArgumentOutOfRangeException(nameof(symbole));
            for (int i = 0; i < List.Length; i++)
            {
                if (symbole == List[i])
                {
                    return (uint)i;
                }
            }
            return (uint)List.Length;
        }

        public char Value2Symbole(uint value)
        {
            return List[(int)(value%List.Length)];
        }

        public uint Length()
        {
            return (uint)List.Length;
        }

        public void Randomize(RandGen chaos)
        {
            for (int i = 0; i < Length(); i++)
            {
                var rand = (int)(chaos.Rand() % Length());
                var temp = List[i];
                List[i] = List[rand];
                List[rand] = temp;
            }
        }
    }
}