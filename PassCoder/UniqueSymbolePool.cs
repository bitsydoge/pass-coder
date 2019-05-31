using System;
using System.Text;

namespace PassCoder
{
    public class UniqueSymbolePool
    {
        private ValuedSymboleList _originalSymboleList;
        private ValuedSymboleList _poolSymboleList;

        public UniqueSymbolePool(ValuedSymboleList originalSymboleList)
        {
            _originalSymboleList = new ValuedSymboleList(originalSymboleList);
            _poolSymboleList = new ValuedSymboleList(_originalSymboleList);
        }

        public char TryAdding(char symbole)
        {
            if (_poolSymboleList.Length() <= 0)
                return '\0';

            for (int i = 0; i < _poolSymboleList.Length(); i++)
            {
                if (_poolSymboleList.Value2Symbole((uint) i) == symbole)
                {
                    _poolSymboleList.List.Remove(i, 1);
                    return symbole;
                }
            }

            for (int i = 0; i < _originalSymboleList.Length(); i++)
            {
                if (_originalSymboleList.Value2Symbole((uint)i) == symbole)
                {
                    return TryAdding(_originalSymboleList.Value2Symbole((uint) ((i + 1) % _originalSymboleList.Length())));
                }
            }

            return '\0';
        }
    }
}